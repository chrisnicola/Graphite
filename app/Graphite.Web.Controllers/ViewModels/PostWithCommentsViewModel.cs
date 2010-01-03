using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Graphite.Core;

namespace Graphite.Web.Controllers.ViewModels
{
	public class PostIndexViewModel {
		public bool IsAuthenticated { get; set; }
		public IEnumerable<PostViewModel> Posts { get; set; }
	}

	public class PostViewModel {
		public Guid Id { get; set;}
    public string Title { get; set; }
		public string AuthorRealName { get; set; }
    public string Content { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime? DatePublished { get; set; }
    public bool AllowComments { get; set; }
    public bool Published { get; set; }
	}

  public class PostWithCommentsViewModel : PostViewModel
  {
    public PostWithCommentsViewModel() {
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
