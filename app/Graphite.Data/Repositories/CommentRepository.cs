using System;
using Graphite.Core;
using Graphite.Core.Contracts.DataInterfaces;
using Graphite.Core.Domain;
using SharpArch.Data.NHibernate;

namespace Graphite.Data.Repositories{
  public class CommentRepository : NHibernateRepositoryWithTypedId<Comment, Guid>, ICommentRepository {}
}