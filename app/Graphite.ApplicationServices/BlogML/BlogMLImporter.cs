using System.Collections.Generic;
using System.Linq;
using System.Xml;
using BlogML.Xml;
using Graphite.Core;

namespace Graphite.ApplicationServices.BlogML
{
	public class BlogMLImporter : IBlogImporter
	{
		private readonly IPostTasks _tasks;
		private readonly IUserTasks _users;
		private readonly IBlogMLToPostMapper _postMapper;
		private readonly IBlogMLToCommentMapper _commentMapper;
		private readonly IList<User> _authors = new List<User>();

		public BlogMLImporter(IPostTasks tasks, IUserTasks users, IBlogMLToPostMapper postMapper, IBlogMLToCommentMapper commentMapper) {
			_tasks = tasks;
			_users = users;
			_postMapper = postMapper;
			_commentMapper = commentMapper;
		}

		public void Import(XmlReader xmlData) {	
			var blog = BlogMLSerializer.Deserialize(xmlData);
			
			ImportAuthorsFromBlog(blog);
			ImportPostsFromBlog(blog);
		}

		private void ImportAuthorsFromBlog(BlogMLBlog blog) {
			foreach (var author in blog.Authors) {
				var user = _users.GetUserByEmail(author.Email) ?? _users.AddUser(new CreateUserDetails
				{Email = author.Email, RealName = author.Title, Username = author.Email.Split('@')[0]});
				_authors.Add(user);
			}
		}

		private void ImportPostsFromBlog(BlogMLBlog blog) {
			foreach (var post in blog.Posts) {
				var importPost = _tasks.ImportPost(_postMapper.MapFrom(post, blog.Categories));
				importPost.Author = _authors.FirstOrDefault();
				importPost.AllowComments = true;
				foreach (BlogMLComment comment in post.Comments) {
					importPost.AddComment(_commentMapper.MapFrom(comment));
				}
			}
		}
	}

	public interface IBlogImporter {
		void Import(XmlReader xmlData);
	}
}
