using System;
using System.ServiceModel.Syndication;

namespace Graphite.ApplicationServices{
	public interface ISyndicationService{
		/// <summary>
		/// Returns a Syndication feed with published posts
		/// </summary>
		/// <param name="requestUrl">The Url of the Syndication Feed</param>
		/// <returns>A SyndicationFeed of blog post data</returns>
		SyndicationFeed GetPostsAsSyndicationFeed(Uri requestUrl);
	}
}