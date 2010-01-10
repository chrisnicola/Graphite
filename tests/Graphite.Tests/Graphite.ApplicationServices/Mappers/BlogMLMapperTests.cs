using System;
using BlogML.Xml;
using Graphite.ApplicationServices.BlogML.Mappers;
using Graphite.Core;
using NUnit.Framework;
using SharpArch.Testing;

namespace Tests.Graphite.ApplicationServices.Mappers {
	[TestFixture]
	public class when_post_is_mapped_to_blogmlpost {
		private BlogMLPost _mlPost;

		[SetUp]
		public void Setup() {
			TestData.PublishedPost.SetIdTo(Guid.NewGuid());
			TestData.PublishedPost.AddComment(TestData.Comment);
			TestData.PublishedPost.Tags.Add(new Tag {Name = "tag"});
			_mlPost = new PostMapper().MapFrom(TestData.PublishedPost);
		}

		[Test]
		public void post_fields_are_mapped() {
			Assert.That(_mlPost.DateCreated == TestData.PublishedPost.DateCreated, "DateCreated not mapped");
			Assert.That(_mlPost.DateModified == TestData.PublishedPost.DateModified, "DateCreated not mapped");
			Assert.That(_mlPost.Title == TestData.PublishedPost.Title, "Title not mapped");
			Assert.That(_mlPost.Approved == TestData.PublishedPost.Published, "Approved not mapped");
			Assert.That(_mlPost.PostUrl == TestData.PublishedPost.Slug, "PostURL not mapped");
			Assert.That(_mlPost.ID == TestData.PublishedPost.Id.ToString(), "Id not mapped");
			Assert.That(_mlPost.Excerpt.Text == TestData.PublishedPost.Exerpt, "Exerpt not mapped");
			Assert.That(_mlPost.Content.Text == TestData.PublishedPost.Content, "Content not mapped");
		} 

		[Test]
		public void comment_content_is_mapped() {
			Assert.That(_mlPost.Comments[0].Content.Text == TestData.Comment.Content, "Content not mapped");
			Assert.That(_mlPost.Comments[0].ID == TestData.Comment.Id.ToString(), "Id not mapped");
			Assert.That(_mlPost.Comments[0].Approved == TestData.Comment.Approved, "Approved not mapped");
			Assert.That(_mlPost.Comments[0].DateCreated == TestData.Comment.DateCreated, "DateCreated not mapped");
			Assert.That(_mlPost.Comments[0].UserEMail == TestData.Comment.EmailAddress, "Email not mapped");
			Assert.That(_mlPost.Comments[0].UserName == TestData.Comment.Author, "Email not mapped");
			Assert.That(_mlPost.Comments[0].UserUrl == TestData.Comment.WebAddress, "Email not mapped");
		}
	}
}