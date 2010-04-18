using System.Collections.Generic;
using Graphite.Core.Contracts.Mapping;
using Graphite.Core.Domain;
using Graphite.Web.Controllers.Home;

namespace Graphite.Web.Controllers.Contracts.Mappers{
	public interface IHomeIndexMapper : IMapper<IEnumerable<Post>, HomeIndexViewModel> {}
}