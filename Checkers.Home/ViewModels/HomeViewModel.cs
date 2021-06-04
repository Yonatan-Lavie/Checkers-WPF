using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.Home.ViewModels
{
    public class HomeViewModel : BindableBase
    {
        private string _message;
        private string _whitePlayer;
        private string _blackPlayer;
        private readonly IEventAggregator _eventAggregator;
        private readonly IRegionManager _regionManger;

        public DelegateCommand StartGameCommand { get; set; }
        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }
        public string WhitePlayer
        {
            get { return _whitePlayer; }
            set 
            { 
                SetProperty(ref _whitePlayer, value);
                StartGameCommand.RaiseCanExecuteChanged();
            }
        }
        public string BlackPlayer
        {
            get { return _blackPlayer; }
            set 
            { 
                SetProperty(ref _blackPlayer, value);
                StartGameCommand.RaiseCanExecuteChanged();
            }
        }

        public HomeViewModel(IEventAggregator eventAggregator, IRegionManager regionManger)
        {
            Message = "Hello from Home View";
            _eventAggregator = eventAggregator;
            _regionManger = regionManger;
            StartGameCommand = new DelegateCommand(StartGame, CanStartGame);
        }

        private bool CanStartGame()
        {
            return (WhitePlayer != null && BlackPlayer != null);
        }

        private void StartGame()
        {
            Message = "Start Game";
            var Players = new NavigationParameters();
            Players.Add("BlackPlayer", BlackPlayer);
            Players.Add("WhitePlayer", WhitePlayer);
            _regionManger.RequestNavigate("ContentRegion", "GameView", Players);
        }
    }
}
