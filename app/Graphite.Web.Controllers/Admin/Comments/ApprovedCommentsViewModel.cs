using System.Collections.Generic;
using Graphite.Core.Domain;

namespace Graphite.Web.Controllers.Admin.Comments{
  public class ApprovedCommentsViewModel {
    public IEnumerable<Comment> Comments { get; set; }
  }
}