using System.Collections.Generic;
using Graphite.Core;
using Graphite.Core.Domain;

namespace Graphite.Web.Controllers.Admin{
  public class UnapprovedCommentsViewModel {
    public IEnumerable<Comment> Comments { get; set; }
  }
}