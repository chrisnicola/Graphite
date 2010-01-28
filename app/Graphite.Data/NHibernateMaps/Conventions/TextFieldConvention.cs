using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;
using Graphite.Core.Attributes;

namespace Graphite.Data.NHibernateMaps.Conventions{
	public class TextFieldConvention : AttributePropertyConvention<TextFieldAttribute>{
		protected override void Apply(TextFieldAttribute attribute, IPropertyInstance instance) {
			instance.CustomType("StringClob");
			instance.CustomSqlType("text");
		}
	}
}