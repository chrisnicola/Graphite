using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Graphite.Core;

namespace Graphite.Web.Controllers.ViewModels
{
  public class PostViewModel
  {
    public PostViewModel() : this(new Post()) { }
    public PostViewModel(Post post) {
      Post = post;
      PostId = post.Id;
      NewComment = new Comment {Post = post};
    }

    public Post Post { get; set; }
    public Guid PostId { get; set; }
    public Comment NewComment { get; set; }
  }
}
