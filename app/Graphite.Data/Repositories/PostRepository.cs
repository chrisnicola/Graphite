using System;
using System.Collections.Generic;
using System.Linq;
using Graphite.Core;
using NHibernate.Linq;
using SharpArch.Core.PersistenceSupport.NHibernate;
using SharpArch.Data.NHibernate;

namespace Graphite.Data.Repositories {
  public class PostRepository : LinqRepository<Post>, IPostRepository {
    public IEnumerable<Post> GetRecentPublishedPosts(int i) {
    	return Session.Linq<Post>()
				.Where(p => p.Published)
				.OrderByDescending(p => p.DatePublished)
				.Take(i);
    }

    public Post GetWithComments(Guid id) {
      var post = Get(id);
      post.Comments.Count();
      return post;
    }
  }

  public class CommentRepository : NHibernateRepositoryWithTypedId<Comment, Guid>, ICommentRepository { }

  public interface IPostRepository : ILinqRepository<Post> {
    /// <summary>
    /// Gets the most recent posts in the database
    /// </summary>
    /// <param name="i">Number of recent posts to get</param>
    IEnumerable<Post> GetRecentPublishedPosts(int i);
    Post GetWithComments(Guid id);
  }

  public interface ICommentRepository : INHibernateRepositoryWithTypedId<Comment, Guid> { }
}