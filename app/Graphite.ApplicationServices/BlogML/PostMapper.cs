using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BlogML.Xml;
using Graphite.Core;
using Graphite.Core.Domain;
using Graphite.Core.MappingInterfaces;

namespace Graphite.ApplicationServices.BlogML{
	public interface IPostToBlogMLMapper : IMapper<Post, BlogMLPost> {}

	public class PostToBlogMLMapper : GenericMapper<Post, BlogMLPost>, IPostToBlogMLMapper{
		public PostToBlogMLMapper() {
			Mapper.CreateMap<Post, BlogMLPost>()
			.ForMember(m => m.Content, o => o.MapFrom(s => BlogMLContent.Create(s.Content, false)))
			.ForMember(m => m.Excerpt, o => o.MapFrom(s => BlogMLContent.Create(s.Excerpt, false)))
			.ForMember(m => m.Approved, o => o.MapFrom(s => s.Published))
			.ForMember(m => m.DateCreated, o => o.MapFrom(s => s.DatePublished ?? s.DateCreated))
			.ForMember(m => m.ID, o => o.MapFrom(s => s.Id.ToString())).ForMember(m => m.PostUrl, o => o.MapFrom(s => s.Slug));
			Mapper.CreateMap<Comment, BlogMLComment>()
			.ForMember(m => m.Content, o => o.MapFrom(s => BlogMLContent.Create(s.Content, false)))
			.ForMember(m => m.ID, o => o.MapFrom(s => s.Id.ToString()))
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

	public class BlogMLToPostMapper : GenericMapper<BlogMLPost, PostImportDetails>, IBlogMLToPostMapper{
		public BlogMLToPostMapper() {
			Mapper.CreateMap<BlogMLPost, PostImportDetails>().ForMember(m => m.Content, o => o.MapFrom(s => s.Content.Text))
			.ForMember(m => m.Excerpt, o => o.MapFrom(s => s.Content.Text))
			.ForMember(m => m.AuthorUserName, o => o.MapFrom(s => s.Authors.Count > 1 ? s.Authors[0].Ref : ""))
			.ForMember(m => m.DatePublished, o => o.MapFrom(s => s.DateCreated))
			.ForMember(m => m.Published, o => o.MapFrom(s => s.Approved));
		}

		public PostImportDetails MapFrom(BlogMLPost blogMLPost, IEnumerable<BlogMLCategory> categories) {
			PostImportDetails post = MapFrom(blogMLPost);
			post.Tags = GetTags(blogMLPost, categories);
			return post;
		}

		static string GetTags(BlogMLPost blogMLPost, IEnumerable<BlogMLCategory> categories) {
			return categories.Where(c => blogMLPost.Categories.Cast<BlogMLCategoryReference>().Any(r => r.Ref == c.ID))
			.Aggregate("", (a, b) => a + "," + b.Title.Replace(' ', '-'), a => a.TrimStart(','));
		}
	}

	public class BlogMLToCommentMapper : GenericMapper<BlogMLComment, Comment>, IBlogMLToCommentMapper{
		public BlogMLToCommentMapper() {
			Mapper.CreateMap<BlogMLComment, Comment>()
			.ForMember(m => m.Id, o => o.Ignore())
			.ForMember(m => m.Content, o => o.MapFrom(s => s.Content.Text))
			.ForMember(m => m.EmailAddress, o => o.MapFrom(s => s.UserEMail))
			.ForMember(m => m.WebAddress, o => o.MapFrom(s => s.UserUrl))
			.ForMember(m => m.Author, o => o.MapFrom(s => s.UserName));
		}
	}

	public interface IBlogMLToCommentMapper : IMapper<BlogMLComment, Comment> {}

	public interface IBlogMLToPostMapper : IMapper<BlogMLPost, PostImportDetails>{
		PostImportDetails MapFrom(BlogMLPost post, IEnumerable<BlogMLCategory> categories);
	}
}