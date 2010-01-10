using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Graphite.ApplicationServices;
using Graphite.Core;
using Graphite.Core.MappingInterfaces;
using Graphite.Web.Controllers.ViewModels;

namespace Graphite.Web.Controllers.Mappers {
	public interface IPostEditDetailsMapper : IMapper<PostEditModel, PostEditDetails> {}

	public class PostEditDetailsMapper : GenericMapper<PostEditModel, PostEditDetails>, IPostEditDetailsMapper {}

	public interface IPostEditModelMapper : IMapper<Post, PostEditModel> {}

	public class PostEditModelMapper : GenericMapper<Post, PostEditModel>, IPostEditModelMapper {
		private readonly IUserTasks _userTasks;

		public PostEditModelMapper(IUserTasks userTasks) {
			Mapper.CreateMap<Post, PostEditModel>().ForMember(m => m.Tags,
				o => o.MapFrom(p => p.Tags.Count > 0 ? p.Tags.Select(t => t.Name).Aggregate((t1, t2) => t1 + " " + t2) : ""));
			_userTasks = userTasks;
		}

		public override PostEditModel MapFrom(Post source) {
			PostEditModel viewmodel = base.MapFrom(source);
			viewmodel.Authors = _userTasks.GetUsers();
			return viewmodel;
		}
	}
}