using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;

namespace MOTP.View.Stations
{
    public partial class Himki : UserControl
    {
        readonly Home _home = new Home();

        public Himki()
        {
            InitializeComponent();

            Stat.Settings.AddRem = true;
        }

        private void TBNacl_Loaded(object sender, RoutedEventArgs e)
        {
            TBNacl.Focus();
        }

        private void CBType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _home.SetCB(CBType, TBNacl, TBPlmb);
        }

        private void TBNacl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (Properties.Settings.Default.autoCyrillify)
                    TBNacl.Text = _home.Cyrillify(TBNacl.Text.Trim());

                if (_home.SetType(e, TBNacl.Text, CBType))
                    return;

                if (!Stat.Settings.AddRem)
                {
                    _home.AddProd(Stat.Himki._listPal, Stat.Himki._listGM, Stat.Himki._listMesh, Stat.Himki._listCont, Stat.Himki._listSave, Stat.Himki._listZas,
                                              Stat.Himki._listPal, TBNacl, TBPlmb, "паллет", "", ListPal);

                    _home.ReloadList(ListPal, Stat.Himki._listPal);
                    _home.ReloadList(ListGM, Stat.Himki._listGM);
                    _home.ReloadList(ListMesh, Stat.Himki._listMesh);
                    _home.ReloadList(ListCont, Stat.Himki._listCont);

                    _home.ReloadDTInfo(Stat.Himki._listPal, TBO_Pal, false);
                    _home.ReloadDTInfo(Stat.Himki._listGM, TBO_GM, false);
                    _home.ReloadDTInfo(Stat.Himki._listMesh, TBO_Mesh, true);
                    _home.ReloadDTInfo(Stat.Himki._listCont, TBO_Cont, true);
                    return;
                }

                if (!_home.FormStr(TBNacl, CBType.SelectedIndex, false))
                {
                    System.Media.SystemSounds.Hand.Play();
                    _home.ClearEnter(TBNacl, TBPlmb);
                    return;
                }

                if (CBType.SelectedIndex == 0 || CBType.SelectedIndex == 1 || CBType.SelectedIndex == 4 || CBType.SelectedIndex == 5)
                {
                    switch (CBType.SelectedIndex)
                    {
                        case 0:
                            _home.AddProd(Stat.Himki._listPal, Stat.Himki._listGM, Stat.Himki._listMesh, Stat.Himki._listCont, Stat.Himki._listSave, Stat.Himki._listZas,
                                              Stat.Himki._listPal, TBNacl, TBPlmb, "паллет", "", ListPal);
                            _home.ReloadList(ListPal, Stat.Himki._listPal);
                            _home.ReloadDTInfo(Stat.Himki._listPal, TBO_Pal, false);
                            break;
                        case 1:
                            _home.AddProd(Stat.Himki._listPal, Stat.Himki._listGM, Stat.Himki._listMesh, Stat.Himki._listCont, Stat.Himki._listSave, Stat.Himki._listZas,
                                              Stat.Himki._listGM, TBNacl, TBPlmb, "гм", "1", ListGM);
                            _home.ReloadList(ListGM, Stat.Himki._listGM);
                            _home.ReloadDTInfo(Stat.Himki._listGM, TBO_GM, false);
                            break;

                        case 4:
                            _home.AddProd(Stat.Himki._listPal, Stat.Himki._listGM, Stat.Himki._listMesh, Stat.Himki._listCont, Stat.Himki._listSave, Stat.Himki._listZas,
                                              Stat.Himki._listSave, TBNacl, TBPlmb, "сейфпакет");
                            break;
                        case 5:
                            _home.AddProd(Stat.Himki._listPal, Stat.Himki._listGM, Stat.Himki._listMesh, Stat.Himki._listCont, Stat.Himki._listSave, Stat.Himki._listZas,
                                              Stat.Himki._listZas, TBNacl, TBPlmb, "гм", "1");
                            break;
                    }
                }

