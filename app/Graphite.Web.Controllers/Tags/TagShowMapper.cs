using AutoMapper;
using Graphite.Core.Contracts.Mapping;
using Graphite.Core.Domain;
using Graphite.Web.Controllers.Contracts.Mappers;
using Graphite.Web.Controllers.Posts;

namespace Graphite.Web.Controllers.Tags{
  public class TagShowMapper : GenericMapper<Tag, TagViewModel>, ITagShowMapper{
		public TagShowMapper() { Mapper.CreateMap<Post, PostShowViewModel>(); }
	}
}