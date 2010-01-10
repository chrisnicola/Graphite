using System.Linq;
using AutoMapper;
using Graphite.ApplicationServices;
using Graphite.Core;
using Graphite.Core.MappingInterfaces;
using Graphite.Web.Controllers.ViewModels;

namespace Graphite.Web.Controllers.Mappers {
	
	public interface IPostNewModelMapper : IMapper<Post, PostNewModel> {}
	public class PostNewModelMapper : GenericMapper<Post, PostNewModel>, IPostNewModelMapper {
		private readonly IUserTasks _userTasks;

		public PostNewModelMapper(IUserTasks userTasks) {
			Mapper.CreateMap<Post, PostNewModel>().ForMember(m => m.Tags,
				o => o.MapFrom(p => p.Tags.Count > 0 ? p.Tags.Select(t => t.Name).Aggregate((t1, t2) => t1 + " " + t2) : ""));
			_userTasks = userTasks;
		}

		public override PostNewModel MapFrom(Post source) {
			PostNewModel viewmodel = base.MapFrom(source);
			viewmodel.Authors = _userTasks.GetUsers();
			return viewmodel;
		}
	}
}