using Graphite.Core;
using Graphite.Core.Contracts.DataInterfaces;
using Graphite.Core.Domain;

namespace Graphite.Data.Repositories{
  public class TagRepository : LinqRepository<Tag>, ITagRepository {}
}