#region

using System;
using SharpArch.Core.DomainModel;

#endregion

namespace Graphite.Core {
  public class Post : EntityWithTypedId<Guid> {
    public Post() {
      DateCreated = DateTime.Now;
      Published = false;
      AllowComments = true;
    }

    public virtual string Title { get; set; }
    public virtual string Content { get; set; }
    public virtual DateTime DateCreated { get; set; }
    public virtual DateTime? DatePublished { get; set; }
    public virtual bool AllowComments { get; set; }
    public virtual bool Published { get; set; }

    public virtual void Publish() {
      Published = true;
      DatePublished = DateTime.Now;
    }
  }
}