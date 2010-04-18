using System;
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
	    "~/post".Route().ShouldMapTo<PostController>(x => x.Index());
	    "~/post/test".Route().ShouldMapTo<PostController>(x => x.Show("test"));
      String.Format("~/post/{0}/id", guid.ToString()).Route().ShouldMapTo<PostController>(x => x.Id(guid));
	  }

    [Test]
    public void xmlfeeds_is_mapped_to_feed_controller() {
      "~/rss".Route().ShouldMapTo<FeedController>(x => x.Rss());
      "~/atom".Route().ShouldMapTo<FeedController>(x => x.Atom());
    }

    [Test]
    public void login_is_mapped_to_login_controller() {
      "~/login".Route().ShouldMapTo<LoginController>(x => x.Show());
    }
	}
}