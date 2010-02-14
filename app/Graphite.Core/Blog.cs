using System;
using System.Collections.Generic;
using SharpArch.Core.DomainModel;

namespace Graphite.Core
{
	public class Blog : EntityWithTypedId<Guid>{
		public string Name { get; set; }
		public string Url { get; set; }
	}
}
