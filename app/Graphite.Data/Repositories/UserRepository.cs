using System;
using System.Collections;
using System.Linq;
using Graphite.Core;
using NHibernate.Linq;
using SharpArch.Core.PersistenceSupport.NHibernate;

namespace Graphite.Data.Repositories {
	public interface IUserRepository : ILinqRepository<User> {
		User GetUser(string username);
		User GetUserByEmail(string email);
	}

	public class UserRepository : LinqRepository<User>, IUserRepository {
		public User GetUser(string username) {
			return Session.Linq<User>().SingleOrDefault(m => m.Username == username);
		}

		public User GetUserByEmail(string email) {
			return Session.Linq<User>().SingleOrDefault(m => m.Email == email);
		}
	}
}