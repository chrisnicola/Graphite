using Graphite.Core;
using Graphite.Data.Repositories;

namespace Graphite.ApplicationServices {
	public class TagTasks : ITagTasks {
		private readonly ITagRepository _tags;
		public TagTasks(ITagRepository tags) { _tags = tags; }

		public Tag GetTagByName(string tagname) { return _tags.FindOne(t => t.Name == tagname); }
	}

	public interface ITagTasks {
		Tag GetTagByName(string tagname);
	}
}