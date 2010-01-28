using System;
using System.Collections.Generic;
using System.Linq;
using Graphite.Core;
using Graphite.Data.Repositories;

namespace Graphite.ApplicationServices
{
	public interface ITagTasks {
		Tag GetTagByName(string tagname);
	}

	public class TagTasks : ITagTasks {
		private readonly ITagRepository _tags;
		public TagTasks(ITagRepository tags) { _tags = tags; }

		public Tag GetTagByName(string tagname) {
			return _tags.FindOne(t => t.Name == tagname);
		}
	}
	public class PostTasks : IPostTasks 
	{
		private readonly IPostRepository _posts;
		private readonly IUserRepository _users;
		private readonly ITagRepository _tags;

		public PostTasks(IPostRepository posts, IUserRepository users, ITagRepository tags) {
			_posts = posts;
			_users = users;
			_tags = tags;
		}

		public Post GetWithComments(Guid id) { return _posts.GetWithComments(id); }
		
		public IEnumerable<Post> GetAll() { return _posts.FindAll(); }
		
		public Post Get(Guid id) { return _posts.Get(id); }
		
		public Post SaveNewPost(PostCreateDetails details) {
			return SaveNewPostForAuthor(details, _users.Get(details.AuthorId));
		}

		private Post SaveNewPostForAuthor(PostDetailsBase details, User author) {
			var post = new Post {
				Title = details.Title,
				Author = author,
				Content = details.Content,
				AllowComments = details.AllowComments,
				Published = details.Published,
				DateCreated = DateTime.Now,
				DateModified = DateTime.Now,
				DatePublished = details.DatePublished,
				Tags = GetTagsFromString(details.Tags)
			};
			if (details.Published)
				post.PublishOn(details.DatePublished);
			post.SetSlugForPost(details.Slug);
			EnsurePostSlugIsUnique(post);
			return _posts.Save(post);
		}

		private IList<Tag> GetTagsFromString(string tagstring) {
			if (string.IsNullOrEmpty(tagstring)) return new List<Tag>();
			var taglist = tagstring.Split(new[] {' ', ',', ';'});
			var tags = _tags.FindAll().Where(t => taglist.Contains(t.Name));
			var newtags = taglist.Where(t => tags.All(tag => tag.Name != t)).Select(t => new Tag {Name = t});
			return tags.Union(newtags).ToList();
		}

		private void EnsurePostSlugIsUnique(Post post) {
			var slug = post.Slug;
			int n = 1;
			var posts = _posts.FindAll().Where(p => p.Slug == post.Slug).ToList();
			while (posts.Where(p => p.Slug == post.Slug && p.Id != post.Id).Count() > 0) {
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
			post.DateModified = DateTime.Now;
			post.SetSlugForPost(details.Slug);
			UpdateTagsForPost(post, details.Tags);
			EnsurePostSlugIsUnique(post);
			if (details.Published)
				post.PublishOn(details.DatePublished);
			return post;
		}

		private void UpdateTagsForPost(Post post, string tagstring) {
			var taglist = GetTagsFromString(tagstring);
			foreach (var tag in post.Tags)
				if (!taglist.Contains(tag)) post.Tags.Remove(tag);
			foreach (var tag in taglist)
				if (!post.Tags.Contains(tag)) post.Tags.Add(tag);
		}

		public void Delete(Guid id) { 
			_posts.Delete(id);
		}

		public IEnumerable<Post> GetRecentPublishedPosts(int i) {
			return _posts.GetRecentPublishedPosts(i);
		}

		public Post ImportPost(PostImportDetails details) {
			return SaveNewPostForAuthor(details, _users.GetUserByEmail(details.AuthorEmail));
		}
	}

	public interface IPostTasks {
		Post GetWithComments(Guid id);
		IEnumerable<Post> GetAll();
		Post Get(Guid id);
		Post SaveNewPost(PostCreateDetails post);
		Post UpdatePost(PostEditDetails post);
		void Delete(Guid id);
		IEnumerable<Post> GetRecentPublishedPosts(int i);
		Post ImportPost(PostImportDetails postDetails);
	}
}
