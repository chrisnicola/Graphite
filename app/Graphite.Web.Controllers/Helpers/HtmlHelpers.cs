using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using MvcContrib.FluentHtml;
using MvcContrib.FluentHtml.Expressions;
using Microsoft.Web.Mvc;
using System.Linq;

namespace Graphite.Web.Controllers.Helpers {
	public static class HtmlHelpers {
		public static string GetLinksForTags<TViewModel>(this IViewModelContainer<TViewModel> view,
			IEnumerable<string> tags) where TViewModel : class {
			var links = tags.Select(t => view.Html.ActionLink<TagController>(x => x.Show(), t))
				.Aggregate((t1,t2) => t1 +" "+ t2);
			return links;
		}

		public static string GetLinksForTags<TViewModel>(this IViewModelContainer<TViewModel> view,
			Expression<Func<TViewModel,IEnumerable<string>>> expression) where TViewModel : class {
				var links = expression.GetValueFrom(view.ViewModel).Select(t => view.Html.ActionLink<TagController>(x => x.Show(), t))
					.Aggregate((t1, t2) => t1 + " " + t2);
			return links;
		}
	}
}