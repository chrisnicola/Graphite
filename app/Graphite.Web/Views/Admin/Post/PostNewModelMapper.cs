using System.Linq;
using AutoMapper;
using Graphite.Core.Contracts.Mapping;
using Graphite.Web.Contracts.Mappers;

namespace Graphite.Web.Views.Admin.Post{
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