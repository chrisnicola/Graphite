using System.Collections.Generic;
using FluentNHibernate.Automapping;
using Graphite.Data.NHibernateMaps;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Metadata;
using NHibernate.Tool.hbm2ddl;
using NUnit.Framework;
using SharpArch.Data.NHibernate;
using SharpArch.Testing.NHibernate;

namespace Tests.Graphite.Data.NHibernateMaps{
	/// <summary>
	/// Provides a means to verify that the target database is in compliance with all mappings.
	/// Taken from http://ayende.com/Blog/archive/2006/08/09/NHibernateMappingCreatingSanityChecks.aspx.
	/// 
	/// If this is failing, the error will likely inform you that there is a missing table or column
	/// which needs to be added to your database.
	/// </summary>
	[TestFixture, Category("DB Tests")]
  
	public class MappingIntegrationTests{
		[SetUp]
		public virtual void SetUp() {
			string[] mappingAssemblies = RepositoryTestsHelper.GetMappingAssemblies();
			configuration = NHibernateSession.Init(new SimpleSessionStorage(), mappingAssemblies,
			                                       new AutoPersistenceModelGenerator().Generate(),
			                                       "../../../../app/Graphite.Web/NHibernate.config");
		}

		[TearDown]
		public virtual void TearDown() {
			NHibernateSession.CloseAllSessions();
			NHibernateSession.Reset();
		}

		Configuration configuration;

		[Test]
		public void CanConfirmDatabaseMatchesMappings() {
			IDictionary<string, IClassMetadata> allClassMetadata =
			NHibernateSession.GetDefaultSessionFactory().GetAllClassMetadata();
			foreach (var entry in allClassMetadata) NHibernateSession.Current.CreateCriteria(entry.Value.GetMappedClass(EntityMode.Poco)).SetMaxResults(0).List();
		}

		/// <summary>
		/// Generates and outputs the database schema SQL to the console
		/// </summary>
		[Test]
		public void CanGenerateDatabaseSchema() {
			ISession session = NHibernateSession.GetDefaultSessionFactory().OpenSession();
			new SchemaExport(configuration).Execute(true, true, false, session.Connection, null);
		}

		[Test, Ignore]
		public void UpdateDatabaseSchema() {
			ISession session = NHibernateSession.GetDefaultSessionFactory().OpenSession();
			new SchemaUpdate(configuration).Execute(true, true);
		}

		[Test, Ignore]
		public void OutputMappingsToXML() {
			AutoPersistenceModel mappings = new AutoPersistenceModelGenerator().Generate();
			mappings.CompileMappings();
			mappings.WriteMappingsTo("C:\\Mappings\\");
		}
	}
}