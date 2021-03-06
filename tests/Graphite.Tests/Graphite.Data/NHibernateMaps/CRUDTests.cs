﻿using Graphite.Core;
using Graphite.Core.Domain;
using Graphite.Data.NHibernateMaps;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using NUnit.Framework;
using SharpArch.Data.NHibernate;
using SharpArch.Testing.NHibernate;

namespace Tests.Graphite.Data.NHibernateMaps{
	[TestFixture]
	public class CRUDTests{
		[SetUp]
		public virtual void SetUp() {
			string[] mappingAssemblies = RepositoryTestsHelper.GetMappingAssemblies();
			Configuration cfg = NHibernateSession.Init(new SimpleSessionStorage(), mappingAssemblies,
			                                           new AutoPersistenceModelGenerator().Generate());
			_session = GetSession();
			new SchemaExport(cfg).Execute(true, true, false, _session.Connection, null);
		}

		[TearDown]
		public virtual void TearDown() {
			NHibernateSession.CloseAllSessions();
			NHibernateSession.Reset();
		}

		ISession _session;

		ISession GetSession() { return NHibernateSession.GetDefaultSessionFactory().OpenSession(); }

		[Test]
		public virtual void CanSaveAndDeletePostWithComments() {
			var post = new Post();
			post.AddComment(new Comment());
			_session.Save(post);
			_session.Flush();
			_session.Delete(post);
			_session.Flush();
		}
	}
}