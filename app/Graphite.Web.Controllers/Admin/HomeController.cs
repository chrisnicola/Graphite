using System.Web.Mvc;

namespace Graphite.Web.Controllers.Admin{
	[HandleError]
	public class HomeController : Controller{
		[Authorize]
		public ActionResult Index() { return View(); }
	}
}