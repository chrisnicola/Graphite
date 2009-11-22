using System;
using System.Web.Mvc;
using Graphite.Core;
using SharpArch.Core.PersistenceSupport;
using SharpArch.Web.NHibernate;

namespace Graphite.Web.Controllers.Admin {
  public class PostController : Controllers.PostController
  {
    public PostController(IRepositoryWithTypedId<Post, Guid> posts) : base(posts) {}

    public ActionResult Edit(Guid? id) {
      if (id.HasValue)
        return View(Posts.Get(id.Value));
      return View(new Post());
    }

    [AcceptVerbs(HttpVerbs.Post)]
    [ValidateAntiForgeryToken]
    [Transaction]
    public ActionResult Edit(Post post) {
      Posts.SaveOrUpdate(post);
      return RedirectToAction("List");
    }
  }
}