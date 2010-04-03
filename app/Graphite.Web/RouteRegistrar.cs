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
using SharpArch.Web.Areas;

namespace Graphite.Web{
	public class RouteRegistrar{
		public static void RegisterRoutesTo(RouteCollection routes) {
			routes.IgnoreRoute("XmlRpc.ashx");
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
			routes.IgnoreRoute("{*favicon}", new {favicon = @"(.*/)?favicon.ico(/.*)?"});
			var configuration = new RouteConfiguration {Namespaces = new[] {typeof (PostController).Namespace}};
			var map = new RestfulRouteMapper(routes, configuration);
			routes.CreateArea("admin", "Graphite.Web.Views.Admin", GetAdminRoutes());
      routes.CreateArea("root", "Graphite.Web.Views", GetRootRoutes());
		}

		static Route[] GetAdminRoutes() {
			var routes = new RouteCollection();
			var map = new RestfulRouteMapper(routes);
			routes.MapRoute(null, "admin/", new {controller = "Post", action = "Index"});
			map.Namespace("admin", m => {
			                       	m.Resources<Views.Admin.Post.PostController>(r => r.AddMemberRoute("id", HttpVerbs.Get));
			                       	m.Resources<UserController>();
			                       	m.Resource<BlogMLController>(r => {
			                       	                             	r.AddMemberRoute<BlogMLController>(c => c.Import(),
			                       	                             	                                   HttpVerbs.Post);
			                       	                             	r.Except("new", "create", "edit", "update", "delete", "destroy");
			                       	                             });
			                       });
			return routes.Cast<Route>().ToArray();
		}

		static Route[] GetRootRoutes() {
			var routes = new RouteCollection();
			var map = new RestfulRouteMapper(routes);
			routes.MapRoute(null, "", new {controller = "Home", action = "Index"});
			map.Namespace("", m => {
			                  	m.Resources<PostController>(r => r.AddMemberRoute("id", HttpVerbs.Get));
			                  	m.Resources<TagController>(
			                  	r => r.Except("index", "new", "create", "edit", "update", "delete", "destroy"));
			                  	m.Resources<HomeController>();
			                  	m.Resource<LoginController>(r => {
			                  	                            	r.AddMemberRoute<LoginController>(c => c.Authenticate(null),
			                  	                            	                                  HttpVerbs.Post);
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