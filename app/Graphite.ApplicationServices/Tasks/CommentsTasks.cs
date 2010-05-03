using System;
using System.Collections.Generic;
using Graphite.Core.Contracts.Services;
using Graphite.Core.Domain;

namespace Graphite.ApplicationServices.Tasks{
  public class CommentsTasks : ICommentsTasks {
    public IEnumerable<Comment> GetApprovedComments() { throw new NotImplementedException(); }

    public IEnumerable<Comment> GetUnapprovedComments() { throw new NotImplementedException(); }

    public IEnumerable<Comment> GetCommentsForPost(Guid postId) { throw new NotImplementedException(); }

    public void AddCommentToPost(Comment comment, Guid? postId) { throw new NotImplementedException(); }
  }
}