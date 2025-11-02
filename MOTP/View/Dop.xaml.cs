using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MOTP.View
{
    public partial class Dop : Window
    {
        private Home _home;
        private List<string> _listPal;
        private List<string> _listGM;
        private List<string> _listMesh;
        private List<string> _listCont;
        private List<string> _listSave;
        private List<string> _listZas;
        private int _numstation;
        public Dop(Home home, List<string> listPal, List<string> listGM, List<string> listMesh, List<string> listCont, List<string> listSave, List<string> listZas, int numstation)
        {
            InitializeComponent();
            _home = home;
            _listPal = listPal;
            _listGM = listGM;
            _listMesh = listMesh;
            _listCont = listCont;
            _listSave = listSave;
            _listZas = listZas;
            _numstation = numstation;
        }

        private void ListSave_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (string item in _listSave)
                ListSave.Items.Add(item);
            _home.ReloadDTInfo(_listSave, TBO_Save, false);
        }

        private void ListZas_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (string item in _listZas)
                ListZas.Items.Add(item);
            _home.ReloadDTInfo(_listZas, TBO_Zas, false);
        }

        private void ListSave_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _home.ListSelectionWeb(ListSave, TBO_Save, _listSave, false, _numstation);
        }

        private void ListZas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _home.ListSelectionWeb(ListZas, TBO_Zas, _listZas, false, _numstation);
        }

        private void BTN_ClrSave_Click(object sender, RoutedEventArgs e)
        {
            _home.ClrList(ListSave, _listSave);
        }

        private void BTN_ClrZas_Click(object sender, RoutedEventArgs e)
        {
            _home.ClrList(ListZas, _listZas);
        }
    }
}
