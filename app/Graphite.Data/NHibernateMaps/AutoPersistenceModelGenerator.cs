using System;
using System.Linq;
using FluentNHibernate;
using FluentNHibernate.Automapping;
using FluentNHibernate.Conventions;
using Graphite.Core;
using SharpArch.Core.DomainModel;
using SharpArch.Data.NHibernate.FluentNHibernate;

namespace Graphite.Data.NHibernateMaps
{

    public class AutoPersistenceModelGenerator : IAutoPersistenceModelGenerator
    {

        #region IAutoPersistenceModelGenerator Members

        public AutoPersistenceModel Generate()
        {
            var mappings = new AutoPersistenceModel();
            mappings.AddEntityAssembly(typeof(Post).Assembly).Where(GetAutoMappingFilter);
            mappings.Conventions.Setup(GetConventions());
            mappings.Setup(GetSetup());
            mappings.IgnoreBase<Entity>();
            mappings.IgnoreBase(typeof(EntityWithTypedId<>));
            mappings.UseOverridesFromAssemblyOf<AutoPersistenceModelGenerator>();

            return mappings;
        }

        #endregion

        private static Action<AutoMappingExpressions> GetSetup()
        {
            return c =>
            {
                c.FindIdentity = type => type.Name == "Id";
            };
        }

        private static Action<IConventionFinder> GetConventions()
        {
            return c =>
            {
                c.Add<Graphite.Data.NHibernateMaps.Conventions.ForeignKeyConvention>();
                c.Add<Graphite.Data.NHibernateMaps.Conventions.HasManyConvention>();
                c.Add<Graphite.Data.NHibernateMaps.Conventions.HasManyToManyConvention>();
                c.Add<Graphite.Data.NHibernateMaps.Conventions.ManyToManyTableNameConvention>();
                c.Add<Graphite.Data.NHibernateMaps.Conventions.PrimaryKeyConvention>();
                c.Add<Graphite.Data.NHibernateMaps.Conventions.ReferenceConvention>();
                c.Add<Graphite.Data.NHibernateMaps.Conventions.TableNameConvention>();
            };
        }

        /// <summary>
        /// Provides a filter for only including types which inherit from the IEntityWithTypedId interface.
        /// </summary>

        private static bool GetAutoMappingFilter(Type t)
        {
            return t.GetInterfaces().Any(x =>
                                         x.IsGenericType &&
                                         x.GetGenericTypeDefinition() == typeof(IEntityWithTypedId<>));
        }
    }
}
