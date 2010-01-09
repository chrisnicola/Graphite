using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Graphite.ApplicationServices;
using Graphite.Core;
using Graphite.Core.MappingInterfaces;
using Graphite.Web.Controllers.ViewModels;

namespace Graphite.Web.Controllers.Mappers {
	public interface IEditPostMapper : IMapper<PostEditModel, PostEditDetails> {}

	public class EditPostMapper : GenericMapper<PostEditModel, PostEditDetails>, IEditPostMapper {}

	public interface IPostEditModelMapper : IMapper<Post, PostEditModel> {}

	public class PostEditModelMapper : GenericMapper<Post, PostEditModel>, IPostEditModelMapper {
		private readonly IUserTasks _userTasks;

		public PostEditModelMapper(IUserTasks userTasks) {
			Mapper.CreateMap<Post, PostEditModel>()
				.ForMember(m => m.Tags,o => o.MapFrom(p => p.Tags.Select(t => t.Name).Aggregate((t1, t2) => t1 + " " + t2)))
				.ForMember(m => m.Categories, o => o.MapFrom(p => p.Categories.Select(c => c.Name).Aggregate((t1, t2) => t1 + " " + t2)));
			_userTasks = userTasks;
		}

		public override PostEditModel MapFrom(Post source) {
			PostEditModel viewmodel = base.MapFrom(source);
			viewmodel.Authors = _userTasks.GetUsers();
			return viewmodel;
		}
	}

	public interface IPostCreateModelMapper : IMapper<Post, PostCreateModel> {}

	public class PostCreateModelMapper : GenericMapper<Post, PostCreateModel>, IPostCreateModelMapper {
		private readonly IUserTasks _userTasks;
		private readonly ICategoryTasks _categoryTasks;

		public PostCreateModelMapper(IUserTasks userTasks, ICategoryTasks categoryTasks) {
			Mapper.CreateMap<Post, PostCreateModel>()
				.ForMember(m => m.Tags, o => o.MapFrom(p => p.Tags.Select(t => t.Name).Aggregate((t1, t2) => t1 + " " + t2)))
				.ForMember(m => m.Categories, o => o.MapFrom(p => p.Categories.Select(c => c.Name).Aggregate((t1, t2) => t1 + " " + t2)));
			_userTasks = userTasks;
			_categoryTasks = categoryTasks;
		}

		public override PostCreateModel MapFrom(Post source) {
			PostCreateModel viewmodel = base.MapFrom(source);
			viewmodel.Authors = _userTasks.GetUsers();
			viewmodel.Categories = _categoryTasks.GetCategoryNames();
			return viewmodel;
		}
	}

	public interface ICategoryTasks {
		IEnumerable<string> GetCategoryNames();
	}
}