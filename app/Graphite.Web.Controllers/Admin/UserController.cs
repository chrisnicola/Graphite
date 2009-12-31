using System;
using System.Web.Mvc;
using Graphite.ApplicationServices;
using Graphite.Core;
using Graphite.Web.Controllers.ActionFilters;
using Graphite.Web.Controllers.Mappers;
using Graphite.Web.Controllers.ViewModels;
using MvcContrib;
using Microsoft.Web.Mvc;
using SharpArch.Web.NHibernate;

namespace Graphite.Web.Controllers.Admin {
	public class UserController : Controller {
		private readonly IUserTasks _userTasks;
		private readonly ICreateUserMapper _createUserMap;
		private readonly IEditUserMapper _editUserMap;

		public UserController(IUserTasks userTasks, ICreateUserMapper createUserMap, IEditUserMapper editUserMap) {
			_userTasks = userTasks;
			_createUserMap = createUserMap;
			_editUserMap = editUserMap;
		}

		[AutoMap(typeof (IUserIndexMapper))]
		public ActionResult Index() { return View(_userTasks.GetUsers()); }

		[AutoMap(typeof (User), typeof (EditUserViewModel))]
		public ActionResult Edit(Guid id) { return View(_userTasks.GetUser(id)); }

		[Transaction, ValidateAntiForgeryToken]
		public ActionResult Update(EditUserViewModel model) {
			try {
				_userTasks.UpdateUser(_editUserMap.MapFrom(model));
				return this.RedirectToAction(x => x.Index());
			} catch (Exception ex) {
				return this.RedirectToAction(x => x.Edit(model.Id));
			}
		}

		public ActionResult New(NewUserViewModel user) { return View(user ?? new NewUserViewModel()); }

		[Transaction, ValidateAntiForgeryToken]
		public ActionResult Create(NewUserViewModel user) {
			try {
				_userTasks.AddUser(_createUserMap.MapFrom(user));
				return this.RedirectToAction(x => x.Index());
			} catch (Exception ex) {
				return View("New", user);
			}
		}
		[AutoMap(typeof(User),typeof(DeleteUserViewModel))]
		public ActionResult Delete(Guid id) { return View(_userTasks.GetUser(id)); }

		[Transaction, ValidateAntiForgeryToken]
		public ActionResult Destroy(DeleteUserViewModel model) {
			_userTasks.RemoveUser(model.Id);
			return this.RedirectToAction(x => x.Index());
		}
	}
}