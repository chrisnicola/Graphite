using System;
using Graphite.ApplicationServices;
using Graphite.Core;
using Graphite.Web.Controllers.Admin;
using Graphite.Web.Controllers.Mappers;
using Graphite.Web.Controllers.ViewModels;
using NUnit.Framework;
using Rhino.Mocks;
using MvcContrib.TestHelper;
using SharpArch.Testing;

namespace Tests.Graphite.Web.Controllers.Admin
{
  [TestFixture]
  public class PostControllerTests
  {
    private IPostTasks _postTasks;
    private PostController _controller;
    private IPostEditDetailsMapper _postEditDetailsMapper;
  	private IPostCreateDetailsMapper _postCreateDetailsMapper;

  	[SetUp]
    public void SetUp() {
      _postTasks = MockRepository.GenerateMock<IPostTasks>();
      _postEditDetailsMapper = MockRepository.GenerateMock<IPostEditDetailsMapper>();
			_postCreateDetailsMapper = MockRepository.GenerateMock<IPostCreateDetailsMapper>();
      _controller = new PostController(_postTasks, _postEditDetailsMapper, _postCreateDetailsMapper);
    }

    [Test]
    public void CanViewAnIndividualPostById() {
      var post = new Post();
      _postTasks.Stub(m => m.GetWithComments(new Guid())).IgnoreArguments().Return(post);
      _controller.Show(new Guid()).AssertViewRendered().ViewData.Model.ShouldBe(post);
    }

    [Test]
    public void CreatesANewPostWhenNewIsCalled() {
      _controller.New(null).AssertViewRendered().ViewData.Model.ShouldBe<PostNewModel>("ViewData.Model is not a Post");
    }

    [Test]
    public void GetsExistingPostFromRepositoryWhenEditIsCalledWithIdValue() {
      var guid = new Guid();
      var post = new Post().SetIdTo(guid) as Post;
      _postTasks.Expect(m => m.Get(guid)).Return(post);
      _controller.Edit(guid).AssertViewRendered().ViewData.Model.ShouldBe(post);
      _postTasks.VerifyAllExpectations();
    }

    [Test]
    public void SavesPostToRepositoryWheUpdated() {
      var post = new PostEditModel();
			var details = new PostEditDetails();
    	_postEditDetailsMapper.Stub(m => m.MapFrom(post)).Return(details);
      _controller.Update(post);
      _postTasks.AssertWasCalled(m => m.UpdatePost(details));
    }

    [Test]
    public void RedirectsToListAfterSavingPost() {
      _controller.Update(new PostEditModel()).AssertActionRedirect().ToAction("Index");
    }
  }
}
