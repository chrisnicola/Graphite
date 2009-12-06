using System;
using System.Web.Mvc;
using Graphite.Core;
using Graphite.Data.Repositories;
using SharpArch.Core.PersistenceSupport;
using SharpArch.Core.PersistenceSupport.NHibernate;
using SharpArch.Web.NHibernate;

namespace Graphite.Web.Controllers.Admin {
  public class PostController : Controllers.PostController {
    public PostController(IPostRepository posts, ICommentRepository comments) : base(posts, comments) { }
    
    public ActionResult New(Post post) {
      return View(post ?? new Post());
    }

    [Transaction, ValidateInput(false)]
    public ActionResult Create(Post post) {
      try {
        _posts.Save(post);
        return RedirectToAction("Show", new { id=post.Id });
      }
      catch {
        return RedirectToAction("New", new {model=post});
      }
    }

    public ActionResult Edit(Guid id) {
      return View(_posts.Get(id));
    }

    [Transaction,ValidateInput(false)]
    public ActionResult Update(Post post) {
      try {
        _posts.SaveOrUpdate(post);
        return RedirectToAction("Index");
      } catch (Exception) {
        return RedirectToAction("Edit", new {model=post});
      }
    }

    public ActionResult Delete(Guid id) {
      return View(_posts.Get(id));
    }

    [Transaction]
    public ActionResult Destroy(Post post) {
      try {
        _posts.Delete(post);
      } catch {}
      return RedirectToAction("Index");
    }
  }
}