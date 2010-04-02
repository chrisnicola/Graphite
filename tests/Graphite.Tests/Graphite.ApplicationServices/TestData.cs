using System;
using BlogML.Xml;
using Graphite.Core;
using Graphite.Core.Domain;
using SharpArch.Testing;

namespace Tests.Graphite.ApplicationServices{
	public class TestData{
		public static readonly Post PublishedPost = new Post {
		                                                     Author = User,
		                                                     AllowComments = true,
		                                                     Content = "Some content here",
		                                                     DateCreated = DateTime.Now,
		                                                     DatePublished = DateTime.Now,
		                                                     DateModified = DateTime.Now,
		                                                     Published = true,
		                                                     Title = "Title",
		                                                     Excerpt = "Some",
		                                                     Slug = "Slug",
		                                                     };

		public static readonly Comment Comment = new Comment {
		                                                     Author = "Someone",
		                                                     EmailAddress = "someone@test.com",
		                                                     Content = "Some comment content"
		                                                     };
		public static BlogMLComment blogMlComment = new BlogMLComment {
		                                                              UserName = "Someone",
		                                                              UserEMail = "someone@test.com",
		                                                              Content =
		                                                              new BlogMLContent {Text = "Some comment content"},
		                                                              ID = Guid.NewGuid().ToString()
		                                                              };

		public static BlogMLPost BlogMlPost = new BlogMLPost {
		                                                     ID = Guid.NewGuid().ToString(),
		                                                     Content = new BlogMLContent {Text = "Some content"},
		                                                     Title = "Title",
		                                                     PostName = "Title",
		                                                     PostUrl = "title",
		                                                     DateCreated = DateTime.Now,
		                                                     DateModified = DateTime.Now,
		                                                     Excerpt = new BlogMLContent {Text = "Some"},
		                                                     HasExcerpt = true
		                                                     };

		public static Post[] Posts = new[] {PublishedPost};

		public static User User = new User {Username = "username", RealName = "Real Name", Email = "user@test.com"};

		public static void Init() {
			PublishedPost.SetIdTo(Guid.NewGuid());
			PublishedPost.AddComment(Comment);
			PublishedPost.Tags.Add(new Tag {Name = "tag"});
			BlogMlPost.Authors.Add("user@test.com");
			BlogMlPost.Comments.Add(blogMlComment);
		}
	}
}