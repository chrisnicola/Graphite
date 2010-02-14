using System.Collections.Generic;
using CookComputing.XmlRpc;

namespace Graphite.ApplicationServices.MetaWeblog{
	public interface IMetaWeblog
	{
		[XmlRpcMethod("blogger.getUsersBlogs")]
		UserBlog[] GetUsersBlogs(string key, string username, string password);
 
		[XmlRpcMethod("metaWeblog.editPost",
		Description="Updates and existing postInfo to a designated blog "
		            + "using the metaWeblog API. Returns true if completed.")]
		bool UpdatePost(
		string postid,
		string username,
		string password,
		PostInfo postInfo,
		bool publish);

		[XmlRpcMethod("metaWeblog.getCategories",
		Description="Retrieves a list of valid categories for a postInfo "
		            + "using the metaWeblog API. Returns the metaWeblog categories "
		            + "struct collection.")]
		CategoryInfo[] GetCategories(
		string blogid,
		string username,
		string password);

		[XmlRpcMethod("metaWeblog.getPost",
		Description="Retrieves an existing postInfo using the metaWeblog "
		            + "API. Returns the metaWeblog struct.")]
		PostInfo GetPost(
		string postid,
		string username,
		string password);

		[XmlRpcMethod("metaWeblog.getRecentPosts",
		Description="Retrieves a list of the most recent existing postInfo "
		            + "using the metaWeblog API. Returns the metaWeblog struct collection.")]
		PostInfo[] GetRecentPosts(
		string blogid,
		string username,
		string password,
		int numberOfPosts);

		[XmlRpcMethod("metaWeblog.newPost",
		Description="Makes a new postInfo to a designated blog using the "
		            + "metaWeblog API. Returns postid as a string.")]
		string NewPost(
		string blogid,
		string username,
		string password,
		PostInfo postInfo,
		bool publish);

		[XmlRpcMethod("metaWeblog.newMediaObject",
		Description = "Makes a new file to a designated blog using the "
		              + "metaWeblog API. Returns url as a string of a struct.")]
		MediaObjectInfo NewMediaObject(
		string blogid,
		string username,
		string password,
		FileData file);

		[XmlRpcMethod("wp.getTags", Description = "Gets tags used by this blog.")]
		List<string> GetTags(string blogid, string username, string password);
	}
}