using AutoMapper;
using Graphite.Core.Contracts.Mapping;
using Graphite.Web.Contracts.Mappers;
using Graphite.Web.Views.Post;

namespace Graphite.Web.Views.Tag{
  public class TagShowMapper : GenericMapper<Core.Domain.Tag, TagViewModel>, ITagShowMapper{
    public TagShowMapper() { Mapper.CreateMap<Core.Domain.Post, PostShowViewModel>(); }
	}
}