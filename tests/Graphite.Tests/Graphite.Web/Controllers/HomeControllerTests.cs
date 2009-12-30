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
    private IPostRepository _repository;
    private HomeController _controller;

    [SetUp]
    public void SetUp() {
      _repository = MockRepository.GenerateMock<IPostRepository>();
      _controller = new HomeController(_repository);
    }

    [Test]
    public void CanListRecentPostsWhenIndexIsCalled() {
    	_controller.Index().AssertViewRendered().ForView("");
    	_repository.AssertWasCalled(m => m.GetRecentPublishedPosts(Arg<int>.Is.Anything));
    }
  }
}
