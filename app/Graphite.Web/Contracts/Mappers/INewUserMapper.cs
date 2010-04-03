using Graphite.Core.Contracts.Mapping;
using Graphite.Core.Domain;
using Graphite.Web.Views.Admin.User;

namespace Graphite.Web.Contracts.Mappers{
	public interface INewUserMapper : IMapper<User, NewUserViewModel> {}
}