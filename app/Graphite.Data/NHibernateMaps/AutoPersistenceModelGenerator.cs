using System;
using System.Linq;
using FluentNHibernate;
using FluentNHibernate.Automapping;
using FluentNHibernate.Conventions;
using Graphite.Core;
using Graphite.Data.NHibernateMaps.Conventions;
using SharpArch.Core.DomainModel;
using SharpArch.Data.NHibernate.FluentNHibernate;
using ForeignKeyConvention = Graphite.Data.NHibernateMaps.Conventions.ForeignKeyConvention;
using ManyToManyTableNameConvention = Graphite.Data.NHibernateMaps.Conventions.ManyToManyTableNameConvention;

namespace Graphite.Data.NHibernateMaps{
	public class AutoPersistenceModelGenerator : IAutoPersistenceModelGenerator{
		#region IAutoPersistenceModelGenerator Members
		public AutoPersistenceModel Generate() {
			var mappings = new AutoPersistenceModel();
			mappings.AddEntityAssembly(typeof (Post).Assembly).Where(GetAutoMappingFilter);
			mappings.Conventions.Setup(GetConventions());
			mappings.Setup(GetSetup());
			mappings.IgnoreBase<Entity>();
			mappings.IgnoreBase(typeof (EntityWithTypedId<>));
			mappings.UseOverridesFromAssemblyOf<AutoPersistenceModelGenerator>();
			return mappings;
		}
		#endregion

		static Action<AutoMappingExpressions> GetSetup() { return c => { c.FindIdentity = type => type.Name == "Id"; }; }

		static Action<IConventionFinder> GetConventions() {
			return c => {
			       	c.Add<ForeignKeyConvention>();
			       	c.Add<HasManyConvention>();
			       	c.Add<HasManyToManyConvention>();
			       	c.Add<ManyToManyTableNameConvention>();
			       	c.Add<PrimaryKeyConvention>();
			       	c.Add<ReferenceConvention>();
			       	c.Add<TableNameConvention>();
			       	c.Add<TextFieldConvention>();
			       };
		}

		/// <summary>
		/// Provides a filter for only including types which inherit from the IEntityWithTypedId interface.
		/// </summary>
		static bool GetAutoMappingFilter(Type t) {
			return t.GetInterfaces().Any(x =>
			                             x.IsGenericType &&
			                             x.GetGenericTypeDefinition() == typeof (IEntityWithTypedId<>));
		}
	}
}