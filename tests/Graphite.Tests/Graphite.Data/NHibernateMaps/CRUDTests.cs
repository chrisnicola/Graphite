using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Graphite.Core;
using Graphite.Data.NHibernateMaps;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using NUnit.Framework;
using SharpArch.Data.NHibernate;
using SharpArch.Testing.NHibernate;

namespace Tests.Graphite.Data.NHibernateMaps
{
  [TestFixture]
  public class CRUDTests
  {
    private ISession _session;

    [SetUp]
    public virtual void SetUp()
    {
      string[] mappingAssemblies = RepositoryTestsHelper.GetMappingAssemblies();
      var cfg = NHibernateSession.Init(new SimpleSessionStorage(), mappingAssemblies,
        new AutoPersistenceModelGenerator().Generate());
      _session = GetSession();
      new SchemaExport(cfg).Execute(true, true, false, _session.Connection, null);
    }

    [Test]
    public virtual void CanSaveAndDeletePostWithComments() {      
      var post = new Post();
      post.AddComment(new Comment());
      _session.Save(post);
      _session.Flush();
      _session.Delete(post);
      _session.Flush();
    }

    private ISession GetSession() { return NHibernateSession.GetDefaultSessionFactory().OpenSession(); }
  }
}
