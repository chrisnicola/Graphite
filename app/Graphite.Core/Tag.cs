using System.Collections.Generic;
using SharpArch.Core.DomainModel;

namespace Graphite.Core {
	public class Tag : Entity {
		[DomainSignature]
		public virtual string Name { get; set; }
		public virtual IList<Post> Posts { get; set; }
	}
}