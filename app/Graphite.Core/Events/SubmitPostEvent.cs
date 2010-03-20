using System;
using Graphite.Core.EventAggregator;

namespace Graphite.Core.Events{
  public class SubmitPostEvent : IEvent{
    public string AuthorUserName { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime? DatePublished { get; set; }
    public bool AllowComments { get; set; }
    public bool Published { get; set; }
    public string Slug { get; set; }
    public string Tags { get; set; }
    public string Excerpt { get; set; }
  }
}