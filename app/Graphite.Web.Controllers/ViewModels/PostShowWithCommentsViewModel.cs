using System.Collections.Generic;
using Graphite.Core;
using Graphite.Core.Domain;

namespace Graphite.Web.Controllers.ViewModels{
	public class PostShowWithCommentsViewModel : PostShowViewModel{
		Comment _newComment;

		public PostShowWithCommentsViewModel() { NewComment = new Comment(); }

		public IList<Comment> Comments { get; set; }
		public Comment NewComment {
			get {
				if (_newComment == null) return new Comment();
				return _newComment;
			}
			set { _newComment = value; }
		}
	}
}