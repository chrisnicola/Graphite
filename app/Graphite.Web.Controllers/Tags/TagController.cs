using System.Web.Mvc;
using Graphite.Core.Contracts.Services;
using Graphite.Web.Controllers.ActionFilters;
using Graphite.Web.Controllers.Contracts.Mappers;

namespace Graphite.Web.Controllers.Tags{
	public class TagController : Controller{
		readonly ITagTasks _tagTasks;

		public TagController(ITagTasks tagTasks) { _tagTasks = tagTasks; }

		[AutoMap(typeof (ITagShowMapper))]
		public ActionResult Show(string id) { return View(_tagTasks.GetTagByName(id)); }
	}
}