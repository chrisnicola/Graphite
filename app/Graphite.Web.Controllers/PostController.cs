using System;
using System.Web.Mvc;
using Graphite.Core;
using Graphite.Data.Repositories;
using Graphite.Web.Controllers.ViewModels;
using SharpArch.Core.PersistenceSupport;
using SharpArch.Core.PersistenceSupport.NHibernate;
using SharpArch.Web.ModelBinder;
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

    public ActionResult Show(Guid id) {
      Post post = _posts.GetWithComments(id);
        return View(new ShowPostWithComments(post));
    }

    public ActionResult Index() {
      return View(_posts.GetAll());
    }

    [Transaction, ValidateInput(false)]
    public ActionResult Update(ShowPostWithComments showPostVm) {
      var post = _posts.Get(showPostVm.PostId);
      try {
        if (CommentIsValid(showPostVm.NewComment)) {
          post.AddComment(showPostVm.NewComment);
          //_comments.Save(showPostVm.NewComment);
        }
      } catch {}
      return View("Show", new ShowPostWithComments(post));
    }

    private static bool CommentIsValid(Comment comment) {
      return !string.IsNullOrEmpty(comment.Author)
        && !string.IsNullOrEmpty(comment.EmailAddress)
          && !string.IsNullOrEmpty(comment.Content);
    }
  }
}
