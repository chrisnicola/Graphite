using System;
using System.Web.Mvc;
using Graphite.ApplicationServices;
using Graphite.Core;
using Graphite.Web.Controllers.ActionFilters;
using Graphite.Web.Controllers.Mappers;
using Graphite.Web.Controllers.ViewModels;
using MvcContrib;
using SharpArch.Web.NHibernate;

namespace Graphite.Web.Controllers.Admin {
	public class PostController : PostControllerBase {
		private readonly IPostEditDetailsMapper _postEditMapper;
		private readonly IPostCreateDetailsMapper _postCreateDetailsMapper;

		public PostController(IPostTasks postTasks, IPostEditDetailsMapper postEditMapper, IPostCreateDetailsMapper postCreateDetailsMapper)
			: base(postTasks) {
			_postEditMapper = postEditMapper;
			_postCreateDetailsMapper = postCreateDetailsMapper;
		}

		[Authorize, AutoMap(typeof (IPostNewModelMapper))]
		public ActionResult New(PostNewModel post) { return View(post ?? new PostNewModel()); }

		[Authorize, Transaction, ValidateAntiForgeryToken, ValidateInput(false)]
		public ActionResult Create(PostNewModel post) {
			try {
				Guid id = PostTasks.SaveNewPost(_postCreateDetailsMapper.MapFrom(post)).Id;
				return this.RedirectToAction(x => x.Show(id));
			} catch {
				return this.RedirectToAction(x => x.New(post));
			}
		}

		[Authorize, AutoMap(typeof (IPostEditModelMapper))]
		public ActionResult Edit(Guid id) { return View(PostTasks.Get(id)); }

		[Authorize, Transaction, ValidateAntiForgeryToken, ValidateInput(false)]
		public ActionResult Update(PostEditModel post) {
			try {
				PostTasks.UpdatePost(_postEditMapper.MapFrom(post));
				return this.RedirectToAction(x => x.Index());
			} catch (Exception) {
				return this.RedirectToAction(x => x.Edit(post.Id));
			}
		}

		[Authorize, AutoMap(typeof (Post), typeof (DeletePostViewModel))]
		public ActionResult Delete(Guid id) { return View(PostTasks.Get(id)); }

		[Authorize, Transaction]
		public ActionResult Destroy(DeletePostViewModel post) {
			try {
				PostTasks.Delete(post.Id);
			} catch (Exception ex) {}
			return RedirectToAction("Index");
		}
	}
}