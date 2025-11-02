using System.Windows;
using System.Windows.Documents;

namespace MOTP.View
{
    public partial class Sett : Window
    {
        private Home _home;
        private int _numststion;
        public Sett(Home home, int numststion)
        {
            InitializeComponent();
            _home = home;
            _numststion = numststion;
        }


        private void RTB_Sdach_Loaded(object sender, RoutedEventArgs e)
        {
            FlowDocument document = new FlowDocument();
            Paragraph paragraph = new Paragraph();

            switch (_numststion)
            {
                case 1: paragraph.Inlines.Add(new Bold(new Run(Stat.Himki.sdach))); break;
                case 2: paragraph.Inlines.Add(new Bold(new Run(Stat.Marta.sdach))); break;
                case 3: paragraph.Inlines.Add(new Bold(new Run(Stat.Puhkino.sdach))); break;
                case 4: paragraph.Inlines.Add(new Bold(new Run(Stat.Privolnay.sdach))); break;
                case 5: paragraph.Inlines.Add(new Bold(new Run(Stat.Vehki.sdach))); break;
                case 6: paragraph.Inlines.Add(new Bold(new Run(Stat.Rybinovay.sdach))); break;
                case 7: paragraph.Inlines.Add(new Bold(new Run(Stat.Sharapovo.sdach))); break;
                case 8: paragraph.Inlines.Add(new Bold(new Run(Stat.Helkovskay.sdach))); break;
                case 9: paragraph.Inlines.Add(new Bold(new Run(Stat.Odincovo.sdach))); break;
                case 10: paragraph.Inlines.Add(new Bold(new Run(Stat.Skladohnay.sdach))); break;
                case 11: paragraph.Inlines.Add(new Bold(new Run(Stat.Pererva.sdach))); break;
                case 12: paragraph.Inlines.Add(new Bold(new Run(Stat.BUhunskay.sdach))); break;
                case 13: paragraph.Inlines.Add(new Bold(new Run(Stat.Egorevsk.sdach))); break;
            }

            document.Blocks.Add(paragraph);
            RTB_Sdach.Document = document;
        }

        private void RTB_Poluch_Loaded(object sender, RoutedEventArgs e)
        {
            FlowDocument document = new FlowDocument();
            Paragraph paragraph = new Paragraph();

            switch (_numststion)
            {
                case 1: paragraph.Inlines.Add(new Bold(new Run(Stat.Himki.poluch))); break;
                case 2: paragraph.Inlines.Add(new Bold(new Run(Stat.Marta.poluch))); break;
                case 3: paragraph.Inlines.Add(new Bold(new Run(Stat.Puhkino.poluch))); break;
                case 4: paragraph.Inlines.Add(new Bold(new Run(Stat.Privolnay.poluch))); break;
                case 5: paragraph.Inlines.Add(new Bold(new Run(Stat.Vehki.poluch))); break;
                case 6: paragraph.Inlines.Add(new Bold(new Run(Stat.Rybinovay.poluch))); break;
                case 7: paragraph.Inlines.Add(new Bold(new Run(Stat.Sharapovo.poluch))); break;
                case 8: paragraph.Inlines.Add(new Bold(new Run(Stat.Helkovskay.poluch))); break;
                case 9: paragraph.Inlines.Add(new Bold(new Run(Stat.Odincovo.poluch))); break;
                case 10: paragraph.Inlines.Add(new Bold(new Run(Stat.Skladohnay.poluch))); break;
                case 11: paragraph.Inlines.Add(new Bold(new Run(Stat.Pererva.poluch))); break;
                case 12: paragraph.Inlines.Add(new Bold(new Run(Stat.BUhunskay.poluch))); break;
                case 13: paragraph.Inlines.Add(new Bold(new Run(Stat.Egorevsk.poluch))); break;
            }

            document.Blocks.Add(paragraph);
            RTB_Poluch.Document = document;
        }

