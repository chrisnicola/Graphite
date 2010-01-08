using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;
using Graphite.Core;

namespace Graphite.Data.NHibernateMaps {
	public class CategoryMap : IAutoMappingOverride<Category> {
		public void Override(AutoMapping<Category> mapping) {
			mapping.References(x => x.Parent).Column("ParentFk");
			mapping.HasMany(x => x.Children).Inverse().KeyColumn("ParentFk");
		}
	}
}