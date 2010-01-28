using Graphite.Core;

namespace Graphite.Data.Repositories{
	public interface ITagRepository : ILinqRepository<Tag> {}

	public class TagRepository : LinqRepository<Tag>, ITagRepository {}
}