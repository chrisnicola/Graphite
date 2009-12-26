using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate.Linq;
using SharpArch.Core.DomainModel;
using SharpArch.Data.NHibernate;

namespace Graphite.Data.Repositories {
	public class LinqRepository<T> : NHibernateRepositoryWithTypedId<T, Guid>, ILinqRepository<T> where T : EntityWithTypedId<Guid> {
		public IEnumerable<T> FindAll(Func<T, bool> predicate) {
			return Session.Linq<T>().Where(predicate);
		}

		public T FindOne(Func<T,bool> predicate) {
			return Session.Linq<T>().SingleOrDefault(predicate);
		}

		public IEnumerable<T> FindAll() {
			return Session.Linq<T>();
		}
	}
}