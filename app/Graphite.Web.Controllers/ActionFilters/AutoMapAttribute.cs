using System;
using System.Web.Mvc;
using AutoMapper;

namespace Graphite.Web.Controllers.ActionFilters {
  [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
  public class AutoMapAttribute : ActionFilterAttribute
  {
    private readonly Type _sourceType;
    private readonly Type _destType;

    public AutoMapAttribute(Type sourceType, Type destType)
    {
      _sourceType = sourceType;
      _destType = destType;
    }

    public override void OnActionExecuted(ActionExecutedContext filterContext)
    {
      var filter = new AutoMapFilter(SourceType, DestType);

      filter.OnActionExecuted(filterContext);
    }

    public Type SourceType
    {
      get { return _sourceType; }
    }

    public Type DestType
    {
      get { return _destType; }
    }
  }

  public class AutoMapFilter : BaseActionFilter
  {
    private readonly Type _sourceType;
    private readonly Type _destType;

    public AutoMapFilter(Type sourceType, Type destType)
    {
      _sourceType = sourceType;
      _destType = destType;
    }

    public override void OnActionExecuted(ActionExecutedContext filterContext)
    {
      var model = filterContext.Controller.ViewData.Model;

      object viewModel = Mapper.Map(model, _sourceType, _destType);

      filterContext.Controller.ViewData.Model = viewModel;
    }
  }

  public abstract class BaseActionFilter : IActionFilter, IResultFilter
  {
    public virtual void OnActionExecuting(ActionExecutingContext filterContext)
    {
    }

    public virtual void OnActionExecuted(ActionExecutedContext filterContext)
    {
    }

    public virtual void OnResultExecuting(ResultExecutingContext filterContext)
    {
    }

    public virtual void OnResultExecuted(ResultExecutedContext filterContext)
    {
    }
  }
}