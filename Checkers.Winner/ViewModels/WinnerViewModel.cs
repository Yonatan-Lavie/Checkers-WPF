using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.Winner.ViewModels
{
    public class WinnerViewModel : BindableBase
    {
        private string _message;
        private readonly IEventAggregator _eventAggregator;

        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        public WinnerViewModel(IEventAggregator eventAggregator)
        {
            Message = "Hello from Winner View";
            _eventAggregator = eventAggregator;
        }
    }
}
