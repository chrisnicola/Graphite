#region

using System.Web.Mvc;
using Graphite.ApplicationServices;
using Graphite.Data.Repositories;
using Graphite.Web.Controllers.ActionFilters;
using Graphite.Web.Controllers.Mappers;

#endregion

namespace Graphite.Web.Controllers {
  [HandleError]
  public class HomeController : Controller {
  	private readonly IPostTasks _postTasks;
  	private readonly IPostRepository _posts;

    public HomeController(IPostTasks postTasks) {
    	_postTasks = postTasks;
    }

		[AutoMap(typeof(IHomeIndexMapper))]
  	public ActionResult Index() {
      return View(_postTasks.GetRecentPublishedPosts(5));
    }
  }
}