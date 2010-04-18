using System.Linq;
using AutoMapper;
using Graphite.Core.Contracts.Mapping;
using Graphite.Core.Messages;
using Graphite.Web.Controllers.Contracts.Mappers;
using Enumerable = System.Linq.Enumerable;

namespace Graphite.Web.Controllers.Admin.Posts{
  public class PostEditDetailsMapper : GenericMapper<PostEditModel, PostEditDetails>, IPostEditDetailsMapper {}

  public class PostEditModelMapper : GenericMapper<Core.Domain.Post, PostEditModel>, IPostEditModelMapper{
		public PostEditModelMapper() {
			Mapper.CreateMap<Core.Domain.Post, PostEditModel>().ForMember(m => m.Tags,
			                                                  o =>
			                                                  o.MapFrom(
			                                                  p =>
			                                                  p.Tags.Count > 0
			                                                  ? p.Tags.Select(t => t.Name).Aggregate((t1, t2) => t1 + " " + t2)
			                                                  : ""));
		}
	}
}