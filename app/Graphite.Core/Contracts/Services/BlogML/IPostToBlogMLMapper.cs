using BlogML.Xml;
using Graphite.Core.Contracts.Mapping;
using Graphite.Core.Domain;

namespace Graphite.Core.Contracts.Services.BlogML{
  public interface IPostToBlogMLMapper : IMapper<Post, BlogMLPost> {}
}