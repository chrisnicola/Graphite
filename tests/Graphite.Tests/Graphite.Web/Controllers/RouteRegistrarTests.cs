using System.Web.Routing;
using Graphite.Web.Controllers;
using MvcContrib.TestHelper;
using NUnit.Framework;

namespace Tests.Graphite.Controllers{
	[TestFixture]
	public class RouteRegistrarTests{
		[SetUp]
		public void SetUp() {
			RouteTable.Routes.Clear();
			RouteRegistrar.RegisterRoutesTo(RouteTable.Routes);
		}

		[Test]
		public void CanVerifyRouteMaps() { "~/".Route().ShouldMapTo<HomeController>(x => x.Index()); }
	}
}