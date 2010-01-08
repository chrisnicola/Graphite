using System.Collections.Generic;
using SharpArch.Core.DomainModel;

namespace Graphite.Core {
	public class Category : Entity {
		[DomainSignature]
		public virtual string Name { get; set; }
		[DomainSignature]
		public virtual Category Parent { get; set; }
		public virtual IList<Category> Children { get; set; }
		public virtual IList<Post> Posts { get; set; }
	}
}