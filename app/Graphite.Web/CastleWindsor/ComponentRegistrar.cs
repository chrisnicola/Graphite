using System.Web.Mvc;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Graphite.Core.Contracts.Mapping;
using Graphite.Web.Controllers.Admin.Posts;
using SharpArch.Core.CommonValidator;
using SharpArch.Core.NHibernateValidator.CommonValidatorAdapter;
using SharpArch.Core.PersistenceSupport;
using SharpArch.Core.PersistenceSupport.NHibernate;
using SharpArch.Data.NHibernate;
using SharpArch.Web.Castle;
using Spark;
using Spark.Web.Mvc;

namespace Graphite.Web.CastleWindsor{
	public static class ComponentRegistrar{
		public static void AddComponentsTo(IWindsorContainer container) {
			SharpArchContrib.Castle.CastleWindsor.ComponentRegistrar.AddComponentsTo(container);
			AddGenericRepositoriesTo(container);
			AddCustomRepositoriesTo(container);
			AddApplicationServicesTo(container);
			AddSparkViewEngineTo(container);
			AddGenericMappersTo(container);
			AddControllerMappersTo(container);
			container.AddComponent("validator", typeof (IValidator), typeof (Validator));
		}

		static void AddSparkViewEngineTo(IWindsorContainer container) {
			container.AddComponent<IViewEngine, SparkViewFactory>();
			container.AddComponent<IViewActivatorFactory, WindsorViewActivator>();
		}

		static void AddApplicationServicesTo(IWindsorContainer container) {
			container.Register(AllTypes.Pick().FromAssemblyNamed("Graphite.ApplicationServices")
			                   .WithService.FirstNonGenericCoreInterface("Graphite.Core.Contracts.Services"));
		}

		static void AddCustomRepositoriesTo(IWindsorContainer container) {
			container.Register(
			AllTypes.Pick().FromAssemblyNamed("Graphite.Data")
			.WithService.FirstNonGenericCoreInterface("Graphite.Core.Contracts.Data"));
		}

		static void AddControllerMappersTo(IWindsorContainer container) {
			container.Register(
			AllTypes.Pick().FromAssemblyNamed("Graphite.Web.Controllers").Where(t => t.IsAssignableFrom(typeof(IMapper)))
			.WithService.FirstNonGenericCoreInterface("Graphite.Web.Controllers"));
		}

		static void AddGenericMappersTo(IWindsorContainer container) {
		  container.AddComponent("genericMapper", typeof (IMapper<,>), typeof (GenericMapper<,>));
		}

		static void AddGenericRepositoriesTo(IWindsorContainer container) {
			container.AddComponent("entityDuplicateChecker", typeof (IEntityDuplicateChecker), typeof (EntityDuplicateChecker));
			container.AddComponent("repositoryType", typeof (IRepository<>), typeof (Repository<>));
			container.AddComponent("nhibernateRepositoryType", typeof (INHibernateRepository<>), typeof (NHibernateRepository<>));
			container.AddComponent("repositoryWithTypedId", typeof (IRepositoryWithTypedId<,>), typeof (RepositoryWithTypedId<,>));
			container.AddComponent("nhibernateRepositoryWithTypedId", typeof (INHibernateRepositoryWithTypedId<,>),
			                       typeof (NHibernateRepositoryWithTypedId<,>));
		}
	}
}