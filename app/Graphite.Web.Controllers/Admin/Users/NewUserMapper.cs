using Graphite.Core.Contracts.Mapping;
using Graphite.Web.Controllers.Contracts.Mappers;

namespace Graphite.Web.Controllers.Admin.Users{
  public class NewUserMapper : GenericMapper<Core.Domain.User, NewUserViewModel>, INewUserMapper {}
}