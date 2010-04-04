
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using Graphite.Web.Controllers;
using RestfulRouting;

namespace Graphite.Web.Controllers{

namespace Graphite.Web{
  public static class RouteRegistrar
  {
    public static void RegisterRoutesTo(RouteCollection routes)
    {
      routes.IgnoreRoute("XmlRpc.ashx");
      routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
      routes.IgnoreRoute("{*favicon}", new { favicon = @"(.*/)?favicon.ico(/.*)?" });
      AreaRegistration.RegisterAllAreas();
      routes.MapRoutes<Routes>();
    }
  }

  public class Routes : RouteSet
  {
    public Routes()
    {
      Map("").To<Home.HomeController>(x => x.Index());
      Area<Home.HomeController>("", () =>
      {
        Resources<Posts.PostController>(() => Member("id", HttpVerbs.Get));
        Resources<Tags.TagController>(() => Only("show"));
        Resource<Login.LoginController>(() =>
        {
          Only("show");
          Member("authenticate", HttpVerbs.Post);
          Member("signout", HttpVerbs.Get);
        });
        Map("rss").To<FeedController>(x => x.Rss());
        Map("atom").To<FeedController>(x => x.Atom());
      });
      Area<Admin.Posts.PostController>("admin", () =>
      {
        Resources<Admin.Posts.PostController>(
          () => Member("id", HttpVerbs.Get));
        Resources<Admin.Users.UserController>();
        Resource<Admin.BlogMLController>(() =>
        {
          Only("show");
          Member("import", HttpVerbs.Post);
        });
      });
    }
  }
}