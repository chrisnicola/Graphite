using System;
using System.Web.Mvc;
using Graphite.ApplicationServices;
using Graphite.Core;
using Graphite.Core.MappingInterfaces;
using Graphite.Data.Repositories;
using Graphite.Web.Controllers.ActionFilters;
using Graphite.Web.Controllers.Mappers;
using Graphite.Web.Controllers.ViewModels;
using SharpArch.Core.PersistenceSupport;
using SharpArch.Core.PersistenceSupport.NHibernate;
using SharpArch.Web.NHibernate;
using MvcContrib;

namespace Graphite.Web.Controllers.Admin {
	public class PostController : PostControllerBase {
  	private readonly IEditPostMapper _editMapper;
  	private readonly ICreatePostMapper _createMapper;

  	public PostController(IPostTasks postTasks, IEditPostMapper editMapper, ICreatePostMapper createMapper) : base(postTasks) {
  		_editMapper = editMapper;
  		_createMapper = createMapper;
  	}

  	[Authorize, AutoMap(typeof(Post), typeof(PostCreateModel))]
		public ActionResult New(PostCreateModel post)
		{
			return View(post ?? new PostCreateModel());
    }

    [Authorize, Transaction, ValidateAntiForgeryToken, ValidateInput(false)]
    public ActionResult Create(PostCreateModel post) {
      try {
				var id = PostTasks.SaveNewPost(_createMapper.MapFrom(post)).Id;
      	return this.RedirectToAction(x => x.Show(id));
      }
      catch {
        return this.RedirectToAction(x => x.New(post));
      }
    }

		[Authorize, AutoMap(typeof(IMapper<Post, PostEditModel>))]
    public ActionResult Edit(Guid id) {
      return View(PostTasks.Get(id));
    }

		[Authorize, Transaction, ValidateAntiForgeryToken, ValidateInput(false)]
    public ActionResult Update(PostEditModel post) {
      try {
        PostTasks.UpdatePost(_editMapper.MapFrom(post));
        return this.RedirectToAction(x => x.Index());
      } catch (Exception) {
				return this.RedirectToAction(x => x.Edit(post.Id));
      }
    }

		[Authorize, AutoMap(typeof(Post), typeof(DeletePostViewModel))]
    public ActionResult Delete(Guid id) {
      return View(PostTasks.Get(id));
    }

		[Authorize, Transaction]
    public ActionResult Destroy(DeletePostViewModel post) {
      try {
				PostTasks.Delete(post.Id);
      } catch (Exception ex) { }
      return RedirectToAction("Index");
    }
  }
}