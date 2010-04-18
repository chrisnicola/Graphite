using System.Collections.Generic;
using Graphite.Core.Contracts.Mapping;
using Graphite.Core.Domain;
using Graphite.Web.Controllers.Admin.Users;

namespace Graphite.Web.Controllers.Contracts.Mappers{
	public interface IUserIndexMapper : IMapper<IEnumerable<User>, UserIndexViewModel> {}
}