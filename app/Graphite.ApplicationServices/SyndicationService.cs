using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using Graphite.Core;
using Graphite.Core.Contracts.DataInterfaces;
using Graphite.Core.Contracts.Services;
using Graphite.Core.Domain;
using Graphite.Data.Repositories;

namespace Graphite.ApplicationServices{
	public class SyndicationService : ISyndicationService{
		readonly IPostRepository _posts;

		public SyndicationService(IPostRepository posts) { _posts = posts; }

		public SyndicationFeed GetPostsAsSyndicationFeed(Uri requestUrl) {
			//Todo: Add settings entities so settings can be loaded here
			IEnumerable<SyndicationItem> items =
			_posts.FindAll().Where(x => x.Published).Select(
			p => SyndicationItemFromPost(p, requestUrl));
			return new SyndicationFeed("Test Title", "My syndication feed", requestUrl, items);
		}

		static SyndicationItem SyndicationItemFromPost(Post p, Uri requestUrl) {
			var item = new SyndicationItem(p.Title, p.Content, new Uri(requestUrl, "post/" + p.Slug), p.Id.ToString(),
			                               new DateTimeOffset(p.DateModified));
			item.PublishDate = new DateTimeOffset(p.DatePublished.Value);
			item.Authors.Add(new SyndicationPerson(p.Author.Email, p.Author.RealName, ""));
			foreach (Tag tag in p.Tags) item.Categories.Add(new SyndicationCategory(tag.Name, requestUrl + "tag/" + tag.Name, tag.Name));
			item.ElementExtensions.Add("comments", "", requestUrl + "post/" + p.Slug);
			return item;
		}
	}
}