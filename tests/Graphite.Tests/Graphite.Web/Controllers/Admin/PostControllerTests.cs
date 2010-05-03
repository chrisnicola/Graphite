using System;
using Graphite.Core.Contracts.Data;
using Graphite.Core.Contracts.Services;
using Graphite.Core.Domain;
using Graphite.Core.Messages;
using Graphite.Web.Controllers.Contracts.Mappers;
using Graphite.Web.Controllers.Posts;
using MvcContrib.TestHelper;
using NUnit.Framework;
using Rhino.Mocks;
using SharpArch.Testing;

namespace Tests.Graphite.Web.Controllers.Admin{
	[TestFixture]
	public class PostControllerTests{
		[SetUp]
		public void SetUp() {
			_postTasks = MockRepository.GenerateMock<IPostTasks>();
			_userTasks = MockRepository.GenerateMock<IUserTasks>();
			_postEditDetailsMapper = MockRepository.GenerateMock<IPostEditDetailsMapper>();
			_postCreateDetailsMapper = MockRepository.GenerateMock<IPostCreateDetailsMapper>();
			_postRepository = MockRepository.GenerateMock<IPostRepository>();
			_controller = new PostsController(_postTasks, _postRepository, _userTasks, _postCreateDetailsMapper, _postEditDetailsMapper);
		}

		IPostTasks _postTasks;
		PostsController _controller;
		IPostEditDetailsMapper _postEditDetailsMapper;
		IPostCreateDetailsMapper _postCreateDetailsMapper;
		IPostRepository _postRepository;
		IUserTasks _userTasks;

		[Test]
		public void CanViewAnIndividualPostById() {
			var post = new Post();
			_postRepository.Stub(m => m.FindOne(Arg.Is<Func<Post, bool>>(null))).IgnoreArguments().Return(post);
			_controller.Id(new Guid()).AssertViewRendered().ViewData.Model.ShouldBe(post);
		}

		[Test]
		public void CreatesANewPostWhenNewIsCalled() {
			_controller.New(new PostNewModel()).AssertViewRendered().ViewData.Model.ShouldBe<PostNewModel>(
			"ViewData.Model is not a PostNewModel");
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
		public void RedirectsToListAfterSavingPost() { _controller.Update(new PostEditModel()).AssertActionRedirect().ToAction("Index"); }
	}
}