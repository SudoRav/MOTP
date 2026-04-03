using ClosedXML.Excel;
using Microsoft.Toolkit.Uwp.Notifications;
using Microsoft.Win32;
using MOTP.Model;
using Spire.Xls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Xml;

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

            if (FindDubl(nacl.Text, Properties.Settings.Default.multifind, listPal, listGM, listMesh, listCont, listSave, listZas) && FindDubl(plmb.Text, Properties.Settings.Default.multifind, listPal, listGM, listMesh, listCont, listSave, listZas))
            {
                stat.Add($"{prim} {nacl.Text.Trim()} {plmb.Text.Trim()}{dopstr}");
            }
            else
            {
                System.Media.SystemSounds.Hand.Play();
                if (Properties.Settings.Default.adddubl)
                {
                    stat.Add($"{prim} {nacl.Text.Trim()} {plmb.Text.Trim()}{dopstr}");
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

        private bool FindDubl(string findstr, bool multyfind, params List<string>[] currentLists)
        {
            if (string.IsNullOrEmpty(findstr))
                return true;

            if (!multyfind)
            {
                foreach (var list in currentLists)
                    if (list.Any(str => str.Contains(findstr)))
                        return false;
                return true;
            }

            var allStations = new List<List<List<string>>>()
    {
        new List<List<string>> { Stat.BUhunskay._listPal, Stat.BUhunskay._listGM, Stat.BUhunskay._listMesh, Stat.BUhunskay._listCont, Stat.BUhunskay._listSave, Stat.BUhunskay._listZas },
        new List<List<string>> { Stat.Helkovskay._listPal, Stat.Helkovskay._listGM, Stat.Helkovskay._listMesh, Stat.Helkovskay._listCont, Stat.Helkovskay._listSave, Stat.Helkovskay._listZas },
        new List<List<string>> { Stat.Himki._listPal, Stat.Himki._listGM, Stat.Himki._listMesh, Stat.Himki._listCont, Stat.Himki._listSave, Stat.Himki._listZas },
        new List<List<string>> { Stat.Marta._listPal, Stat.Marta._listGM, Stat.Marta._listMesh, Stat.Marta._listCont, Stat.Marta._listSave, Stat.Marta._listZas },
        new List<List<string>> { Stat.Odincovo._listPal, Stat.Odincovo._listGM, Stat.Odincovo._listMesh, Stat.Odincovo._listCont, Stat.Odincovo._listSave, Stat.Odincovo._listZas },
        new List<List<string>> { Stat.Pererva._listPal, Stat.Pererva._listGM, Stat.Pererva._listMesh, Stat.Pererva._listCont, Stat.Pererva._listSave, Stat.Pererva._listZas },
        new List<List<string>> { Stat.Privolnay._listPal, Stat.Privolnay._listGM, Stat.Privolnay._listMesh, Stat.Privolnay._listCont, Stat.Privolnay._listSave, Stat.Privolnay._listZas },
        new List<List<string>> { Stat.Puhkino._listPal, Stat.Puhkino._listGM, Stat.Puhkino._listMesh, Stat.Puhkino._listCont, Stat.Puhkino._listSave, Stat.Puhkino._listZas },
        new List<List<string>> { Stat.Rybinovay._listPal, Stat.Rybinovay._listGM, Stat.Rybinovay._listMesh, Stat.Rybinovay._listCont, Stat.Rybinovay._listSave, Stat.Rybinovay._listZas },
        new List<List<string>> { Stat.Sharapovo._listPal, Stat.Sharapovo._listGM, Stat.Sharapovo._listMesh, Stat.Sharapovo._listCont, Stat.Sharapovo._listSave, Stat.Sharapovo._listZas },
        new List<List<string>> { Stat.Skladohnay._listPal, Stat.Skladohnay._listGM, Stat.Skladohnay._listMesh, Stat.Skladohnay._listCont, Stat.Skladohnay._listSave, Stat.Skladohnay._listZas },
        new List<List<string>> { Stat.Vehki._listPal, Stat.Vehki._listGM, Stat.Vehki._listMesh, Stat.Vehki._listCont, Stat.Vehki._listSave, Stat.Vehki._listZas },
    };

            foreach (var stationLists in allStations)
            {
                foreach (var list in stationLists)
                {
                    if (list.Any(str => str.Contains(findstr)))
                        return false;
                }
            }

            return true;
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
                    Process.Start(new ProcessStartInfo($"https:-{splstr1[1]}.zappstore.pro/pallet/{splstr2[1]}") { UseShellExecute = true });

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
                Workbook workbook = new Workbook();
                workbook.LoadFromFile(pathFile);
                Worksheet worksheet = workbook.Worksheets[numws];
                PageSetup pageSetup = worksheet.PageSetup;
                pageSetup.TopMargin = 0.3;
                pageSetup.BottomMargin = 0.3;
                pageSetup.LeftMargin = 0.3;
                pageSetup.RightMargin = 0.3;
                switch (numws)
                {
                    case 0: pageSetup.PrintArea = "A1: R95"; break;
                    case 1: pageSetup.PrintArea = "A1: G73"; break;
                    default: pageSetup.PrintArea = "A1: B2"; break;
                }
                pageSetup.IsPrintHeadings = true;
                pageSetup.IsPrintGridlines = true;
                pageSetup.PrintComments = PrintCommentType.InPlace;
                pageSetup.PrintQuality = 300;
                pageSetup.BlackAndWhite = true;
                pageSetup.Order = OrderType.OverThenDown;
                pageSetup.IsFitToPage = true;
                PrinterSettings settings = workbook.PrintDocument.PrinterSettings;
                settings.PrinterName = Properties.Settings.Default.printerName;
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
                string[] stationNames =
                { "Himki", "Marta", "Puhkino", "Privolnay", "Vehki", "Rybinovay",
          "Sharapovo", "Helkovskay", "Odincovo", "Skladohnay", "Pererva",
          "BUhunskay", "Egorevsk" };

                string[] tags = { "pal", "gm", "mesh", "cont", "save", "zas" };

                var sb = new StringBuilder();
                sb.AppendLine("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
                sb.AppendLine("<data>");

                for (int i = 0; i < stationNames.Length; i++)
                {
                    sb.AppendLine($"  <st name=\"{stationNames[i]}\">");

                    for (int j = 0; j < tags.Length; j++)
                    {
                        foreach (string item in Stat.Settings.arr[i][j])
                            sb.AppendLine($"    <{tags[j]}>{item.Trim()}</{tags[j]}>");
                    }

                    sb.AppendLine(GetStationData(i));

                    sb.AppendLine("  </st>");
                }

                sb.AppendLine("</data>");

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(sb.ToString());

                string pathdata = Path.Combine(Environment.CurrentDirectory, "Saves",
                    $"{LineAdder(DateTime.Now.Day.ToString(), 2)}-{LineAdder(DateTime.Now.Month.ToString(), 2)}-{LineAdder(DateTime.Now.Year.ToString(), 4)}");
                string filename = $"{LineAdder(DateTime.Now.Hour.ToString(), 2)}-{LineAdder(DateTime.Now.Minute.ToString(), 2)}-{LineAdder(DateTime.Now.Second.ToString(), 2)}{dopstr}.xml";

                if (!Directory.Exists(pathdata))
                    Directory.CreateDirectory(pathdata);

                xmlDoc.Save(Path.Combine(pathdata, filename));

                new ToastContentBuilder()
                    .AddArgument("action", "viewConversation")
                    .AddArgument("conversationId", 9813)
                    .AddText("Данные успешно сохранены.")
                    .AddText($"Сохранение данных из MOTP проведено без ошибок. Все данные были сохранены в файле {new Home().GetNameFile(filename)}.xml в папке проекта.")
                    .AddButton(new ToastButton().SetContent("OK"))
                    .Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        static string GetStationData(int index)
        {
            return index switch
            {
                0 => DataDataInp(Stat.Himki.oooinn, Stat.Himki.fio, Stat.Himki.march, Stat.Himki.phone, Stat.Himki.dt, Stat.Himki.auto1, Stat.Himki.auto2, Stat.Himki.autoplomb),
                1 => DataDataInp(Stat.Marta.oooinn, Stat.Marta.fio, Stat.Marta.march, Stat.Marta.phone, Stat.Marta.dt, Stat.Marta.auto1, Stat.Marta.auto2, Stat.Marta.autoplomb),
                2 => DataDataInp(Stat.Puhkino.oooinn, Stat.Puhkino.fio, Stat.Puhkino.march, Stat.Puhkino.phone, Stat.Puhkino.dt, Stat.Puhkino.auto1, Stat.Puhkino.auto2, Stat.Puhkino.autoplomb),
                3 => DataDataInp(Stat.Privolnay.oooinn, Stat.Privolnay.fio, Stat.Privolnay.march, Stat.Privolnay.phone, Stat.Privolnay.dt, Stat.Privolnay.auto1, Stat.Privolnay.auto2, Stat.Privolnay.autoplomb),
                4 => DataDataInp(Stat.Vehki.oooinn, Stat.Vehki.fio, Stat.Vehki.march, Stat.Vehki.phone, Stat.Vehki.dt, Stat.Vehki.auto1, Stat.Vehki.auto2, Stat.Vehki.autoplomb),
                5 => DataDataInp(Stat.Rybinovay.oooinn, Stat.Rybinovay.fio, Stat.Rybinovay.march, Stat.Rybinovay.phone, Stat.Rybinovay.dt, Stat.Rybinovay.auto1, Stat.Rybinovay.auto2, Stat.Rybinovay.autoplomb),
                6 => DataDataInp(Stat.Sharapovo.oooinn, Stat.Sharapovo.fio, Stat.Sharapovo.march, Stat.Sharapovo.phone, Stat.Sharapovo.dt, Stat.Sharapovo.auto1, Stat.Sharapovo.auto2, Stat.Sharapovo.autoplomb),
                7 => DataDataInp(Stat.Helkovskay.oooinn, Stat.Helkovskay.fio, Stat.Helkovskay.march, Stat.Helkovskay.phone, Stat.Helkovskay.dt, Stat.Helkovskay.auto1, Stat.Helkovskay.auto2, Stat.Helkovskay.autoplomb),
                8 => DataDataInp(Stat.Odincovo.oooinn, Stat.Odincovo.fio, Stat.Odincovo.march, Stat.Odincovo.phone, Stat.Odincovo.dt, Stat.Odincovo.auto1, Stat.Odincovo.auto2, Stat.Odincovo.autoplomb),
                9 => DataDataInp(Stat.Skladohnay.oooinn, Stat.Skladohnay.fio, Stat.Skladohnay.march, Stat.Skladohnay.phone, Stat.Skladohnay.dt, Stat.Skladohnay.auto1, Stat.Skladohnay.auto2, Stat.Skladohnay.autoplomb),
                10 => DataDataInp(Stat.Pererva.oooinn, Stat.Pererva.fio, Stat.Pererva.march, Stat.Pererva.phone, Stat.Pererva.dt, Stat.Pererva.auto1, Stat.Pererva.auto2, Stat.Pererva.autoplomb),
                11 => DataDataInp(Stat.BUhunskay.oooinn, Stat.BUhunskay.fio, Stat.BUhunskay.march, Stat.BUhunskay.phone, Stat.BUhunskay.dt, Stat.BUhunskay.auto1, Stat.BUhunskay.auto2, Stat.BUhunskay.autoplomb),
                12 => DataDataInp(Stat.Egorevsk.oooinn, Stat.Egorevsk.fio, Stat.Egorevsk.march, Stat.Egorevsk.phone, Stat.Egorevsk.dt, Stat.Egorevsk.auto1, Stat.Egorevsk.auto2, Stat.Egorevsk.autoplomb),
                _ => "",
            };
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
            if (MessageBox.Show("Импортировать данные?\nЭто приведёт к удалению внесённых данных и замене их новыми из файла!",
                "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Warning) != MessageBoxResult.Yes)
                return;

            try
            {
                OpenFileDialog dialog = new OpenFileDialog
                {
                    InitialDirectory = Environment.CurrentDirectory,
                    CheckPathExists = true,
                    Filter = "Xml Files(*.xml)|*.xml|All Files(*.*)|*.*",
                    FileName = Properties.Settings.Default.savePathFileDir != " " ? Properties.Settings.Default.savePathFileDir : ""
                };

                if (dialog.ShowDialog() != true) return;

                Properties.Settings.Default.savePathFileDir = dialog.FileName;
                Properties.Settings.Default.Save();

                XmlDocument xdoc = new XmlDocument();
                xdoc.Load(Properties.Settings.Default.savePathFileDir);

                XmlElement xroot = xdoc.DocumentElement;

                var stations = new[]
                {
            Stat.Himki.Data, Stat.Marta.Data, Stat.Puhkino.Data, Stat.Privolnay.Data,
            Stat.Vehki.Data, Stat.Rybinovay.Data, Stat.Sharapovo.Data, Stat.Helkovskay.Data,
            Stat.Odincovo.Data, Stat.Skladohnay.Data, Stat.Pererva.Data, Stat.BUhunskay.Data, Stat.Egorevsk.Data
        };

                foreach (var arrRow in Stat.Settings.arr)
                    foreach (var list in arrRow)
                        list.Clear();

                foreach (var station in stations)
                {
                    station.oooinn ??= "ООО ИНН";
                    station.fio ??= "ФИО";
                    station.phone ??= "Телефон";
                    station.dt ??= "Данные водителя";
                    station.auto1 ??= "Марка машины";
                    station.auto2 ??= "Номер машины";
                    station.autoplomb ??= "";
                    station.poluch ??= "г. Москва, вн. тер. г. муниципальный округ Черемушки, ул. Намёткина, д. 12А, помещ. XVII, ком. 9.";
                }

                int el = 0;
                if (xroot != null)
                {
                    int i = -1;
                    foreach (XmlElement xnode in xroot)
                    {
                        i++;
                        if (i >= stations.Length) break;

                        var station = stations[i];

                        foreach (XmlNode childnode in xnode.ChildNodes)
                        {
                            string name = childnode.Name;
                            string value = childnode.InnerText;

                            if (new[] { "pal", "gm", "mesh", "cont", "save", "zas" }.Contains(name))
                            {
                                AddXmlElement(value, name, i);
                                el++;
                            }

                            switch (name)
                            {
                                case "oooinn": station.oooinn = value; break;
                                case "fio": station.fio = value; break;
                                case "march": station.march = value; break;
                                case "phone": station.phone = value; break;
                                case "dt": station.dt = value; break;
                                case "auto1": station.auto1 = value; break;
                                case "auto2": station.auto2 = value; break;
                                case "autoplomb": station.autoplomb = value; break;
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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