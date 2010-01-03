using System.Collections.Generic;

namespace Graphite.Web.Controllers.ViewModels {
	public class PostIndexViewModel {
		public bool IsAuthenticated { get; set; }
		public IEnumerable<PostViewModel> Posts { get; set; }
	}
}