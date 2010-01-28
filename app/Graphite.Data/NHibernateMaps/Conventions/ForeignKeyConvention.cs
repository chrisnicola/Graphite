using System;
using System.Reflection;

namespace Graphite.Data.NHibernateMaps.Conventions{
	public class ForeignKeyConvention : FluentNHibernate.Conventions.ForeignKeyConvention{
		protected override string GetKeyName(PropertyInfo property, Type type) {
			if (property == null) return type.Name + "Fk";
			return property.Name + "Fk";
		}
	}
}