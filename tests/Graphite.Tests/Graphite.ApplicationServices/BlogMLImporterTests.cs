using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;
using BlogML.Xml;
using Graphite.ApplicationServices;
using Graphite.ApplicationServices.BlogML;
using Graphite.Core;
using NUnit.Framework;
using Rhino.Mocks;

namespace Tests.Graphite.ApplicationServices {
	[TestFixture]
	public class BlogMLImporterTests {
		private BlogMLImporter _importer;
		private IPostTasks _tasks;

		[SetUp]
		public void Setup() {
			_tasks = MockRepository.GenerateMock<IPostTasks>();
			_importer = new BlogMLImporter(_tasks);
			_importer.Import(XmlReader.Create(Assembly.GetExecutingAssembly().GetManifestResourceStream("Tests.BlogML.xml"), null));
		}
		
		[Test]
		public void WhenBlogMLIsImportedNewPostsAreSaved() {
			_tasks.AssertWasCalled(m => m.SaveNewPost(Arg<PostCreateDetails>.Matches(p =>
				p.Title == "Even images need boundaries")));
		}

		[Test]
		public void WhenBlogMLIs
	}
}