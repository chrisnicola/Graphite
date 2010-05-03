using System;
using System.Web.Mvc;
using Graphite.Core.Contracts.Services;
using Graphite.Core.Domain;
using Graphite.Web.Controllers.Posts;

namespace Graphite.Web.Controllers.Comments{
  public class CommentsController : Controller {
    private readonly ICommentsTasks _tasks;
    private Guid? _postId;

    public CommentsController(ICommentsTasks tasks) { _tasks = tasks; }

    protected override void OnActionExecuting(ActionExecutingContext filterContext)
    {
      if (!filterContext.RouteData.Values.ContainsKey("postId")) return;
      _postId = new Guid(filterContext.RouteData.Values["postId"].ToString());
      base.OnActionExecuting(filterContext);
    }

    public ActionResult Approved() {
      return View(new ApprovedCommentsViewModel{Comments = _tasks.GetApprovedComments()});
    }

    public ActionResult UnApproved() {
      return View(new UnapprovedCommentsViewModel() { Comments = _tasks.GetUnapprovedComments() });
    }

    public ActionResult Create(NewCommentViewModel comment) {
      if (_postId != null) {
        _tasks.AddCommentToPost(new Comment {
          Author = comment.Author,
          EmailAddress = comment.EmailAddress,
          WebAddress = comment.WebAddress,
          Content = comment.Content,
        }, _postId);
      }
      return RedirectToAction("Show", "Posts", new {id = _postId});
    } 
  }
}