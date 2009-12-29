using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Graphite.Core;
using Graphite.Core.MappingInterfaces;
using Graphite.Web.Controllers.ViewModels;

namespace Graphite.Web.Controllers.Mappers {

	public interface IUserIndexMapper : IMapper<IEnumerable<User>, UserIndexViewModel> { }

	public class UserIndexMapper : GenericMapper<IEnumerable<User>, UserIndexViewModel>, IUserIndexMapper {
		public UserIndexMapper() {
			Mapper.CreateMap<User, UserViewModel>();
		}

		public override UserIndexViewModel MapFrom(IEnumerable<User> source)
		{
			return new UserIndexViewModel { Users = source.Select(u => Mapper.Map<User,UserViewModel>(u)) };
		}
	}
}