using Graphite.Core;
using Graphite.Core.MappingInterfaces;
using Graphite.Web.Controllers.ViewModels;

namespace Graphite.Web.Controllers.Mappers {
	public interface IEditPostMapper : IMapper<PostEditModel, PostEditDetails> { }

	public class EditPostMapper : GenericMapper<PostEditModel, PostEditDetails>, IEditPostMapper {}
}