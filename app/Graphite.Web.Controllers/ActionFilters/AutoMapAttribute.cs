using System;
using System.Web.Mvc;
using AutoMapper;

namespace Graphite.Web.Controllers.ActionFilters {
  public class AutoMapAttribute : ActionFilterAttribute {
    private readonly Type _source;
    private readonly Type _destiny;

    public AutoMapAttribute(Type source, Type destiny) {
      _source = source;
      _destiny = destiny;
    } // AutoMapFilter  

    public override void OnActionExecuted(ActionExecutedContext context) {
      object model = context.Controller.ViewData.Model;
      object viewModel = Mapper.Map(model, _source, _destiny);
      context.Controller.ViewData.Model = viewModel;
    }
  }
}