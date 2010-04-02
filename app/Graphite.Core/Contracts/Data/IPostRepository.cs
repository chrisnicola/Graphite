using System;
using System.Collections.Generic;
using Graphite.Core.Domain;

namespace Graphite.Core.Contracts.Data{
  public interface IPostRepository : ILinqRepository<Post>{
    /// <summary>
    /// Gets the most recent posts in the database
    /// </summary>
    /// <param name="i">Number of recent posts to get</param>
    IEnumerable<Post> GetRecentPublishedPosts(int i);

    Post GetWithComments(Guid id);
  }
}