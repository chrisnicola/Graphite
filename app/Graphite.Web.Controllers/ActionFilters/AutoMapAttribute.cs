using System;
using System.Web.Mvc;
using Graphite.Core.MappingInterfaces;
using Microsoft.Practices.ServiceLocation;

namespace Graphite.Web.Controllers.ActionFilters {
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
	public class AutoMapAttribute : ActionFilterAttribute {
		private readonly Type _sourceType;
		private readonly Type _destType;
		private readonly Type _mapperType;

		public AutoMapAttribute(Type sourceType, Type destType) {
			_sourceType = sourceType;
			_destType = destType;
		}

		public AutoMapAttribute(Type sourceType, Type destType, Type mapperType) {
			_sourceType = sourceType;
			_destType = destType;
			_mapperType = mapperType;
		}

		public Type SourceType { get { return _sourceType; } }

		public Type DestType { get { return _destType; } }

		public override void OnActionExecuted(ActionExecutedContext filterContext) {
			IMapper mapper;
			if (_mapperType != null) mapper = (IMapper) ServiceLocator.Current.GetInstance(_mapperType);
			else mapper = GetDefaultMapperFor(SourceType, DestType);
			var filter = new AutoMapFilter(mapper);
			filter.OnActionExecuted(filterContext);
		}

		private static IMapper GetDefaultMapperFor(Type sourceType, Type destType) {
			Type genericClass = typeof (IMapper<,>);
			Type constructedClass = genericClass.MakeGenericType(new[] {sourceType, destType});
			return (IMapper) Activator.CreateInstance(constructedClass);
		}
	}

	public class AutoMapFilter : EmptyActionFilter {
		private readonly IMapper _mapper;

		public AutoMapFilter(IMapper mapper) { _mapper = mapper; }

		public override void OnActionExecuted(ActionExecutedContext filterContext) {
			object model = filterContext.Controller.ViewData.Model;
			filterContext.Controller.ViewData.Model = _mapper.MapFrom(model);
		}
	}

	public abstract class EmptyActionFilter : IActionFilter, IResultFilter {
		public virtual void OnActionExecuting(ActionExecutingContext filterContext) { }

		public virtual void OnActionExecuted(ActionExecutedContext filterContext) { }

		public virtual void OnResultExecuting(ResultExecutingContext filterContext) { }

		public virtual void OnResultExecuted(ResultExecutedContext filterContext) { }
	}
}