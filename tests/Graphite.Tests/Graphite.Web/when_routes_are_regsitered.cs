using System;
using System.Web.Mvc;
using System.Web.Routing;
using Graphite.Web.Controllers;
using Graphite.Web.Controllers.Comments;
using Graphite.Web.Controllers.Home;
using Graphite.Web.Controllers.Login;
using Graphite.Web.Controllers.Posts;
using Graphite.Web.Controllers.Syndication;
using NUnit.Framework;
using MvcContrib.TestHelper;

namespace Tests.Graphite.Web{
	[TestFixture]
	public class when_routes_are_regsitered{
		[SetUp]
		public void SetUp() {
			RouteTable.Routes.Clear();
			RouteRegistrar.RegisterRoutesTo(RouteTable.Routes);
		}

		[Test]
		public void deafult_route_is_home_index() { "~/".Route().ShouldMapTo<HomeController>(x => x.Index()); }

	  [Test]
	  public void post_is_mapped_to_post_controller() {
	    var guid = Guid.NewGuid();
      //Validate restful routes
	    "~/posts".Route().ShouldMapTo<PostsController>(x => x.Index());
      "~/posts/itemname".Route().ShouldMapTo<PostsController>(x => x.Show("itemname"));
      "~/posts/new".Route().ShouldMapTo<PostsController>(x => x.New(null));
      "~/posts".WithMethod(HttpVerbs.Post).ShouldMapTo<PostsController>(x => x.Create(null));
      String.Format("~/posts/{0}/edit", guid).Route().ShouldMapTo<PostsController>(x => x.Edit(guid));
      String.Format("~/posts/{0}", guid).WithMethod(HttpVerbs.Put).ShouldMapTo<PostsController>(x => x.Update(null));
      String.Format("~/posts/{0}", guid).WithMethod(HttpVerbs.Delete).ShouldMapTo<PostsController>(x => x.Destroy(guid));
      //Validate extra routes
      String.Format("~/posts/{0}/id", guid).Route().ShouldMapTo<PostsController>(x => x.Id(guid));
      String.Format("~/posts/{0}/delete", guid).Route().ShouldMapTo<PostsController>(x => x.Delete(guid));
	  }

    [Test]
    public void xmlfeeds_is_mapped_to_feed_controller() {
      "~/rss".Route().ShouldMapTo<FeedController>(x => x.Rss());
      "~/atom".Route().ShouldMapTo<FeedController>(x => x.Atom());
    }

    [Test]
    public void login_is_mapped_to_login_controller() {
      "~/login".Route().ShouldMapTo<LoginController>(x => x.Login());
      "~/logout".Route().ShouldMapTo<LoginController>(x => x.Logout());
      "~/login".WithMethod(HttpVerbs.Post).ShouldMapTo<LoginController>(x => x.Authenticate(null));
    }

    [Test]
    public void controller_is_nested_below_posts() {
      var guid = Guid.NewGuid();
      String.Format("~/posts/{0}/comments", guid).WithMethod(HttpVerbs.Post).ShouldMapTo<CommentsController>(x => x.Create(null));
      OutBoundUrl.Of<PostsController>(x => x.Show("test")).ShouldMapToUrl("/");
    }

	}
}