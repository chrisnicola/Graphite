using System;
using System.Collections.Generic;

namespace Graphite.Web.Controllers.ViewModels{
	public class UserIndexViewModel{
		public IEnumerable<UserViewModel> Users { get; set; }
	}

	public class UserViewModel{
		public Guid Id { get; set; }
		public string Username { get; set; }
		public string RealName { get; set; }
		public string Email { get; set; }
		public DateTime? LastLogin { get; set; }
		public DateTime? Created { get; set; }
	}
}