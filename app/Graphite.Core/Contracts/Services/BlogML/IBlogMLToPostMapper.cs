using System.Collections.Generic;
using BlogML.Xml;
using Graphite.Core.Contracts.Mapping;
using Graphite.Core.Messages;

namespace Graphite.Core.Contracts.Services.BlogML{
  public interface IBlogMLToPostMapper : IMapper<BlogMLPost, PostImportDetails>{
    PostImportDetails MapFrom(BlogMLPost post, IEnumerable<BlogMLCategory> categories);
  }
}