namespace Tests.Graphite.ApplicationServices.Tasks{
  public class CommentTasksTestBase {
    public virtual void Setup() {
      Tasks = new CommentTasks();
    }
  }

  public class CommentTasks : ICommentTasks {}
}