using System.Web.Mvc;
using System.Xml;
using Graphite.ApplicationServices.BlogML;
using SharpArch.Web.NHibernate;
using MvcContrib;

namespace Graphite.Web.Controllers.Admin{
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