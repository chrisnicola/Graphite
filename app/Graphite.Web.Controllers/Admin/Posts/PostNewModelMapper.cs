using System.Linq;
using AutoMapper;
using Graphite.Core.Contracts.Mapping;
using Enumerable = System.Linq.Enumerable;

namespace Graphite.Web.Controllers.Admin.Posts{
	public interface IPostNewModelMapper : IMapper<Core.Domain.Post, PostNewModel> {}

	public class PostNewModelMapper : GenericMapper<Core.Domain.Post, PostNewModel>, IPostNewModelMapper{
		public PostNewModelMapper() {
			Mapper.CreateMap<Core.Domain.Post, PostNewModel>().ForMember(m => m.Tags,
			                                                 o =>
			                                                 o.MapFrom(
			                                                 p =>
			                                                 p.Tags.Count > 0
			                                                 ? p.Tags.Select(t => t.Name).Aggregate((t1, t2) => t1 + " " + t2)
			                                                 : ""));
		}
	}
}