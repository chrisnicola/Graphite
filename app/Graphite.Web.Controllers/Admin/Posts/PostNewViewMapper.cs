using Graphite.Core.Contracts.Mapping;
using Graphite.Core.Messages;

namespace Graphite.Web.Controllers.Admin.Posts{
	public interface IPostCreateDetailsMapper : IMapper<PostNewModel, PostCreateDetails> {}

	public class PostCreateDetailsDetailsMapper : GenericMapper<PostNewModel, PostCreateDetails>, IPostCreateDetailsMapper {}
}