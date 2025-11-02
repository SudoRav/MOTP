using MOTP.Model;
using MOTP.ViewModel;
using System.Windows;

namespace MOTP.View
{
    public partial class Patterns : Window
    {
        private Home _home;
        public Patterns(Home home)
        {
            InitializeComponent();
            _home = home;

            this.DataContext = new PatternsVM();
        }

        private void TB_TB_PatNacl_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void TB_TB_PatPlmb_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            BTN_RemNac.Focus();
        }

        private void BTN_RemNac_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                PatModManager.RemPatNacs(PatsNac.SelectedIndex);
            }
            catch { }
        }

        private void BTN_RemPlb_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                PatModManager.RemPatPlbs(PatsPlb.SelectedIndex);
            }
            catch { }
        }
    }
}
