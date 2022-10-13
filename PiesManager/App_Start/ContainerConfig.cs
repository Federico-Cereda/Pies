using Autofac;
using Autofac.Integration.WebApi;
using PiesManager.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Data.Entity;
using System.Data.Odbc;
using System.Reflection;
using PiesManager.Models;

namespace PiesManager.App_Start
{
    public static class ContainerConfig
    {
        public static void RegisterContainer(HttpConfiguration httpConfiguration)
        {
            var builder = new ContainerBuilder();

            builder.RegisterApiControllers(typeof(WebApiApplication).Assembly);
            builder.RegisterType<PieRepository>()
                   .As<IPieRepository>()
                   .InstancePerRequest();
            builder.RegisterType<PiesDbContext>().InstancePerRequest();

            var container = builder.Build();
            httpConfiguration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}