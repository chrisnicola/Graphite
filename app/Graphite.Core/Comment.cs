using System;
using System.Collections.Generic;
using Graphite.Core.Attributes;
using NHibernate.Validator.Constraints;
using SharpArch.Core.DomainModel;

namespace Graphite.Core {
  public class Comment : EntityWithTypedId<Guid> {
    public Comment() {
      DateCreated = DateTime.Now;
    }
    public virtual string Author { get; set; }
    public virtual string EmailAddress { get; set; }
    public virtual string WebAddress { get; set; }
		
		[TextField]
    public virtual string Content { get; set; }
    public virtual DateTime DateCreated { get; set; }
    public virtual Post Post { get; set; }
    public virtual Comment Parent { get; set; }
    public virtual IList<Comment> Children { get; set; }
  	public virtual bool Approved { get; set; }
  }
}