using System.Collections.Generic;
using System.Linq;
using Graphite.Core.Contracts.Mapping;
using Graphite.Core.Contracts.TaskInterfaces;
using Graphite.Core.Domain;

namespace Graphite.Web.Controllers.Posts{
	public interface IPostIndexMapper : IMapper<IEnumerable<Post>, PostIndexViewModel> {}

	public class PostIndexMapper : IPostIndexMapper{
		readonly IUserTasks _userTasks;
		readonly IMapper<Core.Domain.Post, PostShowViewModel> _mapper;

		public PostIndexMapper(IUserTasks userTasks, IMapper<Core.Domain.Post, PostShowViewModel> mapper) {
			_userTasks = userTasks;
			_mapper = mapper;
		}

		public PostIndexViewModel MapFrom(IEnumerable<Core.Domain.Post> source) {
			return new PostIndexViewModel
			       {IsAuthenticated = _userTasks.IsLoggedIn(), Posts = source.Select(p => _mapper.MapFrom(p))};
		}

		public object MapFrom(object source) { return MapFrom(source as IEnumerable<Core.Domain.Post>); }
	}
}