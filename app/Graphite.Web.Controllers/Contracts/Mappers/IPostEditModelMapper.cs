using Graphite.Core.Contracts.Mapping;
using Graphite.Web.Controllers.Admin.Posts;

namespace Graphite.Web.Controllers.Contracts.Mappers{
  public interface IPostEditModelMapper : IMapper<Core.Domain.Post, PostEditModel> {}
}