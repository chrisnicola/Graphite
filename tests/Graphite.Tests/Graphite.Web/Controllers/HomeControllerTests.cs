using Graphite.ApplicationServices;
using Graphite.Data.Repositories;
using Graphite.Web.Controllers;
using NUnit.Framework;
using Rhino.Mocks;
using MvcContrib.TestHelper;

namespace Tests.Graphite.Web.Controllers
{
  [TestFixture]
  public class HomeControllerTests
  {
    private IPostTasks _tasks;
    private HomeController _controller;

    [SetUp]
    public void SetUp() {
      _tasks = MockRepository.GenerateMock<IPostTasks>();
      _controller = new HomeController(_tasks);
    }

    [Test]
    public void CanListRecentPostsWhenIndexIsCalled() {
    	_controller.Index().AssertViewRendered().ForView("");
    	_tasks.AssertWasCalled(m => m.GetRecentPublishedPosts(Arg<int>.Is.Anything));
    }
  }
}
