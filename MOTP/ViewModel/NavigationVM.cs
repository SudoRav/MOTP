using MOTP.Utilities;
using System.Windows.Input;

namespace MOTP.ViewModel
{
    public class NavigationVM : ViewModelBase
    {
        private object _currentView;
        public object CurrentView
        {
            get { return _currentView; }
            set { _currentView = value; OnPropertyChanged(); }
        }

        public ICommand HomeCommand { get; set; }
        public ICommand HimkiCommand { get; set; }
        public ICommand MartaCommand { get; set; }
        public ICommand PuhkinoCommand { get; set; }
        public ICommand PrivolnayCommand { get; set; }
        public ICommand VehkiCommand { get; set; }
        public ICommand RybinovayCommand { get; set; }
        public ICommand SharapovoCommand { get; set; }
        public ICommand HelkovskayCommand { get; set; }
        public ICommand OdincovoCommand { get; set; }
        public ICommand SkladohnayCommand { get; set; }
        public ICommand PerervaCommand { get; set; }
        public ICommand BUhunskayCommand { get; set; }
        public ICommand EgorevskCommand { get; set; }

        public NavigationVM()
        {
            HomeCommand = new RelayCommand(_ => GoHome());
            HimkiCommand = new RelayCommand(_ => NavigateToHimki());
            MartaCommand = new RelayCommand(_ => NavigateToMarta());
            PuhkinoCommand = new RelayCommand(_ => NavigateToPuhkino());
            PrivolnayCommand = new RelayCommand(_ => NavigateToPrivolnay());
            VehkiCommand = new RelayCommand(_ => NavigateToVehki());
            RybinovayCommand = new RelayCommand(_ => NavigateToRybinovay());
            SharapovoCommand = new RelayCommand(_ => NavigateToSharapovo());
            HelkovskayCommand = new RelayCommand(_ => NavigateToHelkovskay());
            OdincovoCommand = new RelayCommand(_ => NavigateToOdincovo());
            SkladohnayCommand = new RelayCommand(_ => NavigateToSkladohnay());
            PerervaCommand = new RelayCommand(_ => NavigateToPererva());
            BUhunskayCommand = new RelayCommand(_ => NavigateToBUhunskay());
            EgorevskCommand = new RelayCommand(_ => NavigateToEgorevsk());

            // Startup Page
            CurrentView = new HomeVM();
        }

        private void GoHome() => CurrentView = new HomeVM();

        private void NavigateToHimki() => CurrentView = new StationViewModel(Stat.Himki.Data);
        private void NavigateToMarta() => CurrentView = new StationViewModel(Stat.Marta.Data);
        private void NavigateToPuhkino() => CurrentView = new StationViewModel(Stat.Puhkino.Data);
        private void NavigateToPrivolnay() => CurrentView = new StationViewModel(Stat.Privolnay.Data);
        private void NavigateToVehki() => CurrentView = new StationViewModel(Stat.Vehki.Data);
        private void NavigateToRybinovay() => CurrentView = new StationViewModel(Stat.Rybinovay.Data);
        private void NavigateToSharapovo() => CurrentView = new StationViewModel(Stat.Sharapovo.Data);
        private void NavigateToHelkovskay() => CurrentView = new StationViewModel(Stat.Helkovskay.Data);
        private void NavigateToOdincovo() => CurrentView = new StationViewModel(Stat.Odincovo.Data);
        private void NavigateToSkladohnay() => CurrentView = new StationViewModel(Stat.Skladohnay.Data);
        private void NavigateToPererva() => CurrentView = new StationViewModel(Stat.Pererva.Data);
        private void NavigateToBUhunskay() => CurrentView = new StationViewModel(Stat.BUhunskay.Data);
        private void NavigateToEgorevsk() => CurrentView = new StationViewModel(Stat.Egorevsk.Data);
    }
}
