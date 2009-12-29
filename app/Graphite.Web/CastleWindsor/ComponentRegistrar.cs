using System.Reflection;
using System.Web.Mvc;
using AutoMapper;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Graphite.Core;
using Graphite.Web.Controllers.Mappers;
using Graphite.Web.Controllers.ViewModels;
using SharpArch.Core.CommonValidator;
using SharpArch.Core.NHibernateValidator.CommonValidatorAdapter;
using SharpArch.Core.PersistenceSupport;
using SharpArch.Core.PersistenceSupport.NHibernate;
using SharpArch.Data.NHibernate;
using SharpArch.Web.Castle;
using Spark;
using Spark.Web.Mvc;

namespace Graphite.Web.CastleWindsor {
	public class ComponentRegistrar {
		public static void AddComponentsTo(IWindsorContainer container) {
			AddGenericRepositoriesTo(container);
			AddCustomRepositoriesTo(container);
			AddApplicationServicesTo(container);
			AddSparkViewEngineTo(container);
			AddCustomMappersTo(container);
			container.AddComponent("validator", typeof (IValidator), typeof (Validator));
			CreateAutoMappings();
		}

		private static void AddSparkViewEngineTo(IWindsorContainer container) {
			container.AddComponent<IViewEngine, SparkViewFactory>();
			container.AddComponent<IViewActivatorFactory, WindsorViewActivator>();
		}

		private static void AddApplicationServicesTo(IWindsorContainer container) { container.Register(AllTypes.Pick().FromAssemblyNamed("Graphite.ApplicationServices").WithService.FirstInterface()); }

		private static void AddCustomRepositoriesTo(IWindsorContainer container) {
			container.Register(
				AllTypes.Pick().FromAssemblyNamed("Graphite.Data").WithService.FirstNonGenericCoreInterface("Graphite.Data"));
		}

		private static void AddCustomMappersTo(IWindsorContainer container) {
			container.Register(
				AllTypes.Pick().FromAssembly(Assembly.GetAssembly(typeof(GenericMapper<,>))).
					WithService.FirstNonGenericCoreInterface("Graphite.Web.Controllers.Mappers"));
		}

		private static void AddGenericRepositoriesTo(IWindsorContainer container) {
			container.AddComponent("entityDuplicateChecker", typeof (IEntityDuplicateChecker), typeof (EntityDuplicateChecker));
			container.AddComponent("repositoryType", typeof (IRepository<>), typeof (Repository<>));
			container.AddComponent("nhibernateRepositoryType", typeof (INHibernateRepository<>), typeof (NHibernateRepository<>));
			container.AddComponent("repositoryWithTypedId", typeof (IRepositoryWithTypedId<,>), typeof (RepositoryWithTypedId<,>));
			container.AddComponent("nhibernateRepositoryWithTypedId", typeof (INHibernateRepositoryWithTypedId<,>),
				typeof (NHibernateRepositoryWithTypedId<,>));
		}

		private static void CreateAutoMappings() {
			Mapper.CreateMap<Post, ShowPostWithComments>().ForMember(m => m.NewComment, c => c.Ignore()).ForMember(m => m.Post, a => a.Ignore());
		}
	}
}