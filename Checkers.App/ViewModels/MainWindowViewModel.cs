using Prism.Mvvm;

namespace Checkers.App.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "Checkers Application";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public MainWindowViewModel()
        {

        }
    }
}
