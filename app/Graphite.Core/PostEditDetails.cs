using System;

namespace Graphite.Core{
	public class PostImportDetails : PostDetailsBase{
		public DateTime DateCreated { get; set; }
		public DateTime DateModified { get; set; }
	}

	public class PostEditDetails : PostDetailsBase{
		public Guid Id { get; set; }
	}

	public class PostCreateDetails : PostDetailsBase {}
}