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

    public ActionResult View(Guid id){
      return View(Posts.Get(id));
    }

    public ActionResult Index() {
      return View(Posts.GetAll());
    }
  }
}
