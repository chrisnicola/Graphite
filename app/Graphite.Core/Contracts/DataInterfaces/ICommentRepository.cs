using System;
using Graphite.Core.Domain;
using SharpArch.Core.PersistenceSupport.NHibernate;

namespace Graphite.Core.Contracts.DataInterfaces{
  public interface ICommentRepository : INHibernateRepositoryWithTypedId<Comment, Guid> {}
}