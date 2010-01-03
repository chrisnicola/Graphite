using System;

namespace Graphite.Web.Controllers.ViewModels {
	public class PostItemViewModel {
		public Guid Id { get; set;}
		public string Title { get; set; }
		public string AuthorRealName { get; set; }
		public DateTime DateCreated { get; set; }
		public DateTime? DatePublished { get; set; }
		public bool AllowComments { get; set; }
		public bool Published { get; set; }
	}
}