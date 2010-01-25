using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BlogML.Xml;
using Graphite.Core;
using Graphite.Core.MappingInterfaces;

namespace Graphite.ApplicationServices.BlogML.Mappers {
	public interface IPostToBlogMLMapper : IMapper<Post, BlogMLPost> {}

	public class PostToBlogMLMapper : GenericMapper<Post, BlogMLPost>, IPostToBlogMLMapper
	{
		public PostToBlogMLMapper() {
			Mapper.CreateMap<Post, BlogMLPost>()
				.ForMember(m => m.Content,o => o.MapFrom(s => BlogMLContent.Create(s.Content, false)))
				.ForMember(m => m.Excerpt,o => o.MapFrom(s => BlogMLContent.Create(s.Excerpt, false)))
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

	public class BlogMLToPostMapper : GenericMapper<BlogMLPost, PostImportDetails>, IBlogMlToPostMapper {
		public BlogMLToPostMapper() {
			Mapper.CreateMap<BlogMLPost, PostImportDetails>()
				.ForMember(m => m.Content, o => o.MapFrom(s => s.Content.Text))
				.ForMember(m => m.Excerpt, o => o.MapFrom(s => s.Content.Text))
				.ForMember(m => m.AuthorEmail, o => o.MapFrom(s => s.Authors.Count > 1 ? s.Authors[0].Ref : ""));
		}
	}

	public interface IBlogMlToPostMapper : IMapper<BlogMLPost, PostImportDetails> { }
}