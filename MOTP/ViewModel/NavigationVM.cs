using MOTP.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MOTP.ViewModel
{
    class NavigationVM : ViewModelBase
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

        private void Home(object obj) => CurrentView = new HomeVM();
        private void Himki(object obj) => CurrentView = new HimkiVM();
        private void Marta(object obj) => CurrentView = new MartaVM();
        private void Puhkino(object obj) => CurrentView = new PuhkinoVM();
        private void Privolnay(object obj) => CurrentView = new PrivolnayVM();
        private void Vehki(object obj) => CurrentView = new VehkiVM();
        private void Rybinovay(object obj) => CurrentView = new RybinovayVM();
        private void Sharapovo(object obj) => CurrentView = new SharapovoVM();
        private void Helkovskay(object obj) => CurrentView = new HelkovskayVM();
        private void Odincovo(object obj) => CurrentView = new OdincovoVM();
        private void Skladohnay(object obj) => CurrentView = new SkladohnayVM();
        private void Pererva(object obj) => CurrentView = new PerervaVM();
        private void BUhunskay(object obj) => CurrentView = new BUhunskayVM();
        private void Egorevsk(object obj) => CurrentView = new EgorevskVM();

        public NavigationVM()
        {
            HomeCommand = new RelayCommand(Home);
            HimkiCommand = new RelayCommand(Himki);
            MartaCommand = new RelayCommand(Marta);
            PuhkinoCommand = new RelayCommand(Puhkino);
            PrivolnayCommand = new RelayCommand(Privolnay);
            VehkiCommand = new RelayCommand(Vehki);
            RybinovayCommand = new RelayCommand(Rybinovay);
            SharapovoCommand = new RelayCommand(Sharapovo);
            HelkovskayCommand = new RelayCommand(Helkovskay);
            OdincovoCommand = new RelayCommand(Odincovo);
            SkladohnayCommand = new RelayCommand(Skladohnay);
            PerervaCommand = new RelayCommand(Pererva);
            BUhunskayCommand = new RelayCommand(BUhunskay);
            EgorevskCommand = new RelayCommand(Egorevsk);

            // Startup Page
            CurrentView = new HomeVM();
        }
    }
}
