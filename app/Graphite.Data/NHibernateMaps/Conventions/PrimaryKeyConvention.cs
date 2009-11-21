﻿using FluentNHibernate.Conventions;
using FluentNHibernate.Mapping;

namespace Graphite.Data.NHibernateMaps.Conventions
{
    public class PrimaryKeyConvention : IIdConvention
    {
        public void Apply(FluentNHibernate.Conventions.Instances.IIdentityInstance instance)
        {
            instance.Column("Id");
            instance.GeneratedBy.GuidComb();
        }
    }
}
