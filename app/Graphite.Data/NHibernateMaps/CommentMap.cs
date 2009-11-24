using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;
using Graphite.Core;

namespace Graphite.Data.NHibernateMaps
{
  public class CommentMap : IAutoMappingOverride<Comment>
  {
    public void Override(AutoMapping<Comment> mapping) {
      mapping.References<Comment>(x => x.Parent).Column("ParentFk");
      mapping.HasMany<Comment>(x => x.Children).Inverse().KeyColumn("ParentFk");
    }
  }
}
