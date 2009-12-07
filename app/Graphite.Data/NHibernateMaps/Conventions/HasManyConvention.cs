using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;
using FluentNHibernate.MappingModel;

namespace Graphite.Data.NHibernateMaps.Conventions {
  public class HasManyConvention : IHasManyConvention {
    public void Apply(IOneToManyCollectionInstance instance) {
      instance.Key.Column(instance.EntityType.Name + "Fk");
      instance.Cascade.AllDeleteOrphan();
      instance.Inverse();
    }
  }
}