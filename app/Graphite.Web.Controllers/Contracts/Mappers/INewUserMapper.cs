using Graphite.Core.Contracts.Mapping;
using Graphite.Web.Controllers.Users;

namespace Graphite.Web.Controllers.Contracts.Mappers{
	public interface INewUserMapper : IMapper<Core.Domain.User, NewUserViewModel> {}
}