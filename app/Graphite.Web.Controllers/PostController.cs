using System;
using System.Web.Mvc;
using Graphite.ApplicationServices;
using Graphite.Core;
using Graphite.Web.Controllers.ActionFilters;
using Graphite.Web.Controllers.Mappers;
using Graphite.Web.Controllers.ViewModels;
using SharpArch.Web.NHibernate;

namespace Graphite.Web.Controllers
{
	public class PostControllerBase : Controller {
		protected IPostTasks PostTasks;

		public PostControllerBase(IPostTasks postTasks) {
			PostTasks = postTasks;
		}

		[AutoMap(typeof(Post),typeof(PostShowWithCommentsViewModel))]
		public ActionResult Show(Guid id) {
			Post post = PostTasks.GetWithComments(id);
			return View(post);
		}

		[AutoMap(typeof(IPostIndexMapper))]
		public ActionResult Index() {
			return View(PostTasks.GetAll());
		}
	}

	[HandleError]
  public class PostController : PostControllerBase
  {
		public PostController(IPostTasks postTasks) : base(postTasks) {}

		[Transaction, ValidateInput(false)]
    public ActionResult Update(PostShowWithCommentsViewModel postShowVm) {
			Post post = PostTasks.Get(postShowVm.Id);
      try {
        if (CommentIsValid(postShowVm.NewComment)) {
          post.AddComment(postShowVm.NewComment);
        }
      } catch {}
      return View("Show", post);
    }

    private static bool CommentIsValid(Comment comment) {
      return !string.IsNullOrEmpty(comment.Author)
        && !string.IsNullOrEmpty(comment.EmailAddress)
          && !string.IsNullOrEmpty(comment.Content);
    }
  }
}
