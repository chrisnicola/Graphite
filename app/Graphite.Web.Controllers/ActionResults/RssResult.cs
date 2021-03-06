using System.ServiceModel.Syndication;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Serialization;

namespace Graphite.Web.Controllers.ActionResults{
	/// <summary>
	/// Based on this post: 
	/// http://blogs.msdn.com/jowardel/archive/2009/03/11/asp-net-rss-actionresult.aspx
	/// </summary>
	public class RssResult : ActionResult{
		public RssResult(SyndicationFeed feed) { Feed = feed; }

		public SyndicationFeed Feed { get; set; }

		public override void ExecuteResult(ControllerContext context) {
			context.HttpContext.Response.ContentType = "application/rss+xml";
			var formatter = new Rss20FeedFormatter(Feed);
			using (XmlWriter writer = XmlWriter.Create(context.HttpContext.Response.Output)) formatter.WriteTo(writer);
		}
	}

	public class AtomResult : ActionResult{
		public AtomResult(SyndicationFeed feed) { Feed = feed; }

		public SyndicationFeed Feed { get; set; }

		public override void ExecuteResult(ControllerContext context) {
			context.HttpContext.Response.ContentType = "application/atom+xml";
			var formatter = new Atom10FeedFormatter(Feed);
			using (XmlWriter writer = XmlWriter.Create(context.HttpContext.Response.Output)) 
				formatter.WriteTo(writer);
		}
	}

	public class XmlRpcResult : ActionResult{
		public XmlRpcResponse Response { get; set; }

		public XmlRpcResult(XmlRpcResponse response) { Response = response; }

		public override void ExecuteResult(ControllerContext context) {
			var serializer = new XmlSerializer(typeof(XmlRpcResponse));
			using (XmlWriter writer = XmlWriter.Create(context.HttpContext.Response.Output)) 
				serializer.Serialize(writer, Response);
		}
	}

	public class XmlRpcResponse{
		
	}
}