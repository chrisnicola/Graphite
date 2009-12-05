using System;
using Graphite.Core;
using Graphite.Data.Repositories;
using Graphite.Web.Controllers.Admin;
using NUnit.Framework;
using Rhino.Mocks;
using MvcContrib.TestHelper;
using SharpArch.Testing.NUnit;
using SharpArch.Testing;

namespace Tests.Graphite.Web.Controllers.Admin
{
  [TestFixture]
  public class PostControllerTests
  {
    private IPostRepository _repository;
    private PostController _controller;

    [SetUp]
    public void SetUp() {
      _repository = MockRepository.GenerateMock<IPostRepository>();
      _controller = new PostController(_repository);
    }

    [Test]
    public void CanViewAnIndividualPostById() {
      var post = new Post();
      _repository.Stub(m => m.Get(new Guid())).IgnoreArguments().Return(post);
      _controller.Show(new Guid()).AssertViewRendered().ViewData.Model.ShouldBe(post);
    }

    [Test]
    public void CreatesANewPostWhenNewIsCalled() {
      _controller.New(null).AssertViewRendered().ViewData.Model.ShouldBe<Post>("ViewData.Model is not a Post");
    }

    [Test]
    public void GetsExistingPostFromRepositoryWhenEditIsCalledWithIdValue() {
      var guid = new Guid();
      var post = new Post().SetIdTo(guid) as Post;
      _repository.Expect(m => m.Get(guid)).Return(post);
      _controller.Edit(guid).AssertViewRendered().ViewData.Model.ShouldBe(post);
      _repository.VerifyAllExpectations();
    }

    [Test]
    public void SavesPostToRepositoryWheUpdated() {
      var post = new Post();
      _controller.Update(post);
      _repository.AssertWasCalled(m => m.SaveOrUpdate(post));
    }

    [Test]
    public void RedirectsToListAfterSavingPost() {
      _controller.Update(new Post()).AssertActionRedirect().ToAction("Index");
    }

    [Test]
    public void RedirectsBackToEditIfErrorSavingPost() {
      var post = new Post();
      _repository.Stub(m => m.SaveOrUpdate(post)).Return(post).Throw(new Exception());
    	var result = _controller.Update(post).AssertActionRedirect();
			Assert.That(result.RouteValues["action"] == "Edit");
			Assert.That(result.RouteValues["model"] == post);
    }
  }
}
