using System;
using Graphite.Core;

namespace Graphite.Web.Controllers.ViewModels
{
  public class DeletePost
  {
    public DeletePost() : this(new Post()) {  }
    public DeletePost(Post post) {
      Id = post.Id;
    }

    public Guid Id { get; set; }
  }
}
