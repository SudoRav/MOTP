using System.Collections.Generic;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Win32;
using System.IO;
using System.Windows.Media.Imaging;
using ClosedXML.Excel;
using System.Net.Http;
using MOTP.Model;
using System.Collections.ObjectModel;
using System.Drawing.Printing;
using Spire.Xls;
using Microsoft.Toolkit.Uwp.Notifications;
using System.Xml;
using System.Threading.Tasks;
using System.Threading;
using Aspose.Html;
using System.Net;

namespace MOTP.View
{
    public partial class Home : UserControl
    {
        public Home()
        {
            InitializeComponent();
        }

        private void CB_Autoweb_Loaded(object sender, RoutedEventArgs e)
        {
            CB_Autoweb.IsChecked = Properties.Settings.Default.autoweb;
        }

        private void CB_AddDubl_Loaded(object sender, RoutedEventArgs e)
        {
            CB_AddDubl.IsChecked = Properties.Settings.Default.adddubl;
        }

        private void CB_Autoweb_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.autoweb = CB_Autoweb.IsChecked.Value;
            Properties.Settings.Default.Save();
        }

        private void CB_AddDubl_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.adddubl = CB_AddDubl.IsChecked.Value;
            Properties.Settings.Default.Save();
        }

        private void CB_MultiFind_Loaded(object sender, RoutedEventArgs e)
        {
            CB_MultiFind.IsChecked = Properties.Settings.Default.multifind;
        }

        private void CB_MultiFind_LostFocus(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.multifind = CB_MultiFind.IsChecked.Value;
            Properties.Settings.Default.Save();
        }

        private void TB_FIO_Loaded(object sender, RoutedEventArgs e)
        {
            TB_FIO.Text = Properties.Settings.Default.myFIO;
        }

        private void TB_FIO_LostFocus(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.myFIO = TB_FIO.Text.Trim();
            Properties.Settings.Default.Save();
        }

        private void TB_Dol_Loaded(object sender, RoutedEventArgs e)
        {
            TB_Dol.Text = Properties.Settings.Default.myDOL;
        }

        private void TB_Dol_LostFocus(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.myDOL = TB_Dol.Text.Trim();
            Properties.Settings.Default.Save();
        }

        public bool FormStr(TextBox input, int index, bool enterPlmb)
        {
            bool result = false;

            switch (index)
            {
                case 0: result = RegInp(input.Text, enterPlmb, index); break;
                case 1: result = RegInp(input.Text, enterPlmb, index); break;
                case 2: result = RegInp(input.Text, enterPlmb, index); break;
                case 3: result = RegInp(input.Text, enterPlmb, index); break;
                case 4: result = RegInp(input.Text, enterPlmb, index); break;
                case 5: result = RegInp(input.Text, enterPlmb, index); break;
            }
            new MainWindow().SetTBCol(input, !result);

            return result;
        }

        private bool RegInp(string input, bool enterPlmb, int index)
        {
            if (!enterPlmb)
                return RegMat(input, PatModManager._DataPatNacs, index);
            else
                return RegMat(input, PatModManager._DataPatPlbs, index);
        }
        private bool RegMat(string input, ObservableCollection<PatMod> PMM, int index)
        {
            bool result = false;

            for (int i = 0; i < PMM.Count; i++)
            {
                bool act = false;

                switch (index)
                {
                    case 0: act = PMM[i].ActPal; break;
                    case 1: act = PMM[i].ActGM; break;
                    case 2: act = PMM[i].ActMesh; break;
                    case 3: act = PMM[i].ActCont; break;
                    case 4: act = PMM[i].ActSave; break;
                    case 5: act = PMM[i].ActZas; break;
                }
                if (act)
                {
                    Regex reg = new Regex(PMM[i].PatName);

                    if (reg.IsMatch(input))
                        result = true;
                }
            }

            return result;
        }

        public void AddProd(List<string> listPal, List<string> listGM, List<string> listMesh, List<string> listCont, List<string> listSave, List<string> listZas,
                            List<string> stat, TextBox nacl, TextBox plmb, string prim, string dopstr = "", ListBox list = null)
        {
            if (nacl.Text == "" || nacl.Text == " ")
            {
                System.Media.SystemSounds.Hand.Play();
                return;
            }

            nacl.Text = nacl.Text.Trim().Replace(" ", "_");
            plmb.Text = plmb.Text.Trim().Replace(" ", "_");

            if (!Stat.Settings.AddRem)
            {
                int countrem = 0;

                countrem += RemProd(listPal, nacl, false);
                countrem += RemProd(listGM, nacl, false);
                countrem += RemProd(listMesh, nacl, true);
                countrem += RemProd(listCont, nacl, true);
                countrem += RemProd(listSave, nacl, false);
                countrem += RemProd(listZas, nacl, false);

                if (countrem == 0)
                    System.Media.SystemSounds.Hand.Play();
                else
                    System.Media.SystemSounds.Asterisk.Play();

                ClearEnter(nacl, plmb);
                return;
            }

            if (FindDubl(listPal, listGM, listMesh, listCont, listSave, listZas, nacl.Text, Properties.Settings.Default.multifind) && FindDubl(listPal, listGM, listMesh, listCont, listSave, listZas, plmb.Text, Properties.Settings.Default.multifind))
            {
                stat.Add($"{prim} {nacl.Text.Trim()} {plmb.Text.Trim()}{dopstr}");
                //list?.Items.Add($"{prim} {nacl.Text.Trim()} {plmb.Text.Trim()}{dopstr}");
            }
            else
            {
                System.Media.SystemSounds.Hand.Play();
                if (Properties.Settings.Default.adddubl)
                {
                    stat.Add($"{prim} {nacl.Text.Trim()} {plmb.Text.Trim()}{dopstr}");
                    //list?.Items.Add($"{prim} {nacl.Text.Trim()} {plmb.Text.Trim()}{dopstr}");
                }
            }

            ClearEnter(nacl, plmb);
        }

        private int RemProd(List<string> list, TextBox nacl, bool useplmb)
        {
            foreach (var l in list)
            {
                string[] c = l.Split(' ');

                if (c[1] == nacl.Text)
                {
                    list.Remove(l);
                    return 1;
                }
            }

            return 0;
        }

        public void ImportData(string cliptxt, RichTextBox RTBoooinn, TextBox TB_FIO, TextBox TB_March, TextBox TB_Phone, RichTextBox RTBdt, TextBox TB_Auto1, TextBox TB_Auto2)
        {
            try
            {
                cliptxt = cliptxt.Replace("\r\n", "|");
                cliptxt = cliptxt.Replace("||", "`");
                cliptxt = cliptxt.Replace("|", " ");

                string[] splcliptxt = cliptxt.Split('`');

                FlowDocument document = new FlowDocument();
                Paragraph paragraph = new Paragraph();
                paragraph.Inlines.Add(new Bold(new Run(splcliptxt[0])));
                document.Blocks.Add(paragraph);
                RTBoooinn.Document = document;
                RTBoooinn.Focus();

                TB_March.Text = splcliptxt[1];
                TB_March.Focus();

                FlowDocument document2 = new FlowDocument();
                Paragraph paragraph2 = new Paragraph();
                paragraph2.Inlines.Add(new Bold(new Run(splcliptxt[2])));
                document2.Blocks.Add(paragraph2);
                RTBdt.Document = document2;
                RTBdt.Focus();

                string[] spldt = splcliptxt[2].Split(' ');
                TB_FIO.Text = $"{spldt[0]} {spldt[1]} {spldt[2]}";
                TB_FIO.Focus();

                string[] splphone = splcliptxt[2].Split(' ');
                TB_Phone.Text = "_";
                for (int i = 0; i < splphone.Length; i++)
                    if (splphone[i].Contains("елефон"))
                        TB_Phone.Text = splphone[i + 1];
                TB_Phone.Focus();

                splcliptxt[3] = splcliptxt[3].Replace("\r", " ").Trim();
                string[] splauto = splcliptxt[3].Split(' ');
                TB_Auto1.Text = "";
                for (int i = 0; i < splauto.Length - 1; i++)
                    TB_Auto1.Text += $"{splauto[i]} ";
                TB_Auto2.Text = splauto[splauto.Length - 1];
                TB_Auto1.Focus();
                TB_Auto2.Focus();
            }
            catch (Exception ex) { System.Media.SystemSounds.Asterisk.Play(); MessageBox.Show(ex.Message); }
        }

        public void SetCB(ComboBox CB, TextBox nacl, TextBox plmb)
        {
            switch (CB.SelectedIndex)
            {
                case 0: nacl.Text = ""; plmb.Text = ""; plmb.IsEnabled = false; nacl.Focus(); break;
                case 1: nacl.Text = ""; plmb.Text = ""; plmb.IsEnabled = false; nacl.Focus(); break;
                case 2: nacl.Text = ""; plmb.Text = ""; plmb.IsEnabled = true; nacl.Focus(); break;
                case 3: nacl.Text = ""; plmb.Text = ""; plmb.IsEnabled = true; nacl.Focus(); break;
                case 4: nacl.Text = ""; plmb.Text = ""; plmb.IsEnabled = false; nacl.Focus(); break;
                case 5: nacl.Text = ""; plmb.Text = ""; plmb.IsEnabled = false; nacl.Focus(); break;
            }
        }

        public string Cyrillify(string s)
        {
            var sb = new StringBuilder(s);
            foreach (var kvp in Replacements)
                sb.Replace(kvp.Key, kvp.Value);
            return sb.ToString();
        }

        static readonly Dictionary<char, char> Replacements = new Dictionary<char, char>()
        {
            ['№'] = '#',
            ['й'] = 'q',
            ['ц'] = 'w',
            ['у'] = 'e',
            ['к'] = 'r',
            ['е'] = 't',
            ['н'] = 'y',
            ['г'] = 'u',
            ['ш'] = 'i',
            ['щ'] = 'o',
            ['з'] = 'p',
            ['х'] = '[',
            ['ъ'] = ']',
            ['ф'] = 'a',
            ['ы'] = 's',
            ['в'] = 'd',
            ['а'] = 'f',
            ['п'] = 'g',
            ['р'] = 'h',
            ['о'] = 'j',
            ['л'] = 'k',
            ['д'] = 'l',
            ['ж'] = ';',
            ['э'] = '\'',
            ['я'] = 'z',
            ['ч'] = 'x',
            ['с'] = 'c',
            ['м'] = 'v',
            ['и'] = 'b',
            ['т'] = 'n',
            ['ь'] = 'm',
            ['б'] = ',',
            ['ю'] = '.',
            ['Й'] = 'Q',
            ['Ц'] = 'W',
            ['У'] = 'E',
            ['К'] = 'R',
            ['Е'] = 'T',
            ['Н'] = 'Y',
            ['Г'] = 'U',
            ['Ш'] = 'I',
            ['Щ'] = 'O',
            ['З'] = 'P',
            ['Х'] = '[',
            ['Ъ'] = ']',
            ['Ф'] = 'A',
            ['Ы'] = 'S',
            ['В'] = 'D',
            ['А'] = 'F',
            ['П'] = 'G',
            ['Р'] = 'H',
            ['О'] = 'J',
            ['Л'] = 'K',
            ['Д'] = 'L',
            ['Ж'] = ';',
            ['Э'] = '\'',
            ['Я'] = 'Z',
            ['Ч'] = 'X',
            ['С'] = 'C',
            ['М'] = 'V',
            ['И'] = 'B',
            ['Т'] = 'N',
            ['Ь'] = 'M',
            ['Б'] = ',',
            ['Ю'] = '.',
        };

        public void ClearEnter(TextBox nacl, TextBox plmb)
        {
            nacl.Text = "";
            plmb.Text = "";

            nacl.Focus();
        }

        private bool FindDubl(List<string> listPal, List<string> listGM, List<string> listMesh, List<string> listCont, List<string> listSave, List<string> listZas,
                              string findstr, bool multyfind)
        {
            if (!multyfind)
            {
                foreach (string str in listPal)
                    if (findstr != "")
                        if (str.Contains(findstr))
                            return false;
                foreach (string str in listGM)
                    if (findstr != "")
                        if (str.Contains(findstr))
                            return false;
                foreach (string str in listMesh)
                    if (findstr != "")
                        if (str.Contains(findstr))
                            return false;
                foreach (string str in listCont)
                    if (findstr != "")
                        if (str.Contains(findstr))
                            return false;
                foreach (string str in listSave)
                    if (findstr != "")
                        if (str.Contains(findstr))
                            return false;
                foreach (string str in listZas)
                    if (findstr != "")
                        if (str.Contains(findstr))
                            return false;
                return true;
            }
            else
            {
                //не работает это дерьмо блять
                //List<string>[] vars = [Stat.BUhunskay._listPal, Stat.BUhunskay._listGM, Stat.BUhunskay._listMesh, Stat.BUhunskay._listCont, Stat.BUhunskay._listSave, Stat.BUhunskay._listZas];

                foreach (string str in Stat.BUhunskay._listPal) if (findstr != "") if (str.Contains(findstr)) return false;
                foreach (string str in Stat.BUhunskay._listGM) if (findstr != "") if (str.Contains(findstr)) return false;
                foreach (string str in Stat.BUhunskay._listMesh) if (findstr != "") if (str.Contains(findstr)) return false;
                foreach (string str in Stat.BUhunskay._listCont) if (findstr != "") if (str.Contains(findstr)) return false;
                foreach (string str in Stat.BUhunskay._listSave) if (findstr != "") if (str.Contains(findstr)) return false;
                foreach (string str in Stat.BUhunskay._listZas) if (findstr != "") if (str.Contains(findstr)) return false;

                foreach (string str in Stat.Helkovskay._listPal) if (findstr != "") if (str.Contains(findstr)) return false;
                foreach (string str in Stat.Helkovskay._listGM) if (findstr != "") if (str.Contains(findstr)) return false;
                foreach (string str in Stat.Helkovskay._listMesh) if (findstr != "") if (str.Contains(findstr)) return false;
                foreach (string str in Stat.Helkovskay._listCont) if (findstr != "") if (str.Contains(findstr)) return false;
                foreach (string str in Stat.Helkovskay._listSave) if (findstr != "") if (str.Contains(findstr)) return false;
                foreach (string str in Stat.Helkovskay._listZas) if (findstr != "") if (str.Contains(findstr)) return false;

                foreach (string str in Stat.Himki._listPal) if (findstr != "") if (str.Contains(findstr)) return false;
                foreach (string str in Stat.Himki._listGM) if (findstr != "") if (str.Contains(findstr)) return false;
                foreach (string str in Stat.Himki._listMesh) if (findstr != "") if (str.Contains(findstr)) return false;
                foreach (string str in Stat.Himki._listCont) if (findstr != "") if (str.Contains(findstr)) return false;
                foreach (string str in Stat.Himki._listSave) if (findstr != "") if (str.Contains(findstr)) return false;
                foreach (string str in Stat.Himki._listZas) if (findstr != "") if (str.Contains(findstr)) return false;

                foreach (string str in Stat.Marta._listPal) if (findstr != "") if (str.Contains(findstr)) return false;
                foreach (string str in Stat.Marta._listGM) if (findstr != "") if (str.Contains(findstr)) return false;
                foreach (string str in Stat.Marta._listMesh) if (findstr != "") if (str.Contains(findstr)) return false;
                foreach (string str in Stat.Marta._listCont) if (findstr != "") if (str.Contains(findstr)) return false;
                foreach (string str in Stat.Marta._listSave) if (findstr != "") if (str.Contains(findstr)) return false;
                foreach (string str in Stat.Marta._listZas) if (findstr != "") if (str.Contains(findstr)) return false;

                foreach (string str in Stat.Odincovo._listPal) if (findstr != "") if (str.Contains(findstr)) return false;
                foreach (string str in Stat.Odincovo._listGM) if (findstr != "") if (str.Contains(findstr)) return false;
                foreach (string str in Stat.Odincovo._listMesh) if (findstr != "") if (str.Contains(findstr)) return false;
                foreach (string str in Stat.Odincovo._listCont) if (findstr != "") if (str.Contains(findstr)) return false;
                foreach (string str in Stat.Odincovo._listSave) if (findstr != "") if (str.Contains(findstr)) return false;
                foreach (string str in Stat.Odincovo._listZas) if (findstr != "") if (str.Contains(findstr)) return false;

                foreach (string str in Stat.Pererva._listPal) if (findstr != "") if (str.Contains(findstr)) return false;
                foreach (string str in Stat.Pererva._listGM) if (findstr != "") if (str.Contains(findstr)) return false;
                foreach (string str in Stat.Pererva._listMesh) if (findstr != "") if (str.Contains(findstr)) return false;
                foreach (string str in Stat.Pererva._listCont) if (findstr != "") if (str.Contains(findstr)) return false;
                foreach (string str in Stat.Pererva._listSave) if (findstr != "") if (str.Contains(findstr)) return false;
                foreach (string str in Stat.Pererva._listZas) if (findstr != "") if (str.Contains(findstr)) return false;

                foreach (string str in Stat.Privolnay._listPal) if (findstr != "") if (str.Contains(findstr)) return false;
                foreach (string str in Stat.Privolnay._listGM) if (findstr != "") if (str.Contains(findstr)) return false;
                foreach (string str in Stat.Privolnay._listMesh) if (findstr != "") if (str.Contains(findstr)) return false;
                foreach (string str in Stat.Privolnay._listCont) if (findstr != "") if (str.Contains(findstr)) return false;
                foreach (string str in Stat.Privolnay._listSave) if (findstr != "") if (str.Contains(findstr)) return false;
                foreach (string str in Stat.Privolnay._listZas) if (findstr != "") if (str.Contains(findstr)) return false;

                foreach (string str in Stat.Puhkino._listPal) if (findstr != "") if (str.Contains(findstr)) return false;
                foreach (string str in Stat.Puhkino._listGM) if (findstr != "") if (str.Contains(findstr)) return false;
                foreach (string str in Stat.Puhkino._listMesh) if (findstr != "") if (str.Contains(findstr)) return false;
                foreach (string str in Stat.Puhkino._listCont) if (findstr != "") if (str.Contains(findstr)) return false;
                foreach (string str in Stat.Puhkino._listSave) if (findstr != "") if (str.Contains(findstr)) return false;
                foreach (string str in Stat.Puhkino._listZas) if (findstr != "") if (str.Contains(findstr)) return false;

                foreach (string str in Stat.Rybinovay._listPal) if (findstr != "") if (str.Contains(findstr)) return false;
                foreach (string str in Stat.Rybinovay._listGM) if (findstr != "") if (str.Contains(findstr)) return false;
                foreach (string str in Stat.Rybinovay._listMesh) if (findstr != "") if (str.Contains(findstr)) return false;
                foreach (string str in Stat.Rybinovay._listCont) if (findstr != "") if (str.Contains(findstr)) return false;
                foreach (string str in Stat.Rybinovay._listSave) if (findstr != "") if (str.Contains(findstr)) return false;
                foreach (string str in Stat.Rybinovay._listZas) if (findstr != "") if (str.Contains(findstr)) return false;

                foreach (string str in Stat.Sharapovo._listPal) if (findstr != "") if (str.Contains(findstr)) return false;
                foreach (string str in Stat.Sharapovo._listGM) if (findstr != "") if (str.Contains(findstr)) return false;
                foreach (string str in Stat.Sharapovo._listMesh) if (findstr != "") if (str.Contains(findstr)) return false;
                foreach (string str in Stat.Sharapovo._listCont) if (findstr != "") if (str.Contains(findstr)) return false;
                foreach (string str in Stat.Sharapovo._listSave) if (findstr != "") if (str.Contains(findstr)) return false;
                foreach (string str in Stat.Sharapovo._listZas) if (findstr != "") if (str.Contains(findstr)) return false;

                foreach (string str in Stat.Skladohnay._listPal) if (findstr != "") if (str.Contains(findstr)) return false;
                foreach (string str in Stat.Skladohnay._listGM) if (findstr != "") if (str.Contains(findstr)) return false;
                foreach (string str in Stat.Skladohnay._listMesh) if (findstr != "") if (str.Contains(findstr)) return false;
                foreach (string str in Stat.Skladohnay._listCont) if (findstr != "") if (str.Contains(findstr)) return false;
                foreach (string str in Stat.Skladohnay._listSave) if (findstr != "") if (str.Contains(findstr)) return false;
                foreach (string str in Stat.Skladohnay._listZas) if (findstr != "") if (str.Contains(findstr)) return false;

                foreach (string str in Stat.Vehki._listPal) if (findstr != "") if (str.Contains(findstr)) return false;
                foreach (string str in Stat.Vehki._listGM) if (findstr != "") if (str.Contains(findstr)) return false;
                foreach (string str in Stat.Vehki._listMesh) if (findstr != "") if (str.Contains(findstr)) return false;
                foreach (string str in Stat.Vehki._listCont) if (findstr != "") if (str.Contains(findstr)) return false;
                foreach (string str in Stat.Vehki._listSave) if (findstr != "") if (str.Contains(findstr)) return false;
                foreach (string str in Stat.Vehki._listZas) if (findstr != "") if (str.Contains(findstr)) return false;

                return true;
            }
        }

        public string tmpstr = "";
        public string tmpval = "";
        public string tmpprm = "";
        public string tmpcnt = "0";
        public ListBox tmpsnd;
        public List<string> tmpstt;
        public void ListSelectionWeb(ListBox sender, TextBlock tbo, List<string> stt, bool useplomb, int numstation)
        {
            if (sender.SelectedIndex == -1)
                return;

            try
            {
                string[] splstr1 = sender.SelectedValue.ToString().Trim().Split(' ', '_');
                string[] splstr2 = sender.SelectedValue.ToString().Trim().Split(' ');

                if (!useplomb)
                { tmpval = splstr2[1]; }
                else
                { tmpval = $"{splstr2[1]} {splstr2[2]}"; }

                try
                {
                    if (!useplomb)
                    { tmpcnt = splstr2[2]; }
                    else
                    { tmpcnt = splstr2[3]; }
                }
                catch { tmpcnt = "0"; }

                tmpsnd = sender;
                tmpstt = stt;
                tmpprm = splstr2[0];

                if (Properties.Settings.Default.autoweb)
                    Process.Start(new ProcessStartInfo($"https://{splstr1[1]}.zappstore.pro/pallet/{splstr2[1]}") { UseShellExecute = true });

                Details detail = new Details(this, tbo, stt, useplomb, numstation);
                detail.ShowDialog();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        public void OpenDop(List<string> listSave, List<string> listZas, int numstation)
        {
            Dop dop = new Dop(this, null, null, null, null, listSave, listZas, numstation);
            dop.ShowDialog();
        }
        public void OpenSett(int numstatiom)
        {
            Sett sett = new Sett(this, numstatiom);
            sett.ShowDialog();
        }
        public void OpenPatt()
        {
            Patterns patt = new Patterns(this);
            patt.ShowDialog();
        }

        public void OpenComp(List<string> listPal, List<string> listGM, List<string> listMesh, List<string> listCont, List<string> listSave, List<string> listZas,
                            TextBox TB_Autoplomb, RichTextBox RTBoooinn, TextBox TB_FIO, TextBox TB_March, TextBox TB_Phone, RichTextBox RTBdt, TextBox TB_Auto1, TextBox TB_Auto2,
                            string statsdach, string statpoluch, int numstation)
        {
            string warningline = "";

            if (Properties.Settings.Default.myFIO == "ФИО")
                warningline += "• Не указано ФИО работника\n";

            if (Properties.Settings.Default.myDOL == "Должность")
                warningline += "• Не указана Должность работника\n";

            if (listPal.Count == 0)
                warningline += "• Список паллетов пуст\n";

            if (listGM.Count == 0)
                warningline += "• Список ГМов пуст\n";

            if (listMesh.Count == 0)
                warningline += "• Список мешков пуст\n";

            if (listCont.Count == 0)
                warningline += "• Список контейнеров пуст\n";

            if (listSave.Count == 0)
                warningline += "• Список сейфпакетов пуст\n";

            if (listZas.Count == 0)
                warningline += "• Список засылов пуст\n";

            if (TB_Autoplomb.Text == "")
                warningline += "• Не указана пломба машины\n";

            if (new TextRange(RTBoooinn.Document.ContentStart, RTBoooinn.Document.ContentEnd).Text.Trim() == "ООО ИНН")
                warningline += "• Не указан ООО ИНН водителя\n";

            if (TB_FIO.Text == "ФИО")
                warningline += "• Не указано ФИО водителя\n";

            if (TB_March.Text == "")
                warningline += "• Не указан Маршрут\n";

            if (TB_Phone.Text == "Телефон")
                warningline += "• Не указан Телефон водителя\n";

            if (new TextRange(RTBdt.Document.ContentStart, RTBdt.Document.ContentEnd).Text.Trim() == "Данные водителя")
                warningline += "• Не указаны Данные водителя\n";

            if (TB_Auto1.Text == "Марка машины")
                warningline += "• Не указана Марка машины\n";

            if (TB_Auto2.Text == "Номер машины")
                warningline += "• Не указан Номер машины\n";

            Comp comp = new Comp(this, listPal, listGM, listMesh, listCont, listSave, listZas, TB_Autoplomb, RTBoooinn, TB_FIO, TB_March, TB_Phone, RTBdt, TB_Auto1, TB_Auto2, statsdach, statpoluch, numstation, warningline);
            comp.ShowDialog();
        }

        public void ReloadList(ListBox listin, List<string> listout)
        {
            listin.Items.Clear();
            foreach (string item in listout)
                listin.Items.Add(item);
        }

        public void ReloadDTInfo(List<string> list, TextBlock tbo, bool useplomb)
        {
            if (list != null)
            {
                int count = 0;

                foreach (string tmp2 in list)
                {
                    try
                    {
                        string[] tmp3 = tmp2.Split(' ');
                        if (!useplomb)
                        { count += Convert.ToInt32(tmp3[2]); }
                        else
                        { count += Convert.ToInt32(tmp3[3]); }
                    }
                    catch { }
                }

                tbo.Text = $"{list.Count} / {count}";
            }
        }

        int elementall = 0;
        int countall = 0;
        public string ReloadDTInfoAll(List<string> lista, List<string> listb, List<string> listc, List<string> listd, List<string> liste, List<string> listf)
        {
            elementall = 0;
            countall = 0;

            if (lista.Count > 0) ForeachCount(lista, false);
            if (listb.Count > 0) ForeachCount(listb, false);
            if (listc.Count > 0) ForeachCount(listc, true);
            if (listd.Count > 0) ForeachCount(listd, true);
            if (liste.Count > 0) ForeachCount(liste, false);
            if (listf.Count > 0) ForeachCount(listf, false);

            return $"{elementall} / {countall}";
        }

        private void ForeachCount(List<string> list, bool useplomb)
        {
            if (list == null)
                return;

            foreach (string item in list)
            {
                try
                {
                    elementall = elementall + 1;
                    string[] tmp3 = item.Split(' ');
                    if (!useplomb)
                    { countall += Convert.ToInt32(tmp3[2]); }
                    else
                    { countall += Convert.ToInt32(tmp3[3]); }
                }
                catch { }
            }
        }

        private string richreport = "";
        public void FormOtch(List<string> listPal, List<string> listGM, List<string> listMesh, List<string> listCont, List<string> listSave, List<string> listZas,
                            TextBox TB_Autoplomb, RichTextBox RTBoooinn, TextBox TB_FIO, TextBox TB_March, TextBox TB_Phone, RichTextBox RTBdt, TextBox TB_Auto1, TextBox TB_Auto2,
                            string statsdach, string statpoluch)
        {
            new MainWindow().ActiveCheck(DateTime.Now);

            if (Properties.Settings.Default.autoSaveOtch)
                try
                {
                    SaveData("O");
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }

            try
            {
                richreport = "";

                richreport += TB_March.Text.Trim();
                richreport += "\n\n";

                richreport += "Паллеты:\n";
                for (int i = 0; i < listPal.Count; i++)
                {
                    string[] str = listPal[i].Split(new char[] { ' ' });
                    richreport += $"{str[1]} {str[2]}\n";
                }
                richreport += "\n\n";

                richreport += "ГМы:\n";
                for (int i = 0; i < listGM.Count; i++)
                {
                    string[] str = listGM[i].Split(new char[] { ' ' });
                    richreport += $"{str[1]} {str[2]}\n";
                }
                richreport += "\n\n";

                richreport += "Мешки:\n";
                for (int i = 0; i < listMesh.Count; i++)
                {
                    string[] str = listMesh[i].Split(new char[] { ' ' });
                    try { richreport += $"{str[1]} {str[2]} {str[3]}\n"; }
                    catch { richreport += $"{str[1]} {str[2]}\n"; }
                }
                richreport += "\n\n";

                richreport += "Контейнеры:\n";
                for (int i = 0; i < listCont.Count; i++)
                {
                    string[] str = listCont[i].Split(new char[] { ' ' });
                    try { richreport += $"{str[1]} {str[2]} {str[3]}\n"; }
                    catch { richreport += $"{str[1]} {str[2]}\n"; }
                }
                richreport += "\n\n";

                richreport += "Сейфпакеты:\n";
                for (int i = 0; i < listSave.Count; i++)
                {
                    string[] str = listSave[i].Split(new char[] { ' ' });
                    try { richreport += $"{str[1]} {str[2]} {str[3]}\n"; }
                    catch { richreport += $"{str[1]} {str[2]}\n"; }
                }
                richreport += "\n\n";

                richreport += "Засылы:\n";
                for (int i = 0; i < listZas.Count; i++)
                {
                    string[] str = listZas[i].Split(new char[] { ' ' });
                    try { richreport += $"{str[1]} {str[2]} {str[3]}\n"; }
                    catch { richreport += $"{str[1]} {str[2]}\n"; }
                }
                richreport += "\n\n";

                richreport += "Пломба машины:\n";
                richreport += TB_Autoplomb.Text.Trim();
                richreport += "\n\n";

                richreport += "\n\n";

                richreport += new TextRange(RTBoooinn.Document.ContentStart, RTBoooinn.Document.ContentEnd).Text.Trim();
                richreport += "\n\n";
                richreport += $"{TB_March.Text.Trim()}\n";
                richreport += "\n";
                if (TB_Phone.Text.Trim() != "" && TB_Phone.Text.Trim() != "_" && TB_Phone.Text.Trim() != "Телефон")
                    richreport += $"Телефон: {TB_Phone.Text}\n";
                richreport += new TextRange(RTBdt.Document.ContentStart, RTBdt.Document.ContentEnd).Text.Trim();
                richreport += "\n\n";
                richreport += $"{TB_Auto1.Text.Trim()}\n{TB_Auto2.Text.Trim()}\n";

                Report report = new Report(this, richreport);
                report.ShowDialog();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        public void FormDoc(List<string> listPal, List<string> listGM, List<string> listMesh, List<string> listCont, List<string> listSave, List<string> listZas,
                            TextBox TB_Autoplomb, RichTextBox RTBoooinn, TextBox TB_FIO, TextBox TB_March, TextBox TB_Phone, RichTextBox RTBdt, TextBox TB_Auto1, TextBox TB_Auto2,
                            string statsdach, string statpoluch, int numstation)
        {
            new MainWindow().ActiveCheck(DateTime.Now);

            if (Properties.Settings.Default.autoSaveDoc)
                try
                {
                    SaveData("P");
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }

            try
            {
                OpenFileDialog dialog = new OpenFileDialog
                {
                    InitialDirectory = GetPathFile(Properties.Settings.Default.vbnPathFileDir),
                    CheckPathExists = true,
                    Filter = "Excel Files(*.xlsx)|*.xlsx|All Files(*.*)|*.*"
                };
                if (Properties.Settings.Default.vbnPathFileDir != "")
                {
                    dialog.FileName = Properties.Settings.Default.vbnPathFileDir;
                }
                if (dialog.ShowDialog() == true)
                {
                    Properties.Settings.Default.vbnPathFileDir = dialog.FileName;
                    Properties.Settings.Default.Save();
                }
                else { return; }

                if (GetExtFile(Properties.Settings.Default.vbnPathFileDir) != ".xlsx")
                {
                    MessageBox.Show("Требуется файл формата «xlsx».", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                using (var wb = new XLWorkbook(Properties.Settings.Default.vbnPathFileDir))
                {
                    var ws = wb.Worksheets.Worksheet("Лист заполнения");

                    try { ws.Cell(FindValueWB(wb, "##DATESOST")).Value = $"{DateTime.Now:dd/MM/yyyy}"; } catch { }
                    try { ws.Cell(FindValueWB(wb, "##DATEOTGR")).Value = $"{DateTime.Now:dd/MM/yyyy}"; } catch { }

                    try { ws.Cell(FindValueWB(wb, "##MYFIO")).Value = Properties.Settings.Default.myFIO; } catch { }
                    try { ws.Cell(FindValueWB(wb, "##MYDOL")).Value = Properties.Settings.Default.myDOL; } catch { }

                    //try { ws.Cell(FindValueWB(wb, "##AUTOPLMB")).Value = TB_Autoplomb.Text.Trim(); } catch { }

                    try { ws.Cell(FindValueWB(wb, "##AUTOPLMB")).Value = double.Parse(TB_Autoplomb.Text.Trim()); }
                    catch { ws.Cell(FindValueWB(wb, "##AUTOPLMB")).Value = TB_Autoplomb.Text.Trim(); }

                    //string cargo = "";
                    //if (listPal.Count > 0)
                    //    cargo += $" {listPal.Count} - паллеты,";
                    //if (listMesh.Count > 0)
                    //    cargo += $" {listMesh.Count} - мешки,";
                    //if (listCont.Count > 0)
                    //    cargo += $" {listCont.Count} - контейнеры,";
                    //if (listGM.Count > 0)
                    //    cargo += $" {listGM.Count} - ГМы,";
                    //if (listSave.Count > 0)
                    //    cargo += $" {listSave.Count} - сейфпакеты,";
                    //if (listZas.Count > 0)
                    //    cargo += $" {listZas.Count} - засылы,";

                    string cargo = "";
                    if (listPal.Count > 0)
                        cargo += $" {listPal.Count} - паллеты,";
                    if (listMesh.Count > 0)
                        cargo += $" {listMesh.Count} - мешки,";
                    if (listCont.Count > 0)
                        cargo += $" {listCont.Count} - контейнеры,";
                    if ((listGM.Count + listSave.Count + listZas.Count) > 0)
                        cargo += $" {listGM.Count + listSave.Count + listZas.Count} - ГМы,";

                    try { cargo = cargo.Remove(cargo.Length - 1); } catch { }

                    try { ws.Cell(FindValueWB(wb, "##CARGO")).Value = cargo; } catch { }

                    try { ws.Cell(FindValueWB(wb, "##FIO")).Value = TB_FIO.Text.Trim(); } catch { }
                    try { ws.Cell(FindValueWB(wb, "##DT")).Value = new TextRange(RTBdt.Document.ContentStart, RTBdt.Document.ContentEnd).Text.Trim(); } catch { }

                    try { ws.Cell(FindValueWB(wb, "##OOOINN")).Value = new TextRange(RTBoooinn.Document.ContentStart, RTBoooinn.Document.ContentEnd).Text.Trim(); } catch { }

                    try { ws.Cell(FindValueWB(wb, "##NUMAUTO")).Value = TB_Auto2.Text.Trim(); } catch { }
                    try { ws.Cell(FindValueWB(wb, "##MARKAUTO")).Value = TB_Auto1.Text.Trim(); } catch { }

                    try { ws.Cell(FindValueWB(wb, "##SDACH")).Value = statsdach.Trim(); } catch { }
                    try { ws.Cell(FindValueWB(wb, "##POLUCH")).Value = statpoluch.Trim(); } catch { }

                    if (Stat.Settings.timePRB.Trim() == "")
                        Stat.Settings.timePRB = "_";
                    if (Stat.Settings.timeOTB.Trim() == "")
                        Stat.Settings.timeOTB = "_";
                    try { ws.Cell(FindValueWB(wb, "##TIMEPRB")).Value = Stat.Settings.timePRB.Trim(); } catch { }
                    try { ws.Cell(FindValueWB(wb, "##TIMEOTB")).Value = Stat.Settings.timeOTB.Trim(); } catch { }

                    string ts = "";
                    switch (numstation)
                    {
                        case 1: ts = Stat.Himki.ts; break;
                        case 2: ts = Stat.Marta.ts; break;
                        case 3: ts = Stat.Puhkino.ts; break;
                        case 4: ts = Stat.Privolnay.ts; break;
                        case 5: ts = Stat.Vehki.ts; break;
                        case 6: ts = Stat.Rybinovay.ts; break;
                        case 7: ts = Stat.Sharapovo.ts; break;
                        case 8: ts = Stat.Helkovskay.ts; break;
                        case 9: ts = Stat.Odincovo.ts; break;
                        case 10: ts = Stat.Skladohnay.ts; break;
                        case 11: ts = Stat.Pererva.ts; break;
                        case 12: ts = Stat.BUhunskay.ts; break;
                        case 13: ts = Stat.Egorevsk.ts; break;
                    }

                    int i = -1;

                    for (int j = 0; j < listPal.Count; j++)
                    {
                        i++;
                        try
                        {
                            string[] str = { "", "", "", "" };
                            string[] str2 = listPal[j].Split(' ');

                            try { str[0] = str2[0]; } catch { str[0] = $"##TYPE{i};"; }
                            try { str[1] = str2[1]; } catch { str[1] = $"##NAC{i}"; }
                            try { str[2] = str2[2]; } catch { str[2] = $"##PLB{i}"; }
                            try { str[3] = str2[3]; } catch { str[3] = $"##KOLP{i}"; }

                            WsCell(ws, wb, i, ts, str[1], "", str[2], "1", str[0], "");
                        }
                        catch (Exception ex) { MessageBox.Show(ex.Message); }
                    }

                    for (int j = 0; j < listGM.Count; j++)
                    {
                        i++;
                        try
                        {
                            string[] str = { "", "", "", "" };
                            string[] str2 = listGM[j].Split(' ');

                            try { str[0] = str2[0]; } catch { str[0] = $"##TYPE{i};"; }
                            try { str[1] = str2[1]; } catch { str[1] = $"##NAC{i}"; }
                            try { str[2] = str2[2]; } catch { str[2] = $"##PLB{i}"; }
                            try { str[3] = str2[3]; } catch { str[3] = $"##KOLP{i}"; }

                            WsCell(ws, wb, i, ts, str[1], "", str[2], "1", str[0], "");
                        }
                        catch (Exception ex) { MessageBox.Show(ex.Message); }
                    }

                    for (int j = 0; j < listMesh.Count; j++)
                    {
                        i++;
                        try
                        {
                            string[] str = { "", "", "", "", };
                            string[] str2 = listMesh[j].Split(' ');

                            try { str[0] = str2[0]; } catch { str[0] = $"##TYPE{i};"; }
                            try { str[1] = str2[1]; } catch { str[1] = $"##NAC{i}"; }
                            try { str[2] = str2[2]; } catch { str[2] = $"##PLB{i}"; }
                            try { str[3] = str2[3]; } catch { str[3] = $"##KOLP{i}"; }

                            WsCell(ws, wb, i, ts, str[1], str[2], str[3], "1", str[0], "");
                        }
                        catch (Exception ex) { MessageBox.Show(ex.Message); }
                    }

                    for (int j = 0; j < listCont.Count; j++)
                    {
                        i++;
                        try
                        {
                            string[] str = { "", "", "", "", };
                            string[] str2 = listCont[j].Split(' ');

                            try { str[0] = str2[0]; } catch { str[0] = $"##TYPE{i};"; }
                            try { str[1] = str2[1]; } catch { str[1] = $"##NAC{i}"; }
                            try { str[2] = str2[2]; } catch { str[2] = $"##PLB{i}"; }
                            try { str[3] = str2[3]; } catch { str[3] = $"##KOLP{i}"; }

                            WsCell(ws, wb, i, ts, str[1], str[2], str[3], "1", str[0], "");
                        }
                        catch (Exception ex) { MessageBox.Show(ex.Message); }
                    }

                    for (int j = 0; j < listSave.Count; j++)
                    {
                        i++;
                        try
                        {
                            string[] str = { "", "", "", "" };
                            string[] str2 = listSave[j].Split(' ');

                            try { str[0] = str2[0]; } catch { str[0] = $"##TYPE{i};"; }
                            try { str[1] = str2[1]; } catch { str[1] = $"##NAC{i}"; }
                            try { str[2] = str2[2]; } catch { str[2] = $"##PLB{i}"; }
                            try { str[3] = str2[3]; } catch { str[3] = $"##KOLP{i}"; }

                            WsCell(ws, wb, i, ts, str[1], "", str[2], "1", str[0], "");
                        }
                        catch (Exception ex) { MessageBox.Show(ex.Message); }
                    }

                    for (int j = 0; j < listZas.Count; j++)
                    {
                        i++;
                        try
                        {
                            string[] str = { "", "", "", "" };
                            string[] str2 = listZas[j].Split(' ');

                            try { str[0] = str2[0]; } catch { str[0] = $"##TYPE{i};"; }
                            try { str[1] = str2[1]; } catch { str[1] = $"##NAC{i}"; }
                            try { str[2] = str2[2]; } catch { str[2] = $"##PLB{i}"; }
                            try { str[3] = str2[3]; } catch { str[3] = $"##KOLP{i}"; }

                            WsCell(ws, wb, i, ts, str[1], "", str[2], "1", str[0], "Засыл");
                        }
                        catch (Exception ex) { MessageBox.Show(ex.Message); }
                    }

                    for (; i < 59;)
                    {
                        i++;
                        try
                        {
                            WsCell(ws, wb, i, "", "", "", "", "", "", "");
                        }
                        catch { }
                    }

                    //ws.ActiveCell = ws.Cell("I4"); ; 

                    string pathFile = $@"{GetPathFile(Properties.Settings.Default.vbnPathFileDir)}[DTL] {TB_March.Text.Trim()}{GetExtFile(Properties.Settings.Default.vbnPathFileDir)}";
                    wb.SaveAs(pathFile);

                    //MessageBox.Show("Запись успешно завершена.", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);

                    new ToastContentBuilder()
                        .AddArgument("action", "viewConversation")
                        .AddArgument("conversationId", 9813)
                        .AddText("Запись успешно завершена.")
                        .AddText($"Запись данных из MOTP завершена без ошибок и cохранена в файле:\n{GetNameFile(pathFile)}.")
                        .AddButton(new ToastButton().SetContent("OK"))
                        .Show();

                    if (Properties.Settings.Default.autoOpen)
                        Process.Start(pathFile);

                    if (Properties.Settings.Default.printerName != "")
                    {
                        PrintExcel(pathFile, 4, 0);
                        PrintExcel(pathFile, 4, 1);
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void WsCell(IXLWorksheet ws, XLWorkbook wb, int i, string ts, string nac, string plb, string kolp, string kol, string type, string prim)
        {
            try { ws.Cell(FindValueWB(wb, $"##TS{i}")).Value = double.Parse(ts); }
            catch { ws.Cell(FindValueWB(wb, $"##TS{i}")).Value = ts; }

            try { ws.Cell(FindValueWB(wb, $"##NAC{i}")).Value = double.Parse(nac); }
            catch { ws.Cell(FindValueWB(wb, $"##NAC{i}")).Value = nac; }

            try { ws.Cell(FindValueWB(wb, $"##PLB{i}")).Value = double.Parse(plb); }
            catch { ws.Cell(FindValueWB(wb, $"##PLB{i}")).Value = plb; }

            try { ws.Cell(FindValueWB(wb, $"##KOLP{i}")).Value = double.Parse(kolp); }
            catch { ws.Cell(FindValueWB(wb, $"##KOLP{i}")).Value = kolp; }

            try { ws.Cell(FindValueWB(wb, $"##KOL{i}")).Value = double.Parse(kol); }
            catch { ws.Cell(FindValueWB(wb, $"##KOL{i}")).Value = kol; }

            try { ws.Cell(FindValueWB(wb, $"##TYPE{i}")).Value = double.Parse(type); }
            catch { ws.Cell(FindValueWB(wb, $"##TYPE{i}")).Value = type; }

            try { ws.Cell(FindValueWB(wb, $"##PRIM{i}")).Value = double.Parse(prim); }
            catch { ws.Cell(FindValueWB(wb, $"##PRIM{i}")).Value = prim; }
        }

        private void PrintExcel(string pathFile, int pages, int numws)
        {
            try
            {
                if (pages < 1)
                    pages = 1;

                //Create a workbook

                Workbook workbook = new Workbook();

                //Load an Excel document

                workbook.LoadFromFile(pathFile);

                //Get the first worksheet

                Worksheet worksheet = workbook.Worksheets[numws];

                //Get the PageSetup object of the first worksheet

                PageSetup pageSetup = worksheet.PageSetup;

                //Set page margins

                pageSetup.TopMargin = 0.3;

                pageSetup.BottomMargin = 0.3;

                pageSetup.LeftMargin = 0.3;

                pageSetup.RightMargin = 0.3;

                //Specify print area

                switch (numws)
                {
                    case 0: pageSetup.PrintArea = "A1: R95"; break;
                    case 1: pageSetup.PrintArea = "A1: G73"; break;
                    default: pageSetup.PrintArea = "A1: B2"; break;
                }

                //Specify title row

                //pageSetup.PrintTitleRows = "$1:$2";

                //Allow to print with row/column headings

                pageSetup.IsPrintHeadings = true;

                //Allow to print with gridlines

                pageSetup.IsPrintGridlines = true;

                //Allow to print comments as displayed on worksheet

                pageSetup.PrintComments = PrintCommentType.InPlace;

                //Set printing quality (dpi)

                pageSetup.PrintQuality = 300;

                //Allow to print worksheet in black & white mode

                pageSetup.BlackAndWhite = true;

                //Set the printing order

                pageSetup.Order = OrderType.OverThenDown;

                //Fit worksheet on one page

                pageSetup.IsFitToPage = true;

                //Get PrinterSettings from the workbook

                PrinterSettings settings = workbook.PrintDocument.PrinterSettings;

                //Specify printer name

                settings.PrinterName = Properties.Settings.Default.printerName;

                //Print the workbook

                for (int i = 0; i < pages; i++)
                {
                    workbook.PrintDocument.Print();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private double DParse(string str)
        {
            return double.Parse(str);
        }

        private string FindValueWB(XLWorkbook wb, string fndstr, string namesheet = "Лист заполнения")
        {
            try
            {
                var fnd = wb.Worksheet(namesheet).CellsUsed(x => String.Equals(fndstr, x.Value.ToString()));
                foreach (IXLCell x in fnd)
                    return x.ToString();
                return null;
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); return null; }
        }

        public void ClrList(ListBox list, List<string> _list)
        {
            if (MessageBox.Show("Очистить содержимое списка?", "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                list.Items.Clear();
                _list.Clear();
            }
        }

        public bool SetType(KeyEventArgs e, string TB, ComboBox CB)
        {
            if (e.Key == Key.Enter)
            {
                System.Media.SystemSounds.Asterisk.Play();
                switch (TB)
                {
                    case "##pal": CB.SelectedIndex = -1; CB.SelectedIndex = 0; return true;
                    case "##gm": CB.SelectedIndex = -1; CB.SelectedIndex = 1; return true;
                    case "##mesh": CB.SelectedIndex = -1; CB.SelectedIndex = 2; return true;
                    case "##cont": CB.SelectedIndex = -1; CB.SelectedIndex = 3; return true;
                    case "##save": CB.SelectedIndex = -1; CB.SelectedIndex = 4; return true;
                    case "##zas": CB.SelectedIndex = -1; CB.SelectedIndex = 5; return true;
                }
            }
            return false;
        }

        private void BTN_DownloadImg_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog
            {
                Title = "Сохранить изображение как...",
                OverwritePrompt = true,
                CheckPathExists = true,
                Filter = "Image Files(*.png)|*.png"
            };

            if (dialog.ShowDialog() == true)
            {
                try
                {
                    var encoder = new PngBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create((BitmapSource)IMG_Types.Source));
                    using (FileStream stream = new FileStream(dialog.FileName, FileMode.Create))
                        encoder.Save(stream);
                }
                catch { }
            }
        }

        private string GetNameFile(string str)
        {
            string[] sp1 = { @"\" };
            string[] s1 = str.Split(sp1, StringSplitOptions.RemoveEmptyEntries);
            string[] sp2 = { @"." };
            string[] s2 = s1[s1.Length - 1].Split(sp2, StringSplitOptions.RemoveEmptyEntries);
            return s2[0];
        }
        private string GetExtFile(string str)
        {
            string[] sp = { "." };
            string[] s1 = str.Split(sp, StringSplitOptions.RemoveEmptyEntries);
            return $".{s1[s1.Length - 1]}";
        }
        private string GetPathFile(string str)
        {
            string[] sp = { @"\" };
            string[] s1 = str.Split(sp, StringSplitOptions.RemoveEmptyEntries);

            string sf = null;
            for (int i = 0; i < s1.Length - 1; i++)
                sf += $@"{s1[i]}\";
            return sf;
        }

        private void TB_Pas_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Properties.Settings.Default.crntpassword = TB_Pas.Text.Trim();
                Properties.Settings.Default.Save();

                TB_Pas.Text = "";

                new MainWindow().SetTBCol(TB_Pas, Properties.Settings.Default.crntpassword != Properties.Settings.Default.loadpassword);

                if (Properties.Settings.Default.crntpassword != Properties.Settings.Default.loadpassword)
                    return;

                TB_Pas.IsEnabled = false;
                System.Media.SystemSounds.Beep.Play();
                TB_Pas.Text = "Авторизован";
            }
        }

        private void TB_Pas_Loaded(object sender, RoutedEventArgs e)
        {
            if (Properties.Settings.Default.crntpassword == Properties.Settings.Default.loadpassword)
            {
                TB_Pas.Text = "Авторизован";
                TB_Pas.IsEnabled = false;
            }
            else
            {
                TB_Pas.Text = "";
                TB_Pas.IsEnabled = true;
            }

            new MainWindow().SetTBCol(TB_Pas, Properties.Settings.Default.crntpassword != Properties.Settings.Default.loadpassword);
        }

        private void BTN_Ptrn_Click(object sender, RoutedEventArgs e)
        {
            OpenPatt();
        }

        private void BTN_WebCnt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string url = TB_WebCnt.Text;

                if (url == "")
                    return;

                string str = GetHtmlFromUrl(url);

                MessageBox.Show(str.ToString());
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally { MessageBox.Show("."); }
        }

        public static string GetHtmlFromUrl(string url)
        {
            WebClient webClient = new WebClient();
            webClient.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:45.0) Gecko/20100101 Firefox/45.0");
            webClient.Headers.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8");
            webClient.Headers.Add("Accept-Language", "ru-RU,ru;q=0.8,en-US;q=0.5,en;q=0.3");
            webClient.Headers.Add("ETag", "W/69-1176812108000");
            return webClient.DownloadString(url);
        }

        private static string Parsing(string url)
        {
            try
            {
                using (HttpClientHandler hdl = new HttpClientHandler { AllowAutoRedirect = false, AutomaticDecompression = System.Net.DecompressionMethods.Deflate | System.Net.DecompressionMethods.GZip | System.Net.DecompressionMethods.None })
                {
                    using (var clnt = new HttpClient(hdl))
                    {
                        using (HttpResponseMessage resp = clnt.GetAsync(url).Result)
                        {
                            if (resp.IsSuccessStatusCode)
                            {
                                var html = resp.Content.ReadAsStringAsync().Result;
                                if (!string.IsNullOrEmpty(html))
                                {
                                    HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                                    doc.LoadHtml(html);

                                    return doc.Text;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }

            return null;
        }

        private void CB_Cyrillify_Loaded(object sender, RoutedEventArgs e)
        {
            CB_Cyrillify.IsChecked = Properties.Settings.Default.autoCyrillify;
        }

        private void CB_Cyrillify_LostFocus(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.autoCyrillify = CB_Cyrillify.IsChecked.Value;
            Properties.Settings.Default.Save();
        }

        private void TB_PathFilePrint_Loaded(object sender, RoutedEventArgs e)
        {
            TB_PathFilePrint.Text = Properties.Settings.Default.printerName;
        }

        private void TB_PathFilePrint_LostFocus(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.printerName = TB_PathFilePrint.Text;
            Properties.Settings.Default.Save();
        }

        private void CB_AutoOpen_Loaded(object sender, RoutedEventArgs e)
        {
            CB_AutoOpen.IsChecked = Properties.Settings.Default.autoOpen;
        }

        private void CB_AutoOpen_LostFocus(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.autoOpen = CB_AutoOpen.IsChecked.Value;
            Properties.Settings.Default.Save();
        }

        public void BTN_SaveData_Click(object sender, RoutedEventArgs e)
        {
            SaveData();
        }
        private void BTN_LoadData_Click(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        public async void FoneAutoSave()
        {
            await Task.Run(() =>
            {
                while (true)
                {
                    Thread.Sleep(Properties.Settings.Default.minutAutoSave * 60000);

                    SaveData();
                }
            });
        }


        static public void SaveData(string dopstr = "")
        {
            try
            {
                string data = null;

            data += "<?xml version=\"1.0\" encoding=\"utf-8\" ?>" + "\n";
            data += "<data>" + "\n";

            for (int i = 0; i < 13; i++)
            {
                string name = "";
                switch (i)
                {
                    case 0: name = "Himki"; break;
                    case 1: name = "Marta"; break;
                    case 2: name = "Puhkino"; break;
                    case 3: name = "Privolnay"; break;
                    case 4: name = "Vehki"; break;
                    case 5: name = "Rybinovay"; break;
                    case 6: name = "Sharapovo"; break;
                    case 7: name = "Helkovskay"; break;
                    case 8: name = "Odincovo"; break;
                    case 9: name = "Skladohnay"; break;
                    case 10: name = "Pererva"; break;
                    case 11: name = "BUhunskay"; break;
                    case 12: name = "Egorevsk"; break;
                    default: name = "none"; break;
                }

                data += $"<st name=\"{name}\">" + "\n";

                for (int j = 0; j < 6; j++)
                {
                    switch (j)
                    {
                        case 0:
                            foreach (string item in Stat.Settings.arr[i][j])
                                data += $"<pal>{item.Trim()}</pal>" + "\n";
                            break;
                        case 1:
                            foreach (string item in Stat.Settings.arr[i][j])
                                data += $"<gm>{item.Trim()}</gm>" + "\n";
                            break;
                        case 2:
                            foreach (string item in Stat.Settings.arr[i][j])
                                data += $"<mesh>{item.Trim()}</mesh>" + "\n";
                            break;
                        case 3:
                            foreach (string item in Stat.Settings.arr[i][j])
                                data += $"<cont>{item.Trim()}</cont>" + "\n";
                            break;
                        case 4:
                            foreach (string item in Stat.Settings.arr[i][j])
                                data += $"<save>{item.Trim()}</save>" + "\n";
                            break;
                        case 5:
                            foreach (string item in Stat.Settings.arr[i][j])
                                data += $"<zas>{item.Trim()}</zas>" + "\n";
                            break;
                    }
                }

                switch (i)
                {
                    case 0:
                        data += DataDataInp(Stat.Himki.oooinn, Stat.Himki.fio, Stat.Himki.march, Stat.Himki.phone, Stat.Himki.dt, Stat.Himki.auto1, Stat.Himki.auto2, Stat.Himki.autoplomb);
                        break;
                    case 1:
                        data += DataDataInp(Stat.Marta.oooinn, Stat.Marta.fio, Stat.Marta.march, Stat.Marta.phone, Stat.Marta.dt, Stat.Marta.auto1, Stat.Marta.auto2, Stat.Marta.autoplomb);
                        break;
                    case 2:
                        data += DataDataInp(Stat.Puhkino.oooinn, Stat.Puhkino.fio, Stat.Puhkino.march, Stat.Puhkino.phone, Stat.Puhkino.dt, Stat.Puhkino.auto1, Stat.Puhkino.auto2, Stat.Puhkino.autoplomb);
                        break;
                    case 3:
                        data += DataDataInp(Stat.Privolnay.oooinn, Stat.Privolnay.fio, Stat.Privolnay.march, Stat.Privolnay.phone, Stat.Privolnay.dt, Stat.Privolnay.auto1, Stat.Privolnay.auto2, Stat.Privolnay.autoplomb);
                        break;
                    case 4:
                        data += DataDataInp(Stat.Vehki.oooinn, Stat.Vehki.fio, Stat.Vehki.march, Stat.Vehki.phone, Stat.Vehki.dt, Stat.Vehki.auto1, Stat.Vehki.auto2, Stat.Vehki.autoplomb);
                        break;
                    case 5:
                        data += DataDataInp(Stat.Rybinovay.oooinn, Stat.Rybinovay.fio, Stat.Rybinovay.march, Stat.Rybinovay.phone, Stat.Rybinovay.dt, Stat.Rybinovay.auto1, Stat.Rybinovay.auto2, Stat.Rybinovay.autoplomb);
                        break;
                    case 6:
                        data += DataDataInp(Stat.Sharapovo.oooinn, Stat.Sharapovo.fio, Stat.Sharapovo.march, Stat.Sharapovo.phone, Stat.Sharapovo.dt, Stat.Sharapovo.auto1, Stat.Sharapovo.auto2, Stat.Sharapovo.autoplomb);
                        break;
                    case 7:
                        data += DataDataInp(Stat.Helkovskay.oooinn, Stat.Helkovskay.fio, Stat.Helkovskay.march, Stat.Helkovskay.phone, Stat.Helkovskay.dt, Stat.Helkovskay.auto1, Stat.Helkovskay.auto2, Stat.Helkovskay.autoplomb);
                        break;
                    case 8:
                        data += DataDataInp(Stat.Odincovo.oooinn, Stat.Odincovo.fio, Stat.Odincovo.march, Stat.Odincovo.phone, Stat.Odincovo.dt, Stat.Odincovo.auto1, Stat.Odincovo.auto2, Stat.Odincovo.autoplomb);
                        break;
                    case 9:
                        data += DataDataInp(Stat.Skladohnay.oooinn, Stat.Skladohnay.fio, Stat.Skladohnay.march, Stat.Skladohnay.phone, Stat.Skladohnay.dt, Stat.Skladohnay.auto1, Stat.Skladohnay.auto2, Stat.Skladohnay.autoplomb);
                        break;
                    case 10:
                        data += DataDataInp(Stat.Pererva.oooinn, Stat.Pererva.fio, Stat.Pererva.march, Stat.Pererva.phone, Stat.Pererva.dt, Stat.Pererva.auto1, Stat.Pererva.auto2, Stat.Pererva.autoplomb);
                        break;
                    case 11:
                        data += DataDataInp(Stat.BUhunskay.oooinn, Stat.BUhunskay.fio, Stat.BUhunskay.march, Stat.BUhunskay.phone, Stat.BUhunskay.dt, Stat.BUhunskay.auto1, Stat.BUhunskay.auto2, Stat.BUhunskay.autoplomb);
                        break;
                    case 12:
                        data += DataDataInp(Stat.Egorevsk.oooinn, Stat.Egorevsk.fio, Stat.Egorevsk.march, Stat.Egorevsk.phone, Stat.Egorevsk.dt, Stat.Egorevsk.auto1, Stat.Egorevsk.auto2, Stat.Egorevsk.autoplomb);
                        break;
                }

                data += "</st>" + "\n";
            }

            data += "</data>" + "\n";

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(data);

            string pathdata = Path.Combine(Environment.CurrentDirectory + $@"\Saves\{LineAdder(DateTime.Now.Day.ToString(), 2)}-{LineAdder(DateTime.Now.Month.ToString(), 2)}-{LineAdder(DateTime.Now.Year.ToString(), 4)}");
            string filename = $"{LineAdder(DateTime.Now.Hour.ToString(), 2)}-{LineAdder(DateTime.Now.Minute.ToString(), 2)}-{LineAdder(DateTime.Now.Second.ToString(), 2)}{dopstr}.xml";

            if (!Directory.Exists(pathdata))
                Directory.CreateDirectory(pathdata);
            xmlDoc.Save($"{pathdata}\\{filename}");

            new ToastContentBuilder()
                        .AddArgument("action", "viewConversation")
                        .AddArgument("conversationId", 9813)
                        .AddText("Данные успешно сохранены.")
                        .AddText($"Сохранение данных из MOTP проведено без ошибок. Все данные были сохранены в файле {new Home().GetNameFile(filename)}.xml в папке проекта.")
                        .AddButton(new ToastButton().SetContent("OK"))
                        .Show();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        static private string DataDataInp(string oooinn, string fio, string march, string phone, string dt, string auto1, string auto2, string autoplomb)
        {
            string str = "";

            if (oooinn != "ООО ИНН")
                str += $"<oooinn>{oooinn.Trim()}</oooinn>" + "\n";
            if (fio != "ФИО")
                str += $"<fio>{fio.Trim()}</fio>" + "\n";
            if (march != "")
                str += $"<march>{march.Trim()}</march>" + "\n";
            if (phone != "Телефон")
                str += $"<phone>{phone.Trim()}</phone>" + "\n";
            if (dt != "Данные водителя")
                str += $"<dt>{dt.Trim()}</dt>" + "\n";
            if (auto1 != "Марка машины")
                str += $"<auto1>{auto1.Trim()}</auto1>" + "\n";
            if (auto2 != "Номер машины")
                str += $"<auto2>{auto2.Trim()}</auto2>" + "\n";
            if (autoplomb != "")
                str += $"<autoplomb>{autoplomb.Trim()}</autoplomb>" + "\n";

            return str;
        }

        static private void LoadData()
        {
            if (MessageBox.Show("Импортировать данные?\nЭто приведёт к удалению внесённых, на данным момент, данных и замене их новыми из предоставляемого файла!", "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Warning) != MessageBoxResult.Yes)
                return;

            try
            {
                OpenFileDialog dialog = new OpenFileDialog
                {
                    InitialDirectory = Environment.CurrentDirectory,
                    //InitialDirectory = Properties.Settings.Default.savePathFileDir,
                    CheckPathExists = true,
                    Filter = "Xml Files(*.xml)|*.xml|All Files(*.*)|*.*"
                };
                if (Properties.Settings.Default.savePathFileDir != " ")
                {
                    dialog.FileName = Properties.Settings.Default.savePathFileDir;
                }
                if (dialog.ShowDialog() == true)
                {
                    Properties.Settings.Default.savePathFileDir = dialog.FileName;
                    Properties.Settings.Default.Save();
                }
                else { return; }

                XmlDocument xdoc = new XmlDocument();

                try { xdoc.Load(Properties.Settings.Default.savePathFileDir); }
                catch (Exception ex) { Debug.WriteLine(ex); }

                XmlElement xroot = xdoc.DocumentElement;

                for (int i0 = 0; i0 < 13; i0++)
                    for (int j0 = 0; j0 < 6; j0++)
                        Stat.Settings.arr[i0][j0].Clear();

                Stat.Himki.oooinn = "ООО ИНН"; Stat.Himki.fio = "ФИО"; Stat.Himki.march = "Подольских Курсантов — Химки"; Stat.Himki.phone = "Телефон"; Stat.Himki.dt = "Данные водителя"; Stat.Himki.auto1 = "Марка машины"; Stat.Himki.auto2 = "Номер машины"; Stat.Himki.autoplomb = ""; Stat.Himki.sdach = "г. Москва, Химки, проезд Коммунальный, д. 30а, стр. 1"; Stat.Himki.poluch = "г. Москва, вн. тер. г. муниципальный округ Черемушки, ул. Намёткина, д. 12А, помещ. XVII, ком. 9.";
                Stat.Marta.oooinn = "ООО ИНН"; Stat.Marta.fio = "ФИО"; Stat.Marta.march = "Подольских Курсантов — 8-марта"; Stat.Marta.phone = "Телефон"; Stat.Marta.dt = "Данные водителя"; Stat.Marta.auto1 = "Марка машины"; Stat.Marta.auto2 = "Номер машины"; Stat.Marta.autoplomb = ""; Stat.Marta.sdach = "г. Москва, ул. 8 марта, д. 14 с. 1"; Stat.Marta.poluch = "г. Москва, вн. тер. г. муниципальный округ Черемушки, ул. Намёткина, д. 12А, помещ. XVII, ком. 9.";
                Stat.Puhkino.oooinn = "ООО ИНН"; Stat.Puhkino.fio = "ФИО"; Stat.Puhkino.march = "Подольских Курсантов — Пушкино"; Stat.Puhkino.phone = "Телефон"; Stat.Puhkino.dt = "Данные водителя"; Stat.Puhkino.auto1 = "Марка машины"; Stat.Puhkino.auto2 = "Номер машины"; Stat.Puhkino.autoplomb = ""; Stat.Puhkino.sdach = "МО, г. Пушкино, Ярославское шоссе, 222"; Stat.Puhkino.poluch = "г. Москва, вн. тер. г. муниципальный округ Черемушки, ул. Намёткина, д. 12А, помещ. XVII, ком. 9.";
                Stat.Privolnay.oooinn = "ООО ИНН"; Stat.Privolnay.fio = "ФИО"; Stat.Privolnay.march = "Подольских Курсантов — Привольная"; Stat.Privolnay.phone = "Телефон"; Stat.Privolnay.dt = "Данные водителя"; Stat.Privolnay.auto1 = "Марка машины"; Stat.Privolnay.auto2 = "Номер машины"; Stat.Privolnay.autoplomb = ""; Stat.Privolnay.sdach = "Привольная улица, 8"; Stat.Privolnay.poluch = "г. Москва, вн. тер. г. муниципальный округ Черемушки, ул. Намёткина, д. 12А, помещ. XVII, ком. 9.";
                Stat.Vehki.oooinn = "ООО ИНН"; Stat.Vehki.fio = "ФИО"; Stat.Vehki.march = "Подольских Курсантов — Вешки"; Stat.Vehki.phone = "Телефон"; Stat.Vehki.dt = "Данные водителя"; Stat.Vehki.auto1 = "Марка машины"; Stat.Vehki.auto2 = "Номер машины"; Stat.Vehki.autoplomb = ""; Stat.Vehki.sdach = "Мытищинский р-н, ш. Липкинское, 2-й км, территория ТПЗ \"Алтуфьево\" вл.1, стр.1Б.)"; Stat.Vehki.poluch = "г. Москва, вн. тер. г. муниципальный округ Черемушки, ул. Намёткина, д. 12А, помещ. XVII, ком. 9.";
                Stat.Rybinovay.oooinn = "ООО ИНН"; Stat.Rybinovay.fio = "ФИО"; Stat.Rybinovay.march = "Подольских Курсантов — Рябиновая"; Stat.Rybinovay.phone = "Телефон"; Stat.Rybinovay.dt = "Данные водителя"; Stat.Rybinovay.auto1 = "Марка машины"; Stat.Rybinovay.auto2 = "Номер машины"; Stat.Rybinovay.autoplomb = ""; Stat.Rybinovay.sdach = "Москва ул. Рябиновая 53 стр.2"; Stat.Rybinovay.poluch = "г. Москва, вн. тер. г. муниципальный округ Черемушки, ул. Намёткина, д. 12А, помещ. XVII, ком. 9.";
                Stat.Sharapovo.oooinn = "ООО ИНН"; Stat.Sharapovo.fio = "ФИО"; Stat.Sharapovo.march = "Подольских Курсантов — Шарапово"; Stat.Sharapovo.phone = "Телефон"; Stat.Sharapovo.dt = "Данные водителя"; Stat.Sharapovo.auto1 = "Марка машины"; Stat.Sharapovo.auto2 = "Номер машины"; Stat.Sharapovo.autoplomb = ""; Stat.Sharapovo.sdach = "г. Москва, сельское поселение Марушкинское, д. Шарапово, ул.124 Придорожная, стр. 7А,стр1"; Stat.Sharapovo.poluch = "г. Москва, вн. тер. г. муниципальный округ Черемушки, ул. Намёткина, д. 12А, помещ. XVII, ком. 9.";
                Stat.Helkovskay.oooinn = "ООО ИНН"; Stat.Helkovskay.fio = "ФИО"; Stat.Helkovskay.march = "Подольских Курсантов — Щёлковская"; Stat.Helkovskay.phone = "Телефон"; Stat.Helkovskay.dt = "Данные водителя"; Stat.Helkovskay.auto1 = "Марка машины"; Stat.Helkovskay.auto2 = "Номер машины"; Stat.Helkovskay.autoplomb = ""; Stat.Helkovskay.sdach = "Щелковское шоссе 100к100"; Stat.Helkovskay.poluch = "г. Москва, вн. тер. г. муниципальный округ Черемушки, ул. Намёткина, д. 12А, помещ. XVII, ком. 9.";
                Stat.Odincovo.oooinn = "ООО ИНН"; Stat.Odincovo.fio = "ФИО"; Stat.Odincovo.march = "Подольских Курсантов — Одинцово"; Stat.Odincovo.phone = "Телефон"; Stat.Odincovo.dt = "Данные водителя"; Stat.Odincovo.auto1 = "Марка машины"; Stat.Odincovo.auto2 = "Номер машины"; Stat.Odincovo.autoplomb = ""; Stat.Odincovo.sdach = "Г. Одинцово, Ул Зеленая 10"; Stat.Odincovo.poluch = "г. Москва, вн. тер. г. муниципальный округ Черемушки, ул. Намёткина, д. 12А, помещ. XVII, ком. 9.";
                Stat.Skladohnay.oooinn = "ООО ИНН"; Stat.Skladohnay.fio = "ФИО"; Stat.Skladohnay.march = "Подольских Курсантов — Складочная"; Stat.Skladohnay.phone = "Телефон"; Stat.Skladohnay.dt = "Данные водителя"; Stat.Skladohnay.auto1 = "Марка машины"; Stat.Skladohnay.auto2 = "Номер машины"; Stat.Skladohnay.autoplomb = ""; Stat.Skladohnay.sdach = "г.Москва, ул.Складочная 1с6"; Stat.Skladohnay.poluch = "г. Москва, вн. тер. г. муниципальный округ Черемушки, ул. Намёткина, д. 12А, помещ. XVII, ком. 9.";
                Stat.Pererva.oooinn = "ООО ИНН"; Stat.Pererva.fio = "ФИО"; Stat.Pererva.march = "Подольских Курсантов — Перерва"; Stat.Pererva.phone = "Телефон"; Stat.Pererva.dt = "Данные водителя"; Stat.Pererva.auto1 = "Марка машины"; Stat.Pererva.auto2 = "Номер машины"; Stat.Pererva.autoplomb = ""; Stat.Pererva.sdach = "г. Москва,Перерва, 19с2"; Stat.Pererva.poluch = "г. Москва, вн. тер. г. муниципальный округ Черемушки, ул. Намёткина, д. 12А, помещ. XVII, ком. 9.";
                Stat.BUhunskay.oooinn = "ООО ИНН"; Stat.BUhunskay.fio = "ФИО"; Stat.BUhunskay.march = "Подольских Курсантов — Большая Юшуньская"; Stat.BUhunskay.phone = "Телефон"; Stat.BUhunskay.dt = "Данные водителя"; Stat.BUhunskay.auto1 = "Марка машины"; Stat.BUhunskay.auto2 = "Номер машины"; Stat.BUhunskay.autoplomb = ""; Stat.BUhunskay.sdach = "ул Большая Юшуньская , д. 7"; Stat.BUhunskay.poluch = "г. Москва, вн. тер. г. муниципальный округ Черемушки, ул. Намёткина, д. 12А, помещ. XVII, ком. 9.";
                Stat.Egorevsk.oooinn = "ООО ИНН"; Stat.Egorevsk.fio = "ФИО"; Stat.Egorevsk.march = "Подольских Курсантов — Егоревск"; Stat.Egorevsk.phone = "Телефон"; Stat.Egorevsk.dt = "Данные водителя"; Stat.Egorevsk.auto1 = "Марка машины"; Stat.Egorevsk.auto2 = "Номер машины"; Stat.Egorevsk.autoplomb = ""; Stat.Egorevsk.sdach = "г. Московская область, г.о. Егоревск г. Егоревск,ул Советская, 81"; Stat.Egorevsk.poluch = "г. Москва, вн. тер. г. муниципальный округ Черемушки, ул. Намёткина, д. 12А, помещ. XVII, ком. 9.";

                int el = 0;
                int i = -1;
                if (xroot != null)
                {
                    foreach (XmlElement xnode in xroot)
                    {
                        i++;
                        //XmlNode attr = xnode.Attributes.GetNamedItem("name");
                        //MessageBox.Show($"{attr?.Value} {xnode.ChildNodes.Count.ToString()}");

                        foreach (XmlNode childnode in xnode.ChildNodes)
                        {
                            if (childnode.Name == "pal") { AddXmlElement(childnode.InnerText, "pal", i); el++; }
                            if (childnode.Name == "gm") { AddXmlElement(childnode.InnerText, "gm", i); el++; }
                            if (childnode.Name == "mesh") { AddXmlElement(childnode.InnerText, "mesh", i); el++; }
                            if (childnode.Name == "cont") { AddXmlElement(childnode.InnerText, "cont", i); el++; }
                            if (childnode.Name == "save") { AddXmlElement(childnode.InnerText, "save", i); el++; }
                            if (childnode.Name == "zas") { AddXmlElement(childnode.InnerText, "zas", i); el++; }

                            switch (i)
                            {
                                case 0:
                                    if (childnode.Name == "oooinn") Stat.Himki.oooinn = childnode.InnerText;
                                    if (childnode.Name == "fio") Stat.Himki.fio = childnode.InnerText;
                                    if (childnode.Name == "march") Stat.Himki.march = childnode.InnerText;
                                    if (childnode.Name == "phone") Stat.Himki.phone = childnode.InnerText;
                                    if (childnode.Name == "dt") Stat.Himki.dt = childnode.InnerText;
                                    if (childnode.Name == "auto1") Stat.Himki.auto1 = childnode.InnerText;
                                    if (childnode.Name == "auto2") Stat.Himki.auto2 = childnode.InnerText;
                                    if (childnode.Name == "autoplomb") Stat.Himki.autoplomb = childnode.InnerText;
                                    break;
                                case 1:
                                    if (childnode.Name == "oooinn") Stat.Marta.oooinn = childnode.InnerText;
                                    if (childnode.Name == "fio") Stat.Marta.fio = childnode.InnerText;
                                    if (childnode.Name == "march") Stat.Marta.march = childnode.InnerText;
                                    if (childnode.Name == "phone") Stat.Marta.phone = childnode.InnerText;
                                    if (childnode.Name == "dt") Stat.Marta.dt = childnode.InnerText;
                                    if (childnode.Name == "auto1") Stat.Marta.auto1 = childnode.InnerText;
                                    if (childnode.Name == "auto2") Stat.Marta.auto2 = childnode.InnerText;
                                    if (childnode.Name == "autoplomb") Stat.Marta.autoplomb = childnode.InnerText;
                                    break;
                                case 2:
                                    if (childnode.Name == "oooinn") Stat.Puhkino.oooinn = childnode.InnerText;
                                    if (childnode.Name == "fio") Stat.Puhkino.fio = childnode.InnerText;
                                    if (childnode.Name == "march") Stat.Puhkino.march = childnode.InnerText;
                                    if (childnode.Name == "phone") Stat.Puhkino.phone = childnode.InnerText;
                                    if (childnode.Name == "dt") Stat.Puhkino.dt = childnode.InnerText;
                                    if (childnode.Name == "auto1") Stat.Puhkino.auto1 = childnode.InnerText;
                                    if (childnode.Name == "auto2") Stat.Puhkino.auto2 = childnode.InnerText;
                                    if (childnode.Name == "autoplomb") Stat.Puhkino.autoplomb = childnode.InnerText;
                                    break;
                                case 3:
                                    if (childnode.Name == "oooinn") Stat.Privolnay.oooinn = childnode.InnerText;
                                    if (childnode.Name == "fio") Stat.Privolnay.fio = childnode.InnerText;
                                    if (childnode.Name == "march") Stat.Privolnay.march = childnode.InnerText;
                                    if (childnode.Name == "phone") Stat.Privolnay.phone = childnode.InnerText;
                                    if (childnode.Name == "dt") Stat.Privolnay.dt = childnode.InnerText;
                                    if (childnode.Name == "auto1") Stat.Privolnay.auto1 = childnode.InnerText;
                                    if (childnode.Name == "auto2") Stat.Privolnay.auto2 = childnode.InnerText;
                                    if (childnode.Name == "autoplomb") Stat.Privolnay.autoplomb = childnode.InnerText;
                                    break;
                                case 4:
                                    if (childnode.Name == "oooinn") Stat.Vehki.oooinn = childnode.InnerText;
                                    if (childnode.Name == "fio") Stat.Vehki.fio = childnode.InnerText;
                                    if (childnode.Name == "march") Stat.Vehki.march = childnode.InnerText;
                                    if (childnode.Name == "phone") Stat.Vehki.phone = childnode.InnerText;
                                    if (childnode.Name == "dt") Stat.Vehki.dt = childnode.InnerText;
                                    if (childnode.Name == "auto1") Stat.Vehki.auto1 = childnode.InnerText;
                                    if (childnode.Name == "auto2") Stat.Vehki.auto2 = childnode.InnerText;
                                    if (childnode.Name == "autoplomb") Stat.Vehki.autoplomb = childnode.InnerText;
                                    break;
                                case 5:
                                    if (childnode.Name == "oooinn") Stat.Rybinovay.oooinn = childnode.InnerText;
                                    if (childnode.Name == "fio") Stat.Rybinovay.fio = childnode.InnerText;
                                    if (childnode.Name == "march") Stat.Rybinovay.march = childnode.InnerText;
                                    if (childnode.Name == "phone") Stat.Rybinovay.phone = childnode.InnerText;
                                    if (childnode.Name == "dt") Stat.Rybinovay.dt = childnode.InnerText;
                                    if (childnode.Name == "auto1") Stat.Rybinovay.auto1 = childnode.InnerText;
                                    if (childnode.Name == "auto2") Stat.Rybinovay.auto2 = childnode.InnerText;
                                    if (childnode.Name == "autoplomb") Stat.Rybinovay.autoplomb = childnode.InnerText;
                                    break;
                                case 6:
                                    if (childnode.Name == "oooinn") Stat.Sharapovo.oooinn = childnode.InnerText;
                                    if (childnode.Name == "fio") Stat.Sharapovo.fio = childnode.InnerText;
                                    if (childnode.Name == "march") Stat.Sharapovo.march = childnode.InnerText;
                                    if (childnode.Name == "phone") Stat.Sharapovo.phone = childnode.InnerText;
                                    if (childnode.Name == "dt") Stat.Sharapovo.dt = childnode.InnerText;
                                    if (childnode.Name == "auto1") Stat.Sharapovo.auto1 = childnode.InnerText;
                                    if (childnode.Name == "auto2") Stat.Sharapovo.auto2 = childnode.InnerText;
                                    if (childnode.Name == "autoplomb") Stat.Sharapovo.autoplomb = childnode.InnerText;
                                    break;
                                case 7:
                                    if (childnode.Name == "oooinn") Stat.Helkovskay.oooinn = childnode.InnerText;
                                    if (childnode.Name == "fio") Stat.Helkovskay.fio = childnode.InnerText;
                                    if (childnode.Name == "march") Stat.Helkovskay.march = childnode.InnerText;
                                    if (childnode.Name == "phone") Stat.Helkovskay.phone = childnode.InnerText;
                                    if (childnode.Name == "dt") Stat.Helkovskay.dt = childnode.InnerText;
                                    if (childnode.Name == "auto1") Stat.Helkovskay.auto1 = childnode.InnerText;
                                    if (childnode.Name == "auto2") Stat.Helkovskay.auto2 = childnode.InnerText;
                                    if (childnode.Name == "autoplomb") Stat.Helkovskay.autoplomb = childnode.InnerText;
                                    break;
                                case 8:
                                    if (childnode.Name == "oooinn") Stat.Odincovo.oooinn = childnode.InnerText;
                                    if (childnode.Name == "fio") Stat.Odincovo.fio = childnode.InnerText;
                                    if (childnode.Name == "march") Stat.Odincovo.march = childnode.InnerText;
                                    if (childnode.Name == "phone") Stat.Odincovo.phone = childnode.InnerText;
                                    if (childnode.Name == "dt") Stat.Odincovo.dt = childnode.InnerText;
                                    if (childnode.Name == "auto1") Stat.Odincovo.auto1 = childnode.InnerText;
                                    if (childnode.Name == "auto2") Stat.Odincovo.auto2 = childnode.InnerText;
                                    if (childnode.Name == "autoplomb") Stat.Odincovo.autoplomb = childnode.InnerText;
                                    break;
                                case 9:
                                    if (childnode.Name == "oooinn") Stat.Skladohnay.oooinn = childnode.InnerText;
                                    if (childnode.Name == "fio") Stat.Skladohnay.fio = childnode.InnerText;
                                    if (childnode.Name == "march") Stat.Skladohnay.march = childnode.InnerText;
                                    if (childnode.Name == "phone") Stat.Skladohnay.phone = childnode.InnerText;
                                    if (childnode.Name == "dt") Stat.Skladohnay.dt = childnode.InnerText;
                                    if (childnode.Name == "auto1") Stat.Skladohnay.auto1 = childnode.InnerText;
                                    if (childnode.Name == "auto2") Stat.Skladohnay.auto2 = childnode.InnerText;
                                    if (childnode.Name == "autoplomb") Stat.Skladohnay.autoplomb = childnode.InnerText;
                                    break;
                                case 10:
                                    if (childnode.Name == "oooinn") Stat.Pererva.oooinn = childnode.InnerText;
                                    if (childnode.Name == "fio") Stat.Pererva.fio = childnode.InnerText;
                                    if (childnode.Name == "march") Stat.Pererva.march = childnode.InnerText;
                                    if (childnode.Name == "phone") Stat.Pererva.phone = childnode.InnerText;
                                    if (childnode.Name == "dt") Stat.Pererva.dt = childnode.InnerText;
                                    if (childnode.Name == "auto1") Stat.Pererva.auto1 = childnode.InnerText;
                                    if (childnode.Name == "auto2") Stat.Pererva.auto2 = childnode.InnerText;
                                    if (childnode.Name == "autoplomb") Stat.Pererva.autoplomb = childnode.InnerText;
                                    break;
                                case 11:
                                    if (childnode.Name == "oooinn") Stat.BUhunskay.oooinn = childnode.InnerText;
                                    if (childnode.Name == "fio") Stat.BUhunskay.fio = childnode.InnerText;
                                    if (childnode.Name == "march") Stat.BUhunskay.march = childnode.InnerText;
                                    if (childnode.Name == "phone") Stat.BUhunskay.phone = childnode.InnerText;
                                    if (childnode.Name == "dt") Stat.BUhunskay.dt = childnode.InnerText;
                                    if (childnode.Name == "auto1") Stat.BUhunskay.auto1 = childnode.InnerText;
                                    if (childnode.Name == "auto2") Stat.BUhunskay.auto2 = childnode.InnerText;
                                    if (childnode.Name == "autoplomb") Stat.BUhunskay.autoplomb = childnode.InnerText;
                                    break;
                                case 12:
                                    if (childnode.Name == "oooinn") Stat.Egorevsk.oooinn = childnode.InnerText;
                                    if (childnode.Name == "fio") Stat.Egorevsk.fio = childnode.InnerText;
                                    if (childnode.Name == "march") Stat.Egorevsk.march = childnode.InnerText;
                                    if (childnode.Name == "phone") Stat.Egorevsk.phone = childnode.InnerText;
                                    if (childnode.Name == "dt") Stat.Egorevsk.dt = childnode.InnerText;
                                    if (childnode.Name == "auto1") Stat.Egorevsk.auto1 = childnode.InnerText;
                                    if (childnode.Name == "auto2") Stat.Egorevsk.auto2 = childnode.InnerText;
                                    if (childnode.Name == "autoplomb") Stat.Egorevsk.autoplomb = childnode.InnerText;
                                    break;
                            }
                        }
                    }
                }

                new ToastContentBuilder()
                        .AddArgument("action", "viewConversation")
                        .AddArgument("conversationId", 9813)
                        .AddText("Данные успешно импортированы.")
                        .AddText($"Импортирование данных из файла {new Home().GetNameFile(Properties.Settings.Default.savePathFileDir)}.xml завершена без ошибок. Всего загружено {el} элементов таблиц.")
                        .AddButton(new ToastButton().SetContent("OK"))
                        .Show();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        static private void AddXmlElement(string str, string element, int i)
        {
            switch (element)
            {
                case "pal":
                    Stat.Settings.arr[i][0].Add(str);
                    break;
                case "gm":
                    Stat.Settings.arr[i][1].Add(str);
                    break;
                case "mesh":
                    Stat.Settings.arr[i][2].Add(str);
                    break;
                case "cont":
                    Stat.Settings.arr[i][3].Add(str);
                    break;
                case "save":
                    Stat.Settings.arr[i][4].Add(str);
                    break;
                case "zas":
                    Stat.Settings.arr[i][5].Add(str);
                    break;
            }
        }

        private void CB_TimeSave_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void CB_TimeSave_Loaded(object sender, RoutedEventArgs e)
        {
            CB_TimeSave.SelectedIndex = Properties.Settings.Default.minutAutoSave - 1;
        }

        private void CB_TimeSave_LostFocus(object sender, RoutedEventArgs e)
        {
            if (Properties.Settings.Default.minutAutoSave != CB_TimeSave.SelectedIndex + 1)
            {
                Properties.Settings.Default.minutAutoSave = CB_TimeSave.SelectedIndex + 1;
                Properties.Settings.Default.Save();
            }
        }

        private void CB_AutoSaveDoc_Loaded(object sender, RoutedEventArgs e)
        {
            CB_AutoSaveDoc.IsChecked = Properties.Settings.Default.autoSaveDoc;
        }

        private void CB_AutoSaveDoc_LostFocus(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.autoSaveDoc = CB_AutoSaveDoc.IsChecked.Value;
            Properties.Settings.Default.Save();
        }

        private void CB_AutoSaveOtch_Loaded(object sender, RoutedEventArgs e)
        {
            CB_AutoSaveOtch.IsChecked = Properties.Settings.Default.autoSaveOtch;
        }

        private void CB_AutoSaveOtch_LostFocus(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.autoSaveOtch = CB_AutoSaveOtch.IsChecked.Value;
            Properties.Settings.Default.Save();
        }

        public static string LineAdder(string str, int length, string add = "0")
        {
            int leng = length - str.Length;
            
            string nulls = "";
            for (int i = 0; i < leng; i++)
                nulls += add;

            return $"{nulls}{str}";
        }
    }
}