using Graphite.Core.Contracts.Mapping;
using Graphite.Core.Domain;
using Graphite.Web.Controllers.Tags;

namespace Graphite.Web.Controllers.Contracts.Mappers{
  public interface ITagShowMapper : IMapper<Tag, TagViewModel> {}
}