namespace Graphite.Web.Views {
	using System.Collections.Generic;
	using MvcContrib.FluentHtml;
	using MvcContrib.FluentHtml.Behaviors;
	using Spark.Web.Mvc;

	public abstract class SparkModelViewPage<T> : SparkView<T>, IViewModelContainer<T> where T : class
	{
		private readonly List<IBehaviorMarker> _behaviors = new List<IBehaviorMarker>();

		protected SparkModelViewPage()
		{
			//this._behaviors.Add(new ValidationBehavior(() => ViewData.ModelState));
		}

		public IEnumerable<IBehaviorMarker> Behaviors
		{
			get { return this._behaviors; }
		}

		public string HtmlNamePrefix { get; set; }

		public T ViewModel
		{
			get { return Model; }
		}
	}
}