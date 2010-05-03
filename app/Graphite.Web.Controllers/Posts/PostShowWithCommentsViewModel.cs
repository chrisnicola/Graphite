using System;
using System.Collections.Generic;
using Graphite.Core.Domain;

namespace Graphite.Web.Controllers.Posts{
	public class PostShowWithCommentsViewModel : PostShowViewModel{
		public IList<Comment> Comments { get; set; }
	  private NewCommentViewModel _newComment;
	  public NewCommentViewModel NewComment {
      get { return _newComment ?? new NewCommentViewModel(); } 
      set { _newComment = value; }
	  }
	}

  public class NewCommentViewModel{
    public virtual string Author { get; set; }
    public virtual string EmailAddress { get; set; }
    public virtual string WebAddress { get; set; }
    public virtual string Content { get; set; }
    public virtual Guid ParentId { get; set; }
  }
}