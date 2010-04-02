using Graphite.Core.Contracts.Mapping;
using Graphite.Core.Messages;
using Graphite.Web.Controllers.Admin.Users;

namespace Graphite.Web.Controllers.Contracts.Mappers{
  public interface IEditUserMapper : IMapper<EditUserViewModel, EditUserDetails> {}
}