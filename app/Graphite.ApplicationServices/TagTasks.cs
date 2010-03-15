using System;
using System.Collections.Generic;
using Graphite.Core;
using Graphite.Core.Domain;
using Graphite.Data.Repositories;

namespace Graphite.ApplicationServices{
	public class TagTasks : ITagTasks{
		readonly ITagRepository _tags;

		public TagTasks(ITagRepository tags) { _tags = tags; }

		public Tag GetTagByName(string tagname) { return _tags.FindOne(t => t.Name == tagname); }

		public IEnumerable<Tag> GetAllTags() { return _tags.FindAll(); }
	}

	public interface ITagTasks{
		Tag GetTagByName(string tagname);
		IEnumerable<Tag> GetAllTags();
	}
}