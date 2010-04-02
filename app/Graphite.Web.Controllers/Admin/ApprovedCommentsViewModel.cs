using System;
using System.Collections.Generic;
using Graphite.Core;

namespace Graphite.Web.Controllers.Admin{
  public class ApprovedCommentsViewModel {
    public IEnumerable<Comment> Comments { get; set; }
  }
}