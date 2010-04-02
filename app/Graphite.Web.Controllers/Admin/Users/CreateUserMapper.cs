using Graphite.Core.Contracts.MappingInterfaces;
using Graphite.Core.Messages;

namespace Graphite.Web.Controllers.Admin.Users{
	public interface ICreateUserMapper : IMapper<NewUserViewModel, CreateUserDetails> {}

	public class CreateUserMapper : GenericMapper<NewUserViewModel, CreateUserDetails>, ICreateUserMapper {}

	public interface IEditUserMapper : IMapper<EditUserViewModel, EditUserDetails> {}

	public class EditUserMapper : GenericMapper<EditUserViewModel, EditUserDetails>, IEditUserMapper {}
}