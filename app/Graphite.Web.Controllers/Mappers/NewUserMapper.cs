using Graphite.Core;
using Graphite.Core.Contracts.MappingInterfaces;
using Graphite.Core.Domain;
using Graphite.Web.Controllers.ViewModels;

namespace Graphite.Web.Controllers.Mappers{
	public interface INewUserMapper : IMapper<User, NewUserViewModel> {}

	public class NewUserMapper : GenericMapper<User, NewUserViewModel>, INewUserMapper {}
}