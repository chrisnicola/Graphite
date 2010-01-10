﻿using System;

namespace Graphite.Core {
	public class PostDetailsBase {
		public Guid AuthorId { get; set; }
		public string Title { get; set; }
		public string Content { get; set; }
		public DateTime? DatePublished { get; set; }
		public bool AllowComments { get; set; }
		public bool Published { get; set; }
		public string Slug { get; set; }
		public string Tags { get; set; }
	}
}