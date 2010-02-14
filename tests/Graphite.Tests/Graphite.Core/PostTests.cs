using Graphite.Core;
using NUnit.Framework;

namespace Tests.Graphite.Core{
	public class when_post_slug_is_set{
		Post _post;

		[SetUp]
		public void Setup() {
			_post = new Post {Title = "postInfo Title"};
			_post.SetSlugForPost("This is a test!#");
		}

		[Test]
		public void null_slug_is_created_from_title() {
			_post.Slug = null;
			_post.SetSlugForPost(null);
			Assert.That(_post.Slug, Is.EqualTo("post-title"));
		}

		[Test]
		public void slug_replaces_spaces_with_hyphens() { Assert.That(_post.Slug, Is.EqualTo("this-is-a-test")); }

		[Test]
		public void slug_contains_no_symbol_characters() { Assert.That(_post.Slug.ToCharArray(), Is.All.Not.Matches<char>(c => char.IsSymbol(c))); }

		[Test]
		public void slug_is_all_lowercase() { Assert.That(_post.Slug, Is.EqualTo(_post.Slug.ToLower())); }
	}
}