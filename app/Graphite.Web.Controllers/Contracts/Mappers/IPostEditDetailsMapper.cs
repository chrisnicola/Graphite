using Graphite.Core.Contracts.Mapping;
using Graphite.Core.Messages;
using Graphite.Web.Controllers.Posts;

namespace Graphite.Web.Controllers.Contracts.Mappers{
  public interface IPostEditDetailsMapper : IMapper<PostEditModel, PostEditDetails> {}
}