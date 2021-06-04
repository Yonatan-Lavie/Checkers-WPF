using Checkers.Winner.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace Checkers.Winner
{
    public class WinnerModule : IModule
    {
        private readonly IRegionManager _regionManager;

        public WinnerModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }
        public void OnInitialized(IContainerProvider containerProvider)
        {
            //_regionManager.RegisterViewWithRegion("ContentRegion", typeof(WinnerView));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }
    }
}