using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using BlogML.Xml;
using Graphite.ApplicationServices.BlogML.Mappers;

namespace Graphite.ApplicationServices.BlogML
{
	public class BlogMLImporter : IBlogImporter
	{
		private readonly IPostTasks _tasks;
		private readonly IBlogMlToPostMapper _mapper;

		public BlogMLImporter(IPostTasks tasks, IBlogMlToPostMapper mapper) {
			_tasks = tasks;
			_mapper = mapper;
		}

		public void Import(XmlReader xmlData) {	
			var blog = BlogMLSerializer.Deserialize(xmlData);
			AddPostsFromBlog(blog);
		}

		private void AddPostsFromBlog(BlogMLBlog blog) {
			foreach (var post in blog.Posts) {
				var postDetails = _mapper.MapFrom(post);
				_tasks.ImportPost(postDetails);
			}
		}
	}

	public interface IBlogImporter {
		void Import(XmlReader xmlData);
	}
}
