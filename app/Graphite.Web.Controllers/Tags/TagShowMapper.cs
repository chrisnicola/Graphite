using AutoMapper;
using Graphite.Core.Contracts.MappingInterfaces;
using Graphite.Core.Domain;
using Graphite.Web.Controllers.Posts;

namespace Graphite.Web.Controllers.Tags{
	public interface ITagShowMapper : IMapper<Tag, TagViewModel> {}

	public class TagShowMapper : GenericMapper<Tag, TagViewModel>, ITagShowMapper{
		public TagShowMapper() { Mapper.CreateMap<Post, PostShowViewModel>(); }
	}
}