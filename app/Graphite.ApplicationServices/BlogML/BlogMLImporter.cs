using System.Collections.Generic;
using System.Linq;
using System.Xml;
using BlogML.Xml;
using Graphite.Core;
using Graphite.Core.Contracts.Services;
using Graphite.Core.Domain;
using Graphite.Core.Messages;

namespace Graphite.ApplicationServices.BlogML{
	public class BlogMLImporter : IBlogImporter{
		readonly IPostTasks _tasks;
		readonly IUserTasks _users;
		readonly IBlogMLToPostMapper _postMapper;
		readonly IBlogMLToCommentMapper _commentMapper;
		readonly IList<User> _authors = new List<User>();

		public BlogMLImporter(IPostTasks tasks, IUserTasks users, IBlogMLToPostMapper postMapper,
		                      IBlogMLToCommentMapper commentMapper) {
			_tasks = tasks;
			_users = users;
			_postMapper = postMapper;
			_commentMapper = commentMapper;
		}

		public void Import(XmlReader xmlData) {
			BlogMLBlog blog = BlogMLSerializer.Deserialize(xmlData);
			ImportAuthorsFromBlog(blog);
			ImportPostsFromBlog(blog);
		}

		void ImportAuthorsFromBlog(BlogMLBlog blog) {
			foreach (BlogMLAuthor author in blog.Authors) {
				User user = _users.GetUserByEmail(author.Email) ?? _users.AddUser(new CreateUserDetails {
				                                                                                        Email = author.Email,
				                                                                                        RealName = author.Title,
				                                                                                        Username = author.Email.Split('@')[0]
				                                                                                        });
				_authors.Add(user);
			}
		}

		void ImportPostsFromBlog(BlogMLBlog blog) {
			foreach (BlogMLPost post in blog.Posts) {
				Post importPost = _tasks.ImportPost(_postMapper.MapFrom(post, blog.Categories));
				importPost.Author = _authors.FirstOrDefault();
				importPost.AllowComments = true;
				foreach (BlogMLComment comment in post.Comments) importPost.AddComment(_commentMapper.MapFrom(comment));
			}
		}
	}

	public interface IBlogImporter{
		void Import(XmlReader xmlData);
	}
}