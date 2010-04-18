using Graphite.Core.Contracts.Mapping;
using Graphite.Web.Controllers.Contracts.Mappers;

namespace Graphite.Web.Controllers.Users{
  public class NewUserMapper : GenericMapper<Core.Domain.User, NewUserViewModel>, INewUserMapper {}
}