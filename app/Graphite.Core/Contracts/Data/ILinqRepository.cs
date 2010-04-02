using System;
using System.Collections.Generic;
using SharpArch.Core;
using SharpArch.Core.DomainModel;

namespace Graphite.Core.Contracts.Data{
  public interface ILinqRepository<T> where T : EntityWithTypedId<Guid>{
    IEnumerable<T> FindAll(Func<T, bool> predicate);

    T FindOne(Func<T, bool> predicate);

    T Get(Guid id, Enums.LockMode lockMode);

    T Load(Guid id);

    T Load(Guid id, Enums.LockMode lockMode);

    IList<T> FindAll(T exampleInstance, params string[] propertiesToExclude);

    T FindOne(T exampleInstance, params string[] propertiesToExclude);

    T Save(T entity);

    T Update(T entity);

    void Evict(T entity);

    T Get(Guid id);

    IEnumerable<T> FindAll();

    IList<T> FindAll(IDictionary<string, object> propertyValuePairs);

    T FindOne(IDictionary<string, object> propertyValuePairs);

    void Delete(T entity);

    void Delete(Guid id);

    T SaveOrUpdate(T entity);
  }
}