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
    private readonly IPostTasks _postTasks;
    private readonly IPostRepository _posts;
    private readonly IUserTasks _userTasks;
    private readonly IPostEditDetailsMapper _postEditMapper;
    private readonly IPostCreateDetailsMapper _postCreateMapper;

    public PostController(IPostTasks postTasks, 
      IPostRepository posts, 
      IUserTasks userTasks, 
      IPostCreateDetailsMapper postCreateMapper, 
      IPostEditDetailsMapper postEditMapper) {
      _postTasks = postTasks;
      _posts = posts;
      _userTasks = userTasks;
      _postCreateMapper = postCreateMapper;
      _postEditMapper = postEditMapper;
    }

    [AutoMap(typeof (Post), typeof (PostShowWithCommentsViewModel))]
    public ActionResult Id(Guid id) { return View("Show", _posts.FindOne(p => p.Id == id)); }

    [AutoMap(typeof (Post), typeof (PostShowWithCommentsViewModel))]
    public ActionResult Show(string id) { return View(_posts.FindOne(p => p.Slug == id)); }

    [AutoMap(typeof (IPostIndexMapper))]
    public ActionResult Index() { return View(_postTasks.GetAll()); }

    [Authorize]
    public ActionResult New(PostNewModel post) {
      var userName = _userTasks.GetCurrentUserName();
      return View(post ?? new PostNewModel{AuthorUserName = userName});
    }

    [Authorize, Transaction, ValidateAntiForgeryToken, ValidateInput(false)]
    public ActionResult Create(PostNewModel post) {
      try {
        post.AuthorUserName = _userTasks.GetCurrentUserName();
        Post newPost = _postTasks.SaveNewPost(_postCreateMapper.MapFrom(post));
        return this.RedirectToAction(x => x.Show(newPost.Slug));
      } catch {
        return this.RedirectToAction(x => x.New(post));
      }
    }

    [Authorize, AutoMap(typeof (IPostEditModelMapper))]
    public ActionResult Edit(Guid id) { return View(_postTasks.Get(id)); }

    [Authorize, Transaction, ValidateAntiForgeryToken, ValidateInput(false)]
    public ActionResult Update(PostEditModel post) {
      try {
        post.AuthorUserName = _userTasks.GetCurrentUserName();
        _postTasks.UpdatePost(_postEditMapper.MapFrom(post));
        return this.RedirectToAction(x => x.Index());
      } catch (Exception) {
        return this.RedirectToAction(x => x.Edit(post.Id));
      }
    }

    [Authorize, AutoMap(typeof (Post), typeof (DeletePostViewModel))]
    public ActionResult Delete(Guid id) { return View(_postTasks.Get(id)); }

    [Authorize, Transaction]
    public ActionResult Destroy(Guid id) {
      try {
        _postTasks.Delete(id);
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