using Graphite.Core;
using Graphite.Core.MappingInterfaces;
using Graphite.Web.Controllers.ViewModels;

namespace Graphite.Web.Controllers.Mappers {
	public interface ICreatePostMapper : IMapper<PostCreateModel, PostCreateDetails> { }

	public class CreatePostMapper : GenericMapper<PostCreateModel, PostCreateDetails>, ICreatePostMapper { }
}