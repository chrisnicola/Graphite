using Graphite.Core.Contracts.Mapping;
using Graphite.Core.Messages;
using Graphite.Web.Contracts.Mappers;

namespace Graphite.Web.Views.Admin.User{
  public class CreateUserMapper : GenericMapper<NewUserViewModel, CreateUserDetails>, ICreateUserMapper {}

  public class EditUserMapper : GenericMapper<EditUserViewModel, EditUserDetails>, IEditUserMapper {}
}