using System;
using System.Web.Mvc;
using MvcContrib;
using Graphite.Core.Contracts.DataInterfaces;
using Graphite.Core.Contracts.TaskInterfaces;
using Graphite.Core.Domain;
using Graphite.Web.Controllers.ActionFilters;
using Graphite.Web.Controllers.Mappers;
using Graphite.Web.Controllers.ViewModels;

namespace Graphite.Web.Controllers{
	public class PostControllerBase : Controller{
		protected readonly IPostTasks PostTasks;
		readonly IPostRepository _posts;

		public PostControllerBase(IPostTasks postTasks, IPostRepository posts) {
			PostTasks = postTasks;
			_posts = posts;
		}

		[AutoMap(typeof (Post), typeof (PostShowWithCommentsViewModel))]
		public ActionResult Id(Guid id) { return View(_posts.FindOne(p => p.Id == id)); }

		[AutoMap(typeof (Post), typeof (PostShowWithCommentsViewModel))]
		public ActionResult Show(string id) { return View(_posts.FindOne(p => p.Slug == id)); }

		[AutoMap(typeof (IPostIndexMapper))]
		public ActionResult Index() { return View(PostTasks.GetAll()); }
	}

	[HandleError]
	public class PostController : PostControllerBase{
		public PostController(IPostTasks postTasks, IPostRepository posts) : base(postTasks, posts) { }

		[ValidateInput(false)]
		public ActionResult Update(PostShowWithCommentsViewModel postShowVm) {
			Post post = PostTasks.Get(postShowVm.Id);
			try {
				if (CommentIsValid(postShowVm.NewComment)) post.AddComment(postShowVm.NewComment);
			} catch {}
			return this.RedirectToAction(c => c.Show(post.Slug));
		}

		static bool CommentIsValid(Comment comment) {
			return !string.IsNullOrEmpty(comment.Author)
			       && !string.IsNullOrEmpty(comment.EmailAddress)
			       && !string.IsNullOrEmpty(comment.Content);
		}
	}
}