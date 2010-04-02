using Graphite.Core;
using Graphite.Core.Contracts.MappingInterfaces;
using Graphite.Core.Messages;
using Graphite.Web.Controllers.ViewModels;

namespace Graphite.Web.Controllers.Mappers{
	public interface ICreateUserMapper : IMapper<NewUserViewModel, CreateUserDetails> {}

	public class CreateUserMapper : GenericMapper<NewUserViewModel, CreateUserDetails>, ICreateUserMapper {}

	public interface IEditUserMapper : IMapper<EditUserViewModel, EditUserDetails> {}

	public class EditUserMapper : GenericMapper<EditUserViewModel, EditUserDetails>, IEditUserMapper {}
}