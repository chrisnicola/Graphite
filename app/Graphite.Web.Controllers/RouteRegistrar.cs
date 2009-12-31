#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using MvcContrib.SimplyRestful;
using SharpArch.Web.Areas;

#endregion

namespace Graphite.Web.Controllers {
  public class RouteRegistrar {
    public static void RegisterRoutesTo(RouteCollection routes) {
      routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
      routes.IgnoreRoute("{*favicon}", new {favicon = @"(.*/)?favicon.ico(/.*)?"});
      routes.CreateArea("Admin", "Graphite.Web.Controllers.Admin",GetRestfulRoutes("Admin/"));
      routes.CreateArea("Root", "Graphite.Web.Controllers",GetRestfulRoutes(""));
    }

    private static Route[] GetRestfulRoutes(string areaPrefix) {
      var routes = new RouteCollection {GetDefaultRoute(areaPrefix)};
    	SimplyRestfulRouteHandler.BuildRoutes(routes, areaPrefix + "{controller}", null, null);
      return routes.Cast<Route>().ToArray();
    }

    private static Route GetDefaultRoute(string areaPrefix) {
      return new Route(areaPrefix + "{controller}/{action}",
        new RouteValueDictionary(new {controller = "Home", action = "Index"}), new MvcRouteHandler());
    }
  }
}