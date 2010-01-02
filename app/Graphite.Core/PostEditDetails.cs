using System;

namespace Graphite.Core {
	public class PostEditDetails {
		public Guid Id { get; set; }
		public User Author { get; set; }
		public string Title { get; set; }
		public string Content { get; set; }
		public DateTime? DatePublished { get; set; }
		public bool AllowComments { get; set; }
		public bool Published { get; set; }
		public string Slug { get; set; }
	}
	public class PostCreateDetails {
		public User Author { get; set; }
		public string Title { get; set; }
		public string Content { get; set; }
		public DateTime? DatePublished { get; set; }
		public bool AllowComments { get; set; }
		public bool Published { get; set; }
		public string Slug { get; set; }
	}
}