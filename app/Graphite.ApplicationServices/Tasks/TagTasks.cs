using System.Collections.Generic;
using Graphite.Core;
using Graphite.Core.Contracts.Data;
using Graphite.Core.Contracts.Services;
using Graphite.Core.Domain;

namespace Graphite.ApplicationServices.Tasks{
  public class TagTasks : ITagTasks{
    readonly ITagRepository _tags;

    public TagTasks(ITagRepository tags) { _tags = tags; }

    public Tag GetTagByName(string tagname) { return _tags.FindOne(t => t.Name == tagname); }

    public IEnumerable<Tag> GetAllTags() { return _tags.FindAll(); }
  }
}