        private void RTB_Sdach_LostFocus(object sender, RoutedEventArgs e)
        {
            switch (_numststion)
            {
                case 1: Stat.Himki.sdach = new TextRange(RTB_Sdach.Document.ContentStart, RTB_Sdach.Document.ContentEnd).Text.Trim(); break;
                case 2: Stat.Marta.sdach = new TextRange(RTB_Sdach.Document.ContentStart, RTB_Sdach.Document.ContentEnd).Text.Trim(); break;
                case 3: Stat.Puhkino.sdach = new TextRange(RTB_Sdach.Document.ContentStart, RTB_Sdach.Document.ContentEnd).Text.Trim(); break;
                case 4: Stat.Privolnay.sdach = new TextRange(RTB_Sdach.Document.ContentStart, RTB_Sdach.Document.ContentEnd).Text.Trim(); break;
                case 5: Stat.Vehki.sdach = new TextRange(RTB_Sdach.Document.ContentStart, RTB_Sdach.Document.ContentEnd).Text.Trim(); break;
                case 6: Stat.Rybinovay.sdach = new TextRange(RTB_Sdach.Document.ContentStart, RTB_Sdach.Document.ContentEnd).Text.Trim(); break;
                case 7: Stat.Sharapovo.sdach = new TextRange(RTB_Sdach.Document.ContentStart, RTB_Sdach.Document.ContentEnd).Text.Trim(); break;
                case 8: Stat.Helkovskay.sdach = new TextRange(RTB_Sdach.Document.ContentStart, RTB_Sdach.Document.ContentEnd).Text.Trim(); break;
                case 9: Stat.Odincovo.sdach = new TextRange(RTB_Sdach.Document.ContentStart, RTB_Sdach.Document.ContentEnd).Text.Trim(); break;
                case 10: Stat.Skladohnay.sdach = new TextRange(RTB_Sdach.Document.ContentStart, RTB_Sdach.Document.ContentEnd).Text.Trim(); break;
                case 11: Stat.Pererva.sdach = new TextRange(RTB_Sdach.Document.ContentStart, RTB_Sdach.Document.ContentEnd).Text.Trim(); break;
                case 12: Stat.BUhunskay.sdach = new TextRange(RTB_Sdach.Document.ContentStart, RTB_Sdach.Document.ContentEnd).Text.Trim(); break;
                case 13: Stat.Egorevsk.sdach = new TextRange(RTB_Sdach.Document.ContentStart, RTB_Sdach.Document.ContentEnd).Text.Trim(); break;
            }
        }

