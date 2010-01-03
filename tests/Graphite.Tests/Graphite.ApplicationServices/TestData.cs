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
			LastEdited= DateTime.Now,
			Published = true,
			Title = "Title"
		};
	}
}