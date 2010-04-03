using System;
using System.Web.Mvc;
using Graphite.Core.Contracts.Services;
using Graphite.Web.ActionResults;

namespace Graphite.Web.Views.Feed{
	public class FeedController : Controller{
		readonly ISyndicationService _syndication;

		public FeedController(ISyndicationService syndication) { _syndication = syndication; }

		public ActionResult Rss() { return new RssResult(_syndication.GetPostsAsSyndicationFeed(GetBaseUri())); }

		Uri GetBaseUri() { return new Uri(Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + '/'); }

		public ActionResult Atom() { return new AtomResult(_syndication.GetPostsAsSyndicationFeed(GetBaseUri())); }
	}
}