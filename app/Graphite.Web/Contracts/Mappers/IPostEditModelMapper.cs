using Graphite.Core.Contracts.Mapping;
using Graphite.Core.Domain;
using Graphite.Web.Views.Admin.Post;

namespace Graphite.Web.Contracts.Mappers{
  public interface IPostEditModelMapper : IMapper<Post, PostEditModel> {}
}