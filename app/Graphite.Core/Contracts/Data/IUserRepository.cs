using Graphite.Core.Domain;

namespace Graphite.Core.Contracts.Data{
  public interface IUserRepository : ILinqRepository<User>{
    User GetUser(string username);

    User GetUserByEmail(string email);
  }
}