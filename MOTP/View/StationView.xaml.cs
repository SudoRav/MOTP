using ClosedXML.Excel;
using Microsoft.Toolkit.Uwp.Notifications;
using Microsoft.Win32;
using MOTP.ViewModel;
using Spire.Xls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Printing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;

namespace MOTP.View
{
    public partial class StationView : UserControl
    {
        private string _richReport = string.Empty;

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
            MessageBox.Show("Открыть дополнительные списки");
        }

        private void BTN_ImpDt_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Импорт данных");
        }

        private void BTN_Otch_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is not StationViewModel vm)
                return;

            FormOtch(
                vm.PalList,
                vm.GMList,
                vm.MeshList,
                vm.ContList,
                vm.Station._listSave ?? new List<string>(),
                vm.Station._listZas ?? new List<string>(),
                TB_autoplomb,
                RTBoooinn,
                TB_FIO,
                TB_March,
                TB_Phone,
                RTBdt,
                TB_Auto1,
                TB_Auto2,
                vm.Sdach,
                vm.Poluch);
        }

        private void BTN_Form_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is not StationViewModel vm)
                return;

            FormDoc(
                vm.PalList,
                vm.GMList,
                vm.MeshList,
                vm.ContList,
                vm.Station._listSave ?? new List<string>(),
                vm.Station._listZas ?? new List<string>(),
                TB_autoplomb,
                RTBoooinn,
                TB_FIO,
                TB_March,
                TB_Phone,
                RTBdt,
                TB_Auto1,
                TB_Auto2,
                vm.Sdach,
                vm.Poluch,
                vm.Station.numstation);
        }

        public void FormOtch(
            IList<string> listPal,
            IList<string> listGM,
            IList<string> listMesh,
            IList<string> listCont,
            IList<string> listSave,
            IList<string> listZas,
            TextBox tbAutoplomb,
            RichTextBox rtbOooinn,
            TextBox tbFio,
            TextBox tbMarch,
            TextBox tbPhone,
            RichTextBox rtbDt,
            TextBox tbAuto1,
            TextBox tbAuto2,
            string statsdach,
            string statpoluch)
        {
            new MainWindow().ActiveCheck(DateTime.Now);

            if (Properties.Settings.Default.autoSaveOtch)
            {
                try
                {
                    Home.SaveData("O");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            try
            {
                _richReport = string.Empty;

                _richReport += tbMarch.Text.Trim();
                _richReport += "\n\n";

                _richReport += "Паллеты:\n";
                for (int i = 0; i < listPal.Count; i++)
                {
                    string[] str = listPal[i].Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    if (str.Length >= 3)
                        _richReport += $"{str[1]} {str[2]}\n";
                }
                _richReport += "\n\n";

                _richReport += "ГМы:\n";
                for (int i = 0; i < listGM.Count; i++)
                {
                    string[] str = listGM[i].Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    if (str.Length >= 3)
                        _richReport += $"{str[1]} {str[2]}\n";
                }
                _richReport += "\n\n";

                _richReport += "Мешки:\n";
                for (int i = 0; i < listMesh.Count; i++)
                {
                    string[] str = listMesh[i].Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    if (str.Length >= 4)
                        _richReport += $"{str[1]} {str[2]} {str[3]}\n";
                    else if (str.Length >= 3)
                        _richReport += $"{str[1]} {str[2]}\n";
                }
                _richReport += "\n\n";

                _richReport += "Контейнеры:\n";
                for (int i = 0; i < listCont.Count; i++)
                {
                    string[] str = listCont[i].Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    if (str.Length >= 4)
                        _richReport += $"{str[1]} {str[2]} {str[3]}\n";
                    else if (str.Length >= 3)
                        _richReport += $"{str[1]} {str[2]}\n";
                }
                _richReport += "\n\n";

                _richReport += "Сейфпакеты:\n";
                for (int i = 0; i < listSave.Count; i++)
                {
                    string[] str = listSave[i].Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    if (str.Length >= 4)
                        _richReport += $"{str[1]} {str[2]} {str[3]}\n";
                    else if (str.Length >= 3)
                        _richReport += $"{str[1]} {str[2]}\n";
                }
                _richReport += "\n\n";

                _richReport += "Засылы:\n";
                for (int i = 0; i < listZas.Count; i++)
                {
                    string[] str = listZas[i].Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    if (str.Length >= 4)
                        _richReport += $"{str[1]} {str[2]} {str[3]}\n";
                    else if (str.Length >= 3)
                        _richReport += $"{str[1]} {str[2]}\n";
                }
                _richReport += "\n\n";

                _richReport += "Пломба машины:\n";
                _richReport += tbAutoplomb.Text.Trim();
                _richReport += "\n\n";
                _richReport += "\n\n";

                _richReport += new TextRange(rtbOooinn.Document.ContentStart, rtbOooinn.Document.ContentEnd).Text.Trim();
                _richReport += "\n\n";
                _richReport += $"{tbMarch.Text.Trim()}\n";
                _richReport += "\n";
                if (tbPhone.Text.Trim() != string.Empty && tbPhone.Text.Trim() != "_" && tbPhone.Text.Trim() != "Телефон")
                    _richReport += $"Телефон: {tbPhone.Text}\n";
                _richReport += new TextRange(rtbDt.Document.ContentStart, rtbDt.Document.ContentEnd).Text.Trim();
                _richReport += "\n\n";
                _richReport += $"{tbAuto1.Text.Trim()}\n{tbAuto2.Text.Trim()}\n";

                Report report = new Report(null, _richReport);
                report.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void FormDoc(
            IList<string> listPal,
            IList<string> listGM,
            IList<string> listMesh,
            IList<string> listCont,
            IList<string> listSave,
            IList<string> listZas,
            TextBox tbAutoplomb,
            RichTextBox rtbOooinn,
            TextBox tbFio,
            TextBox tbMarch,
            TextBox tbPhone,
            RichTextBox rtbDt,
            TextBox tbAuto1,
            TextBox tbAuto2,
            string statsdach,
            string statpoluch,
            int numstation)
        {
            new MainWindow().ActiveCheck(DateTime.Now);

            if (Properties.Settings.Default.autoSaveDoc)
            {
                try
                {
                    Home.SaveData("P");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            try
            {
                OpenFileDialog dialog = new OpenFileDialog
                {
                    InitialDirectory = GetPathFile(Properties.Settings.Default.vbnPathFileDir),
                    CheckPathExists = true,
                    Filter = "Excel Files(*.xlsx)|*.xlsx|All Files(*.*)|*.*"
                };

                if (Properties.Settings.Default.vbnPathFileDir != string.Empty)
                    dialog.FileName = Properties.Settings.Default.vbnPathFileDir;

                if (dialog.ShowDialog() == true)
                {
                    Properties.Settings.Default.vbnPathFileDir = dialog.FileName;
                    Properties.Settings.Default.Save();
                }
                else
                {
                    return;
                }

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

                    try { ws.Cell(FindValueWB(wb, "##AUTOPLMB")).Value = double.Parse(tbAutoplomb.Text.Trim()); }
                    catch { ws.Cell(FindValueWB(wb, "##AUTOPLMB")).Value = tbAutoplomb.Text.Trim(); }

                    string cargo = string.Empty;
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
                    try { ws.Cell(FindValueWB(wb, "##FIO")).Value = tbFio.Text.Trim(); } catch { }
                    try { ws.Cell(FindValueWB(wb, "##DT")).Value = new TextRange(rtbDt.Document.ContentStart, rtbDt.Document.ContentEnd).Text.Trim(); } catch { }
                    try { ws.Cell(FindValueWB(wb, "##OOOINN")).Value = new TextRange(rtbOooinn.Document.ContentStart, rtbOooinn.Document.ContentEnd).Text.Trim(); } catch { }
                    try { ws.Cell(FindValueWB(wb, "##NUMAUTO")).Value = tbAuto2.Text.Trim(); } catch { }
                    try { ws.Cell(FindValueWB(wb, "##MARKAUTO")).Value = tbAuto1.Text.Trim(); } catch { }
                    try { ws.Cell(FindValueWB(wb, "##SDACH")).Value = statsdach.Trim(); } catch { }
                    try { ws.Cell(FindValueWB(wb, "##POLUCH")).Value = statpoluch.Trim(); } catch { }

                    if (Stat.Settings.timePRB.Trim() == string.Empty)
                        Stat.Settings.timePRB = "_";
                    if (Stat.Settings.timeOTB.Trim() == string.Empty)
                        Stat.Settings.timeOTB = "_";

                    try { ws.Cell(FindValueWB(wb, "##TIMEPRB")).Value = Stat.Settings.timePRB.Trim(); } catch { }
                    try { ws.Cell(FindValueWB(wb, "##TIMEOTB")).Value = Stat.Settings.timeOTB.Trim(); } catch { }

                    string ts = GetStationTs(numstation);

                    int i = -1;
                    FillRows(ws, wb, ref i, ts, listPal, usePlomb: false, prim: "");
                    FillRows(ws, wb, ref i, ts, listGM, usePlomb: false, prim: "");
                    FillRows(ws, wb, ref i, ts, listMesh, usePlomb: true, prim: "");
                    FillRows(ws, wb, ref i, ts, listCont, usePlomb: true, prim: "");
                    FillRows(ws, wb, ref i, ts, listSave, usePlomb: false, prim: "");
                    FillRows(ws, wb, ref i, ts, listZas, usePlomb: false, prim: "Засыл");

                    for (; i < 59;)
                    {
                        i++;
                        try { WsCell(ws, wb, i, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty); } catch { }
                    }

                    string pathFile = $@"{GetPathFile(Properties.Settings.Default.vbnPathFileDir)}[DTL] {tbMarch.Text.Trim()}{GetExtFile(Properties.Settings.Default.vbnPathFileDir)}";
                    wb.SaveAs(pathFile);

                    new ToastContentBuilder()
                        .AddArgument("action", "viewConversation")
                        .AddArgument("conversationId", 9813)
                        .AddText("Запись успешно завершена.")
                        .AddText($"Запись данных из MOTP завершена без ошибок и cохранена в файле:\n{GetNameFile(pathFile)}.")
                        .AddButton(new ToastButton().SetContent("OK"))
                        .Show();

                    if (Properties.Settings.Default.autoOpen)
                        Process.Start(pathFile);

                    if (Properties.Settings.Default.printerName != string.Empty)
                    {
                        PrintExcel(pathFile, 4, 0);
                        PrintExcel(pathFile, 4, 1);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private static string GetStationTs(int numstation)
        {
            return numstation switch
            {
                1 => Stat.Himki.ts,
                2 => Stat.Marta.ts,
                3 => Stat.Puhkino.ts,
                4 => Stat.Privolnay.ts,
                5 => Stat.Vehki.ts,
                6 => Stat.Rybinovay.ts,
                7 => Stat.Sharapovo.ts,
                8 => Stat.Helkovskay.ts,
                9 => Stat.Odincovo.ts,
                10 => Stat.Skladohnay.ts,
                11 => Stat.Pererva.ts,
                12 => Stat.BUhunskay.ts,
                13 => Stat.Egorevsk.ts,
                _ => string.Empty,
            };
        }

        private static void FillRows(IXLWorksheet ws, XLWorkbook wb, ref int i, string ts, IList<string> source, bool usePlomb, string prim)
        {
            for (int j = 0; j < source.Count; j++)
            {
                i++;
                try
                {
                    string[] str = { string.Empty, string.Empty, string.Empty, string.Empty };
                    string[] str2 = source[j].Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                    try { str[0] = str2[0]; } catch { str[0] = $"##TYPE{i};"; }
                    try { str[1] = str2[1]; } catch { str[1] = $"##NAC{i}"; }
                    try { str[2] = str2[2]; } catch { str[2] = $"##PLB{i}"; }
                    try { str[3] = str2[3]; } catch { str[3] = $"##KOLP{i}"; }

                    if (usePlomb)
                        WsCell(ws, wb, i, ts, str[1], str[2], str[3], "1", str[0], prim);
                    else
                        WsCell(ws, wb, i, ts, str[1], string.Empty, str[2], "1", str[0], prim);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private static void WsCell(IXLWorksheet ws, XLWorkbook wb, int i, string ts, string nac, string plb, string kolp, string kol, string type, string prim)
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

        private static void PrintExcel(string pathFile, int pages, int numws)
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
                    workbook.PrintDocument.Print();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private static string FindValueWB(XLWorkbook wb, string fndstr, string namesheet = "Лист заполнения")
        {
            try
            {
                var fnd = wb.Worksheet(namesheet).CellsUsed(x => string.Equals(fndstr, x.Value.ToString()));
                foreach (IXLCell x in fnd)
                    return x.ToString();
                return null;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }

        private static string GetNameFile(string str)
        {
            string[] sp1 = { @"\" };
            string[] s1 = str.Split(sp1, StringSplitOptions.RemoveEmptyEntries);
            string[] sp2 = { @"." };
            string[] s2 = s1[s1.Length - 1].Split(sp2, StringSplitOptions.RemoveEmptyEntries);
            return s2[0];
        }

        private static string GetExtFile(string str)
        {
            string[] sp = { "." };
            string[] s1 = str.Split(sp, StringSplitOptions.RemoveEmptyEntries);
            return $".{s1[s1.Length - 1]}";
        }

        private static string GetPathFile(string str)
        {
            string[] sp = { @"\" };
            string[] s1 = str.Split(sp, StringSplitOptions.RemoveEmptyEntries);

            string sf = null;
            for (int i = 0; i < s1.Length - 1; i++)
                sf += $@"{s1[i]}\";
            return sf;
        }

        #endregion
    }
}