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
      AddControllerMappersTo(container);
			AddCustomRepositoriesTo(container);
      AddApplicationServicesTo(container);
			AddGenericMappersTo(container);
      AddGenericRepositoriesTo(container);
			container.AddComponent("validator", typeof (IValidator), typeof (Validator));
      AddSparkViewEngineTo(container);
		}

		static void AddSparkViewEngineTo(IWindsorContainer container) {
			container.AddComponent<IViewEngine, SparkViewFactory>();
			container.AddComponent<IViewActivatorFactory, WindsorViewActivator>();
		}

		static void AddApplicationServicesTo(IWindsorContainer container) {
      container.Register(AllTypes.Pick().FromAssemblyNamed("Graphite.ApplicationServices")
			                   .WithService.FirstNonGenericCoreInterface("Graphite.Core.Contracts.Services")
                         .WithService.FirstNonGenericCoreInterface("Graphite.Core.Contracts.Tasks"));
		}

		static void AddCustomRepositoriesTo(IWindsorContainer container) {
			container.Register(
      AllTypes.Of(typeof(IRepositoryWithTypedId<,>)).FromAssemblyNamed("Graphite.Data")
			.WithService.FirstNonGenericCoreInterface("Graphite.Core.Contracts.Data"));
		}

		static void AddControllerMappersTo(IWindsorContainer container) {
			container.Register(
			AllTypes.Of<IMapper>().FromAssemblyNamed("Graphite.Web.Controllers")
      .WithService.FirstNonGenericCoreInterface("Graphite.Web.Controllers.Contracts.Mappers"));
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