using Graphite.Core;
using Graphite.Core.Domain;

namespace Graphite.Data.Repositories{
	public interface ITagRepository : ILinqRepository<Tag> {}

	public class TagRepository : LinqRepository<Tag>, ITagRepository {}
}