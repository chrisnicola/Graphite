using System;

namespace Graphite.Core {
	public class EditUserDetails {
		public Guid Id { get; set; }
		public string Username { get; set; }
		public string RealName { get; set; }
		public string Email { get; set; }
		public string NewPassword { get; set; }
	}
}