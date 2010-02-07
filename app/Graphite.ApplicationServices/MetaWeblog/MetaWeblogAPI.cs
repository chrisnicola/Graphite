using System;
using System.Collections.Generic;
using CookComputing.XmlRpc;
using Graphite.Core;
using Graphite.Core.MappingInterfaces;
using SharpArch.Web.NHibernate;

namespace Graphite.ApplicationServices.MetaWeblog
{
	public class MetaWeblog : XmlRpcService, IMetaWeblog {
		private readonly IUserTasks _userTasks;
		private readonly IPostTasks _postTasks;

		public MetaWeblog(IUserTasks userTasks, IPostTasks postTasks) {
			_userTasks = userTasks;
			_postTasks = postTasks;
		}

		public bool UpdatePost(string postid, string username, string password, Post post, bool publish) {
			ValidateUser(username, password);
			try {
				_postTasks.UpdatePost(new PostEditDetails {
					Title = post.title,
					AllowComments = (bool) post.mt_allow_comments,
					AuthorUserName = username,
					Content = post.description,
					Excerpt = post.mt_excerpt,
					Id = new Guid(postid),
					Published = publish,
					Tags = post.mt_keywords,
					Slug = post.wp_slug
				});
			} catch (Exception ex) { return false; }
			return true;
		}

		public CategoryInfo[] GetCategories(string blogid, string username, string password) { throw new NotImplementedException(); }

		public Post GetPost(string postid, string username, string password) { throw new NotImplementedException(); }

		public Post[] GetRecentPosts(string blogid, string username, string password, int numberOfPosts) { throw new NotImplementedException(); }

		public string NewPost(string blogid, string username, string password, Post post, bool publish) { throw new NotImplementedException(); }

		public UrlData NewMediaObject(string blogid, string username, string password, FileData file) { throw new NotImplementedException(); }

		public List<string> GetTags(string blogid, string username, string password) { throw new NotImplementedException(); }

		private void ValidateUser(string username, string password)
		{
			if (!_userTasks.ValidateUser(username, password)) throw new XmlRpcFaultException(0, "User is not valid!");
		}

	} 

	[XmlRpcMissingMapping(MappingAction.Ignore)]
	public struct Enclosure
	{
		public int length;
		public string type;
		public string url;
	}

	[XmlRpcMissingMapping(MappingAction.Ignore)]
	public struct Source
	{
		public string name;
		public string url;
	}

	[XmlRpcMissingMapping(MappingAction.Ignore)]
	public struct Post
	{
		[XmlRpcMissingMapping(MappingAction.Error)]
		[XmlRpcMember(Description="Required when posting.")]
		public DateTime dateCreated;
		[XmlRpcMissingMapping(MappingAction.Error)]
		[XmlRpcMember(Description="Required when posting.")]
		public string description;
		[XmlRpcMissingMapping(MappingAction.Error)]
		[XmlRpcMember(Description="Required when posting.")]
		public string title;

		public string[] categories;
		public Enclosure enclosure;
		public string link;
		public string permalink;
		[XmlRpcMember(
		Description="Not required when posting. Depending on server may "
		            + "be either string or integer. "
		            + "Use Convert.ToInt32(postid) to treat as integer or "
		            + "Convert.ToString(postid) to treat as string")]
		public object postid;       
		public Source source;
		public string userid;

		public object mt_allow_comments; 
		public object mt_allow_pings; 
		public object mt_convert_breaks;
		public string mt_text_more;
		public string mt_excerpt;
		public string mt_keywords;
		public string wp_slug;
	}

	public struct CategoryInfo
	{
		public string description;
		public string htmlUrl;
		public string rssUrl;
		public string title;
		public string categoryid;
	}

	public struct Category
	{
		public string categoryId;
		public string categoryName;
	}

	public struct FileData
	{
		public byte[] bits;
		public string name;
		public string type;
	}

	public struct UrlData
	{
		public string url;
	}
}


