﻿using System.Collections.Generic;

namespace Graphite.Web.Controllers.Posts{
	public class PostIndexViewModel {
		public bool IsAuthenticated { get; set; }
		public IEnumerable<PostShowViewModel> Posts { get; set; }
	}
}