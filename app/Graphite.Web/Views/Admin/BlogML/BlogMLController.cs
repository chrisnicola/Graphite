using System.Web.Mvc;
using System.Xml;
using Graphite.ApplicationServices.BlogML;
using MvcContrib;
using SharpArch.Web.NHibernate;

namespace Graphite.Web.Views.Admin.BlogML{
	public class BlogMLController : Controller{
		readonly IBlogImporter _importer;

		public BlogMLController(IBlogImporter importer) { _importer = importer; }

		public ActionResult Show() { return View(); }

		[Transaction]
		public ActionResult Import() {
			if (Request.Files["blogml"] != null) _importer.Import(XmlReader.Create(Request.Files["blogml"].InputStream));
			return this.RedirectToAction(x => x.Show());
		}
	}
}