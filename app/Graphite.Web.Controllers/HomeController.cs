#region

using System.Web.Mvc;

#endregion

namespace Graphite.Web.Controllers {
  [HandleError]
  public class HomeController : Controller {
    public virtual ActionResult Index() { return View(); }
  }
}