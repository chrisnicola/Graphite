using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;
using FluentNHibernate.MappingModel;
using Graphite.Core.Attributes;
using NHibernate.Type;

namespace Graphite.Data.NHibernateMaps.Conventions {
	public class TextFieldConvention : AttributePropertyConvention<TextFieldAttribute> {
		protected override void Apply(TextFieldAttribute attribute, IPropertyInstance instance) {
			instance.CustomType("StringClob");
			instance.CustomSqlType("text");
		}
	}
}