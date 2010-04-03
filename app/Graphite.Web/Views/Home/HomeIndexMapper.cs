using System.Collections.Generic;
using System.Linq;
using Graphite.Core.Contracts.Mapping;
using Graphite.Core.Contracts.Services;
using Graphite.Web.Contracts.Mappers;
using Graphite.Web.Views.Post;

namespace Graphite.Web.Views.Home{
  public class HomeIndexMapper : IHomeIndexMapper{
		readonly IUserTasks _userTasks;
    readonly IMapper<Core.Domain.Post, PostShowViewModel> _mapper;

    public HomeIndexMapper(IUserTasks userTasks, IMapper<Core.Domain.Post, PostShowViewModel> mapper)
    {
			_userTasks = userTasks;
			_mapper = mapper;
		}

    public HomeIndexViewModel MapFrom(IEnumerable<Core.Domain.Post> source)
    {
			return new HomeIndexViewModel
			       {IsAuthenticated = _userTasks.IsLoggedIn(), Posts = source.Select(p => _mapper.MapFrom(p))};
		}

    public object MapFrom(object source) { return MapFrom(source as IEnumerable<Core.Domain.Post>); }
	}
}