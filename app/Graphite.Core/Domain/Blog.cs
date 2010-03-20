using System;
using SharpArch.Core.DomainModel;

namespace Graphite.Core.Domain
{
  public class Blog : EntityWithTypedId<Guid>{
    public string Name { get; set; }
    public string Url { get; set; }
  }
}