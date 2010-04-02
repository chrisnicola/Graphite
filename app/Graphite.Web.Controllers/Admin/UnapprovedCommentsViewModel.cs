using System.Collections.Generic;
using Graphite.Core;

namespace Graphite.Web.Controllers.Admin{
  public class UnapprovedCommentsViewModel {
    public IEnumerable<Comment> Comments { get; set; }
  }
}