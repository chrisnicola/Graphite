using System;
using System.Web.Mvc;
using Graphite.Core;
using Graphite.Data.Repositories;
using SharpArch.Web.NHibernate;

namespace Graphite.Web.Controllers.Admin {
  public class PostController : Controllers.PostController {
    public PostController(IPostRepository posts) : base(posts) { }

    public ActionResult Edit() {
      return View(new Post());
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
        return RedirectToAction("List");
      } catch (Exception) {
        return View(post);
      }
    }
  }
}