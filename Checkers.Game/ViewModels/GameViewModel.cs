using Checkers.Models;
using Checkers.Services;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.Game.ViewModels
{
    public class GameViewModel : BindableBase, INavigationAware
    {

        #region Fileds
        private GameEngineService _gameEngineService;
        private string _message;
        private BoardModel _board;
        #endregion

        #region Properties
        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }
        public BoardModel Board
        {
            get { return _board; }
            set { SetProperty(ref _board, value); }
        }
        #endregion





        #region Commands
        public DelegateCommand GoHomeCommand { get; set; }
        public DelegateCommand<CellModel> SelectCellCommand { get; private set; }
        #endregion

        #region Constructor
        public GameViewModel(IEventAggregator eventAggregator, IRegionManager regionManger, GameEngineService gameEngineService)
        {
            Message = "Hello from Game View";
            _eventAggregator = eventAggregator;
            _regionManger = regionManger;
            _gameEngineService = gameEngineService;
            GoHomeCommand = new DelegateCommand(GoHome);
            SelectCellCommand = new DelegateCommand<CellModel>(SelectCell);
        }

        private void SelectCell(CellModel c)
        {
            _gameEngineService.CellSelected(ref _board, c);

        }
        #endregion

        #region Prism Properties
        private IRegionNavigationJournal _journal;
        private readonly IEventAggregator _eventAggregator;
        private readonly IRegionManager _regionManger;
        #endregion

        #region Prism Navigation
        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            _journal = navigationContext.NavigationService.Journal;

            if (navigationContext.Parameters.ContainsKey("BlackPlayer") && navigationContext.Parameters.ContainsKey("WhitePlayer"))
            {
                string playerOne = navigationContext.Parameters.GetValue<string>("BlackPlayer");
                string playerTwo = navigationContext.Parameters.GetValue<string>("WhitePlayer");
                Board =  _gameEngineService.GameInit(playerOne, playerTwo);
            }
        }
        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }
        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }
        private void GoHome()
        {
            _regionManger.RequestNavigate("ContentRegion", "HomeView");
        }
        #endregion
    }
}
