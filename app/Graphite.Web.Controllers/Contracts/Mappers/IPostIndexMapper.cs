using System.Collections.Generic;
using Graphite.Core.Contracts.Mapping;
using Graphite.Core.Domain;
using Graphite.Web.Controllers.Posts;

namespace Graphite.Web.Controllers.Contracts.Mappers{
	public interface IPostIndexMapper : IMapper<IEnumerable<Post>, PostIndexViewModel> {}
}