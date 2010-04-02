using System.Collections.Generic;
using System.Web.Mvc;
using Graphite.Core;
using Graphite.Core.Contracts.TaskInterfaces;
using Graphite.Core.Domain;
using Graphite.Web.Controllers.Admin;
using Graphite.Web.Controllers.Admin.Comments;
using NUnit.Framework;
using Rhino.Mocks;
using MvcContrib.TestHelper;

namespace Tests.Graphite.Web.Controllers.Admin{
  
  public static class TestHelper{
    public static T AssertViewData<T>(this ActionResult result) { return result.AssertViewRendered().WithViewData<T>(); }
  }

  public class CommentsControllerTestBase {
    protected ICommentsTasks Tasks;
    protected CommentsController Controller;
    protected ActionResult Result;
    protected IEnumerable<Comment> TestAppComments = new[] {
      new Comment {Approved = true},
      new Comment {Approved = true},
      new Comment {Approved = true},
    };
    protected IEnumerable<Comment> TestUnappComments = new[] {
      new Comment {Approved = false},
      new Comment {Approved = false},
      new Comment {Approved = false},
    };
    public virtual void SetUp() {
      Tasks = MockRepository.GenerateStub<ICommentsTasks>();
      Tasks.Stub(t => t.GetApprovedComments()).Return(TestAppComments);
      Tasks.Stub(t => t.GetUnapprovedComments()).Return(TestUnappComments);
      Controller = new CommentsController(Tasks);
    }
  }

  [TestFixture]
  public class when_approved_comments_requested : CommentsControllerTestBase{
    [SetUp]
    public override void SetUp() {
      base.SetUp();
      Result = Controller.Approved();
    }

    [Test] public void approved_comments_view_is_rendered() { Result.AssertViewRendered(); }

    [Test] public void view_is_rendered_with_approvedcommentsviewmodel() { Result.AssertViewData<ApprovedCommentsViewModel>().ShouldNotBeNull("View Data is null"); }

    [Test] public void action_call_getapprovedcomments_task() { Tasks.AssertWasCalled(t => t.GetApprovedComments()); }

    [Test] public void viewdata_contains_a_list_of_comments() { Result.AssertViewData<ApprovedCommentsViewModel>().Comments.ShouldBe(TestAppComments); }
  }

  public class when_unapproved_comments_requested : CommentsControllerTestBase{
    [SetUp]
    public override void SetUp() {
      base.SetUp();
      Result = Controller.UnApproved();
    }
    [Test] public void view_is_rendered() { Result.AssertViewRendered(); }

    [Test]
    public void view_is_rendered_with_unapprovedcommentsviewmodel() { Result.AssertViewData<UnapprovedCommentsViewModel>().ShouldNotBeNull("View Data is null"); }

    [Test]
    public void action_call_getunapprovedcomments_task() { Tasks.AssertWasCalled(t => t.GetUnapprovedComments()); }

    [Test]
    public void viewdata_contains_a_list_of_comments() { Result.AssertViewData<UnapprovedCommentsViewModel>().Comments.ShouldBe(TestUnappComments); }
  }
}