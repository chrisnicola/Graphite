﻿using System;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Castle.Windsor;
using CommonServiceLocator.WindsorAdapter;
using Graphite.Data.NHibernateMaps;
using Graphite.Web.CastleWindsor;
using Graphite.Web.Controllers;
using Graphite.Web.Controllers.Home;
using log4net.Config;
using Microsoft.Practices.ServiceLocation;
using MvcContrib.Castle;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using RestfulRouting;
using SharpArch.Data.NHibernate;
using SharpArch.Web.ModelBinder;
using SharpArch.Web.NHibernate;

namespace Graphite.Web{
	// Note: For instructions on enabling IIS6 or IIS7 classic mode, 
	// visit http://go.microsoft.com/?LinkId=9394801

	public class MvcApplication : HttpApplication{
		WebSessionStorage _webSessionStorage;

		protected void Application_Start() {
      XmlConfigurator.Configure();
      //ViewEngines.Engines.Clear();
      //ViewEngines.Engines.Add(new RestfulRoutingViewEngine());
			InitializeServiceLocator();
		  ModelBinders.Binders.DefaultBinder = new DefaultModelBinder();
			
      AreaRegistration.RegisterAllAreas();
			RouteRegistrar.RegisterRoutesTo(RouteTable.Routes);
      //InputBuilder.BootStrap();
		}

	  /// <summary>
	  /// Instantiate the container and add all Controllers that derive from 
	  /// WindsorController to the container.  Also associate the Controller 
	  /// with the WindsorContainer ControllerFactory.
	  /// </summary>
	  static void InitializeServiceLocator() {
			IWindsorContainer container = new WindsorContainer();
      ComponentRegistrar.AddComponentsTo(container);
      ControllerBuilder.Current.SetControllerFactory(new WindsorControllerFactory(container));
			container.RegisterControllers(typeof (HomeController).Assembly);
			ServiceLocator.SetLocatorProvider(() => new WindsorServiceLocator(container));
	    return;
	  }

		public override void Init() {
			base.Init();
			// The WebSessionStorage must be created during the Init() to tie in HttpApplication events
			_webSessionStorage = new WebSessionStorage(this);
		}

		/// <summary>
		/// Due to issues on IIS7, the NHibernate initialization cannot reside in Init() but
		/// must only be called once.  Consequently, we invoke a thread-safe singleton class to 
		/// ensure it's only initialized once.
		/// </summary>
		protected void Application_BeginRequest(object sender, EventArgs e) { NHibernateInitializer.Instance().InitializeNHibernateOnce(InitializeNHibernateSessionFactory); }

		/// <summary>
		/// If you need to communicate to multiple databases, you'd add a line to this method to
		/// initialize the other database as well.
		/// </summary>
		void InitializeNHibernateSessionFactory() {
			Configuration cfg = NHibernateSession.Init(_webSessionStorage, new[] {Server.MapPath("~/bin/Graphite.Data.dll")},
			                                           new AutoPersistenceModelGenerator().Generate(),
			                                           Server.MapPath("~/NHibernate.config"));
			new SchemaUpdate(cfg).Execute(true, true);
		}

		protected void Application_Error(object sender, EventArgs e) {
			// Useful for debugging
			Exception ex = Server.GetLastError();
			var reflectionTypeLoadException = ex as ReflectionTypeLoadException;
		}
	}
}