using System.Collections.Generic;
using Graphite.ApplicationServices;
using Graphite.Core;
using Graphite.Core.MappingInterfaces;
using Graphite.Web.Controllers.ViewModels;

namespace Graphite.Web.Controllers.Mappers {
	public interface IEditPostMapper : IMapper<PostEditModel, PostEditDetails> { }

	public class EditPostMapper : GenericMapper<PostEditModel, PostEditDetails>, IEditPostMapper {}

	public interface IPostEditModelMapper : IMapper<Post, PostEditModel> {}

	public class PostEditModelMapper : GenericMapper<Post, PostEditModel>, IPostEditModelMapper {
		private readonly IUserTasks _userTasks;
		public PostEditModelMapper(IUserTasks userTasks) { _userTasks = userTasks; }

		public override PostEditModel MapFrom(Post source)
		{
			var viewmodel = base.MapFrom(source);
			viewmodel.Authors = _userTasks.GetUsers();
			return viewmodel;
		}
	}
	public interface IPostCreateModelMapper : IMapper<Post, PostCreateModel> { }

	public class PostCreateModelMapper : GenericMapper<Post, PostCreateModel>, IPostCreateModelMapper
	{
		private readonly IUserTasks _userTasks;
		public PostCreateModelMapper(IUserTasks userTasks) { _userTasks = userTasks; }

		public override PostCreateModel MapFrom(Post source)
		{
			var viewmodel = base.MapFrom(source);
			viewmodel.Authors = _userTasks.GetUsers();
			return viewmodel;
		}
	}

}