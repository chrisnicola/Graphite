#region

using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using RestfulRouting;
using SharpArch.Web.Areas;

#endregion

namespace Graphite.Web.Controllers {
	public class RouteRegistrar {
		public static void RegisterRoutesTo(RouteCollection routes) {
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
			routes.IgnoreRoute("{*favicon}", new {favicon = @"(.*/)?favicon.ico(/.*)?"});
			var configuration = new RouteConfiguration { Namespaces = new[] { typeof(PostController).Namespace } };
			var map = new RestfulRouteMapper(routes, configuration);
			routes.CreateArea("admin", typeof(Admin.PostController).Namespace, GetAdminRoutes());
			routes.CreateArea("root", typeof(PostController).Namespace, GetRootRoutes());
		}

		private static Route[] GetAdminRoutes() {
			var routes = new RouteCollection();
			var map = new RestfulRouteMapper(routes);
			routes.MapRoute(null, "admin/", new { controller = "Home", action = "Index"});
			map.Namespace("admin", m =>
			{
				m.Resources<Admin.PostController>();
				m.Resources<Admin.UserController>();
				m.Resources<Admin.HomeController>();
			});
			return routes.Cast<Route>().ToArray();
		}

		private static Route[] GetRootRoutes()
		{
			var routes = new RouteCollection();
			var map = new RestfulRouteMapper(routes);
			routes.MapRoute(null, "", new { controller = "Home", action = "Index" });
			map.Namespace("", m =>
			{
				m.Resources<PostController>();
				m.Resources<HomeController>();
				m.Resource<LoginController>(r => {
					r.AddMemberRoute<LoginController>(c => c.Authenticate(null), HttpVerbs.Post);
					r.AddMemberRoute<LoginController>(c => c.SignOut(), HttpVerbs.Get);
					r.Except("new", "create", "edit", "update", "delete", "destroy");
				});
				m.Resource<FeedController>(r => {
					r.ShowName = "rss";
					r.Except("new", "create", "edit", "update", "delete", "destroy");
					r.AddMemberRoute<FeedController>(c => c.Rss(), HttpVerbs.Get);
					r.AddMemberRoute<FeedController>(c => c.Atom(), HttpVerbs.Get);
				});
			});
			return routes.Cast<Route>().ToArray();
		}

	}
}