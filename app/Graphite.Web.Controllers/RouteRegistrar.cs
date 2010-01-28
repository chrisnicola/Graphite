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
				m.Resources<Admin.PostController>(r => r.AddMemberRoute("id", HttpVerbs.Get));
				m.Resources<Admin.UserController>();
				m.Resources<Admin.HomeController>();
				m.Resource<Admin.BlogMLController>(r => {
					r.AddMemberRoute<Admin.BlogMLController>(c => c.Import(), HttpVerbs.Post);
					r.Except("new", "create", "edit", "update", "delete", "destroy");
				});
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
				m.Resources<PostController>(r => r.AddMemberRoute("id", HttpVerbs.Get));
				m.Resources<TagController>(r => r.Except("index", "new", "create", "edit", "update", "delete", "destroy"));
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