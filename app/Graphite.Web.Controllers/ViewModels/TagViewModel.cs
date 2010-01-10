using System.Collections.Generic;

namespace Graphite.Web.Controllers.ViewModels {
	public class TagViewModel {
		public string Name { get; set; }
		public IEnumerable<PostShowViewModel> Posts { get; set; }
	}
}