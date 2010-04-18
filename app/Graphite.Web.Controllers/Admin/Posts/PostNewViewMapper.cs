using Graphite.Core.Contracts.Mapping;
using Graphite.Core.Messages;
using Graphite.Web.Controllers.Contracts.Mappers;

namespace Graphite.Web.Controllers.Admin.Posts{
  public class PostCreateDetailsDetailsMapper : GenericMapper<PostNewModel, PostCreateDetails>, IPostCreateDetailsMapper {}
}