using Graphite.ApplicationServices;
using Graphite.Core.Contracts.Services;
using Graphite.Web.Controllers;
using Graphite.Web.Controllers.Home;
using MvcContrib.TestHelper;
using NUnit.Framework;
using Rhino.Mocks;

namespace Tests.Graphite.Web.Controllers{
	[TestFixture]
	public class HomeControllerTests{
		[SetUp]
		public void SetUp() {
			_tasks = MockRepository.GenerateMock<IPostTasks>();
			_controller = new HomeController(_tasks);
		}

		IPostTasks _tasks;
		HomeController _controller;

		[Test]
		public void CanListRecentPostsWhenIndexIsCalled() {
			_controller.Index().AssertViewRendered().ForView("");
			_tasks.AssertWasCalled(m => m.GetRecentPublishedPosts(Arg<int>.Is.Anything));
		}
	}
}