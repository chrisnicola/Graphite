using System.Linq;
using Graphite.Core;
using Graphite.Core.Contracts.Data;
using Graphite.Core.Domain;
using NHibernate.Linq;

namespace Graphite.Data.Repositories{
  public class UserRepository : LinqRepository<User>, IUserRepository{
		public User GetUser(string username) { return Session.Linq<User>().SingleOrDefault(m => m.Username == username); }

		public User GetUserByEmail(string email) { return Session.Linq<User>().SingleOrDefault(m => m.Email == email); }
	}
}