using System;
using System.Web.Mvc;
using Graphite.Core.Contracts.Data;
using Graphite.Core.Contracts.Services;
using Graphite.Core.Domain;
using Graphite.Web.Controllers.ActionFilters;
using Graphite.Web.Controllers.Contracts.Mappers;
using MvcContrib;
using SharpArch.Web.NHibernate;

namespace Graphite.Web.Controllers.Posts{
  public class PostController : Controller{
    private readonly IPostTasks PostTasks;
    private readonly IPostRepository _posts;
    private IUserTasks _userTasks;
    private IPostEditDetailsMapper _postEditMapper;
    private IPostCreateDetailsMapper _postCreateDetailsMapper;

    public PostController(IPostTasks postTasks, IPostRepository posts) {
      PostTasks = postTasks;
      _posts = posts;
    }

    [AutoMap(typeof (Post), typeof (PostShowWithCommentsViewModel))]
    public ActionResult Id(Guid id) { return View(_posts.FindOne(p => p.Id == id)); }

    [AutoMap(typeof (Post), typeof (PostShowWithCommentsViewModel))]
    public ActionResult Show(string id) { return View(_posts.FindOne(p => p.Slug == id)); }

    [AutoMap(typeof (IPostIndexMapper))]
    public ActionResult Index() { return View(PostTasks.GetAll()); }

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

    /*
    [ValidateInput(false)]
    public ActionResult Update(PostShowWithCommentsViewModel postShowVm) {
      Post post = PostTasks.Get(postShowVm.Id);
      try {
        if (CommentIsValid(postShowVm.NewComment)) post.AddComment(postShowVm.NewComment);
      } catch {}
      return this.RedirectToAction(c => c.Show(post.Slug));
    }

    private static bool CommentIsValid(Comment comment) {
      return !string.IsNullOrEmpty(comment.Author)
        && !string.IsNullOrEmpty(comment.EmailAddress)
          && !string.IsNullOrEmpty(comment.Content);
    }*/
  }
}