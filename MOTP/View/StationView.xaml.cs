using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MOTP.ViewModel;

namespace MOTP.View
{
    public partial class StationView : UserControl
    {
        public StationView()
        {
            InitializeComponent();
        }

        #region KeyDown обработчики

        private void TBNacl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && DataContext is StationViewModel vm)
                vm.AddEntryFromKeyboard();
        }

        private void TBPlmb_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && DataContext is StationViewModel vm)
                vm.AddPlombFromKeyboard();
        }

        #endregion

        #region Очистка списков

        private void BTN_ClrPal_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is StationViewModel vm)
            {
                vm.PalList.Clear();
                vm.PersistToStation();
            }
        }

        private void BTN_ClrGM_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is StationViewModel vm)
            {
                vm.GMList.Clear();
                vm.PersistToStation();
            }
        }

        private void BTN_ClrMesh_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is StationViewModel vm)
            {
                vm.MeshList.Clear();
                vm.PersistToStation();
            }
        }

        private void BTN_ClrCont_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is StationViewModel vm)
            {
                vm.ContList.Clear();
                vm.PersistToStation();
            }
        }

        #endregion

        #region Дополнительные кнопки

        private void BTN_DopLists_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is StationViewModel vm)
            {
                MessageBox.Show("Открыть дополнительные списки");
            }
        }

        private void BTN_ImpDt_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is StationViewModel vm)
            {
                MessageBox.Show("Импорт данных");
            }
        }

        #endregion
    }
}
