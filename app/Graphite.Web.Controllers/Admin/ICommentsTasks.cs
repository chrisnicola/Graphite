using System.Collections.Generic;
using Graphite.Core;

namespace Graphite.Web.Controllers.Admin{
  public interface ICommentsTasks {
    IEnumerable<Comment> GetApprovedComments();

    IEnumerable<Comment> GetUnapprovedComments();
  }
}