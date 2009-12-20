using System;
using System.Linq;
using Graphite.Core;
using NHibernate.Linq;
using SharpArch.Core.PersistenceSupport.NHibernate;
using SharpArch.Data.NHibernate;

namespace Graphite.Data.Repositories {
	public interface IUserRepository : INHibernateRepositoryWithTypedId<User, Guid> {
		User GetUser(string username);
		User GetUserByEmail(string email);
	}

	public class UserRepository : NHibernateRepositoryWithTypedId<User, Guid>, IUserRepository {
		public User GetUser(string username) {
			return Session.Linq<User>().SingleOrDefault(m => m.Username == username);
		}

		public User GetUserByEmail(string email) {
			return Session.Linq<User>().SingleOrDefault(m => m.Email == email);
		}
	}
}