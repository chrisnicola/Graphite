using Graphite.Core;
using Graphite.Core.MappingInterfaces;
using Graphite.Web.Controllers.ViewModels;

namespace Graphite.Web.Controllers.Mappers{
	public interface INewUserMapper : IMapper<User, NewUserViewModel> {}

	public class NewUserMapper : GenericMapper<User, NewUserViewModel>, INewUserMapper {}
}