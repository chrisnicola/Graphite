using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;

namespace Graphite.Data.NHibernateMaps.Conventions {
  public class ReferenceConvention : IReferenceConvention {
    public void Apply(IManyToOneInstance instance) {
      if (instance.Property.Name == "Parent") instance.Column("ParentFk");
      else instance.Column(instance.Property.Name + "Fk");
    }
  }
}