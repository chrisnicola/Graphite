using Graphite.Core.Contracts.Mapping;
using Graphite.Core.Messages;
using Graphite.Web.Views.Admin.User;

namespace Graphite.Web.Contracts.Mappers{
  public interface ICreateUserMapper : IMapper<NewUserViewModel, CreateUserDetails> {}
}