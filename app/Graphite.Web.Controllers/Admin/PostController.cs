using System;
using System.Web.Mvc;
using MvcContrib;
using Graphite.Core.Contracts.DataInterfaces;
using Graphite.Core.Contracts.TaskInterfaces;
using Graphite.Core.Domain;
using Graphite.Web.Controllers.ActionFilters;
using Graphite.Web.Controllers.Mappers;
using Graphite.Web.Controllers.ViewModels;
using SharpArch.Web.NHibernate;

namespace Graphite.Web.Controllers.Admin{
	public class PostController : PostControllerBase{
		readonly IUserTasks _userTasks;
		readonly IPostRepository _posts;
		readonly IPostEditDetailsMapper _postEditMapper;
		readonly IPostCreateDetailsMapper _postCreateDetailsMapper;

		public PostController(IPostTasks postTasks, IUserTasks userTasks, IPostRepository posts,
		                      IPostEditDetailsMapper postEditMapper, IPostCreateDetailsMapper postCreateDetailsMapper)
		: base(postTasks, posts) {
			_userTasks = userTasks;
			_posts = posts;
			_postEditMapper = postEditMapper;
			_postCreateDetailsMapper = postCreateDetailsMapper;
		}

		[Authorize]
		public ActionResult New(PostNewModel post) {
			post.AuthorUserName = _userTasks.GetCurrentUserName();
			return View(post);
		}

		[Authorize, Transaction, ValidateAntiForgeryToken, ValidateInput(false)]
		public ActionResult Create(PostNewModel post) {
			try {
				post.AuthorUserName = _userTasks.GetCurrentUserName();
				Post newPost = PostTasks.SaveNewPost(_postCreateDetailsMapper.MapFrom(post));
				return this.RedirectToAction(x => x.Show(newPost.Slug));
			} catch {
				return this.RedirectToAction(x => x.New(post));
			}
		}

		[Authorize, AutoMap(typeof (IPostEditModelMapper))]
		public ActionResult Edit(Guid id) { return View(PostTasks.Get(id)); }

		[Authorize, Transaction, ValidateAntiForgeryToken, ValidateInput(false)]
		public ActionResult Update(PostEditModel post) {
			try {
				post.AuthorUserName = _userTasks.GetCurrentUserName();
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