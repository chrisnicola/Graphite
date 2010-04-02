using System.Collections.Generic;
using Graphite.Core.Domain;

namespace Graphite.Core.Contracts.TaskInterfaces{
  public interface ITagTasks{
    Tag GetTagByName(string tagname);
    IEnumerable<Tag> GetAllTags();
  }
}