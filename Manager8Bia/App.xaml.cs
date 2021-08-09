
using Autofac;
using Manager8Bia.IoC;
using Manager8Bia.Models;
using Manager8Bia.Services;
using Manager8Bia.Views;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;

namespace Manager8Bia
{

    public partial class App : Application
    {
        public IServiceProvider ServiceProvider { get; private set; }

        public IConfiguration Configuration { get; private set; }
        protected override void OnStartup(StartupEventArgs e)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<ProviderDataStore<Category>>().As<IDataStore<Category>>();
            builder.RegisterType<ProviderDataStore<DayHistory>>().As<IDataStore<DayHistory>>();
            builder.RegisterType<ProviderDataStore<MonthHistory>>().As<IDataStore<MonthHistory>>();
            var container = builder.Build();

            container.Resolve<IDataStore<Category>>();
            container.Resolve<IDataStore<DayHistory>>();
            container.Resolve<IDataStore<MonthHistory>>();
        }
    }
}
