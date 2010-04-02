using System.Collections.Generic;
using System.Linq;
using Graphite.Core;
using Graphite.Core.Contracts.MappingInterfaces;
using Graphite.Core.Domain;
using Graphite.Web.Controllers.ViewModels;

namespace Graphite.Web.Controllers.Mappers{
	public interface IUserIndexMapper : IMapper<IEnumerable<User>, UserIndexViewModel> {}

	public class UserIndexMapper : IUserIndexMapper{
		readonly IMapper<User, UserViewModel> _mapper;

		public UserIndexMapper(IMapper<User, UserViewModel> mapper) { _mapper = mapper; }

		public UserIndexViewModel MapFrom(IEnumerable<User> source) { return new UserIndexViewModel {Users = source.Select(u => _mapper.MapFrom(u))}; }

		public object MapFrom(object source) { return MapFrom(source as IEnumerable<User>); }
	}
}