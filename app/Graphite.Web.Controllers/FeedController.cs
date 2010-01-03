using System;
using System.Web.Mvc;
using Graphite.ApplicationServices;
using Graphite.Web.Controllers.ActionResults;

namespace Graphite.Web.Controllers
{
	public class FeedController : Controller
	{
		private readonly ISyndicationService _syndication;
		public FeedController(ISyndicationService syndication) { _syndication = syndication; }

		public ActionResult Rss() {
			return new RssResult(_syndication.GetPostsAsSyndicationFeed(GetBaseUri()));
		}

		private Uri GetBaseUri() { 
			return new Uri(Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + '/');
		}

		public ActionResult Atom() {
			return new AtomResult(_syndication.GetPostsAsSyndicationFeed(GetBaseUri()));
		}
	}
}