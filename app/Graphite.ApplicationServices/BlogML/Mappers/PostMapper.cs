using AutoMapper;
using BlogML.Xml;
using Graphite.Core;
using Graphite.Core.MappingInterfaces;

namespace Graphite.ApplicationServices.BlogML.Mappers {
	public interface IPostToBlogMlMapper : IMapper<Post, BlogMLPost> {}

	public class PostToBlogMlMapper : GenericMapper<Post, BlogMLPost>, IPostToBlogMlMapper {
		public PostToBlogMlMapper() {
			Mapper.CreateMap<Post, BlogMLPost>()
				.ForMember(m => m.Content,o => o.MapFrom(s => BlogMLContent.Create(s.Content, false)))
				.ForMember(m => m.Excerpt,o => o.MapFrom(s => BlogMLContent.Create(s.Exerpt, false)))
				.ForMember(m => m.ID,o => o.MapFrom(s => s.Id.ToString())).ForMember(m => m.PostUrl, o => o.MapFrom(s => s.Slug));
			Mapper.CreateMap<Comment, BlogMLComment>()
				.ForMember(m => m.Content,o => o.MapFrom(s => BlogMLContent.Create(s.Content, false)))
				.ForMember(m => m.ID,o => o.MapFrom(s => s.Id.ToString()))
				.ForMember(m => m.UserEMail, o => o.MapFrom(s => s.EmailAddress))
				.ForMember(m => m.UserUrl, o => o.MapFrom(s => s.WebAddress))
				.ForMember(m => m.UserName, o => o.MapFrom(s => s.Author));
		}

		public override BlogMLPost MapFrom(Post source) {
			BlogMLPost mlPost = base.MapFrom(source);
			foreach (Comment comment in source.Comments) mlPost.Comments.Add(Mapper.Map<Comment, BlogMLComment>(comment));
			foreach (Tag tag in source.Tags) mlPost.Categories.Add(tag.Name);
			mlPost.Authors.Add(new BlogMLAuthorReference {Ref = source.Author.Email});
			return mlPost;
		}
	}

	public class BlogMlToPostMapper : GenericMapper<BlogMLPost, Post>, IBlogMlToPostMapper {
		public BlogMlToPostMapper() {
			
		}
	}

	public interface IBlogMlToPostMapper : IMapper<BlogMLPost, Post> {}
}