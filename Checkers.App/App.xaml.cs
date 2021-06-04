using Checkers.App.Views;
using Checkers.Game;
using Checkers.Game.Views;
using Checkers.Home;
using Checkers.Home.Views;
using Checkers.Winner;
using Checkers.Winner.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using System.Windows;

namespace Checkers.App
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        private readonly IRegionManager regionManager = new RegionManager();

        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<GameView>();
            containerRegistry.RegisterForNavigation<HomeView>();
            containerRegistry.RegisterForNavigation<WinnerView>();
        }
        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            //moduleCatalog.AddModule<HomeModule>();
            //moduleCatalog.AddModule<GameModule>();
            //moduleCatalog.AddModule<WinnerModule>();
        }
        protected override void OnInitialized()
        {
            base.OnInitialized();
            regionManager.RegisterViewWithRegion("ContentRegion", typeof(HomeView));
        }

    }
}
