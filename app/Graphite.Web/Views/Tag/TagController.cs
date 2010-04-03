using System.Web.Mvc;
using Graphite.Core.Contracts.Services;
using Graphite.Web.ActionFilters;
using Graphite.Web.Contracts.Mappers;

namespace Graphite.Web.Views.Tag{
	public class TagController : Controller{
		readonly ITagTasks _tagTasks;

		public TagController(ITagTasks tagTasks) { _tagTasks = tagTasks; }

		[AutoMap(typeof (ITagShowMapper))]
		public ActionResult Show(string id) { return View(_tagTasks.GetTagByName(id)); }
	}
}