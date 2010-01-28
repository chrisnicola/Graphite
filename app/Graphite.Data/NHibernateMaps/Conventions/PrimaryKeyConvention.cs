using System;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;
using SharpArch.Core.DomainModel;

namespace Graphite.Data.NHibernateMaps.Conventions{
	public class PrimaryKeyConvention : IIdConvention{
		public void Apply(IIdentityInstance instance) {
			instance.Column("Id");
			if (instance.EntityType.IsAssignableFrom(typeof (EntityWithTypedId<Guid>))) instance.GeneratedBy.GuidComb();
			if (instance.EntityType.IsAssignableFrom(typeof (Entity))) instance.GeneratedBy.HiLo("100");
		}
	}
}