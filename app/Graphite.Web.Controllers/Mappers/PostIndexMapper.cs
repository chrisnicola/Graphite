using System.Collections.Generic;
using System.Linq;
using Graphite.ApplicationServices;
using Graphite.Core;
using Graphite.Core.MappingInterfaces;
using Graphite.Web.Controllers.ViewModels;

namespace Graphite.Web.Controllers.Mappers {
	public interface IPostIndexMapper : IMapper<IEnumerable<Post>, PostIndexViewModel> { }

	public class PostIndexMapper : IPostIndexMapper {
		private readonly IUserTasks _userTasks;
		private readonly IMapper<Post, PostViewModel> _mapper;
		public PostIndexMapper(IUserTasks userTasks, IMapper<Post, PostViewModel> mapper) {
			_userTasks = userTasks;
			_mapper = mapper;
		}

		public PostIndexViewModel MapFrom(IEnumerable<Post> source) {
			return new PostIndexViewModel {IsAuthenticated = _userTasks.IsLoggedIn(), Posts = source.Select(p => _mapper.MapFrom(p))};
		}
		public object MapFrom(object source) { return MapFrom(source as IEnumerable<Post>); }
	}
}