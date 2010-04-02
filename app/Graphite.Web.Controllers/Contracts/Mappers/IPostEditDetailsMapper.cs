using Graphite.Core.Contracts.Mapping;
using Graphite.Core.Messages;
using Graphite.Web.Controllers.Admin.Posts;

namespace Graphite.Web.Controllers.Contracts.Mappers{
  public interface IPostEditDetailsMapper : IMapper<PostEditModel, PostEditDetails> {}
}