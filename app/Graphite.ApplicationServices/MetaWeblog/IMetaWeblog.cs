using System.Collections.Generic;
using CookComputing.XmlRpc;

namespace Graphite.ApplicationServices.MetaWeblog{
	public interface IMetaWeblog
	{
		[XmlRpcMethod("metaWeblog.editPost",
		Description="Updates and existing post to a designated blog "
		            + "using the metaWeblog API. Returns true if completed.")]
		bool UpdatePost(
		string postid,
		string username,
		string password,
		Post post,
		bool publish);

		[XmlRpcMethod("metaWeblog.getCategories",
		Description="Retrieves a list of valid categories for a post "
		            + "using the metaWeblog API. Returns the metaWeblog categories "
		            + "struct collection.")]
		CategoryInfo[] GetCategories(
		string blogid,
		string username,
		string password);

		[XmlRpcMethod("metaWeblog.getPost",
		Description="Retrieves an existing post using the metaWeblog "
		            + "API. Returns the metaWeblog struct.")]
		Post GetPost(
		string postid,
		string username,
		string password);

		[XmlRpcMethod("metaWeblog.getRecentPosts",
		Description="Retrieves a list of the most recent existing post "
		            + "using the metaWeblog API. Returns the metaWeblog struct collection.")]
		Post[] GetRecentPosts(
		string blogid,
		string username,
		string password,
		int numberOfPosts);

		[XmlRpcMethod("metaWeblog.newPost",
		Description="Makes a new post to a designated blog using the "
		            + "metaWeblog API. Returns postid as a string.")]
		string NewPost(
		string blogid,
		string username,
		string password,
		Post post,
		bool publish);

		[XmlRpcMethod("metaWeblog.newMediaObject",
		Description = "Makes a new file to a designated blog using the "
		              + "metaWeblog API. Returns url as a string of a struct.")]
		UrlData NewMediaObject(
		string blogid,
		string username,
		string password,
		FileData file);

		[XmlRpcMethod("wp.getTags", Description = "Gets tags used by this blog.")]
		List<string> GetTags(string blogid, string username, string password);
	}
}