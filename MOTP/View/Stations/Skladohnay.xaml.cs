using DocumentFormat.OpenXml.Office2010.ExcelAc;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;

namespace MOTP.View.Stations
{
    public partial class Skladohnay : UserControl
    {
        readonly Home _home = new Home();

    public Skladohnay()
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
            _home.AddProd(Stat.Skladohnay._listPal, Stat.Skladohnay._listGM, Stat.Skladohnay._listMesh, Stat.Skladohnay._listCont, Stat.Skladohnay._listSave, Stat.Skladohnay._listZas,
                                      Stat.Skladohnay._listPal, TBNacl, TBPlmb, "паллет", "", ListPal);

            _home.ReloadList(ListPal, Stat.Skladohnay._listPal);
            _home.ReloadList(ListGM, Stat.Skladohnay._listGM);
            _home.ReloadList(ListMesh, Stat.Skladohnay._listMesh);
            _home.ReloadList(ListCont, Stat.Skladohnay._listCont);

            _home.ReloadDTInfo(Stat.Skladohnay._listPal, TBO_Pal, false);
            _home.ReloadDTInfo(Stat.Skladohnay._listGM, TBO_GM, false);
            _home.ReloadDTInfo(Stat.Skladohnay._listMesh, TBO_Mesh, true);
            _home.ReloadDTInfo(Stat.Skladohnay._listCont, TBO_Cont, true);
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
                    _home.AddProd(Stat.Skladohnay._listPal, Stat.Skladohnay._listGM, Stat.Skladohnay._listMesh, Stat.Skladohnay._listCont, Stat.Skladohnay._listSave, Stat.Skladohnay._listZas,
                                      Stat.Skladohnay._listPal, TBNacl, TBPlmb, "паллет", "", ListPal);
                    _home.ReloadList(ListPal, Stat.Skladohnay._listPal);
                    _home.ReloadDTInfo(Stat.Skladohnay._listPal, TBO_Pal, false);
                    break;
                case 1:
                    _home.AddProd(Stat.Skladohnay._listPal, Stat.Skladohnay._listGM, Stat.Skladohnay._listMesh, Stat.Skladohnay._listCont, Stat.Skladohnay._listSave, Stat.Skladohnay._listZas,
                                      Stat.Skladohnay._listGM, TBNacl, TBPlmb, "гм", "1", ListGM);
                    _home.ReloadList(ListGM, Stat.Skladohnay._listGM);
                    _home.ReloadDTInfo(Stat.Skladohnay._listGM, TBO_GM, false);
                    break;

