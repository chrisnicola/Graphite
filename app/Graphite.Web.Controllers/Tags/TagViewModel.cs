using System.Collections.Generic;
using Graphite.Web.Controllers.Posts;

namespace Graphite.Web.Controllers.Tags{
	public class TagViewModel{
		public string Name { get; set; }
		public IEnumerable<PostShowViewModel> Posts { get; set; }
	}
}