using Manager8Bia.Models;
using Manager8Bia.Services;
using Manager8Bia.ViewModels;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager8Bia.IoC
{
    class IocConfiguration : NinjectModule
    {
        public override void Load()
        {
            Bind<IDataStore<Category>>().To<ProviderDataStore<Category>>().InSingletonScope(); // Reuse same storage every time
            Bind<IDataStore<DayHistory>>().To<ProviderDataStore<DayHistory>>().InSingletonScope();
            Bind<IDataStore<MonthHistory>>().To<ProviderDataStore<MonthHistory>>().InSingletonScope();

            Bind<HomeViewModel>().ToSelf().InTransientScope(); // Create new instance every time
            Bind<MainViewModel>().ToSelf().InTransientScope();
        }
    }
}
