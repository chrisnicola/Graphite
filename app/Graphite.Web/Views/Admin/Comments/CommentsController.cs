using System.Web.Mvc;
using Graphite.Core.Contracts.Tasks;

namespace Graphite.Web.Views.Admin.Comments{
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