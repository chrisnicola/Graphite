#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using NHibernate.Validator.Constraints;
using SharpArch.Core.DomainModel;

#endregion

namespace Graphite.Core {
  public class Post : EntityWithTypedId<Guid> {
  	public Post() {
      DateCreated = DateTime.Now;
      Published = false;
      AllowComments = true;
      Comments = new List<Comment>();
    }
		public virtual User Author { get; set; }
    public virtual string Title { get; set; }
    public virtual string Content { get; set; }
    public virtual DateTime DateCreated { get; set; }
		public virtual DateTime LastEdited { get; set; }
		public virtual DateTime? DatePublished { get; set; }
    public virtual bool AllowComments { get; set; }
    public virtual bool Published { get; set; }
    public virtual IList<Comment> Comments { get; set; }
		public virtual IList<Tag> Tags { get; set; }
		[DomainSignature]
  	public virtual string Slug { get; set; }

    public virtual void AddComment(Comment comment) {
      comment.Post = this;
      if (!Comments.Contains(comment))
        Comments.Add(comment);
    }

  	public virtual void SetSlugForPost(string slug) { 
			if (Slug == slug) return;
			if (string.IsNullOrEmpty(slug)) CreateSlugFromTitle();
			else Slug = slug;
		}

		public virtual void PublishOn(DateTime? datePublished) {
			Published = true;
			DatePublished = datePublished ?? DateTime.Now;
		}

		public virtual string GetTagsString() {
			return Tags.Select(t => t.Name).Aggregate((t1, t2) => t1 + " " + t2);
		}

		public virtual IEnumerable<string> GetTagsList() {
			return Tags.Select(t => t.Name);
		}

  	private void CreateSlugFromTitle() {
  		var slug = Regex.Replace(Title, @"/[^\w ]+/g", "");
			slug = Regex.Replace(slug, @"/ +/g", "-");
  		Slug = slug;
		}
  }
}