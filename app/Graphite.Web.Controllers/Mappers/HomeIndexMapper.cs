﻿using System.Collections.Generic;
using System.Linq;
using Graphite.ApplicationServices;
using Graphite.Core;
using Graphite.Core.MappingInterfaces;
using Graphite.Web.Controllers.ViewModels;

namespace Graphite.Web.Controllers.Mappers {
	public interface IHomeIndexMapper : IMapper<IEnumerable<Post>, HomeIndexViewModel> {}
	public class HomeIndexMapper : IHomeIndexMapper {
		private readonly IUserTasks _userTasks;
		private readonly IMapper<Post, PostShowViewModel> _mapper;

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