using AutoMapper;
using Graphite.Core.Contracts.MappingInterfaces;
using Graphite.Core.Messages;
using Enumerable = System.Linq.Enumerable;

namespace Graphite.Web.Controllers.Admin.Posts{
	public interface IPostEditDetailsMapper : IMapper<PostEditModel, PostEditDetails> {}

	public class PostEditDetailsMapper : GenericMapper<PostEditModel, PostEditDetails>, IPostEditDetailsMapper {}

	public interface IPostEditModelMapper : IMapper<Core.Domain.Post, PostEditModel> {}

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