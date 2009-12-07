using System;
using System.Collections.Generic;
using NHibernate.Validator.Constraints;
using SharpArch.Core.DomainModel;

namespace Graphite.Core {
  public class Comment : EntityWithTypedId<Guid> {
    public Comment() {
      DatePosted = DateTime.Now;
    }
    public virtual string Author { get; set; }
    public virtual string EmailAddress { get; set; }
    public virtual string WebAddress { get; set; }
    public virtual string Content { get; set; }
    public virtual DateTime DatePosted { get; set; }
    public virtual Post Post { get; set; }
    public virtual Comment Parent { get; set; }
    public virtual IList<Comment> Children { get; set; }
  }
}