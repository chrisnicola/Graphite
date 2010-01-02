using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Graphite.ApplicationServices;
using Graphite.Web.Controllers.ActionResults;

namespace Graphite.Web.Controllers
{
	public class RssController : Controller
	{
		private readonly ISyndicationService _syndication;
		public RssController(ISyndicationService syndication) { _syndication = syndication; }

		public ActionResult Index() {
			return new RssResult(_syndication.GetPostsAsSyndicationFeed(Request.Url));
		}
	}

}
