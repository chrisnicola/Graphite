#region

using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using MvcContrib.SimplyRestful;
using RestfulRouting;
using SharpArch.Web.Areas;

#endregion

namespace Graphite.Web.Controllers {
  public class RouteRegistrar {
    public static void RegisterRoutesTo(RouteCollection routes) {
      routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
      routes.IgnoreRoute("{*favicon}", new {favicon = @"(.*/)?favicon.ico(/.*)?"});
			var configuration = new RouteConfiguration { Namespaces = new[] { typeof(PostController).Namespace } };

			var map = new RestfulRouteMapper(RouteTable.Routes, configuration);

			// this is mapped using the default namespaces defined above
			map.Resources<PostController>();

    	map.Namespace("admin", typeof (Admin.PostController).Namespace, m => {
    	                                                                            	m.Resources
    	                                                                            		<Admin.PostController>();
    	                                                                            	m.Resources
    	                                                                            		<Admin.PostController>();
    	                                                                            });

			routes.CreateArea("Admin", typeof(Admin.UserController).Namespace, GetDefaultRoute("Admin/"));
			routes.CreateArea("Root", typeof(PostController).Namespace, GetDefaultRoute(""));
    }

  	private static Route[] GetAdminRoutes() {
			var routes = new RouteCollection();
  		var map = new RestfulRouteMapper(routes);
  		map.Namespace("Admin", m => m.Resources<Admin.PostController>());
  		map.Namespace("Admin", m => m.Resources<Admin.UserController>());
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