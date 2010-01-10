using AutoMapper;
using BlogML.Xml;
using Graphite.Core;
using Graphite.Core.MappingInterfaces;

namespace Graphite.ApplicationServices.BlogML.Mappers {
	public interface IPostMapper : IMapper<Post, BlogMLPost> {}
	public class PostMapper : GenericMapper<Post, BlogMLPost> {
		public PostMapper() {
			Mapper.CreateMap<Post, BlogMLPost>()
				.ForMember(m => m.Content,o => o.MapFrom(s => BlogMLContent.Create(s.Content, false)))
				.ForMember(m => m.Excerpt, o => o.MapFrom(s => BlogMLContent.Create(s.Exerpt, false)))
				.ForMember(m => m.ID, o => o.MapFrom(s => s.Id.ToString()))
				.ForMember(m => m.PostUrl, o => o.MapFrom(s => s.Slug));
			//Mapper.CreateMap<string, BlogMLContent>();
			//Mapper.CreateMap<User, BlogMLAuthor>();
			Mapper.CreateMap<Comment, BlogMLComment>()
				.ForMember(m => m.Content, o => o.MapFrom(s => BlogMLContent.Create(s.Content, false)))
				.ForMember(m => m.ID, o => o.MapFrom(s => s.Id.ToString()))
				.ForMember(m => m.UserEMail, o => o.MapFrom(s => s.EmailAddress))
				.ForMember(m => m.UserUrl, o => o.MapFrom(s => s.WebAddress))
				.ForMember(m => m.UserName, o => o.MapFrom(s => s.Author));
				
		}

		public override BlogMLPost MapFrom(Post source)
		{
			var mlPost = base.MapFrom(source);
			foreach(var comment in source.Comments) mlPost.Comments.Add(Mapper.Map<Comment, BlogMLComment>(comment));
			foreach (var tag in source.Tags) mlPost.Categories.Add(tag.Name);
			mlPost.Authors.Add(new BlogMLAuthorReference {Ref = source.Author.Email});
			return mlPost;
		}
	}
}