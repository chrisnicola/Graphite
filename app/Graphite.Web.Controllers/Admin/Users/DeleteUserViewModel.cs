using System;

namespace Graphite.Web.Controllers.Admin.Users{
	public class DeleteUserViewModel{
		public Guid Id { get; set; }
		public string Username { get; set; }
	}
}