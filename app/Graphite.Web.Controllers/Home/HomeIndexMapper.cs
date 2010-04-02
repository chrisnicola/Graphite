using System.Collections.Generic;
using System.Linq;
using Graphite.Core.Contracts.MappingInterfaces;
using Graphite.Core.Contracts.TaskInterfaces;
using Graphite.Core.Domain;
using Graphite.Web.Controllers.Posts;

namespace Graphite.Web.Controllers.Home{
	public interface IHomeIndexMapper : IMapper<IEnumerable<Post>, HomeIndexViewModel> {}

	public class HomeIndexMapper : IHomeIndexMapper{
		readonly IUserTasks _userTasks;
		readonly IMapper<Post, PostShowViewModel> _mapper;

		public HomeIndexMapper(IUserTasks userTasks, IMapper<Post, PostShowViewModel> mapper) {
			_userTasks = userTasks;
			_mapper = mapper;
		}

		public HomeIndexViewModel MapFrom(IEnumerable<Post> source) {
			return new HomeIndexViewModel
			       {IsAuthenticated = _userTasks.IsLoggedIn(), Posts = source.Select(p => _mapper.MapFrom(p))};
		}

		public object MapFrom(object source) { return MapFrom(source as IEnumerable<Post>); }
	}
}