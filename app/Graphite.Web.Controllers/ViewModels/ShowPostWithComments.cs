using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Graphite.Core;

namespace Graphite.Web.Controllers.ViewModels
{
  public class ShowPostWithComments
  {
    public ShowPostWithComments() {
    	NewComment = new Comment();
    }

    public Post Post { get; set; }
    public virtual Guid Id { get; set;}
    public virtual string Title { get; set; }
    public virtual string Content { get; set; }
    public virtual DateTime DateCreated { get; set; }
    public virtual DateTime? DatePublished { get; set; }
    public virtual bool AllowComments { get; set; }
    public virtual bool Published { get; set; }
    public virtual IList<Comment> Comments { get; set; }
		public Comment NewComment { get; set; }
  }
}
