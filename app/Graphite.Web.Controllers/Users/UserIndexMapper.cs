using System.Collections.Generic;
using System.Linq;
using Graphite.Core.Contracts.Mapping;
using Graphite.Web.Controllers.Contracts.Mappers;

namespace Graphite.Web.Controllers.Users{
  public class UserIndexMapper : IUserIndexMapper{
		readonly IMapper<Core.Domain.User, UserViewModel> _mapper;

		public UserIndexMapper(IMapper<Core.Domain.User, UserViewModel> mapper) { _mapper = mapper; }

		public UserIndexViewModel MapFrom(IEnumerable<Core.Domain.User> source) { return new UserIndexViewModel {Users = source.Select(u => _mapper.MapFrom(u))}; }

		public object MapFrom(object source) { return MapFrom(source as IEnumerable<Core.Domain.User>); }
	}
}