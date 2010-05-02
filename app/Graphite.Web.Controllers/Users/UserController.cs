using System;
using System.Web.Mvc;
using Graphite.Core.Contracts.Services;
using Graphite.Web.Controllers.ActionFilters;
using Graphite.Web.Controllers.Contracts.Mappers;
using MvcContrib;

namespace Graphite.Web.Controllers.Users{
	public class UserController : Controller{
		readonly IUserTasks _userTasks;
		readonly ICreateUserMapper _createUserMap;
		readonly IEditUserMapper _editUserMap;

		public UserController(IUserTasks userTasks, ICreateUserMapper createUserMap, IEditUserMapper editUserMap) {
			_userTasks = userTasks;
			_createUserMap = createUserMap;
			_editUserMap = editUserMap;
		}

		[Authorize, AutoMap(typeof (IUserIndexMapper))]
		public ActionResult Index() { return View(_userTasks.GetUsers()); }

		[Authorize, AutoMap(typeof (Core.Domain.User), typeof (EditUserViewModel))]
		public ActionResult Edit(Guid id) { return View(_userTasks.GetUser(id)); }

		[Authorize, ValidateAntiForgeryToken]
		public ActionResult Update(EditUserViewModel model) {
			try {
				_userTasks.UpdateUser(_editUserMap.MapFrom(model));
				return this.RedirectToAction(x => x.Index());
			} catch (Exception ex) {
				return this.RedirectToAction(x => x.Edit(model.Id));
			}
		}

		public ActionResult New(NewUserViewModel user) { return View(user ?? new NewUserViewModel()); }

		[ValidateAntiForgeryToken]
		public ActionResult Create(NewUserViewModel user) {
			try {
				_userTasks.AddUser(_createUserMap.MapFrom(user));
				return this.RedirectToAction(x => x.Index());
			} catch (Exception ex) {
				return View("New", user);
			}
		}

		[Authorize, AutoMap(typeof (Core.Domain.User), typeof (DeleteUserViewModel))]
		public ActionResult Delete(Guid id) { return View(_userTasks.GetUser(id)); }

		[Authorize, ValidateAntiForgeryToken]
		public ActionResult Destroy(DeleteUserViewModel model) {
			_userTasks.RemoveUser(model.Id);
			return this.RedirectToAction(x => x.Index());
		}
	}
}