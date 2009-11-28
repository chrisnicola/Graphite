using System;
using System.Web.Mvc;
using Graphite.Core;
using Graphite.Data.Repositories;
using SharpArch.Web.NHibernate;

namespace Graphite.Web.Controllers.Admin {
  public class PostController : Controllers.PostController {
    public PostController(IPostRepository posts) : base(posts) { }

    public ActionResult New() {
      return View(new Post());
    }

    [AcceptVerbs(HttpVerbs.Post)]
    [ValidateAntiForgeryToken]
    [ValidateInput(false)]
    [Transaction]
    public ActionResult Create(Post post) {
      try {
        Posts.Save(post);
        return RedirectToAction("");
      }
      catch {
        return RedirectToAction("New");
      }
    }

    public ActionResult Edit(Guid id) {
      return View(Posts.Get(id));
    }

    [AcceptVerbs(HttpVerbs.Post)]
    [ValidateAntiForgeryToken]
    [ValidateInput(false)]
    [Transaction]
    public ActionResult Edit(Post post) {
      try {
        Posts.SaveOrUpdate(post);
        return RedirectToAction("");
      } catch (Exception) {
        return View(post);
      }
    }

    [AcceptVerbs(HttpVerbs.Delete)]
    [ValidateAntiForgeryToken]
    [ValidateInput(false)]
    [Transaction]
    public ActionResult Delete(Guid id) {
      try {
        Posts.Delete(Posts.Get(id));
      } catch (Exception) { }
      return RedirectToAction("");
    }
  }
}