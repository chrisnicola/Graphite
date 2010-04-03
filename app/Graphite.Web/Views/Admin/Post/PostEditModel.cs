using System;

namespace Graphite.Web.Views.Admin.Post{
	public class PostEditModel : PostEditModelBase{
		public Guid Id { get; set; }
		public DateTime DateCreated { get; set; }
		public DateTime DateModified { get; set; }
	}
}