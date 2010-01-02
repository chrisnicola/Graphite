using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text;
using Graphite.Core;
using Graphite.Data.Repositories;
using NUnit.Framework;
using Rhino.Mocks;
using SharpArch.Testing.NUnit;

namespace Tests.Graphite.ApplicationServices
{
	public interface ISyndicationService {
		/// <summary>
		/// Returns a Syndication feed with published posts
		/// </summary>
		/// <param name="requestUrl">The Url of the Syndication Feed</param>
		/// <returns>A SyndicationFeed of blog post data</returns>
		SyndicationFeed GetPostsAsSyndicationFeed(Uri requestUrl);
	}
	public class SyndicationService : ISyndicationService
	{
		private readonly IPostRepository _posts;
		public SyndicationService(IPostRepository posts) { _posts = posts; }

		public SyndicationFeed GetPostsAsSyndicationFeed(Uri requestUrl) {
			//Todo: Add settings entities so settings can be loaded here
			var items =
				_posts.FindAll().Where(x => x.Published).Select(
					p =>
						new SyndicationItem(p.Title, p.Content, new Uri("~/Post/" + p.Slug, UriKind.Relative), p.Id.ToString(),
							new DateTimeOffset(p.LastEdited)));
			return new SyndicationFeed("Test Title", "My syndication feed", requestUrl, items);
		}
	}

	[TestFixture]
	public class SyndicationServiceTests {
		private IPostRepository _repository;
		private SyndicationService _service;
		private SyndicationFeed _feed;

		[SetUp]
		public void SetUp() {
			_repository = MockRepository.GenerateStub<IPostRepository>();
			_service = new SyndicationService(_repository);
			_repository.Stub(m => m.FindAll()).Return(new[] { TestData.PublishedPost });
			_feed = _service.GetPostsAsSyndicationFeed(new Uri("http://test.com/testrss"));
		}

		[Test]
		public void SyndicationFeedContainsPosts() { Assert.That(_feed.Items.Count() == 1); }
	}

	public static class TestData {
		public static Post[] Posts = new [] { PublishedPost };

		public static readonly Post PublishedPost = new Post {
			AllowComments = true,
			Content = "Some content here",
			DateCreated = DateTime.Now,
			DatePublished = DateTime.Now,
			Published = true,
			Title = "Title"
		};
	}
}