                TBPlmb.Focus();
            }
        }

        private void TBPlmb_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (Properties.Settings.Default.autoCyrillify)
                    TBPlmb.Text = _home.Cyrillify(TBPlmb.Text.Trim());

                if (_home.SetType(e, TBPlmb.Text, CBType))
                    return;

                if (!_home.FormStr(TBPlmb, CBType.SelectedIndex, true))
                {
                    System.Media.SystemSounds.Hand.Play();
                    _home.ClearEnter(TBNacl, TBPlmb);
                    return;
                }

                if (CBType.SelectedIndex == 2 || CBType.SelectedIndex == 3)
                {
                    switch (CBType.SelectedIndex)
                    {
                        case 2:
                            _home.AddProd(Stat.Himki._listPal, Stat.Himki._listGM, Stat.Himki._listMesh, Stat.Himki._listCont, Stat.Himki._listSave, Stat.Himki._listZas,
                                              Stat.Himki._listMesh, TBNacl, TBPlmb, "мешок", "", ListMesh);
                            _home.ReloadList(ListMesh, Stat.Himki._listMesh);
                            _home.ReloadDTInfo(Stat.Himki._listMesh, TBO_Mesh, true);
                            break;
                        case 3:
                            _home.AddProd(Stat.Himki._listPal, Stat.Himki._listGM, Stat.Himki._listMesh, Stat.Himki._listCont, Stat.Himki._listSave, Stat.Himki._listZas,
                                              Stat.Himki._listCont, TBNacl, TBPlmb, "контейнер", "", ListCont);
                            _home.ReloadList(ListCont, Stat.Himki._listCont);
                            _home.ReloadDTInfo(Stat.Himki._listCont, TBO_Cont, true);
                            break;
                    }
                }
            }

        }

        private void RTBoooinn_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            RTBoooinn.SelectAll();
        }

        private void RTBdt_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            RTBdt.SelectAll();
        }
        private void TB_Auto1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            TB_Auto1.SelectAll();
        }

        private void TB_Auto2_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            TB_Auto2.SelectAll();
        }

        private void ListPal_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _home.ListSelectionWeb(ListPal, TBO_Pal, Stat.Himki._listPal, false, Stat.Himki.numstation);
        }

        private void ListGM_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _home.ListSelectionWeb(ListGM, TBO_GM, Stat.Himki._listGM, false, Stat.Himki.numstation);
        }

        private void ListMesh_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _home.ListSelectionWeb(ListMesh, TBO_Mesh, Stat.Himki._listMesh, true, Stat.Himki.numstation);
        }

        private void ListCont_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _home.ListSelectionWeb(ListCont, TBO_Cont, Stat.Himki._listCont, true, Stat.Himki.numstation);
        }

        public void BTN_Otch_Click(object sender, RoutedEventArgs e)
        {
            if (Properties.Settings.Default.crntpassword != Properties.Settings.Default.loadpassword)
                return;

            _home.FormOtch(Stat.Himki._listPal, Stat.Himki._listGM, Stat.Himki._listMesh, Stat.Himki._listCont, Stat.Himki._listSave, Stat.Himki._listZas,
                           TB_autoplomb, RTBoooinn, TB_FIO, TB_March, TB_Phone, RTBdt, TB_Auto1, TB_Auto2, Stat.Himki.sdach, Stat.Himki.poluch);
        }

        private void ListPal_Loaded(object sender, RoutedEventArgs e)
        {
            _home.ReloadList(ListPal, Stat.Himki._listPal);
            _home.ReloadDTInfo(Stat.Himki._listPal, TBO_Pal, false);
        }

        private void ListGM_Loaded(object sender, RoutedEventArgs e)
        {
            _home.ReloadList(ListGM, Stat.Himki._listGM);
            _home.ReloadDTInfo(Stat.Himki._listGM, TBO_GM, false);
        }

        private void ListMesh_Loaded(object sender, RoutedEventArgs e)
        {
            _home.ReloadList(ListMesh, Stat.Himki._listMesh);
            _home.ReloadDTInfo(Stat.Himki._listMesh, TBO_Mesh, true);
        }

        private void ListCont_Loaded(object sender, RoutedEventArgs e)
        {
            _home.ReloadList(ListCont, Stat.Himki._listCont);
            _home.ReloadDTInfo(Stat.Himki._listCont, TBO_Cont, true);
        }


        private void RTBoooinn_Loaded(object sender, RoutedEventArgs e)
        {
            FlowDocument document = new FlowDocument();
            Paragraph paragraph = new Paragraph();
            paragraph.Inlines.Add(new Bold(new Run(Stat.Himki.oooinn)));
            document.Blocks.Add(paragraph);
            RTBoooinn.Document = document;
        }

        private void TB_FIO_Loaded(object sender, RoutedEventArgs e)
        {
            TB_FIO.Text = Stat.Himki.fio;
        }

        private void RTBdt_Loaded(object sender, RoutedEventArgs e)
        {
            FlowDocument document = new FlowDocument();
            Paragraph paragraph = new Paragraph();
            paragraph.Inlines.Add(new Bold(new Run(Stat.Himki.dt)));
            document.Blocks.Add(paragraph);
            RTBdt.Document = document;
        }

        private void TB_Auto1_Loaded(object sender, RoutedEventArgs e)
        {
            TB_Auto1.Text = Stat.Himki.auto1;
        }

        private void TB_Auto2_Loaded(object sender, RoutedEventArgs e)
        {
            TB_Auto2.Text = Stat.Himki.auto2;
        }

        private void BTN_ClrPal_Click(object sender, RoutedEventArgs e)
        {
            _home.ClrList(ListPal, Stat.Himki._listPal);
            _home.ReloadDTInfo(Stat.Himki._listPal, TBO_Pal, false);
        }

        private void BTN_ClrGM_Click(object sender, RoutedEventArgs e)
        {
            _home.ClrList(ListGM, Stat.Himki._listGM);
            _home.ReloadDTInfo(Stat.Himki._listGM, TBO_GM, false);
        }

        private void BTN_ClrMesh_Click(object sender, RoutedEventArgs e)
        {
            _home.ClrList(ListMesh, Stat.Himki._listMesh);
            _home.ReloadDTInfo(Stat.Himki._listMesh, TBO_Mesh, true);
        }

        private void BTN_ClrCont_Click(object sender, RoutedEventArgs e)
        {
            _home.ClrList(ListCont, Stat.Himki._listCont);
            _home.ReloadDTInfo(Stat.Himki._listCont, TBO_Cont, true);
        }

        private void RTBoooinn_LostFocus(object sender, RoutedEventArgs e)
        {
            Stat.Himki.oooinn = new TextRange(RTBoooinn.Document.ContentStart, RTBoooinn.Document.ContentEnd).Text;
        }

        private void TB_FIO_LostFocus(object sender, RoutedEventArgs e)
        {
            Stat.Himki.fio = TB_FIO.Text;
        }

        private void RTBdt_LostFocus(object sender, RoutedEventArgs e)
        {
            Stat.Himki.dt = new TextRange(RTBdt.Document.ContentStart, RTBdt.Document.ContentEnd).Text;
        }

        private void TB_Auto1_LostFocus(object sender, RoutedEventArgs e)
        {
            Stat.Himki.auto1 = TB_Auto1.Text;
        }

        private void TB_Auto2_LostFocus(object sender, RoutedEventArgs e)
        {
            Stat.Himki.auto2 = TB_Auto2.Text;
        }

        private void BTN_DopLists_Click(object sender, RoutedEventArgs e)
        {
            _home.OpenDop(Stat.Himki._listSave, Stat.Himki._listZas, Stat.Himki.numstation);
        }

        private void TB_March_Loaded(object sender, RoutedEventArgs e)
        {
            TB_March.Text = Stat.Himki.march;
        }

        private void TB_March_LostFocus(object sender, RoutedEventArgs e)
        {
            Stat.Himki.march = TB_March.Text;
        }

        private void BTN_Sett_Click(object sender, RoutedEventArgs e)
        {
            _home.OpenSett(Stat.Himki.numstation);
        }

        private void BTN_Form_Click(object sender, RoutedEventArgs e)
        {
            if (Properties.Settings.Default.crntpassword != Properties.Settings.Default.loadpassword)
                return;

            _home.OpenComp(Stat.Himki._listPal, Stat.Himki._listGM, Stat.Himki._listMesh, Stat.Himki._listCont, Stat.Himki._listSave, Stat.Himki._listZas, TB_autoplomb, RTBoooinn, TB_FIO, TB_March, TB_Phone, RTBdt, TB_Auto1, TB_Auto2, Stat.Himki.sdach, Stat.Himki.poluch, Stat.Himki.numstation);
        }

        private void TB_Phone_Loaded(object sender, RoutedEventArgs e)
        {
            TB_Phone.Text = Stat.Himki.phone;
        }

        private void TB_Phone_LostFocus(object sender, RoutedEventArgs e)
        {
            Stat.Himki.phone = TB_Phone.Text;
        }

        private void TB_autoplomb_Loaded(object sender, RoutedEventArgs e)
        {
            TB_autoplomb.Text = Stat.Himki.autoplomb;
        }

        private void TB_autoplomb_LostFocus(object sender, RoutedEventArgs e)
        {
            Stat.Himki.autoplomb = TB_autoplomb.Text;
        }

        private void BTN_Add_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void BTN_Add_Click(object sender, RoutedEventArgs e)
        {
            Stat.Settings.AddRem = true;

            _home.ClearEnter(TBNacl, TBPlmb);
            TBNacl.Focus();

            if (Stat.Settings.AddRem)
            {
                BTN_Add.Opacity = 1;
                BTN_Rem.Opacity = 0.5;
            }
            else
            {
                BTN_Add.Opacity = 0.5;
                BTN_Rem.Opacity = 1;
            }
        }

        private void BTN_Rem_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void BTN_Rem_Click(object sender, RoutedEventArgs e)
        {
            Stat.Settings.AddRem = false;

            _home.ClearEnter(TBNacl, TBPlmb);
            TBNacl.Focus();

            if (Stat.Settings.AddRem)
            {
                BTN_Add.Opacity = 1;
                BTN_Rem.Opacity = 0.5;
            }
            else
            {
                BTN_Add.Opacity = 0.5;
                BTN_Rem.Opacity = 1;
            }
        }

        private void BTN_ImpDt_Click(object sender, RoutedEventArgs e)
        {
            _home.ImportData(Clipboard.GetText(), RTBoooinn, TB_FIO, TB_March, TB_Phone, RTBdt, TB_Auto1, TB_Auto2);
        }

        private void TBO_Pal_ValueChanged(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Текст изменен!");
        }

        private void _Loaded(object sender, RoutedEventArgs e)
        {
            Thread.Sleep(10);
            _home.ReloadList(ListPal, Stat.Himki._listPal);
            _home.ReloadList(ListGM, Stat.Himki._listGM);
            _home.ReloadList(ListMesh, Stat.Himki._listMesh);
            _home.ReloadList(ListCont, Stat.Himki._listCont);
        }

        private void TBO_Pal_Loaded(object sender, RoutedEventArgs e)
        {
            _home.ReloadDTInfo(Stat.Himki._listPal, TBO_Pal, false);
        }
        private void TBO_GM_Loaded(object sender, RoutedEventArgs e)
        {
            _home.ReloadDTInfo(Stat.Himki._listGM, TBO_GM, false);
        }
        private void TBO_Mesh_Loaded(object sender, RoutedEventArgs e)
        {
            _home.ReloadDTInfo(Stat.Himki._listMesh, TBO_Mesh, true);
        }

        private void TBO_Cont_Loaded(object sender, RoutedEventArgs e)
        {
            _home.ReloadDTInfo(Stat.Himki._listCont, TBO_Cont, true);
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            new MainWindow().ActiveCheck(DateTime.Now);
        }
    }
}
