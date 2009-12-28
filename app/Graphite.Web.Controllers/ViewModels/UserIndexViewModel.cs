using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Graphite.Web.Controllers.ViewModels
{
	public class UserIndexViewModel {
		IList<UserViewModel> Users { get; set; }
	}

	public class UserViewModel {
		public Guid Id { get; set;}
		public string Username { get; set; }
		public string Email { get; set; }
		public string PasswordQuestion { get; set; }
	}
}
