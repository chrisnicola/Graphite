using System.Web.Mvc;
using Graphite.ApplicationServices;
using Graphite.Web.Controllers.ViewModels;
using MvcContrib;
using MvcContrib.Attributes;
using SharpArch.Web.NHibernate;

namespace Graphite.Web.Controllers{
	public class LoginController : Controller{
		readonly IUserTasks _userTasks;

		public LoginController(IUserTasks userTasks) { _userTasks = userTasks; }

		public ActionResult Show() {
			if (_userTasks.IsLoggedIn()) return RedirectToAction("Index", "Home", new {area = "Admin"});
			return View(new LoginViewModel());
		}

		[AcceptPost, ValidateAntiForgeryToken, Transaction]
		public ActionResult Authenticate(LoginViewModel model) {
			_userTasks.AuthenticateUser(model.Username, model.Password);
			return RedirectToAction("Index", "Home", new {area = "Admin"});
		}

		public ActionResult SignOut() {
			_userTasks.SignOut();
			return this.RedirectToAction<HomeController>(x => x.Index());
		}
	}
}