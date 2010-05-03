using Graphite.ApplicationServices.Tasks;
using Graphite.Core.Contracts.Services;

namespace Tests.Graphite.ApplicationServices.Tasks{
  public class CommentTasksTestBase {
    
    protected ICommentsTasks Tasks;
    public virtual void Setup() {
      Tasks = new CommentsTasks();
    }


  }
}