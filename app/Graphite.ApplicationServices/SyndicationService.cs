using System;
using System.Linq;
using System.ServiceModel.Syndication;
using Graphite.Core;
using Graphite.Data.Repositories;

namespace Graphite.ApplicationServices {
	public class SyndicationService : ISyndicationService
	{
		private readonly IPostRepository _posts;
		public SyndicationService(IPostRepository posts) { _posts = posts; }

		public SyndicationFeed GetPostsAsSyndicationFeed(Uri requestUrl) {
			//Todo: Add settings entities so settings can be loaded here
			var items =
				_posts.FindAll().Where(x => x.Published).Select(
					p =>
						SyndicationItemFromPost(p, requestUrl));
			return new SyndicationFeed("Test Title", "My syndication feed", requestUrl, items);
		}

		private static SyndicationItem SyndicationItemFromPost(Post p, Uri requestUrl) {
			var item = new SyndicationItem(p.Title, p.Content, new Uri(requestUrl, "/Post/" + p.Slug), p.Id.ToString(),
				new DateTimeOffset(p.LastEdited));
			item.PublishDate = new DateTimeOffset(p.DatePublished.Value);
			item.Authors.Add(new SyndicationPerson(p.Author.Email, p.Author.RealName, ""));
			return item;
		}
	}
}