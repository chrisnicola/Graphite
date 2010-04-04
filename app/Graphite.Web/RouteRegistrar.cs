using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using Graphite.Web.Views.Admin.BlogML;
using Graphite.Web.Views.Admin.User;
using Graphite.Web.Views.Feed;
using Graphite.Web.Views.Home;
using Graphite.Web.Views.Login;
using Graphite.Web.Views.Post;
using Graphite.Web.Views.Tag;
using RestfulRouting;
using RestfulRouting.Mappings;
using SharpArch.Web.Areas;

namespace Graphite.Web{
  public static class RouteRegistrar
  {
    public static void RegisterRoutesTo(RouteCollection routes)
    {
      routes.IgnoreRoute("XmlRpc.ashx");
      routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
      routes.IgnoreRoute("{*favicon}", new { favicon = @"(.*/)?favicon.ico(/.*)?" });
      routes.MapRoutes<Routes>();
    }
  }

  public class Routes : RouteSet
  {
    public Routes()
    {
      Map("").To<HomeController>(x => x.Index());
      Map("rss").To<FeedController>(x => x.Rss());
      /*Area<HomeController>("", () =>
      {
        Resources<PostController>(() => Member("id", HttpVerbs.Get));
        Resources<TagController>(() => Only("show"));
        Resource<LoginController>(() =>
        {
          Only("show");
          Member("authenticate", HttpVerbs.Post);
          Member("signout", HttpVerbs.Get);
        });
        Map("rss").To<FeedController>(x => x.Rss());
        Map("atom").To<FeedController>(x => x.Atom());
      });
      Area<Views.Admin.Post.PostController>("admin", () =>
      {
        Resources<Views.Admin.Post.PostController>(
          () => Member("id", HttpVerbs.Get));
        Resources<UserController>();
        Resource<BlogMLController>(() =>
        {
          Only("show");
          Member("import", HttpVerbs.Post);
        });
      });*/
    }
  }
}