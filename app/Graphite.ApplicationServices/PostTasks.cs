using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Graphite.Core;
using Graphite.Data.Repositories;

namespace Graphite.ApplicationServices
{
	public class PostTasks : IPostTasks 
	{
		private readonly IPostRepository _posts;
		private readonly IUserRepository _users;

		public PostTasks(IPostRepository posts, IUserRepository users) {
			_posts = posts;
			_users = users;
		}

		public Post GetWithComments(Guid id) { return _posts.GetWithComments(id); }
		public IEnumerable<Post> GetAll() { return _posts.FindAll(); }
		public Post Get(Guid id) { return _posts.Get(id); }
		public Post SaveNewPost(PostCreateDetails details) {
			var post = new Post
			             {
										Title = details.Title,
			             	Author = _users.Get(details.AuthorId),
										Content = details.Content,
										AllowComments = details.AllowComments,
										Published = details.Published,
										DateCreated = DateTime.Now,
										LastEdited = DateTime.Now,
										DatePublished = details.DatePublished
			             };
			if (details.Published)
				post.PublishOn(details.DatePublished);
			post.SetSlugForPost(details.Slug);
			EnsurePostSlugIsUnique(post);
			return _posts.Save(post);
		}

		private void EnsurePostSlugIsUnique(Post post) {
			var slug = post.Slug;
			int n = 1;
			while (_posts.FindAll(p => p.Slug == post.Slug && p.Id != post.Id).Count() > 0) {
				post.Slug = slug + n;
				n++;
			}
		}

		public Post UpdatePost(PostEditDetails details) {
			var post = _posts.Get(details.Id);
			post.Title = details.Title;
			post.Author = _users.Get(details.AuthorId);
			post.Content = details.Content;
			post.AllowComments = details.AllowComments;
			post.DateCreated = DateTime.Now;
			post.LastEdited = DateTime.Now;
			post.SetSlugForPost(details.Slug);
			EnsurePostSlugIsUnique(post);
			if (details.Published)
				post.PublishOn(details.DatePublished);
			return post;
		}
		public void Delete(Guid id) { 
			_posts.Delete(id);
		}
	}

	public interface IPostTasks {
		Post GetWithComments(Guid id);
		IEnumerable<Post> GetAll();
		Post Get(Guid id);
		Post SaveNewPost(PostCreateDetails post);
		Post UpdatePost(PostEditDetails post);
		void Delete(Guid id);
	}
}
