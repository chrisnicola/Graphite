using Graphite.Core.Contracts.Mapping;

namespace Graphite.Web.Controllers.Admin.Users{
	public interface INewUserMapper : IMapper<Core.Domain.User, NewUserViewModel> {}

	public class NewUserMapper : GenericMapper<Core.Domain.User, NewUserViewModel>, INewUserMapper {}
}