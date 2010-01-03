using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Graphite.Core;

namespace Graphite.Web.Controllers.ViewModels
{
	public class PostItemViewModel {
		public Guid Id { get; set;}
    public string Title { get; set; }
		public string AuthorRealName { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime? DatePublished { get; set; }
    public bool AllowComments { get; set; }
    public bool Published { get; set; }
	}

	public class PostShowViewModel : PostItemViewModel {
		public string Content { get; set; }
	}

  public class PostShowWithCommentsViewModel : PostShowViewModel
  {
    public PostShowWithCommentsViewModel() {
    	NewComment = new Comment();
    }
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
}
