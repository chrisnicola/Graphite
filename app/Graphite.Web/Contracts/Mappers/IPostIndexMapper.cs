using System.Collections.Generic;
using Graphite.Core.Contracts.Mapping;
using Graphite.Core.Domain;
using Graphite.Web.Views.Post;

namespace Graphite.Web.Contracts.Mappers{
	public interface IPostIndexMapper : IMapper<IEnumerable<Post>, PostIndexViewModel> {}
}