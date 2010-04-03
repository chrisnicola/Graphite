using System.Linq;
using AutoMapper;
using Graphite.Core.Contracts.Mapping;
using Graphite.Core.Messages;
using Graphite.Web.Contracts.Mappers;

namespace Graphite.Web.Views.Admin.Post{
  public class PostEditDetailsMapper : GenericMapper<PostEditModel, PostEditDetails>, IPostEditDetailsMapper {}

  public class PostEditModelMapper : GenericMapper<Core.Domain.Post, PostEditModel>, IPostEditModelMapper{
		public PostEditModelMapper() {
			Mapper.CreateMap<Core.Domain.Post, PostEditModel>().ForMember(m => m.Tags,
			                                                  o =>
			                                                  o.MapFrom(
			                                                  p =>
			                                                  p.Tags.Count > 0
			                                                  ? Enumerable.Aggregate<string>(p.Tags.Select(t => t.Name), (t1, t2) => t1 + " " + t2)
			                                                  : ""));
		}
	}
}