using System.Collections.Generic;
using Graphite.Core.Domain;

namespace Graphite.Web.Controllers.Comments{
  public class ApprovedCommentsViewModel {
    public IEnumerable<Comment> Comments { get; set; }
  }
}