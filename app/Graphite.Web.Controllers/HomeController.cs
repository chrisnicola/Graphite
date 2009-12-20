#region

using System.Web.Mvc;
using Graphite.Data.Repositories;

#endregion

namespace Graphite.Web.Controllers {
  [HandleError]
  public class HomeController : Controller {
    private readonly IPostRepository _posts;

    public HomeController(IPostRepository posts) {
      _posts = posts;
    }

    public ActionResult Index() {
      return View(_posts.GetRecentPublishedPosts(5));
    }
  }
}