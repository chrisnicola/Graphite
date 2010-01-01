using System;
using System.Web.Mvc;
using Graphite.Core;
using Graphite.Data.Repositories;
using Graphite.Web.Controllers.ViewModels;
using SharpArch.Core.PersistenceSupport;
using SharpArch.Core.PersistenceSupport.NHibernate;
using SharpArch.Web.NHibernate;

namespace Graphite.Web.Controllers.Admin {
  public class PostController : Controllers.PostController {
    public PostController(IPostRepository posts, ICommentRepository comments) : base(posts, comments) { }
		[Authorize]
    public ActionResult New(Post post) {
      return View(post ?? new Post());
    }

    [Authorize, Transaction, ValidateAntiForgeryToken, ValidateInput(false)]
    public ActionResult Create(Post post) {
      try {
        _posts.Save(post);
        return View("Show", post);
      }
      catch {
        return View("New", post);
      }
    }
		[Authorize]
    public ActionResult Edit(Guid id) {
      return View(_posts.Get(id));
    }

		[Authorize, Transaction, ValidateInput(false)]
    public ActionResult Update(Post post) {
      try {
        _posts.SaveOrUpdate(post);
        return RedirectToAction("Index");
      } catch (Exception) {
        return RedirectToAction("Edit", new {model=post});
      }
    }
		[Authorize]
    public ActionResult Delete(Post post) {
      return View(new DeletePost(post));
    }

		[Authorize, Transaction]
    public ActionResult Destroy(DeletePost post) {
      try {
        _posts.Delete(_posts.GetWithComments(post.Id));
      } catch {}
      return RedirectToAction("Index");
    }
  }
}