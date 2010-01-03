using System.Collections.Generic;
using System.Linq;
using System.Text;
using Graphite.Core;

namespace Graphite.Web.Controllers.ViewModels
{
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
