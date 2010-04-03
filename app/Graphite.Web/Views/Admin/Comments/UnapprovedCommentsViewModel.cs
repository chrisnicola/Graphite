using System.Collections.Generic;
using Graphite.Core.Domain;

namespace Graphite.Web.Views.Admin.Comments{
  public class UnapprovedCommentsViewModel {
    public IEnumerable<Comment> Comments { get; set; }
  }
}