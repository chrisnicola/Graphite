using Graphite.Core;
using Graphite.Data.Repositories;

namespace Graphite.Data.Repositories {
	public interface ITagRepository : ILinqRepository<Tag> {}
	public class TagRepository : LinqRepository<Tag>, ITagRepository {}
}