using Graphite.Core;
using Graphite.Core.Contracts.Data;
using Graphite.Core.Domain;

namespace Graphite.Data.Repositories{
  public class TagRepository : LinqRepository<Tag>, ITagRepository {}
}