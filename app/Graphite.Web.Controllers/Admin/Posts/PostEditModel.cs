using System;

namespace Graphite.Web.Controllers.Admin.Posts{
	public class PostEditModel : PostEditModelBase{
		public Guid Id { get; set; }
		public DateTime DateCreated { get; set; }
		public DateTime DateModified { get; set; }
	}
}