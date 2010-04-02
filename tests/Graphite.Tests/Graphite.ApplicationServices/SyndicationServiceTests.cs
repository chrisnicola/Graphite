using System;
using System.Linq;
using System.ServiceModel.Syndication;
using Graphite.ApplicationServices;
using Graphite.Core.Contracts.Data;
using Graphite.Data.Repositories;
using NUnit.Framework;
using Rhino.Mocks;

namespace Tests.Graphite.ApplicationServices{
	[TestFixture]
	public class SyndicationServiceTests{
		[SetUp]
		public void SetUp() {
			_repository = MockRepository.GenerateStub<IPostRepository>();
			_service = new SyndicationService(_repository);
			_repository.Stub(m => m.FindAll()).Return(new[] {TestData.PublishedPost});
			_feed = _service.GetPostsAsSyndicationFeed(new Uri("http://test.com/testrss"));
		}

		IPostRepository _repository;
		SyndicationService _service;
		SyndicationFeed _feed;

		[Test]
		public void SyndicationFeedContainsPosts() { Assert.That(_feed.Items.Count() == 1); }

		[Test]
		public void SyndicationFeedPostHasCorrectValues() {
			Assert.AreEqual(_feed.Items.First().Title.Text, TestData.PublishedPost.Title);
			Assert.AreEqual(((TextSyndicationContent) _feed.Items.First().Content).Text, TestData.PublishedPost.Content);
			Assert.AreEqual(_feed.Items.First().LastUpdatedTime.LocalDateTime, TestData.PublishedPost.DateModified);
			Assert.AreEqual(_feed.Items.First().PublishDate.LocalDateTime, TestData.PublishedPost.DatePublished);
		}

		[Test]
		public void SyndicationFeedHasAuthorData() {
			Assert.AreEqual(_feed.Items.First().Authors.First().Name, TestData.PublishedPost.Author.RealName);
			Assert.AreEqual(_feed.Items.First().Authors.First().Email, TestData.PublishedPost.Author.Email);
		}
	}
}