using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;

namespace MOTP.View
{
    public partial class Comp : Window
    {
        private Home _home;
        private List<string> _listPal;
        private List<string> _listGM;
        private List<string> _listMesh;
        private List<string> _listCont;
        private List<string> _listSave;
        private List<string> _listZas;
        private TextBox _TB_Autoplomb;
        private RichTextBox _RTBoooinn;
        private TextBox _TB_FIO;
        private TextBox _TB_March;
        private TextBox _TB_Phone;
        private RichTextBox _RTBdt;
        private TextBox _TB_Auto1;
        private TextBox _TB_Auto2;
        private string _statsdach;
        private string _statpoluch;
        private int _numstation;
        private string _warningtext;

        public Comp(Home home, List<string> listPal, List<string> listGM, List<string> listMesh, List<string> listCont, List<string> listSave, List<string> listZas,
                    TextBox TB_Autoplomb, RichTextBox RTBoooinn, TextBox TB_FIO, TextBox TB_March, TextBox TB_Phone, RichTextBox RTBdt, TextBox TB_Auto1, TextBox TB_Auto2,
                    string statsdach, string statpoluch, int numstation, string warningtext)
        {
            InitializeComponent();

            _home = home;
            _listPal = listPal;
            _listGM = listGM;
            _listCont = listCont;
            _listMesh = listMesh;
            _listSave = listSave;
            _listZas = listZas;
            _TB_Autoplomb = TB_Autoplomb;
            _RTBoooinn = RTBoooinn;
            _TB_FIO = TB_FIO;
            _TB_March = TB_March;
            _TB_Phone = TB_Phone;
            _RTBdt = RTBdt;
            _TB_Auto1 = TB_Auto1;
            _TB_Auto2 = TB_Auto2;
            _statsdach = statsdach;
            _statpoluch = statpoluch;
            _numstation = numstation;
            _warningtext = warningtext;
        }

        private void BTN_Cenel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BTN_Comp_Click(object sender, RoutedEventArgs e)
        {
            _home.FormDoc(_listPal, _listGM, _listMesh, _listCont, _listSave, _listZas, _TB_Autoplomb, _RTBoooinn, _TB_FIO, _TB_March, _TB_Phone, _RTBdt, _TB_Auto1, _TB_Auto2, _statsdach, _statpoluch, _numstation);
            Close();
        }

        private void TB_PRB_Loaded(object sender, RoutedEventArgs e)
        {
            TB_PRB.Text = $"{DateTime.Now.ToString("HH:mm")}";
            Stat.Settings.timePRB = TB_PRB.Text.Trim();
        }

        private void TB_PRB_LostFocus(object sender, RoutedEventArgs e)
        {
            Stat.Settings.timePRB = TB_PRB.Text.Trim();
        }

        private void TB_OTB_Loaded(object sender, RoutedEventArgs e)
        {
            TB_OTB.Text = $"{DateTime.Now.ToString("HH:mm")}";
            Stat.Settings.timeOTB = TB_OTB.Text.Trim() ;
        }

        private void TB_OTB_LostFocus(object sender, RoutedEventArgs e)
        {
            Stat.Settings.timeOTB = TB_OTB.Text.Trim();
        }

        private void RTB_WarningText_Loaded(object sender, RoutedEventArgs e)
        {
            FlowDocument document = new FlowDocument();
            Paragraph paragraph = new Paragraph();
            paragraph.Inlines.Add(new Bold(new Run(_warningtext)));
            document.Blocks.Add(paragraph);
            RTB_WarningText.Document = document;
        }
    }
}
