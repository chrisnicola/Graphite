using Graphite.Core.Domain;

namespace Graphite.Core.Contracts.DataInterfaces{
  public interface IUserRepository : ILinqRepository<User>{
    User GetUser(string username);

    User GetUserByEmail(string email);
  }
}