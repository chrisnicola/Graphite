using System.Linq;
using AutoMapper;
using Graphite.ApplicationServices;
using Graphite.Core;
using Graphite.Core.MappingInterfaces;
using Graphite.Data.Repositories;
using Graphite.Web.Controllers.ViewModels;

namespace Graphite.Web.Controllers.Mappers {
	
	public interface IPostNewModelMapper : IMapper<Post, PostNewModel> {}
	public class PostNewModelMapper : GenericMapper<Post, PostNewModel>, IPostNewModelMapper {

		public PostNewModelMapper() {
			Mapper.CreateMap<Post, PostNewModel>().ForMember(m => m.Tags,
				o => o.MapFrom(p => p.Tags.Count > 0 ? p.Tags.Select(t => t.Name).Aggregate((t1, t2) => t1 + " " + t2) : ""));
		}
	}
}