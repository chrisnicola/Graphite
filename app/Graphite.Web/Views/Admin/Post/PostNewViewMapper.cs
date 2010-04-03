using Graphite.Core.Contracts.Mapping;
using Graphite.Core.Messages;
using Graphite.Web.Contracts.Mappers;

namespace Graphite.Web.Views.Admin.Post{
  public class PostCreateDetailsDetailsMapper : GenericMapper<PostNewModel, PostCreateDetails>, IPostCreateDetailsMapper {}
}