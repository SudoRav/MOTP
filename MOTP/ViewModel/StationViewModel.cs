using MOTP.Utilities;
using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace MOTP.ViewModel
{
    public class StationViewModel : ViewModelBase
    {
        private string _richReport = string.Empty;
        public Stat.StationData Station { get; }

        public string StationName => Station?.march ?? "Неизвестно";


        public ObservableCollection<string> PalList { get; } = new();
        public ObservableCollection<string> GMList { get; } = new();
        public ObservableCollection<string> MeshList { get; } = new();
        public ObservableCollection<string> ContList { get; } = new();


        private string _entryText;
        public string EntryText { get => _entryText; set { _entryText = value; OnPropertyChanged(); } }


        private string _entryPlomb;
        public string EntryPlomb { get => _entryPlomb; set { _entryPlomb = value; OnPropertyChanged(); } }


        public IList Types { get; } = new[] { "Паллет", "ГМ", "Мешок", "Контейнер", "Сейфпакет", "Засыл" };
        public int SelectedTypeIndex { get; set; }


        public object SelectedListItem { get; set; }


        public string OOOINN { get => Station.oooinn; set { Station.oooinn = value; OnPropertyChanged(); } }
        public string FIO { get => Station.fio; set { Station.fio = value; OnPropertyChanged(); } }
        public string March { get => Station.march; set { Station.march = value; OnPropertyChanged(); } }
        public string Phone { get => Station.phone; set { Station.phone = value; OnPropertyChanged(); } }
        public string DT { get => Station.dt; set { Station.dt = value; OnPropertyChanged(); } }
        public string Auto1 { get => Station.auto1; set { Station.auto1 = value; OnPropertyChanged(); } }
        public string Auto2 { get => Station.auto2; set { Station.auto2 = value; OnPropertyChanged(); } }
        public string Autoplomb { get => Station.autoplomb; set { Station.autoplomb = value; OnPropertyChanged(); } }
        public string Sdach { get => Station.sdach; set { Station.sdach = value; OnPropertyChanged(); } }
        public string Poluch { get => Station.poluch; set { Station.poluch = value; OnPropertyChanged(); } }

        public ICommand AddCommand { get; }
        public ICommand RemoveCommand { get; }
        public ICommand OpenFormCommand { get; }
        public ICommand GenerateReportCommand { get; }
        public ICommand OpenSettingsCommand { get; }

        public StationViewModel(Stat.StationData station)
        {
            Station = station ?? throw new ArgumentNullException(nameof(station));

            if (Station._listPal != null) foreach (var o in Station._listPal) PalList.Add(o);
            if (Station._listGM != null) foreach (var o in Station._listGM) GMList.Add(o);
            if (Station._listMesh != null) foreach (var o in Station._listMesh) MeshList.Add(o);
            if (Station._listCont != null) foreach (var o in Station._listCont) ContList.Add(o);


            AddCommand = new RelayCommand(_ => AddEntry());
            RemoveCommand = new RelayCommand(_ => RemoveSelected());


            //OpenFormCommand = new RelayCommand(_ => FormOtch());
            //GenerateReportCommand = new RelayCommand(_ => FormDoc());
            OpenSettingsCommand = new RelayCommand(_ => OpenSett());
        }

        private void AddEntry()
        {
            if (string.IsNullOrWhiteSpace(EntryText)) return;
            switch (SelectedTypeIndex)
            {
                case 0: PalList.Add(EntryText); break; // паллет
                case 1: GMList.Add(EntryText); break; // гм
                case 2: MeshList.Add(EntryText); break; // мешок
                case 3: ContList.Add(EntryText); break; // контейнер
                case 4: Station._listSave?.Add(EntryText); break; // сейфпакет
                case 5: Station._listZas?.Add(EntryText); break; // гм(зас)
            }

            EntryText = string.Empty;
            EntryPlomb = string.Empty;

            PersistToStation();
        }

        private void RemoveSelected()
        {
            if (SelectedListItem == null) return;
            if (Station._listPal != null) foreach (var s in Station._listPal) PalList.Add(s);
            if (Station._listGM != null) foreach (var s in Station._listGM) GMList.Add(s);
            if (Station._listMesh != null) foreach (var s in Station._listMesh) MeshList.Add(s);
            if (Station._listCont != null) foreach (var s in Station._listCont) ContList.Add(s);

            PersistToStation();
        }


        public void PersistToStation()
        {
            Station._listPal = PalList.ToList();
            Station._listGM = GMList.ToList();
            Station._listMesh = MeshList.ToList();
            Station._listCont = ContList.ToList();
        }

        private void OpenSett() 
        {
            MessageBox.Show("настройки");
        }
        public void AddEntryFromKeyboard()
        {
            if (!string.IsNullOrWhiteSpace(EntryText))
                AddEntry();
        }

        public void AddPlombFromKeyboard()
        {
            if (!string.IsNullOrWhiteSpace(EntryPlomb))
                AddEntry();
        }
    }
}
