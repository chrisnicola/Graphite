using Graphite.Core;
using Graphite.Core.Contracts.MappingInterfaces;
using Graphite.Web.Controllers.ViewModels;

namespace Graphite.Web.Controllers.Mappers{
	public interface IPostCreateDetailsMapper : IMapper<PostNewModel, PostCreateDetails> {}

	public class PostCreateDetailsDetailsMapper : GenericMapper<PostNewModel, PostCreateDetails>, IPostCreateDetailsMapper {}
}