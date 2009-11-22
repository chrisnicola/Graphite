using System;
using System.Web.Mvc;
using Graphite.Core;
using SharpArch.Core.PersistenceSupport;

namespace Graphite.Web.Controllers
{
  public class PostController : Controller
  {
    protected readonly IRepositoryWithTypedId<Post, Guid> Posts;
    public PostController(IRepositoryWithTypedId<Post,Guid> posts) { Posts = posts; }

    public virtual ActionResult List() {
      return View(Posts.GetAll());
    }
  }
}
