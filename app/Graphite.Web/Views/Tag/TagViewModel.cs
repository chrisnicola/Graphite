using System.Collections.Generic;
using Graphite.Web.Views.Post;

namespace Graphite.Web.Views.Tag{
	public class TagViewModel{
		public string Name { get; set; }
		public IEnumerable<PostShowViewModel> Posts { get; set; }
	}
}