using System;
using System.Web.Mvc;
using System.Web.Routing;
using Graphite.Web.Controllers;
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
	    "~/post".Route().ShouldMapTo<PostController>(x => x.Index());
      "~/post/itemname".Route().ShouldMapTo<PostController>(x => x.Show("itemname"));
      "~/post/new".Route().ShouldMapTo<PostController>(x => x.New(null));
      "~/post".WithMethod(HttpVerbs.Post).ShouldMapTo<PostController>(x => x.Create(null));
      String.Format("~/post/{0}/edit", guid).Route().ShouldMapTo<PostController>(x => x.Edit(guid));
      String.Format("~/post/{0}", guid).WithMethod(HttpVerbs.Put).ShouldMapTo<PostController>(x => x.Update(null));
      String.Format("~/post/{0}", guid).WithMethod(HttpVerbs.Delete).ShouldMapTo<PostController>(x => x.Destroy(guid));
      //Validate extra routes
      String.Format("~/post/{0}/id", guid).Route().ShouldMapTo<PostController>(x => x.Id(guid));
      String.Format("~/post/{0}/delete", guid).Route().ShouldMapTo<PostController>(x => x.Delete(guid));
	  }

    [Test]
    public void xmlfeeds_is_mapped_to_feed_controller() {
      "~/rss".Route().ShouldMapTo<FeedController>(x => x.Rss());
      "~/atom".Route().ShouldMapTo<FeedController>(x => x.Atom());
    }

    [Test]
    public void login_is_mapped_to_login_controller() {
      "~/login".Route().ShouldMapTo<LoginController>(x => x.Login());
      "~/login/signout".Route().ShouldMapTo<LoginController>(x => x.Logout());
      "~/login/authenticate".WithMethod(HttpVerbs.Post).ShouldMapTo<LoginController>(x => x.Authenticate(null));
    }


	}
}