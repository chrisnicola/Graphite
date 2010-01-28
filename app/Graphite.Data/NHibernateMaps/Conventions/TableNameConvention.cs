using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;

namespace Graphite.Data.NHibernateMaps.Conventions{
	public class TableNameConvention : IClassConvention{
		public void Apply(IClassInstance instance) { instance.Table(Inflector.Net.Inflector.Pluralize(instance.EntityType.Name)); }
	}
}