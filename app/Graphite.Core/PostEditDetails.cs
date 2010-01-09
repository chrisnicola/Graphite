using System;

namespace Graphite.Core {
	public class PostEditDetails : PostDetailsBase {
		public Guid Id { get; set; }
	}

	public class PostCreateDetails : PostDetailsBase { }
	
}