using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
			return new RssResult(_syndication.GetPostsAsSyndicationFeed(Request.Url));
		}

		public ActionResult Atom() {
			return new AtomResult(_syndication.GetPostsAsSyndicationFeed(Request.Url));
		}
	}

}
