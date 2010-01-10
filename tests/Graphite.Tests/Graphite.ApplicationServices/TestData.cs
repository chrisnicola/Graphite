using System;
using Graphite.Core;

namespace Tests.Graphite.ApplicationServices {
	public static class TestData {
		public static Post[] Posts = new [] { PublishedPost };

		public static User User = new User {Username = "username", RealName = "Real Name", Email = "user@test.com"};

		public static readonly Post PublishedPost = new Post {
			Author = User,
			AllowComments = true,
			Content = "Some content here",
			DateCreated = DateTime.Now,
			DatePublished = DateTime.Now,
			DateModified= DateTime.Now,
			Published = true,
			Title = "Title",
			Exerpt = "Some",
			Slug = "Slug",
		};

		public static readonly Comment Comment = new Comment {Author = "Someone", EmailAddress = "someone@test.com", Content = "Some comment content"};
	}
}