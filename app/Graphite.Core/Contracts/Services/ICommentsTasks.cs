using System;
using System.Collections.Generic;
using Graphite.Core.Domain;

namespace Graphite.Core.Contracts.Services{
  public interface ICommentsTasks {
    IEnumerable<Comment> GetApprovedComments();
    IEnumerable<Comment> GetUnapprovedComments();
    IEnumerable<Comment> GetCommentsForPost(Guid postId);
    void AddCommentToPost(Comment comment, Guid? postId);
  }
}