        private void RTB_Poluch_LostFocus(object sender, RoutedEventArgs e)
        {
            switch (_numststion)
            {
                case 1: Stat.Himki.poluch = new TextRange(RTB_Poluch.Document.ContentStart, RTB_Poluch.Document.ContentEnd).Text.Trim(); break;
                case 2: Stat.Marta.poluch = new TextRange(RTB_Poluch.Document.ContentStart, RTB_Poluch.Document.ContentEnd).Text.Trim(); break;
                case 3: Stat.Puhkino.poluch = new TextRange(RTB_Poluch.Document.ContentStart, RTB_Poluch.Document.ContentEnd).Text.Trim(); break;
                case 4: Stat.Privolnay.poluch = new TextRange(RTB_Poluch.Document.ContentStart, RTB_Poluch.Document.ContentEnd).Text.Trim(); break;
                case 5: Stat.Vehki.poluch = new TextRange(RTB_Poluch.Document.ContentStart, RTB_Poluch.Document.ContentEnd).Text.Trim(); break;
                case 6: Stat.Rybinovay.poluch = new TextRange(RTB_Poluch.Document.ContentStart, RTB_Poluch.Document.ContentEnd).Text.Trim(); break;
                case 7: Stat.Sharapovo.poluch = new TextRange(RTB_Poluch.Document.ContentStart, RTB_Poluch.Document.ContentEnd).Text.Trim(); break;
                case 8: Stat.Helkovskay.poluch = new TextRange(RTB_Poluch.Document.ContentStart, RTB_Poluch.Document.ContentEnd).Text.Trim(); break;
                case 9: Stat.Odincovo.poluch = new TextRange(RTB_Poluch.Document.ContentStart, RTB_Poluch.Document.ContentEnd).Text.Trim(); break;
                case 10: Stat.Skladohnay.poluch = new TextRange(RTB_Poluch.Document.ContentStart, RTB_Poluch.Document.ContentEnd).Text.Trim(); break;
                case 11: Stat.Pererva.poluch = new TextRange(RTB_Poluch.Document.ContentStart, RTB_Poluch.Document.ContentEnd).Text.Trim(); break;
                case 12: Stat.BUhunskay.poluch = new TextRange(RTB_Poluch.Document.ContentStart, RTB_Poluch.Document.ContentEnd).Text.Trim(); break;
                case 13: Stat.Egorevsk.poluch = new TextRange(RTB_Poluch.Document.ContentStart, RTB_Poluch.Document.ContentEnd).Text.Trim(); break;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите удалить все внесённые данные это станции?", "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                return;

            switch (_numststion)
            {
                case 1:
                    Stat.Himki._listPal.Clear(); Stat.Himki._listGM.Clear(); Stat.Himki._listMesh.Clear(); Stat.Himki._listCont.Clear(); Stat.Himki._listSave.Clear(); Stat.Himki._listZas.Clear();
                    Stat.Himki.oooinn = "ООО ИНН"; Stat.Himki.fio = "ФИО"; Stat.Himki.march = "Подольских Курсантов — Химки"; Stat.Himki.phone = "Телефон"; Stat.Himki.dt = "Данные водителя"; Stat.Himki.auto1 = "Марка машины"; Stat.Himki.auto2 = "Номер машины"; Stat.Himki.autoplomb = ""; Stat.Himki.sdach = "г. Москва, Химки, проезд Коммунальный, д. 30а, стр. 1"; Stat.Himki.poluch = "г. Москва, вн. тер. г. муниципальный округ Черемушки, ул. Намёткина, д. 12А, помещ. XVII, ком. 9.";
                    break;
                case 2:
                    Stat.Marta._listPal.Clear(); Stat.Marta._listGM.Clear(); Stat.Marta._listMesh.Clear(); Stat.Marta._listCont.Clear(); Stat.Marta._listSave.Clear(); Stat.Marta._listZas.Clear();
                    Stat.Marta.oooinn = "ООО ИНН"; Stat.Marta.fio = "ФИО"; Stat.Marta.march = "Подольских Курсантов — 8-марта"; Stat.Marta.phone = "Телефон"; Stat.Marta.dt = "Данные водителя"; Stat.Marta.auto1 = "Марка машины"; Stat.Marta.auto2 = "Номер машины"; Stat.Marta.autoplomb = ""; Stat.Marta.sdach = "г. Москва, ул. 8 марта, д. 14 с. 1"; Stat.Marta.poluch = "г. Москва, вн. тер. г. муниципальный округ Черемушки, ул. Намёткина, д. 12А, помещ. XVII, ком. 9.";
                    break;
                case 3:
                    Stat.Puhkino._listPal.Clear(); Stat.Puhkino._listGM.Clear(); Stat.Puhkino._listMesh.Clear(); Stat.Puhkino._listCont.Clear(); Stat.Puhkino._listSave.Clear(); Stat.Puhkino._listZas.Clear();
                    Stat.Puhkino.oooinn = "ООО ИНН"; Stat.Puhkino.fio = "ФИО"; Stat.Puhkino.march = "Подольских Курсантов — Пушкино"; Stat.Puhkino.phone = "Телефон"; Stat.Puhkino.dt = "Данные водителя"; Stat.Puhkino.auto1 = "Марка машины"; Stat.Puhkino.auto2 = "Номер машины"; Stat.Puhkino.autoplomb = ""; Stat.Puhkino.sdach = "МО, г. Пушкино, Ярославское шоссе, 222"; Stat.Puhkino.poluch = "г. Москва, вн. тер. г. муниципальный округ Черемушки, ул. Намёткина, д. 12А, помещ. XVII, ком. 9.";
                    break;
                case 4:
                    Stat.Privolnay._listPal.Clear(); Stat.Privolnay._listGM.Clear(); Stat.Privolnay._listMesh.Clear(); Stat.Privolnay._listCont.Clear(); Stat.Privolnay._listSave.Clear(); Stat.Privolnay._listZas.Clear();
                    Stat.Privolnay.oooinn = "ООО ИНН"; Stat.Privolnay.fio = "ФИО"; Stat.Privolnay.march = "Подольских Курсантов — Привольная"; Stat.Privolnay.phone = "Телефон"; Stat.Privolnay.dt = "Данные водителя"; Stat.Privolnay.auto1 = "Марка машины"; Stat.Privolnay.auto2 = "Номер машины"; Stat.Privolnay.autoplomb = ""; Stat.Privolnay.sdach = "Привольная улица, 8"; Stat.Privolnay.poluch = "г. Москва, вн. тер. г. муниципальный округ Черемушки, ул. Намёткина, д. 12А, помещ. XVII, ком. 9.";
                    break;
                case 5:
                    Stat.Vehki._listPal.Clear(); Stat.Vehki._listGM.Clear(); Stat.Vehki._listMesh.Clear(); Stat.Vehki._listCont.Clear(); Stat.Vehki._listSave.Clear(); Stat.Vehki._listZas.Clear();
                    Stat.Vehki.oooinn = "ООО ИНН"; Stat.Vehki.fio = "ФИО"; Stat.Vehki.march = "Подольских Курсантов — Вешки"; Stat.Vehki.phone = "Телефон"; Stat.Vehki.dt = "Данные водителя"; Stat.Vehki.auto1 = "Марка машины"; Stat.Vehki.auto2 = "Номер машины"; Stat.Vehki.autoplomb = ""; Stat.Vehki.sdach = "Мытищинский р-н, ш. Липкинское, 2-й км, территория ТПЗ \"Алтуфьево\" вл.1, стр.1Б.)"; Stat.Vehki.poluch = "г. Москва, вн. тер. г. муниципальный округ Черемушки, ул. Намёткина, д. 12А, помещ. XVII, ком. 9.";
                    break;
                case 6:
                    Stat.Rybinovay._listPal.Clear(); Stat.Rybinovay._listGM.Clear(); Stat.Rybinovay._listMesh.Clear(); Stat.Rybinovay._listCont.Clear(); Stat.Rybinovay._listSave.Clear(); Stat.Rybinovay._listZas.Clear();
                    Stat.Rybinovay.oooinn = "ООО ИНН"; Stat.Rybinovay.fio = "ФИО"; Stat.Rybinovay.march = "Подольских Курсантов — Рябиновая"; Stat.Rybinovay.phone = "Телефон"; Stat.Rybinovay.dt = "Данные водителя"; Stat.Rybinovay.auto1 = "Марка машины"; Stat.Rybinovay.auto2 = "Номер машины"; Stat.Rybinovay.autoplomb = ""; Stat.Rybinovay.sdach = "Москва ул. Рябиновая 53 стр.2"; Stat.Rybinovay.poluch = "г. Москва, вн. тер. г. муниципальный округ Черемушки, ул. Намёткина, д. 12А, помещ. XVII, ком. 9.";
                    break;
                case 7:
                    Stat.Sharapovo._listPal.Clear(); Stat.Sharapovo._listGM.Clear(); Stat.Sharapovo._listMesh.Clear(); Stat.Sharapovo._listCont.Clear(); Stat.Sharapovo._listSave.Clear(); Stat.Sharapovo._listZas.Clear();
                    Stat.Sharapovo.oooinn = "ООО ИНН"; Stat.Sharapovo.fio = "ФИО"; Stat.Sharapovo.march = "Подольских Курсантов — Шарапово"; Stat.Sharapovo.phone = "Телефон"; Stat.Sharapovo.dt = "Данные водителя"; Stat.Sharapovo.auto1 = "Марка машины"; Stat.Sharapovo.auto2 = "Номер машины"; Stat.Sharapovo.autoplomb = ""; Stat.Sharapovo.sdach = "г. Москва, сельское поселение Марушкинское, д. Шарапово, ул.124 Придорожная, стр. 7А,стр1"; Stat.Sharapovo.poluch = "г. Москва, вн. тер. г. муниципальный округ Черемушки, ул. Намёткина, д. 12А, помещ. XVII, ком. 9.";
                    break;
                case 8:
                    Stat.Helkovskay._listPal.Clear(); Stat.Helkovskay._listGM.Clear(); Stat.Helkovskay._listMesh.Clear(); Stat.Helkovskay._listCont.Clear(); Stat.Helkovskay._listSave.Clear(); Stat.Helkovskay._listZas.Clear();
                    Stat.Helkovskay.oooinn = "ООО ИНН"; Stat.Helkovskay.fio = "ФИО"; Stat.Helkovskay.march = "Подольских Курсантов — Щёлковская"; Stat.Helkovskay.phone = "Телефон"; Stat.Helkovskay.dt = "Данные водителя"; Stat.Helkovskay.auto1 = "Марка машины"; Stat.Helkovskay.auto2 = "Номер машины"; Stat.Helkovskay.autoplomb = ""; Stat.Helkovskay.sdach = "Щелковское шоссе 100к100"; Stat.Helkovskay.poluch = "г. Москва, вн. тер. г. муниципальный округ Черемушки, ул. Намёткина, д. 12А, помещ. XVII, ком. 9.";
                    break;
                case 9:
                    Stat.Odincovo._listPal.Clear(); Stat.Odincovo._listGM.Clear(); Stat.Odincovo._listMesh.Clear(); Stat.Odincovo._listCont.Clear(); Stat.Odincovo._listSave.Clear(); Stat.Odincovo._listZas.Clear();
                    Stat.Odincovo.oooinn = "ООО ИНН"; Stat.Odincovo.fio = "ФИО"; Stat.Odincovo.march = "Подольских Курсантов — Одинцово"; Stat.Odincovo.phone = "Телефон"; Stat.Odincovo.dt = "Данные водителя"; Stat.Odincovo.auto1 = "Марка машины"; Stat.Odincovo.auto2 = "Номер машины"; Stat.Odincovo.autoplomb = ""; Stat.Odincovo.sdach = "Г. Одинцово, Ул Зеленая 10"; Stat.Odincovo.poluch = "г. Москва, вн. тер. г. муниципальный округ Черемушки, ул. Намёткина, д. 12А, помещ. XVII, ком. 9.";
                    break;
                case 10:
                    Stat.Skladohnay._listPal.Clear(); Stat.Skladohnay._listGM.Clear(); Stat.Skladohnay._listMesh.Clear(); Stat.Skladohnay._listCont.Clear(); Stat.Skladohnay._listSave.Clear(); Stat.Skladohnay._listZas.Clear();
                    Stat.Skladohnay.oooinn = "ООО ИНН"; Stat.Skladohnay.fio = "ФИО"; Stat.Skladohnay.march = "Подольских Курсантов — Складочная"; Stat.Skladohnay.phone = "Телефон"; Stat.Skladohnay.dt = "Данные водителя"; Stat.Skladohnay.auto1 = "Марка машины"; Stat.Skladohnay.auto2 = "Номер машины"; Stat.Skladohnay.autoplomb = ""; Stat.Skladohnay.sdach = "г.Москва, ул.Складочная 1с6"; Stat.Skladohnay.poluch = "г. Москва, вн. тер. г. муниципальный округ Черемушки, ул. Намёткина, д. 12А, помещ. XVII, ком. 9.";
                    break;
                case 11:
                    Stat.Pererva._listPal.Clear(); Stat.Pererva._listGM.Clear(); Stat.Pererva._listMesh.Clear(); Stat.Pererva._listCont.Clear(); Stat.Pererva._listSave.Clear(); Stat.Pererva._listZas.Clear();
                    Stat.Pererva.oooinn = "ООО ИНН"; Stat.Pererva.fio = "ФИО"; Stat.Pererva.march = "Подольских Курсантов — Перерва"; Stat.Pererva.phone = "Телефон"; Stat.Pererva.dt = "Данные водителя"; Stat.Pererva.auto1 = "Марка машины"; Stat.Pererva.auto2 = "Номер машины"; Stat.Pererva.autoplomb = ""; Stat.Pererva.sdach = "г. Москва,Перерва, 19с2"; Stat.Pererva.poluch = "г. Москва, вн. тер. г. муниципальный округ Черемушки, ул. Намёткина, д. 12А, помещ. XVII, ком. 9.";
                    break;
                case 12:
                    Stat.BUhunskay._listPal.Clear(); Stat.BUhunskay._listGM.Clear(); Stat.BUhunskay._listMesh.Clear(); Stat.BUhunskay._listCont.Clear(); Stat.BUhunskay._listSave.Clear(); Stat.BUhunskay._listZas.Clear();
                    Stat.BUhunskay.oooinn = "ООО ИНН"; Stat.BUhunskay.fio = "ФИО"; Stat.BUhunskay.march = "Подольских Курсантов — Большая Юшуньская"; Stat.BUhunskay.phone = "Телефон"; Stat.BUhunskay.dt = "Данные водителя"; Stat.BUhunskay.auto1 = "Марка машины"; Stat.BUhunskay.auto2 = "Номер машины"; Stat.BUhunskay.autoplomb = ""; Stat.BUhunskay.sdach = "ул Большая Юшуньская , д. 7"; Stat.BUhunskay.poluch = "г. Москва, вн. тер. г. муниципальный округ Черемушки, ул. Намёткина, д. 12А, помещ. XVII, ком. 9.";
                    break;
                case 13:
                    Stat.Egorevsk._listPal.Clear(); Stat.Egorevsk._listGM.Clear(); Stat.Egorevsk._listMesh.Clear(); Stat.Egorevsk._listCont.Clear(); Stat.Egorevsk._listSave.Clear(); Stat.Egorevsk._listZas.Clear();
                    Stat.Egorevsk.oooinn = "ООО ИНН"; Stat.Egorevsk.fio = "ФИО"; Stat.Egorevsk.march = "Подольских Курсантов — Егоревск"; Stat.Egorevsk.phone = "Телефон"; Stat.Egorevsk.dt = "Данные водителя"; Stat.Egorevsk.auto1 = "Марка машины"; Stat.Egorevsk.auto2 = "Номер машины"; Stat.Egorevsk.autoplomb = ""; Stat.Egorevsk.sdach = "г. Московская область, г.о. Егоревск г. Егоревск,ул Советская, 81"; Stat.Egorevsk.poluch = "г. Москва, вн. тер. г. муниципальный округ Черемушки, ул. Намёткина, д. 12А, помещ. XVII, ком. 9.";
                    break;
            }
        }
    }
}
