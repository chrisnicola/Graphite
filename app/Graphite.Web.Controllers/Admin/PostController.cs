using System;
using System.Web.Mvc;
using Graphite.Core;
using Graphite.Data.Repositories;
using SharpArch.Web.NHibernate;

namespace Graphite.Web.Controllers.Admin {
  public class PostController : Controllers.PostController {
    public PostController(IPostRepository posts) : base(posts) { }
    
    public ActionResult New(Post post) {
      return View(post ?? new Post());
    }

    [Transaction, ValidateInput(false)]
    public ActionResult Create(Post post) {
      try {
        Posts.Save(post);
        return RedirectToAction("Show", new { id=post.Id });
      }
      catch {
        return RedirectToAction("New", new {model=post});
      }
    }

    public ActionResult Edit(Guid id) {
      return View(Posts.Get(id));
    }

    [Transaction,ValidateInput(false)]
    public ActionResult Update(Post post) {
      try {
        Posts.SaveOrUpdate(post);
        return RedirectToAction("Index");
      } catch (Exception) {
        return RedirectToAction("Edit", new {model=post});
      }
    }

    public ActionResult Delete(Guid id) {
      return View(Posts.Get(id));
    }

    [Transaction]
    public ActionResult Destroy(Post post) {
      try {
        Posts.Delete(post);
      } catch {}
      return RedirectToAction("Index");
    }
  }
}