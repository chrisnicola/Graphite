using System;
using System.Web.Mvc;
using Graphite.Core;
using Graphite.Data.Repositories;
using Graphite.Web.Controllers.ActionFilters;
using Graphite.Web.Controllers.ViewModels;
using SharpArch.Web.NHibernate;

namespace Graphite.Web.Controllers
{
  [HandleError]
  public class PostController : Controller
  {
    protected readonly IPostRepository _posts;
    protected readonly ICommentRepository _comments;

    public PostController(IPostRepository posts, ICommentRepository comments) {
      _posts = posts;
      _comments = comments;
    }

    [AutoMap(typeof(Post),typeof(ShowPostWithComments))]
    public ActionResult Show(Guid id) {
      Post post = _posts.GetWithComments(id);
      return View(post);
    }

    public ActionResult Index() {
      return View(_posts.FindAll());
    }

    [Transaction, ValidateInput(false)]
    public ActionResult Update(ShowPostWithComments showPostVm) {
      var post = _posts.Get(showPostVm.Id);
      try {
        if (CommentIsValid(showPostVm.NewComment)) {
          post.AddComment(showPostVm.NewComment);
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
