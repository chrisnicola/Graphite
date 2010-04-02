using System;
using System.Collections.Generic;
using Graphite.Core.Contracts.TaskInterfaces;
using Graphite.Core.Domain;

namespace Tests.Graphite.ApplicationServices.Tasks{
  public class CommentTasksTestBase {
    
    protected ICommentsTasks Tasks;
    public virtual void Setup() {
      Tasks = new CommentsTasks();
    }
  }

  public class CommentsTasks : ICommentsTasks {
    public IEnumerable<Comment> GetApprovedComments() { throw new NotImplementedException(); }

    public IEnumerable<Comment> GetUnapprovedComments() { throw new NotImplementedException(); }
  }
}