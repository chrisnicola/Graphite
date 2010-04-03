using System.Collections.Generic;
using Graphite.Core.Contracts.Mapping;
using Graphite.Core.Domain;
using Graphite.Web.Views.Home;

namespace Graphite.Web.Contracts.Mappers{
	public interface IHomeIndexMapper : IMapper<IEnumerable<Post>, HomeIndexViewModel> {}
}