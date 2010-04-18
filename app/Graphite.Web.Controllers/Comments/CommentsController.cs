using System.Web.Mvc;
using Graphite.Core.Contracts.Services;

namespace Graphite.Web.Controllers.Comments{
  public class CommentsController : Controller {
    private readonly ICommentsTasks _tasks;

    public CommentsController(ICommentsTasks tasks) { _tasks = tasks; }

    public ActionResult Approved() {
      return View(new ApprovedCommentsViewModel{Comments = _tasks.GetApprovedComments()});
    }

    public ActionResult UnApproved() {
      return View(new UnapprovedCommentsViewModel() { Comments = _tasks.GetUnapprovedComments() });
    }
  }
}