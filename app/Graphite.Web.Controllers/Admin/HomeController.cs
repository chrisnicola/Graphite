using System.Web.Mvc;

namespace Graphite.Web.Controllers.Admin {
  [HandleError]
  public class HomeController : Controller
  {
    public ActionResult Index()
    {
      return View();
    }
  }
}