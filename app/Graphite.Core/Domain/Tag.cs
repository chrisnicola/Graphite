using System;
using System.Collections.Generic;
using SharpArch.Core.DomainModel;

namespace Graphite.Core.Domain{
  public class Tag : EntityWithTypedId<Guid>{
    [DomainSignature]
    public virtual string Name { get; set; }
    public virtual IList<Post> Posts { get; set; }
  }
}