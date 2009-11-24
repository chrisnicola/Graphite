using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;

namespace Graphite.Data.NHibernateMaps.Conventions {
  public class HasManyConvention : IHasManyConvention {
    public void Apply(IOneToManyCollectionInstance instance) {
      instance.Key.Column(instance.EntityType.Name + "Fk");
    }
  }
}