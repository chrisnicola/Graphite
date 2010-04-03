using System;
using System.Collections.Generic;
using System.Linq;
using CookComputing.XmlRpc;
using Graphite.Core;
using Graphite.Core.Contracts.Services;
using Graphite.Core.Domain;
using Graphite.Core.Messages;
using Graphite.Core.MetaWeblog;
using Microsoft.Practices.ServiceLocation;

namespace Graphite.ApplicationServices.MetaWeblog{
	//Todo: Replace HttpHandler with MVC based XmlRpc API
	public class MetaWeblog : XmlRpcService, IMetaWeblog{
		private readonly IUserTasks _userTasks;
		private readonly IPostTasks _postTasks;
		private readonly ITagTasks _tagTasks;

		public MetaWeblog() {
			//Note: This is because I am not sure how else to handle DI with http handlers
			_userTasks = ServiceLocator.Current.GetInstance<IUserTasks>();
			_postTasks = ServiceLocator.Current.GetInstance<IPostTasks>();
			_tagTasks = ServiceLocator.Current.GetInstance<ITagTasks>();
		}

		public MetaWeblog(IUserTasks userTasks, IPostTasks postTasks, ITagTasks tagTasks) {
			_userTasks = userTasks;
			_postTasks = postTasks;
			_tagTasks = tagTasks;
		}

		public UserBlog[] GetUsersBlogs(string key, string username, string password) {
			ValidateUser(username, password);
			var blog = new UserBlog {
				blogid = "0",
				isAdmin = true,
				blogName = "Graphite",
				url = "http://localhost"
			};
			return new[] {blog};
		}

		public bool UpdatePost(string postid, string username, string password, PostInfo postInfo, bool publish) {
			ValidateUser(username, password);
			try {
				_postTasks.UpdatePost(new PostEditDetails {
					Title = postInfo.title,
					AllowComments = postInfo.mt_allow_comments,
					AuthorUserName = username,
					Content = postInfo.description,
					Excerpt = postInfo.mt_excerpt,
					Id = new Guid(postid),
					Published = publish,
					Tags = postInfo.mt_keywords,
					Slug = postInfo.wp_slug
				});
				return true;
			} catch (Exception ex) {
				return false;
			}
		}

		public CategoryInfo[] GetCategories(string blogid, string username, string password) { throw new NotImplementedException(); }

		public PostInfo GetPost(string postid, string username, string password) {
			ValidateUser(username, password);
			return MapPostToPostInfo(_postTasks.Get(new Guid("postid")));
		}

		public PostInfo[] GetRecentPosts(string blogid, string username, string password, int numberOfPosts) {
			ValidateUser(username, password);
			return _postTasks.GetRecentPublishedPosts(numberOfPosts).Select(p => MapPostToPostInfo(p)).ToArray();
		}

		public string NewPost(string blogid, string username, string password, PostInfo postInfo, bool publish) {
			ValidateUser(username, password);
			try
			{
				var post = _postTasks.SaveNewPost(new PostCreateDetails
				{
					Title = postInfo.title,
					AllowComments = postInfo.mt_allow_comments,
					AuthorUserName = username,
					Content = postInfo.description,
					Excerpt = postInfo.mt_excerpt,
					Published = publish,
					Tags = postInfo.mt_keywords,
					Slug = postInfo.wp_slug
				});
				return ("http://localhost/post/" + post.Slug);
			}
			catch (Exception ex) {
				return "";
			}
		}

		public MediaObjectInfo NewMediaObject(string blogid, string username, string password, FileData file) { throw new NotImplementedException(); }

		public List<string> GetTags(string blogid, string username, string password) {
			ValidateUser(username, password);
			return _tagTasks.GetAllTags().Select(t => t.Name).ToList();
		}

		private PostInfo MapPostToPostInfo(Post post) {
			return new PostInfo {
				dateCreated = post.DateCreated,
				description = post.Content,
				mt_allow_comments = post.AllowComments,
				mt_excerpt = post.Excerpt,
				mt_keywords = post.GetTagsString(),
				postid = post.Id.ToString(),
				title = post.Title,
				userid = post.Author.Id.ToString(),
				wp_slug = post.Slug
			};
		}

		private void ValidateUser(string username, string password) { if (!_userTasks.ValidateUser(username, password)) throw new XmlRpcFaultException(0, "User is not valid!"); }
	}
}