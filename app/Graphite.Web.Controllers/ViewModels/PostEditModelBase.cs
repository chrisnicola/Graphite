using System;
using System.Collections.Generic;
using Graphite.Core;

namespace Graphite.Web.Controllers.ViewModels {
	public abstract class PostEditModelBase {
		public IEnumerable<User> Authors { get; set; }
		public Guid AuthorId { get; set;}
		public string Title { get; set; }
		public string Content { get; set; }
		public DateTime? DatePublished { get; set; }
		public bool AllowComments { get; set; }
		public bool Published { get; set; }
		public string Slug { get; set; }
	}
}