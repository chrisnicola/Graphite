using System.Collections.Generic;
using System.Linq;
using Graphite.ApplicationServices;
using Graphite.Core;
using Graphite.Core.Contracts.MappingInterfaces;
using Graphite.Core.Contracts.TaskInterfaces;
using Graphite.Core.Domain;
using Graphite.Web.Controllers.ViewModels;

namespace Graphite.Web.Controllers.Mappers{
	public interface IPostIndexMapper : IMapper<IEnumerable<Post>, PostIndexViewModel> {}

	public class PostIndexMapper : IPostIndexMapper{
		readonly IUserTasks _userTasks;
		readonly IMapper<Post, PostShowViewModel> _mapper;

		public PostIndexMapper(IUserTasks userTasks, IMapper<Post, PostShowViewModel> mapper) {
			_userTasks = userTasks;
			_mapper = mapper;
		}

		public PostIndexViewModel MapFrom(IEnumerable<Post> source) {
			return new PostIndexViewModel
			       {IsAuthenticated = _userTasks.IsLoggedIn(), Posts = source.Select(p => _mapper.MapFrom(p))};
		}

		public object MapFrom(object source) { return MapFrom(source as IEnumerable<Post>); }
	}
}