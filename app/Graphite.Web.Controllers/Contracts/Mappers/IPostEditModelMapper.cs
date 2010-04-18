using Graphite.Core.Contracts.Mapping;
using Graphite.Web.Controllers.Posts;

namespace Graphite.Web.Controllers.Contracts.Mappers{
  public interface IPostEditModelMapper : IMapper<Core.Domain.Post, PostEditModel> {}
}