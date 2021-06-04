using Checkers.Game.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace Checkers.Game
{
    public class BoardModule : IModule
    {
        private readonly IRegionManager _regionManager;

        public BoardModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }
        public void OnInitialized(IContainerProvider containerProvider)
        {
            //_regionManager.RegisterViewWithRegion("ContentRegion", typeof(GameView));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }
    }
}