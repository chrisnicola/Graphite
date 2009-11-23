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
    public void CreatesANewPostWhenEditIsCalledWithNoIdValue() {
      _controller.Edit().AssertViewRendered().ViewData.Model.ShouldBe<Post>("ViewData.Model is not a Post");
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
    public void SavesPostToRepositoryWhenEditIsPosted() {
      var post = new Post();
      _controller.Edit(post);
      _repository.AssertWasCalled(m => m.SaveOrUpdate(post));
    }

    [Test]
    public void RedirectsToListAfterSavingPost() {
      _controller.Edit(new Post()).AssertActionRedirect().ToAction("List");
    }

    [Test]
    public void RedirectsBackToEditIfErrorSavingPost() {
      var post = new Post();
      _repository.Stub(m => m.SaveOrUpdate(post)).Return(post).Throw(new Exception());
      _controller.Edit(post).AssertViewRendered().ViewData.Model.ShouldBe(post);
    }
  }
}
