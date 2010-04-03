using System.Collections.Generic;
using Graphite.Core.Domain;

namespace Graphite.Web.Views.Post{
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