using System.Web.Mvc;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Graphite.Core.MappingInterfaces;
using Graphite.Web.Controllers.Mappers;
using SharpArch.Core.CommonValidator;
using SharpArch.Core.NHibernateValidator.CommonValidatorAdapter;
using SharpArch.Core.PersistenceSupport;
using SharpArch.Core.PersistenceSupport.NHibernate;
using SharpArch.Data.NHibernate;
using SharpArch.Web.Castle;
using Spark;
using Spark.Web.Mvc;

namespace Graphite.Web.CastleWindsor{
	public class ComponentRegistrar{
		public static void AddComponentsTo(IWindsorContainer container) {
			SharpArchContrib.Castle.CastleWindsor.ComponentRegistrar.AddComponentsTo(container);
			AddGenericRepositoriesTo(container);
			AddCustomRepositoriesTo(container);
			AddApplicationServicesTo(container);
			AddSparkViewEngineTo(container);
			AddGenericMappersTo(container);
			AddCustomMappersTo(container);
			container.AddComponent("validator", typeof (IValidator), typeof (Validator));
		}

		static void AddSparkViewEngineTo(IWindsorContainer container) {
			container.AddComponent<IViewEngine, SparkViewFactory>();
			container.AddComponent<IViewActivatorFactory, WindsorViewActivator>();
		}

		static void AddApplicationServicesTo(IWindsorContainer container) {
			container.Register(AllTypes.Pick().FromAssemblyNamed("Graphite.ApplicationServices")
			                   .WithService.FirstNonGenericCoreInterface("Graphite.ApplicationServices"));
		}

		static void AddCustomRepositoriesTo(IWindsorContainer container) {
			container.Register(
			AllTypes.Pick().FromAssemblyNamed("Graphite.Data")
			.WithService.FirstNonGenericCoreInterface("Graphite.Data"));
		}

		static void AddCustomMappersTo(IWindsorContainer container) {
			container.Register(
			AllTypes.Pick().FromAssembly(typeof (PostEditDetailsMapper).Assembly).
			WithService.FirstNonGenericCoreInterface("Graphite.Web.Controllers.Mappers"));
		}

		static void AddGenericMappersTo(IWindsorContainer container) { container.AddComponent("genericMapper", typeof (IMapper<,>), typeof (GenericMapper<,>)); }

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