using System.Web.Mvc;
using Graphite.ApplicationServices;
using Graphite.Web.Controllers.ActionFilters;
using Graphite.Web.Controllers.Mappers;

namespace Graphite.Web.Controllers{
	public class TagController : Controller{
		readonly ITagTasks _tagTasks;

		public TagController(ITagTasks tagTasks) { _tagTasks = tagTasks; }

		[AutoMap(typeof (ITagShowMapper))]
		public ActionResult Show(string id) { return View(_tagTasks.GetTagByName(id)); }
	}
}