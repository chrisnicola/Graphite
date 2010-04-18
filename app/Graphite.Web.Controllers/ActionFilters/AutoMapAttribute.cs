using System;
using System.Web.Mvc;
using Graphite.Core.Contracts.Mapping;
using Microsoft.Practices.ServiceLocation;

namespace Graphite.Web.Controllers.ActionFilters{
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
	public class AutoMapAttribute : ActionFilterAttribute{
		readonly Type _sourceType;
		readonly Type _destType;
		readonly Type _mapperType;

		public AutoMapAttribute(Type sourceType, Type destType) {
			_sourceType = sourceType;
			_destType = destType;
		}

		public AutoMapAttribute(Type mapperType) { _mapperType = mapperType; }

		public Type SourceType { get { return _sourceType; } }

		public Type DestType { get { return _destType; } }

		public override void OnActionExecuted(ActionExecutedContext filterContext) {
			IMapper mapper;
			if (_mapperType != null) mapper = (IMapper) ServiceLocator.Current.GetInstance(_mapperType);
			else mapper = GetDefaultMapperFor(SourceType, DestType);
			var filter = new AutoMapFilter(mapper);
			filter.OnActionExecuted(filterContext);
		}

		static IMapper GetDefaultMapperFor(Type sourceType, Type destType) {
			Type genericClass = typeof (GenericMapper<,>);
			Type constructedClass = genericClass.MakeGenericType(new[] {sourceType, destType});
			return (IMapper) Activator.CreateInstance(constructedClass);
		}
	}

	public class AutoMapFilter : EmptyActionFilter{
		readonly IMapper _mapper;

		public AutoMapFilter(IMapper mapper) { _mapper = mapper; }

		public override void OnActionExecuted(ActionExecutedContext filterContext) {
			object model = filterContext.Controller.ViewData.Model;
			filterContext.Controller.ViewData.Model = _mapper.MapFrom(model);
		}
	}

	public abstract class EmptyActionFilter : IActionFilter, IResultFilter{
		public virtual void OnActionExecuting(ActionExecutingContext filterContext) { }

		public virtual void OnActionExecuted(ActionExecutedContext filterContext) { }

		public virtual void OnResultExecuting(ResultExecutingContext filterContext) { }

		public virtual void OnResultExecuted(ResultExecutedContext filterContext) { }
	}
}