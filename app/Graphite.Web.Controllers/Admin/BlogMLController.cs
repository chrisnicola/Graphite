using System;
using System.IO;
using System.Web.Mvc;
using System.Xml;
using Graphite.ApplicationServices.BlogML;
using MvcContrib.ActionResults;
using Microsoft.Web.Mvc;
using MvcContrib;

namespace Graphite.Web.Controllers.Admin {
	public class BlogMLController : Controller {
		private readonly IBlogImporter _importer;
		public BlogMLController(IBlogImporter importer) { _importer = importer; }

		public ActionResult Index() {
			return View();
		}

		public ActionResult Import() {
			if (Request.Files["blogml"] != null)
				_importer.Import(XmlReader.Create(Request.Files["blogml"].InputStream));
			return this.RedirectToAction(x => x.Index());
		}
	}

}