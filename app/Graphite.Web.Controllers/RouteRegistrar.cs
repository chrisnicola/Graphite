#region

using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using Graphite.Web.Controllers.Admin;
using RestfulRouting;
using SharpArch.Web.Areas;

#endregion

namespace Graphite.Web.Controllers {
	public class RouteRegistrar {
		public static void RegisterRoutesTo(RouteCollection routes) {
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
			routes.IgnoreRoute("{*favicon}", new {favicon = @"(.*/)?favicon.ico(/.*)?"});
			var map = new RestfulRouteMapper(RouteTable.Routes);
			map.Namespace("admin", typeof(Admin.PostController).Namespace, m => m.Resources<Admin.PostController>());
			routes.CreateArea("admin", typeof(Admin.PostController).Namespace, GetDefaultRoute("Admin/"));
			map.Namespace("", typeof(PostController).Namespace, m => m.Resources<PostController>());
			routes.CreateArea("root", typeof(PostController).Namespace, GetDefaultRoute(""));
			
		}

		private static Route[] GetAdminRoutes() {
			var routes = new RouteCollection();
			var map = new RestfulRouteMapper(routes);
			map.Namespace("Admin", m => m.Resources<Admin.PostController>());
			map.Namespace("Admin", m => m.Resources<UserController>());
			//routes.Add(GetDefaultRoute("Admin/"));
			return routes.Cast<Route>().ToArray();
		}

		private static Route[] GetRootRoutes() {
			var routes = new RouteCollection();
			var map = new RestfulRouteMapper(routes);
			map.Resources<PostController>();
			//routes.Add(GetDefaultRoute(""));
			return routes.Cast<Route>().ToArray();
		}

		private static Route GetDefaultRoute(string areaPrefix) {
			return new Route(areaPrefix + "{controller}/{action}",
				new RouteValueDictionary(new {controller = "Home", action = "Index"}), new MvcRouteHandler());
		}
	}
}