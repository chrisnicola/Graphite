using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using MvcContrib.FluentHtml;
using MvcContrib.FluentHtml.Expressions;

namespace Graphite.Web.Helpers{
  public static class HtmlHelpers
  {
    public static string GetLinksForTags<TViewModel>(this IViewModelContainer<TViewModel> view, IEnumerable<string> tags)
    where TViewModel : class
    {
      if (tags.Count() == 0) return "";
      string links =
      tags.Select(t => "<a href='/tag/" + HttpUtility.UrlEncode(t) + "'>" + t + "</a>").Aggregate((t1, t2) => t1 + " " + t2);
      return links;
    }

    public static string GetLinksForTags<TViewModel>(this IViewModelContainer<TViewModel> view,
                                                     Expression<Func<TViewModel, IEnumerable<string>>> expression)
    where TViewModel : class { return view.GetLinksForTags(expression.GetValueFrom(view.ViewModel)); }

  }
}