                case 4:
                    _home.AddProd(Stat.Skladohnay._listPal, Stat.Skladohnay._listGM, Stat.Skladohnay._listMesh, Stat.Skladohnay._listCont, Stat.Skladohnay._listSave, Stat.Skladohnay._listZas,
                                      Stat.Skladohnay._listSave, TBNacl, TBPlmb, "сейфпакет");
                    break;
                case 5:
                    _home.AddProd(Stat.Skladohnay._listPal, Stat.Skladohnay._listGM, Stat.Skladohnay._listMesh, Stat.Skladohnay._listCont, Stat.Skladohnay._listSave, Stat.Skladohnay._listZas,
                                      Stat.Skladohnay._listZas, TBNacl, TBPlmb, "гм", "1");
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
                    _home.AddProd(Stat.Skladohnay._listPal, Stat.Skladohnay._listGM, Stat.Skladohnay._listMesh, Stat.Skladohnay._listCont, Stat.Skladohnay._listSave, Stat.Skladohnay._listZas,
                                      Stat.Skladohnay._listMesh, TBNacl, TBPlmb, "мешок", "", ListMesh);
                    _home.ReloadList(ListMesh, Stat.Skladohnay._listMesh);
                    _home.ReloadDTInfo(Stat.Skladohnay._listMesh, TBO_Mesh, true);
                    break;
                case 3:
                    _home.AddProd(Stat.Skladohnay._listPal, Stat.Skladohnay._listGM, Stat.Skladohnay._listMesh, Stat.Skladohnay._listCont, Stat.Skladohnay._listSave, Stat.Skladohnay._listZas,
                                      Stat.Skladohnay._listCont, TBNacl, TBPlmb, "контейнер", "", ListCont);
                    _home.ReloadList(ListCont, Stat.Skladohnay._listCont);
                    _home.ReloadDTInfo(Stat.Skladohnay._listCont, TBO_Cont, true);
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
    _home.ListSelectionWeb(ListPal, TBO_Pal, Stat.Skladohnay._listPal, false, Stat.Skladohnay.numstation);
}

private void ListGM_SelectionChanged(object sender, SelectionChangedEventArgs e)
{
    _home.ListSelectionWeb(ListGM, TBO_GM, Stat.Skladohnay._listGM, false, Stat.Skladohnay.numstation);
}

private void ListMesh_SelectionChanged(object sender, SelectionChangedEventArgs e)
{
    _home.ListSelectionWeb(ListMesh, TBO_Mesh, Stat.Skladohnay._listMesh, true, Stat.Skladohnay.numstation);
}

private void ListCont_SelectionChanged(object sender, SelectionChangedEventArgs e)
{
    _home.ListSelectionWeb(ListCont, TBO_Cont, Stat.Skladohnay._listCont, true, Stat.Skladohnay.numstation);
}

public void BTN_Otch_Click(object sender, RoutedEventArgs e)
{
    if (Properties.Settings.Default.crntpassword != Properties.Settings.Default.loadpassword)
        return;

    _home.FormOtch(Stat.Skladohnay._listPal, Stat.Skladohnay._listGM, Stat.Skladohnay._listMesh, Stat.Skladohnay._listCont, Stat.Skladohnay._listSave, Stat.Skladohnay._listZas,
                   TB_autoplomb, RTBoooinn, TB_FIO, TB_March, TB_Phone, RTBdt, TB_Auto1, TB_Auto2, Stat.Skladohnay.sdach, Stat.Skladohnay.poluch);
}

private void ListPal_Loaded(object sender, RoutedEventArgs e)
{
    _home.ReloadList(ListPal, Stat.Skladohnay._listPal);
    _home.ReloadDTInfo(Stat.Skladohnay._listPal, TBO_Pal, false);
}

private void ListGM_Loaded(object sender, RoutedEventArgs e)
{
    _home.ReloadList(ListGM, Stat.Skladohnay._listGM);
    _home.ReloadDTInfo(Stat.Skladohnay._listGM, TBO_GM, false);
}

private void ListMesh_Loaded(object sender, RoutedEventArgs e)
{
    _home.ReloadList(ListMesh, Stat.Skladohnay._listMesh);
    _home.ReloadDTInfo(Stat.Skladohnay._listMesh, TBO_Mesh, true);
}

private void ListCont_Loaded(object sender, RoutedEventArgs e)
{
    _home.ReloadList(ListCont, Stat.Skladohnay._listCont);
    _home.ReloadDTInfo(Stat.Skladohnay._listCont, TBO_Cont, true);
}


private void RTBoooinn_Loaded(object sender, RoutedEventArgs e)
{
    FlowDocument document = new FlowDocument();
    Paragraph paragraph = new Paragraph();
    paragraph.Inlines.Add(new Bold(new Run(Stat.Skladohnay.oooinn)));
    document.Blocks.Add(paragraph);
    RTBoooinn.Document = document;
}

private void TB_FIO_Loaded(object sender, RoutedEventArgs e)
{
    TB_FIO.Text = Stat.Skladohnay.fio;
}

private void RTBdt_Loaded(object sender, RoutedEventArgs e)
{
    FlowDocument document = new FlowDocument();
    Paragraph paragraph = new Paragraph();
    paragraph.Inlines.Add(new Bold(new Run(Stat.Skladohnay.dt)));
    document.Blocks.Add(paragraph);
    RTBdt.Document = document;
}

private void TB_Auto1_Loaded(object sender, RoutedEventArgs e)
{
    TB_Auto1.Text = Stat.Skladohnay.auto1;
}

private void TB_Auto2_Loaded(object sender, RoutedEventArgs e)
{
    TB_Auto2.Text = Stat.Skladohnay.auto2;
}

private void BTN_ClrPal_Click(object sender, RoutedEventArgs e)
{
    _home.ClrList(ListPal, Stat.Skladohnay._listPal);
    _home.ReloadDTInfo(Stat.Skladohnay._listPal, TBO_Pal, false);
}

private void BTN_ClrGM_Click(object sender, RoutedEventArgs e)
{
    _home.ClrList(ListGM, Stat.Skladohnay._listGM);
    _home.ReloadDTInfo(Stat.Skladohnay._listGM, TBO_GM, false);
}

private void BTN_ClrMesh_Click(object sender, RoutedEventArgs e)
{
    _home.ClrList(ListMesh, Stat.Skladohnay._listMesh);
    _home.ReloadDTInfo(Stat.Skladohnay._listMesh, TBO_Mesh, true);
}

private void BTN_ClrCont_Click(object sender, RoutedEventArgs e)
{
    _home.ClrList(ListCont, Stat.Skladohnay._listCont);
    _home.ReloadDTInfo(Stat.Skladohnay._listCont, TBO_Cont, true);
}

private void RTBoooinn_LostFocus(object sender, RoutedEventArgs e)
{
    Stat.Skladohnay.oooinn = new TextRange(RTBoooinn.Document.ContentStart, RTBoooinn.Document.ContentEnd).Text;
}

private void TB_FIO_LostFocus(object sender, RoutedEventArgs e)
{
    Stat.Skladohnay.fio = TB_FIO.Text;
}

private void RTBdt_LostFocus(object sender, RoutedEventArgs e)
{
    Stat.Skladohnay.dt = new TextRange(RTBdt.Document.ContentStart, RTBdt.Document.ContentEnd).Text;
}

private void TB_Auto1_LostFocus(object sender, RoutedEventArgs e)
{
    Stat.Skladohnay.auto1 = TB_Auto1.Text;
}

private void TB_Auto2_LostFocus(object sender, RoutedEventArgs e)
{
    Stat.Skladohnay.auto2 = TB_Auto2.Text;
}

private void BTN_DopLists_Click(object sender, RoutedEventArgs e)
{
    _home.OpenDop(Stat.Skladohnay._listSave, Stat.Skladohnay._listZas, Stat.Skladohnay.numstation);
}

private void TB_March_Loaded(object sender, RoutedEventArgs e)
{
    TB_March.Text = Stat.Skladohnay.march;
}

private void TB_March_LostFocus(object sender, RoutedEventArgs e)
{
    Stat.Skladohnay.march = TB_March.Text;
}

private void BTN_Sett_Click(object sender, RoutedEventArgs e)
{
    _home.OpenSett(Stat.Skladohnay.numstation);
}

private void BTN_Form_Click(object sender, RoutedEventArgs e)
{
    if (Properties.Settings.Default.crntpassword != Properties.Settings.Default.loadpassword)
        return;

    _home.OpenComp(Stat.Skladohnay._listPal, Stat.Skladohnay._listGM, Stat.Skladohnay._listMesh, Stat.Skladohnay._listCont, Stat.Skladohnay._listSave, Stat.Skladohnay._listZas, TB_autoplomb, RTBoooinn, TB_FIO, TB_March, TB_Phone, RTBdt, TB_Auto1, TB_Auto2, Stat.Skladohnay.sdach, Stat.Skladohnay.poluch, Stat.Skladohnay.numstation);
}

private void TB_Phone_Loaded(object sender, RoutedEventArgs e)
{
    TB_Phone.Text = Stat.Skladohnay.phone;
}

private void TB_Phone_LostFocus(object sender, RoutedEventArgs e)
{
    Stat.Skladohnay.phone = TB_Phone.Text;
}

private void TB_autoplomb_Loaded(object sender, RoutedEventArgs e)
{
    TB_autoplomb.Text = Stat.Skladohnay.autoplomb;
}

private void TB_autoplomb_LostFocus(object sender, RoutedEventArgs e)
{
    Stat.Skladohnay.autoplomb = TB_autoplomb.Text;
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
    _home.ReloadList(ListPal, Stat.Skladohnay._listPal);
    _home.ReloadList(ListGM, Stat.Skladohnay._listGM);
    _home.ReloadList(ListMesh, Stat.Skladohnay._listMesh);
    _home.ReloadList(ListCont, Stat.Skladohnay._listCont);
}

private void TBO_Pal_Loaded(object sender, RoutedEventArgs e)
{
    _home.ReloadDTInfo(Stat.Skladohnay._listPal, TBO_Pal, false);
}
private void TBO_GM_Loaded(object sender, RoutedEventArgs e)
{
    _home.ReloadDTInfo(Stat.Skladohnay._listGM, TBO_GM, false);
}
private void TBO_Mesh_Loaded(object sender, RoutedEventArgs e)
{
    _home.ReloadDTInfo(Stat.Skladohnay._listMesh, TBO_Mesh, true);
}

private void TBO_Cont_Loaded(object sender, RoutedEventArgs e)
{
    _home.ReloadDTInfo(Stat.Skladohnay._listCont, TBO_Cont, true);
}

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            new MainWindow().ActiveCheck(DateTime.Now);
        }
    }
}
