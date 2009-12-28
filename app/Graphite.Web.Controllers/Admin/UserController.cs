using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Security;
using Graphite.ApplicationServices;
using Graphite.Core;
using Graphite.Web.Controllers.ActionFilters;
using SharpArch.Web.NHibernate;

namespace Graphite.Web.Controllers.Admin
{
	public class UserController : Controller
	{
		private readonly IUserTasks _userTasks;
		public UserController(IUserTasks userTasks) { _userTasks = userTasks; }

		[AutoMap(typeof)]
		public ActionResult Index() {
			return View(_userTasks.GetUsers());
		}

		public ActionResult Show(Guid id) {
			return View(_userTasks.GetUser(id));
		}

		public ActionResult New(User user) {
			return View(user ?? new User());
		}

		[Transaction]
		public ActionResult Create(User user) {
			try {
				_userTasks.AddUser(user);
				return View("Show", user);
			}
			catch (Exception ex) {
				return View("New", user);
			}
		}
	}
}
