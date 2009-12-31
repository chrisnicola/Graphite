using System;
using SharpArch.Core.DomainModel;

namespace Graphite.Core {
	public class User : EntityWithTypedId<Guid> {
		public virtual string Salt { get; set; }
		public virtual string Username { get; set; }
		public virtual string Email { get; set; }
		public virtual string Password { get; set; }
		public virtual string PasswordQuestion { get; set; }
		public virtual string PasswordAnswer { get; set; }
		public virtual DateTime CreationDate { get; set; }
		public virtual string RealName { get; set; }
		public virtual DateTime? LastLogin { get; set; }
	}
}