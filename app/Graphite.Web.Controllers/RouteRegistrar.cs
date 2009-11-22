#region

using System.Web.Mvc;
using System.Web.Routing;
using SharpArch.Web.Areas;

#endregion

namespace Graphite.Web.Controllers {
  public class RouteRegistrar {
    public static void RegisterRoutesTo(RouteCollection routes) {
      routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
      routes.IgnoreRoute("{*favicon}", new {favicon = @"(.*/)?favicon.ico(/.*)?"});
      // The areas below must be registered from greater subareas to fewer;
      // i.e., the root area should be the last area registered
      // Example illustrative routes with a nested area - note that the order of registration is important
      //routes.CreateArea("Organization/Department", "Graphite.Web.Controllers.Organization.Department",
      //    routes.MapRoute(null, "Organization/Department/{controller}/{action}", new { action = "Index" }),
      //    routes.MapRoute(null, "Organization/Department/{controller}/{action}/{id}")
      //);
      //routes.CreateArea("Organization", "Graphite.Web.Controllers.Organization",
      //    routes.MapRoute(null, "Organization/{controller}/{action}", new { action = "Index" }),
      //    routes.MapRoute(null, "Organization/{controller}/{action}/{id}")
      //);
      routes.CreateArea("Admin", "Graphite.Web.Controllers.Admin",
        routes.MapRoute(null, "Admin/{controller}/{action}", new {controller = "Home", action = "Index"}));
      // Routing config for the root area
      routes.CreateArea("Root", "Graphite.Web.Controllers",
        routes.MapRoute(null, "{controller}/{action}", new {controller = "Home", action = "Index"}));
    }
  }
}