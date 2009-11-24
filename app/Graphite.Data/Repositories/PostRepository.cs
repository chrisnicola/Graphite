﻿using System;
using System.Collections.Generic;
using System.Linq;
using Graphite.Core;
using NHibernate.Linq;
using SharpArch.Core.PersistenceSupport.NHibernate;
using SharpArch.Data.NHibernate;

namespace Graphite.Data.Repositories {
  public class PostRepository : NHibernateRepositoryWithTypedId<Post, Guid>, IPostRepository {
    public IEnumerable<Post> GetRecentPosts(int i) { return Session.Linq<Post>().Where(p => p.Published).OrderByDescending(p => p.DatePublished).Take(i); }
  }

  public interface IPostRepository : INHibernateRepositoryWithTypedId<Post, Guid> {
    /// <summary>
    /// Gets the most recent posts in the database
    /// </summary>
    /// <param name="i">Number of recent posts to get</param>
    IEnumerable<Post> GetRecentPosts(int i);
  }
}