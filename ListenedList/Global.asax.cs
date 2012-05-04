using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using Core.Infrastructure.Logging;
using StructureMap;
using Core.Infrastructure;
using System.Data.SqlClient;
using System.Configuration;

namespace ListenedList
{
    public class Global : System.Web.HttpApplication
    {
        LogWriter writer = new LogWriter();

        void Application_Start( object sender, EventArgs e ) {
            
             log4net.Config.XmlConfigurator.Configure();

            OnStart();

            SqlDependency.Start(ConfigurationManager.ConnectionStrings["Listened"].ConnectionString);
        }

        protected virtual void OnStart()
        {
            //Setup Ioc container and related services
            Bootstrap();

            // uncomment the following line if you wish to have StructureMap verify its configuration.  ASP.NET error page will be generated if configuration is incorrect.
            ObjectFactory.AssertConfigurationIsValid();
            System.Diagnostics.Debug.Write(ObjectFactory.WhatDoIHave());
        }

        private void Bootstrap()
        {
            //setup IoC container
            ObjectFactory.Initialize(x =>
            {
                x.AddRegistry(new Core.CoreRegistry());
                x.AddRegistry(new Data.DataRegistry());
                //x.AddRegistry(new PhishMarket.PhishMarketRegistry());
            });

            Ioc.InitializeWith(new DependencyResolverFactory(new DependencyResolver()));
        }

        void Application_End( object sender, EventArgs e ) {
            //  Code that runs on application shutdown
            SqlDependency.Stop(ConfigurationManager.ConnectionStrings["Listened"].ConnectionString);
        }

        void Application_Error( object sender, EventArgs e ) {
            // Code that runs when an unhandled error occurs

        }

        void Session_Start( object sender, EventArgs e ) {
            // Code that runs when a new session is started

        }

        void Session_End( object sender, EventArgs e ) {
            // Code that runs when a session ends. 
            // Note: The Session_End event is raised only when the sessionstate mode
            // is set to InProc in the Web.config file. If session mode is set to StateServer 
            // or SQLServer, the event is not raised.

        }

    }
}
