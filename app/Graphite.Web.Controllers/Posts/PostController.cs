using System;
using System.Web.Mvc;
using Graphite.Core.Contracts.Data;
using Graphite.Core.Contracts.Tasks;
using Graphite.Core.Domain;
using Graphite.Web.Controllers.ActionFilters;
using Graphite.Web.Controllers.Contracts.Mappers;
using MvcContrib;

namespace Graphite.Web.Controllers.Posts{
	public class PostControllerBase : Controller{
		protected readonly IPostTasks PostTasks;
		readonly IPostRepository _posts;

		public PostControllerBase(IPostTasks postTasks, IPostRepository posts) {
			PostTasks = postTasks;
			_posts = posts;
		}

		[AutoMap(typeof (Core.Domain.Post), typeof (PostShowWithCommentsViewModel))]
		public ActionResult Id(Guid id) { return View(_posts.FindOne(p => p.Id == id)); }

		[AutoMap(typeof (Core.Domain.Post), typeof (PostShowWithCommentsViewModel))]
		public ActionResult Show(string id) { return View(_posts.FindOne(p => p.Slug == id)); }

		[AutoMap(typeof (IPostIndexMapper))]
		public ActionResult Index() { return View(PostTasks.GetAll()); }
	}

	[HandleError]
	public class PostController : PostControllerBase{
		public PostController(IPostTasks postTasks, IPostRepository posts) : base(postTasks, posts) { }

		[ValidateInput(false)]
		public ActionResult Update(PostShowWithCommentsViewModel postShowVm) {
			Core.Domain.Post post = PostTasks.Get(postShowVm.Id);
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