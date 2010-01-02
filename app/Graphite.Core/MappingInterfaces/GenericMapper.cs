using AutoMapper;

namespace Graphite.Core.MappingInterfaces {
	public class GenericMapper<TSource,TDest> : IMapper<TSource, TDest> where TDest : class where TSource : class {
		public GenericMapper() {
			Mapper.CreateMap<TSource, TDest>();
		}
		public virtual TDest MapFrom(TSource source) {
			return Mapper.Map<TSource, TDest>(source);
		}
		public object MapFrom(object source) { return MapFrom(source as TSource); }
	}
}