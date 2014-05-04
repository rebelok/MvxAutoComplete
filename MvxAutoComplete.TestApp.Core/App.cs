using Cirrious.CrossCore;
using Cirrious.CrossCore.IoC;
using MvxAutoComplete.Core.Interfaces;
using MvxAutoComplete.TestApp.Core.Models;

namespace MvxAutoComplete.TestApp.Core
{
    public class App : Cirrious.MvvmCross.ViewModels.MvxApplication
    {
        public override void Initialize()
        {
            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();
            Mvx.LazyConstructAndRegisterSingleton<ISearcher, NYTimesSearcher>();
            Mvx.LazyConstructAndRegisterSingleton<IHistory, History>();
            RegisterAppStart<ViewModels.FirstViewModel>();
        }
    }
}