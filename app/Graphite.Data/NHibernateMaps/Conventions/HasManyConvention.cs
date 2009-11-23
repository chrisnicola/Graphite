using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;

namespace Graphite.Data.NHibernateMaps.Conventions {
  public class HasManyConvention : IHasManyConvention {
    public void Apply(IOneToManyCollectionInstance instance) {
      if (instance.Member.Name == "Children") instance.Key.Column("ParentFk");
      else instance.Key.Column(instance.EntityType.Name + "Fk");
    }
  }
}