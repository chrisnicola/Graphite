using System;
using System.Web.Mvc;
using Graphite.Core;
using Graphite.Data.Repositories;
using SharpArch.Core.PersistenceSupport;

namespace Graphite.Web.Controllers
{
  [HandleError]
  public class PostController : Controller
  {
    protected readonly IPostRepository Posts;
    public PostController(IPostRepository posts) { Posts = posts; }

    public virtual ActionResult List() {
      return View(Posts.GetAll());
    }
  }
}
