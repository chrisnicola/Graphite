using System.Web.Mvc;
using System.Web.Routing;
using Graphite.Web.Controllers.Home;
using Graphite.Web.Controllers.Login;
using Graphite.Web.Controllers.Posts;
using Graphite.Web.Controllers.Tags;
using RestfulRouting;

namespace Graphite.Web.Controllers{
  public static class RouteRegistrar{
    public static void RegisterRoutesTo(RouteCollection routes) {
      routes.IgnoreRoute("XmlRpc.ashx");
      routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
      routes.IgnoreRoute("{*favicon}", new {favicon = @"(.*/)?favicon.ico(/.*)?"});
      routes.MapRoutes<Routes>();
    }
  }

  public class Routes : RouteSet{
    public Routes() {
      Map("").To<HomeController>(x => x.Index());
      Area<HomeController>("", () =>
        Map("").To<HomeController>(x => x.Index()));
      Map("rss").To<FeedController>(x => x.Rss());
      Map("atom").To<FeedController>(x => x.Atom());
      Resources<PostController>(() => Member("id", HttpVerbs.Get));
      Resources<TagController>(() => Only("show"));
      Resource<LoginController>(() => {
                                  Only("show");
                                  Member("authenticate", HttpVerbs.Post);
                                  Member("signout", HttpVerbs.Get);
                                });
      /*Area<Admin.Posts.PostController>("admin", () => {
                                                  Resources<Admin.Posts.PostController>(
                                                    () => Member("id", HttpVerbs.Get));
                                                  Resources<UserController>();
                                                  Resource<BlogMLController>(() => {
                                                                               Only("show");
                                                                               Member("import", HttpVerbs.Post);
                                                                             });
                                                });*/
    }
  }
}