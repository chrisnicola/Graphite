using System;

namespace Graphite.Core {
	public class PostImportDetails  : PostDetailsBase {
		public string AuthorEmail { get; set; }
	}

	public class PostEditDetails : PostDetailsBase {
		public Guid Id { get; set; }
	}

	public class PostCreateDetails : PostDetailsBase {
		
	}
	
}