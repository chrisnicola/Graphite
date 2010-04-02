namespace Graphite.Core.Contracts.MappingInterfaces{
  public interface IMapper<TSource, TDest> : IMapper
    where TSource : class
    where TDest : class{
    TDest MapFrom(TSource source);
    }

  public interface IMapper{
    object MapFrom(object source);
  }
}