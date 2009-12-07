using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Graphite.Core;

namespace Graphite.Web.Controllers.ViewModels
{
  public class ShowPostWithComments
  {
    public ShowPostWithComments() : this(new Post()) { }
    public ShowPostWithComments(Post post) {
      Post = post;
      PostId = post.Id;
      NewComment = new Comment {Post = post};
    }

    public Post Post { get; set; }
    public Guid PostId { get; set; }
    public Comment NewComment { get; set; }
  }
}
