using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;
using Graphite.Core;

namespace Graphite.Data.NHibernateMaps{
	public class CommentMap : IAutoMappingOverride<Comment>{
		public void Override(AutoMapping<Comment> mapping) {
			mapping.References(x => x.Parent).Column("ParentFk");
			mapping.HasMany(x => x.Children).Inverse().KeyColumn("ParentFk");
		}
	}
}