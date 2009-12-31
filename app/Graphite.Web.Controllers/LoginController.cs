using System.Web.Mvc;
using Graphite.ApplicationServices;
using Graphite.Web.Controllers.ViewModels;
using MvcContrib;
using MvcContrib.Attributes;
using SharpArch.Web.NHibernate;

namespace Graphite.Web.Controllers {
	public class LoginController : Controller {
		private readonly IUserTasks _userTasks;
  	public LoginController(IUserTasks userTasks) { _userTasks = userTasks; }
		
		[AcceptGet]
		public ActionResult Index() {
			return View(new LoginViewModel());
		}

		[AcceptPost, ValidateAntiForgeryToken, Transaction]
		public ActionResult Authenticate(LoginViewModel model) {
			_userTasks.AuthenticateUser(model.Username, model.Password);
			return this.RedirectToAction<Admin.HomeController>(x => x.Index());
		}
	}
}