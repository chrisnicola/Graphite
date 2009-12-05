using System;
using System.Web.Mvc;
using Graphite.Core;
using SharpArch.Core.PersistenceSupport.NHibernate;
using SharpArch.Web.NHibernate;

namespace Graphite.Web.Controllers {
	[HandleError]
	public class CommentController : Controller {
		protected readonly INHibernateRepositoryWithTypedId<Comment, Guid> Comments;
		public CommentController(INHibernateRepositoryWithTypedId<Comment, Guid> comments) { Comments = comments; }

		public ActionResult Show(Guid id) { return View(Comments.Get(id)); }

		public ActionResult Index() { return View(Comments.GetAll()); }

		[Transaction, ValidateInput(false)]
		public ActionResult Create(Comment comment) {
			try {
				Comments.Save(comment);
				return RedirectToAction("Show", "Post", new {id = comment.Post.Id});
			} catch {
				return RedirectToAction("New", new {model = comment});
			}
		}
	}
}