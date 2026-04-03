using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MOTP.View
{
    public partial class Details : Window
    {
        readonly Home _home;
        private readonly TextBlock _tbo;
        private List<string> _list;
        private bool _useplmb;
        private int _numstation;

        public Details(Home home, TextBlock tbo, List<string> list, bool useplmb, int numstation)
        {
            InitializeComponent();
            _home = home;
            _tbo = tbo;
            _list = list;
            _useplmb = useplmb;

            TB_Info.Text = _home.tmpval;
            TB_Prim.Text = _home.tmpprm;
            if (_home.tmpcnt != "0")
                TB_Count.Text = _home.tmpcnt;
            else TB_Count.Text = "";
            _tbo = tbo;

            _numstation = numstation;

            double screenHeight = SystemParameters.FullPrimaryScreenHeight;
            double screenWidth = SystemParameters.FullPrimaryScreenWidth;
            this.Top = (screenHeight - this.Height - (screenHeight - this.Height) / 4);
            this.Left = (screenWidth - this.Width) / 2;
        }

        private void TBcount_Loaded(object sender, RoutedEventArgs e)
        {
            TB_Count.Focus();
        }

        private void BTSetKol_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _home.tmpstr = TB_Count.Text;

                int i = _home.tmpsnd.SelectedIndex;
                _home.tmpsnd.Items.RemoveAt(i);
                _home.tmpstt.RemoveAt(i);

                _home.tmpsnd.Items.Insert(i, $"{TB_Prim.Text} {TB_Info.Text} {_home.tmpstr}".Trim());
                _home.tmpstt.Insert(i, $"{TB_Prim.Text} {TB_Info.Text} {_home.tmpstr}".Trim());

                _home.tmpsnd.Items.Refresh();
                _home.tmpsnd.SelectedIndex = -1;

                _home.ReloadDTInfo(_list, _tbo, _useplmb);

                Close();
            }
            catch
            {
                Console.WriteLine(e.ToString());
            }
        }

        private void Del_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _home.tmpstr = TB_Count.Text;

                int i = _home.tmpsnd.SelectedIndex;
                _home.tmpsnd.Items.RemoveAt(i);
                _home.tmpstt.RemoveAt(i);

                _home.tmpsnd.Items.Refresh();
                _home.tmpsnd.SelectedIndex = -1;

                Close();
            }
            catch
            {
                Debug.WriteLine(e.ToString());
            }

            Close();
        }

        private void TBcount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                BTSetKol_Click(sender, e);

                Close();
            }
        }

        private void BTweb_Click(object sender, RoutedEventArgs e)
        {
            string[] splstr1 = TB_Info.ToString().Trim().Split(' ', '_');
            string[] splstr2 = TB_Info.ToString().Trim().Split(' ');

            Process.Start(new ProcessStartInfo($"https://{splstr1[1]}.zappstore.pro/pallet/{splstr2[1]}") { UseShellExecute = true });
        }

        private void TB_Count_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            TB_Count.SelectAll();
        }

        private void TB_Info_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            TB_Info.SelectAll();
        }

        private void TB_Prim_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            TB_Prim.SelectAll();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            _home.tmpsnd.Items.Refresh();
            _home.tmpsnd.SelectedIndex = -1;
        }

        private void TB_Prim_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                BTSetKol_Click(sender, e);

                Close();
            }
        }

        private void TB_Info_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                BTSetKol_Click(sender, e);

                Close();
            }
        }

        private void TBo_Com_Loaded(object sender, RoutedEventArgs e)
        {
            //switch (_numstation)
            //{
            //    case 1: TBo_Com.Text = Properties.Settings.Default.ComHimki; break;
            //    case 2: TBo_Com.Text = Properties.Settings.Default.ComMarta; break;
            //    case 3: TBo_Com.Text = Properties.Settings.Default.ComPuhkino; break;
            //    case 4: TBo_Com.Text = Properties.Settings.Default.ComPrivolnay; break;
            //    case 5: TBo_Com.Text = Properties.Settings.Default.ComVehki; break;
            //    case 6: TBo_Com.Text = Properties.Settings.Default.ComRybinovay; break;
            //    case 7: TBo_Com.Text = Properties.Settings.Default.ComSharapovo; break;
            //    case 8: TBo_Com.Text = Properties.Settings.Default.ComHelkovskay; break;
            //    case 9: TBo_Com.Text = Properties.Settings.Default.ComOdincovo; break;
            //    case 10: TBo_Com.Text = Properties.Settings.Default.ComSkladohnay; break;
            //    case 11: TBo_Com.Text = Properties.Settings.Default.ComPererva; break;
            //    case 12: TBo_Com.Text = Properties.Settings.Default.ComBUhunskay; break;
            //    case 13: TBo_Com.Text = Properties.Settings.Default.ComEgorevsk; break;
            //    default: TBo_Com.Text = ""; break;
            //}
        }

        private void TBo_Com_LostFocus(object sender, RoutedEventArgs e)
        {
            //switch (_numstation)
            //{
            //    case 1: Properties.Settings.Default.ComHimki = TBo_Com.Text; break;
            //    case 2: Properties.Settings.Default.ComMarta = TBo_Com.Text; break;
            //    case 3: Properties.Settings.Default.ComPuhkino = TBo_Com.Text; break;
            //    case 4: Properties.Settings.Default.ComPrivolnay = TBo_Com.Text; break;
            //    case 5: Properties.Settings.Default.ComVehki = TBo_Com.Text; break;
            //    case 6: Properties.Settings.Default.ComRybinovay = TBo_Com.Text; break;
            //    case 7: Properties.Settings.Default.ComSharapovo = TBo_Com.Text; break;
            //    case 8: Properties.Settings.Default.ComHelkovskay = TBo_Com.Text; break;
            //    case 9: Properties.Settings.Default.ComOdincovo = TBo_Com.Text; break;
            //    case 10: Properties.Settings.Default.ComSkladohnay = TBo_Com.Text; break;
            //    case 11: Properties.Settings.Default.ComPererva = TBo_Com.Text; break;
            //    case 12: Properties.Settings.Default.ComBUhunskay = TBo_Com.Text; break;
            //    case 13: Properties.Settings.Default.ComEgorevsk = TBo_Com.Text; break;
            //    default: break;
            //}
        }
    }
}
