using System;
using System.Collections.Generic;
using Graphite.Core;
using Graphite.Core.Domain;

namespace Graphite.ApplicationServices
{
	public class BlogTasks
	{
	}

	public interface IBlogTasks{
		Blog GetBlog(Guid id);

		IEnumerable<Blog> GetAllBlogs();
      
		IEnumerable<Blog> GetBlogsForUser(string username);
	}
}
