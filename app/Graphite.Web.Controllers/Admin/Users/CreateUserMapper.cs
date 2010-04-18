using Graphite.Core.Contracts.Mapping;
using Graphite.Core.Messages;
using Graphite.Web.Controllers.Contracts.Mappers;

namespace Graphite.Web.Controllers.Admin.Users{
  public class CreateUserMapper : GenericMapper<NewUserViewModel, CreateUserDetails>, ICreateUserMapper {}

  public class EditUserMapper : GenericMapper<EditUserViewModel, EditUserDetails>, IEditUserMapper {}
}