using System;
using System.Collections.Generic;

namespace Graphite.Web.Controllers.ViewModels
{
	public class UserIndexViewModel {
		public IEnumerable<UserViewModel> Users { get; set; }
	}

	public class UserViewModel {
		public Guid Id { get; set;}
		public string Username { get; set; }
		public string Email { get; set; }
		public string PasswordQuestion { get; set; }
	}

	public class NewUserViewModel : UserViewModel {
		public string Password { get; set; }
		public string PasswordAnswer { get; set; }
	}
}


