using System.Web.Mvc;
using Graphite.Core.Contracts.Services;
using Graphite.Web.Controllers.Home;
using MvcContrib;
using MvcContrib.Attributes;
using SharpArch.Web.NHibernate;

namespace Graphite.Web.Controllers.Login{
	public class LoginController : Controller{
		readonly IUserTasks _userTasks;

		public LoginController(IUserTasks userTasks) { _userTasks = userTasks; }

		public ActionResult Login() {
			if (_userTasks.IsLoggedIn()) return RedirectToAction("Index", "Home");
			return View(new LoginViewModel {Password = "test", Username = "test"});
		}

		[HttpPost, ValidateAntiForgeryToken, Transaction]
		public ActionResult Authenticate(LoginViewModel model) {
			var user = _userTasks.AuthenticateUser(model.Username, model.Password);
      if (user == null) return RedirectToAction("Create", "User");
			return RedirectToAction("Index", "Home", new {area = "Admin"});
		}

		public ActionResult Logout() {
			_userTasks.SignOut();
			return this.RedirectToAction<HomeController>(x => x.Index());
		}
	}
}