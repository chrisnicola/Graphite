using System;
using System.Web.Mvc;
using MvcContrib;
using Graphite.ApplicationServices;
using Graphite.Core;
using Graphite.Data.Repositories;
using Graphite.Web.Controllers.ActionFilters;
using Graphite.Web.Controllers.Mappers;
using Graphite.Web.Controllers.ViewModels;
using SharpArch.Web.NHibernate;

namespace Graphite.Web.Controllers
{
	public class PostControllerBase : Controller {
		protected readonly IPostTasks PostTasks;
		private readonly IPostRepository _posts;

		public PostControllerBase(IPostTasks postTasks, IPostRepository posts) {
			PostTasks = postTasks;
			_posts = posts;
		}

		[AutoMap(typeof(Post),typeof(PostShowWithCommentsViewModel))]
		public ActionResult Id(Guid id) {
			return View(_posts.FindOne(p => p.Id == id));
		}

		[AutoMap(typeof(Post), typeof(PostShowWithCommentsViewModel))]
		public ActionResult Show(string id) {
			return View(_posts.FindOne(p => p.Slug == id));
		}

		[AutoMap(typeof(IPostIndexMapper))]
		public ActionResult Index() {
			return View(PostTasks.GetAll());
		}
	}

	[HandleError]
  public class PostController : PostControllerBase
  {
		public PostController(IPostTasks postTasks, IPostRepository posts) : base(postTasks, posts) {}

		[Transaction, ValidateInput(false)]
    public ActionResult Update(PostShowWithCommentsViewModel postShowVm) {
			Post post = PostTasks.Get(postShowVm.Id);
      try {
        if (CommentIsValid(postShowVm.NewComment)) {
          post.AddComment(postShowVm.NewComment);
        }
      } catch {}
      return this.RedirectToAction(c => c.Show(post.Slug));
    }

    private static bool CommentIsValid(Comment comment) {
      return !string.IsNullOrEmpty(comment.Author)
        && !string.IsNullOrEmpty(comment.EmailAddress)
          && !string.IsNullOrEmpty(comment.Content);
    }
  }
}
