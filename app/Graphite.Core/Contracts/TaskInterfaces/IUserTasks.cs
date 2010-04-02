using System;
using System.Collections.Generic;
using Graphite.Core.Domain;
using Graphite.Core.Messages;

namespace Graphite.Core.Contracts.TaskInterfaces{
  public interface IUserTasks{
    IEnumerable<User> GetUsers();

    User AddUser(CreateUserDetails user);

    User UpdateUser(EditUserDetails user);

    User AuthenticateUser(string username, string password);

    bool ValidateUser(string username, string password);

    User GetUser(Guid id);

    User GetUserByEmail(string email);

    string GetCurrentUserName();

    void RemoveUser(Guid id);

    bool IsLoggedIn();

    void SignOut();
  }
}