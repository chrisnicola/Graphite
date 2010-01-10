using System;
using System.Web.Mvc;
using Graphite.Core;
using Graphite.Data.Repositories;
using SharpArch.Core.PersistenceSupport.NHibernate;
using SharpArch.Web.NHibernate;

namespace Graphite.Web.Controllers {
	[HandleError]
	public class CommentController : Controller {
	  private readonly IPostRepository _posts;
	  private readonly INHibernateRepositoryWithTypedId<Comment, Guid> _comments;
		public CommentController(IPostRepository posts, INHibernateRepositoryWithTypedId<Comment, Guid> comments) {
		  _posts = posts;
		  _comments = comments;
		}

	  public ActionResult Show(Guid id) { return View(_comments.Get(id)); }

		public ActionResult Index() { return View(_comments.GetAll()); }

    public ActionResult New(Guid postId) { return View(new Comment() {Post = _posts.Get(postId)}); }

		[Transaction, ValidateInput(false)]
		public ActionResult Create(Comment comment) {
			try {
        comment.DateCreated = DateTime.Now;
				_comments.Save(comment);
				return RedirectToAction("Show", "Post", new {id = comment.Post.Id});
			} catch {
				return RedirectToAction("New", new {model = comment});
			}
		}
	}
}