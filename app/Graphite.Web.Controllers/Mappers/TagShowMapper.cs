using AutoMapper;
using Graphite.Core;
using Graphite.Core.MappingInterfaces;
using Graphite.Web.Controllers.ViewModels;

namespace Graphite.Web.Controllers.Mappers{
	public interface ITagShowMapper : IMapper<Tag, TagViewModel> {}

	public class TagShowMapper : GenericMapper<Tag, TagViewModel>, ITagShowMapper{
		public TagShowMapper() { Mapper.CreateMap<Post, PostShowViewModel>(); }
	}
}