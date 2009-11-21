using System;
using SharpArch.Core.DomainModel;

namespace Graphite.Core
{
    public class Post : EntityWithTypedId<Guid> {
        public Post() {
            DateCreated = DateTime.Now;
            Published = false;
            AllowComments = true;
        }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DatePublished { get; set; }
        public DateTime LastUpdated { get; set; }
        public bool AllowComments { get; set; }
        public bool Published { get; set; }
    }
}
