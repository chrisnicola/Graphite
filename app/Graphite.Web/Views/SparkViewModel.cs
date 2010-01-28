using System.Collections.Generic;
using MvcContrib.FluentHtml;
using MvcContrib.FluentHtml.Behaviors;
using Spark.Web.Mvc;

namespace Graphite.Web.Views{
	public abstract class SparkModelViewPage<T> : SparkView<T>, IViewModelContainer<T> where T : class{
		readonly List<IBehaviorMarker> _behaviors = new List<IBehaviorMarker>();

		protected SparkModelViewPage() { _behaviors.Add(new ValidationBehavior(() => ViewData.ModelState)); }

		public IEnumerable<IBehaviorMarker> Behaviors { get { return _behaviors; } }

		public string HtmlNamePrefix { get; set; }

		public T ViewModel { get { return Model; } }
	}

	public abstract class SparkModelViewPage : SparkModelViewPage<object> {}
}