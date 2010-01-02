using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Graphite.Core;

namespace Graphite.Web.Controllers.ViewModels
{
  public class PostWithCommentsViewModel
  {
    public PostWithCommentsViewModel() {
    	NewComment = new Comment();
    }

    public Guid Id { get; set;}
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime? DatePublished { get; set; }
    public bool AllowComments { get; set; }
    public bool Published { get; set; }
    public IList<Comment> Comments { get; set; }
  	private Comment _newComment;
  	public Comment NewComment {
  		get {
				if (_newComment == null) return new Comment();
  			return _newComment;
  		} 
			set { _newComment = value; }
  	}
  }

	public abstract class PostEditModelBase {
		public string Title { get; set; }
		public string Content { get; set; }
		public DateTime? DatePublished { get; set; }
		public bool AllowComments { get; set; }
		public bool Published { get; set; }
		public string Slug { get; set; }
	}

	public class PostEditModel : PostEditModelBase {
		public Guid Id { get; set; }
		public DateTime DateCreated { get; set; }
		public DateTime LastEdited { get; set; }
	}

	public class PostCreateModel : PostEditModelBase { }
}
