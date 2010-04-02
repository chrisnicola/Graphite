using System;
using System.Collections.Generic;
using System.Linq;
using Graphite.Core;
using Graphite.Core.Contracts.Data;
using Graphite.Core.Domain;
using NHibernate.Linq;

namespace Graphite.Data.Repositories{
	public class PostRepository : LinqRepository<Post>, IPostRepository{
		public IEnumerable<Post> GetRecentPublishedPosts(int i) {
			return Session.Linq<Post>()
			.Where(p => p.Published)
			.OrderByDescending(p => p.DatePublished)
			.Take(i);
		}

		public Post GetWithComments(Guid id) {
			Post post = Get(id);
			post.Comments.Count();
			return post;
		}
	}
}