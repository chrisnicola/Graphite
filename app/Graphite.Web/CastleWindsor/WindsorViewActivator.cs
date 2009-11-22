using System;
using Castle.Core;
using Castle.MicroKernel;
using Spark;

namespace Graphite.Web.CastleWindsor
{
    /// <summary>
    /// Provides Windsor's Kernel capabilities to Spark's view activator infrastructure
    /// </summary>
    public class WindsorViewActivator : IViewActivatorFactory
    {
        private readonly IKernel _kernel;

        public WindsorViewActivator(IKernel kernel)
        {
            _kernel = kernel;
        }

        #region IViewActivatorFactory Members

        public IViewActivator Register(Type type)
        {
            _kernel.AddComponent(type.AssemblyQualifiedName, typeof(ISparkView), type, LifestyleType.Transient);
            return new Activator(_kernel, type.AssemblyQualifiedName);
        }

        public void Unregister(Type type, IViewActivator activator)
        {
            _kernel.RemoveComponent(type.AssemblyQualifiedName);
        }

        #endregion

        #region Nested type: Activator

        private class Activator : IViewActivator
        {
            private readonly IKernel kernel;
            private readonly string key;

            public Activator(IKernel kernel, string key)
            {
                this.kernel = kernel;
                this.key = key;
            }

            #region IViewActivator Members

            public ISparkView Activate(Type type)
            {
                return (ISparkView)kernel.Resolve(key, typeof(ISparkView));
            }

            public void Release(Type type, ISparkView view)
            {
                kernel.ReleaseComponent(view);
            }

            #endregion
        }

        #endregion
    }
}