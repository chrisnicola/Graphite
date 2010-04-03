using Graphite.Core.Contracts.Mapping;
using Graphite.Web.Contracts.Mappers;

namespace Graphite.Web.Views.Admin.User{
  public class NewUserMapper : GenericMapper<Core.Domain.User, NewUserViewModel>, INewUserMapper {}
}