﻿using System;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Web.UI.WebControls;
using Graphite.ApplicationServices;
using Graphite.Data.Repositories;
using NUnit.Framework;
using Rhino.Mocks;

namespace Tests.Graphite.ApplicationServices
{
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

		[Test]
		public void SyndicationFeedPostHasCorrectValues() {
			Assert.AreEqual(_feed.Items.First().Title.Text, TestData.PublishedPost.Title);
			Assert.AreEqual(((TextSyndicationContent) _feed.Items.First().Content).Text, TestData.PublishedPost.Content);
			Assert.AreEqual(_feed.Items.First().LastUpdatedTime.LocalDateTime, TestData.PublishedPost.LastEdited);
			Assert.AreEqual(_feed.Items.First().PublishDate.LocalDateTime, TestData.PublishedPost.DatePublished);
		}

		[Test]
		public void SyndicationFeedHasAuthorData() {
			Assert.AreEqual(_feed.Items.First().Authors.First().Name, TestData.PublishedPost.Author.RealName);
			Assert.AreEqual(_feed.Items.First().Authors.First().Email, TestData.PublishedPost.Author.Email);
		}
	}
}