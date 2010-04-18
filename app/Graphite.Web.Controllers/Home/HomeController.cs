#region
using System.Web.Mvc;
using Graphite.Core.Contracts.Data;
using Graphite.Core.Contracts.Services;
using Graphite.Web.Controllers.ActionFilters;
using Graphite.Web.Controllers.Contracts.Mappers;

#endregion

namespace Graphite.Web.Controllers.Home{
	[HandleError]
	public class HomeController : Controller{
		readonly IPostTasks _postTasks;
		readonly IPostRepository _posts;

		public HomeController(IPostTasks postTasks) { _postTasks = postTasks; }

		[AutoMap(typeof (IHomeIndexMapper))]
		public ActionResult Index() { return View(_postTasks.GetRecentPublishedPosts(5)); }
	}
}