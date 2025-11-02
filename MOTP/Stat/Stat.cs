using System;
using System.Collections.Generic;

namespace Stat
{
    public static class Settings
    {
        public static string Dol = "";
        public static string FIO = "";

        public static string timePRB = "";
        public static string timeOTB = "";

        public static bool AddRem = true;

        public static List<string>[][] arr = new[]{
            // 0 - 5 lists; 6 - oooinn; 7 - fio; 8 - march; 9 - phone; 10 - dt; 11 - auto1; 12 - auto2; 13 - autoplomb;
            new List<string>[] { Himki._listPal, Himki._listGM, Himki._listMesh, Himki._listCont, Himki._listSave, Himki._listZas },
            new List<string>[] { Marta._listPal, Marta._listGM, Marta._listMesh, Marta._listCont, Marta._listSave, Marta._listZas },
            new List<string>[] { Puhkino._listPal, Puhkino._listGM, Puhkino._listMesh, Puhkino._listCont, Puhkino._listSave, Puhkino._listZas },
            new List<string>[] { Privolnay._listPal, Privolnay._listGM, Privolnay._listMesh, Privolnay._listCont, Privolnay._listSave, Privolnay._listZas },
            new List<string>[] { Vehki._listPal, Vehki._listGM, Vehki._listMesh, Vehki._listCont, Vehki._listSave, Vehki._listZas },
            new List<string>[] { Rybinovay._listPal, Rybinovay._listGM, Rybinovay._listMesh, Rybinovay._listCont, Rybinovay._listSave, Rybinovay._listZas },
            new List<string>[] { Sharapovo._listPal, Sharapovo._listGM, Sharapovo._listMesh, Sharapovo._listCont, Sharapovo._listSave, Sharapovo._listZas },
            new List<string>[] { Helkovskay._listPal, Helkovskay._listGM, Helkovskay._listMesh, Helkovskay._listCont, Helkovskay._listSave, Helkovskay._listZas },
            new List<string>[] { Odincovo._listPal, Odincovo._listGM, Odincovo._listMesh, Odincovo._listCont, Odincovo._listSave, Odincovo._listZas },
            new List<string>[] { Skladohnay._listPal, Skladohnay._listGM, Skladohnay._listMesh, Skladohnay._listCont, Skladohnay._listSave, Skladohnay._listZas },
            new List<string>[] { Pererva._listPal, Pererva._listGM, Pererva._listMesh, Pererva._listCont, Pererva._listSave, Pererva._listZas },
            new List<string>[] { BUhunskay._listPal, BUhunskay._listGM, BUhunskay._listMesh, BUhunskay._listCont, BUhunskay._listSave, BUhunskay._listZas },
            new List<string>[] { Egorevsk._listPal, Egorevsk._listGM, Egorevsk._listMesh, Egorevsk._listCont, Egorevsk._listSave, Egorevsk._listZas },
        };

        public static DateTime timeEnd = DateTime.MinValue;
    }

    public static class Himki
    {
        public static int numstation = 1;

        public static List<string> _listPal = new List<string>();
        public static List<string> _listGM = new List<string>();
        public static List<string> _listMesh = new List<string>();
        public static List<string> _listCont = new List<string>();
        public static List<string> _listSave = new List<string>();
        public static List<string> _listZas = new List<string>();

        public static string oooinn = "ООО ИНН";
        public static string fio = "ФИО";
        public static string march = "Подольских Курсантов — Химки";
        public static string phone = "Телефон";
        public static string dt = "Данные водителя";
        public static string auto1 = "Марка машины";
        public static string auto2 = "Номер машины";
        public static string autoplomb = "";

        public static string sdach = "г. Москва, Химки, проезд Коммунальный, д. 30а, стр. 1";
        public static string poluch = "г. Москва, вн. тер. г. муниципальный округ Черемушки, ул. Намёткина, д. 12А, помещ. XVII, ком. 9.";
        public static string ts = "Transit city (MOO-3)";
    }

    public static class Marta
    {
        public static int numstation = 2;

        public static List<string> _listPal = new List<string>();
        public static List<string> _listGM = new List<string>();
        public static List<string> _listMesh = new List<string>();
        public static List<string> _listCont = new List<string>();
        public static List<string> _listSave = new List<string>();
        public static List<string> _listZas = new List<string>();

        public static string oooinn = "ООО ИНН";
        public static string fio = "ФИО";
        public static string march = "Подольских Курсантов — 8-марта";
        public static string phone = "Телефон";
        public static string dt = "Данные водителя";
        public static string auto1 = "Марка машины";
        public static string auto2 = "Номер машины";
        public static string autoplomb = "";

        public static string sdach = "г. Москва, ул. 8 марта, д. 14 с. 1";
        public static string poluch = "г. Москва, вн. тер. г. муниципальный округ Черемушки, ул. Намёткина, д. 12А, помещ. XVII, ком. 9.";
        public static string ts = "Transit city (MOO-5)";
    }

    public static class Puhkino
    {
        public static int numstation = 3;

        public static List<string> _listPal = new List<string>();
        public static List<string> _listGM = new List<string>();
        public static List<string> _listMesh = new List<string>();
        public static List<string> _listCont = new List<string>();
        public static List<string> _listSave = new List<string>();
        public static List<string> _listZas = new List<string>();

        public static string oooinn = "ООО ИНН";
        public static string fio = "ФИО";
        public static string march = "Подольских Курсантов — Пушкино";
        public static string phone = "Телефон";
        public static string dt = "Данные водителя";
        public static string auto1 = "Марка машины";
        public static string auto2 = "Номер машины";
        public static string autoplomb = "";

        public static string sdach = "МО, г. Пушкино, Ярославское шоссе, 222";
        public static string poluch = "г. Москва, вн. тер. г. муниципальный округ Черемушки, ул. Намёткина, д. 12А, помещ. XVII, ком. 9.";
        public static string ts = "Transit city (MOO-4)";
    }

    public static class Privolnay
    {
        public static int numstation = 4;

        public static List<string> _listPal = new List<string>();
        public static List<string> _listGM = new List<string>();
        public static List<string> _listMesh = new List<string>();
        public static List<string> _listCont = new List<string>();
        public static List<string> _listSave = new List<string>();
        public static List<string> _listZas = new List<string>();

        public static string oooinn = "ООО ИНН";
        public static string fio = "ФИО";
        public static string march = "Подольских Курсантов — Привольная";
        public static string phone = "Телефон";
        public static string dt = "Данные водителя";
        public static string auto1 = "Марка машины";
        public static string auto2 = "Номер машины";
        public static string autoplomb = "";

        public static string sdach = "Привольная улица, 8";
        public static string poluch = "г. Москва, вн. тер. г. муниципальный округ Черемушки, ул. Намёткина, д. 12А, помещ. XVII, ком. 9.";
        public static string ts = "Transit city (MSK-2)";
    }

    public static class Vehki
    {
        public static int numstation = 5;

        public static List<string> _listPal = new List<string>();
        public static List<string> _listGM = new List<string>();
        public static List<string> _listMesh = new List<string>();
        public static List<string> _listCont = new List<string>();
        public static List<string> _listSave = new List<string>();
        public static List<string> _listZas = new List<string>();

        public static string oooinn = "ООО ИНН";
        public static string fio = "ФИО";
        public static string march = "Подольских Курсантов — Вешки";
        public static string phone = "Телефон";
        public static string dt = "Данные водителя";
        public static string auto1 = "Марка машины";
        public static string auto2 = "Номер машины";
        public static string autoplomb = "";

        public static string sdach = "Мытищинский р-н, ш. Липкинское, 2-й км, территория ТПЗ \"Алтуфьево\" вл.1, стр.1Б.)";
        public static string poluch = "г. Москва, вн. тер. г. муниципальный округ Черемушки, ул. Намёткина, д. 12А, помещ. XVII, ком. 9.";
        public static string ts = "Transit city (MOO-10)";
    }

    public static class Rybinovay
    {
        public static int numstation = 6;

        public static List<string> _listPal = new List<string>();
        public static List<string> _listGM = new List<string>();
        public static List<string> _listMesh = new List<string>();
        public static List<string> _listCont = new List<string>();
        public static List<string> _listSave = new List<string>();
        public static List<string> _listZas = new List<string>();

        public static string oooinn = "ООО ИНН";
        public static string fio = "ФИО";
        public static string march = "Подольских Курсантов — Рябиновая";
        public static string phone = "Телефон";
        public static string dt = "Данные водителя";
        public static string auto1 = "Марка машины";
        public static string auto2 = "Номер машины";
        public static string autoplomb = "";

        public static string sdach = "г. Москва ул. Рябиновая 53 стр.2";
        public static string poluch = "г. Москва, вн. тер. г. муниципальный округ Черемушки, ул. Намёткина, д. 12А, помещ. XVII, ком. 9.";
        public static string ts = "Transit city (MSK-4)";
    }

    public static class Sharapovo
    {
        public static int numstation = 7;

        public static List<string> _listPal = new List<string>();
        public static List<string> _listGM = new List<string>();
        public static List<string> _listMesh = new List<string>();
        public static List<string> _listCont = new List<string>();
        public static List<string> _listSave = new List<string>();
        public static List<string> _listZas = new List<string>();

        public static string oooinn = "ООО ИНН";
        public static string fio = "ФИО";
        public static string march = "Подольских Курсантов — Шарапово";
        public static string phone = "Телефон";
        public static string dt = "Данные водителя";
        public static string auto1 = "Марка машины";
        public static string auto2 = "Номер машины";
        public static string autoplomb = "";

        public static string sdach = "г. Москва, сельское поселение Марушкинское, д. Шарапово, ул.124 Придорожная, стр. 7А,стр1";
        public static string poluch = "г. Москва, вн. тер. г. муниципальный округ Черемушки, ул. Намёткина, д. 12А, помещ. XVII, ком. 9.";
        public static string ts = "Transit (SHA)";
    }

    public static class Helkovskay
    {
        public static int numstation = 8;

        public static List<string> _listPal = new List<string>();
        public static List<string> _listGM = new List<string>();
        public static List<string> _listMesh = new List<string>();
        public static List<string> _listCont = new List<string>();
        public static List<string> _listSave = new List<string>();
        public static List<string> _listZas = new List<string>();

        public static string oooinn = "ООО ИНН";
        public static string fio = "ФИО";
        public static string march = "Подольских Курсантов — Щёлковская";
        public static string phone = "Телефон";
        public static string dt = "Данные водителя";
        public static string auto1 = "Марка машины";
        public static string auto2 = "Номер машины";
        public static string autoplomb = "";

        public static string sdach = "Щелковское шоссе 100к100";
        public static string poluch = "г. Москва, вн. тер. г. муниципальный округ Черемушки, ул. Намёткина, д. 12А, помещ. XVII, ком. 9.";
        public static string ts = "Transit city (MSK-9)";
    }

    public static class Odincovo
    {
        public static int numstation = 9;

        public static List<string> _listPal = new List<string>();
        public static List<string> _listGM = new List<string>();
        public static List<string> _listMesh = new List<string>();
        public static List<string> _listCont = new List<string>();
        public static List<string> _listSave = new List<string>();
        public static List<string> _listZas = new List<string>();

        public static string oooinn = "ООО ИНН";
        public static string fio = "ФИО";
        public static string march = "Подольских Курсантов — Одинцово";
        public static string phone = "Телефон";
        public static string dt = "Данные водителя";
        public static string auto1 = "Марка машины";
        public static string auto2 = "Номер машины";
        public static string autoplomb = "";

        public static string sdach = "г. Одинцово, Ул Зеленая 10";
        public static string poluch = "г. Москва, вн. тер. г. муниципальный округ Черемушки, ул. Намёткина, д. 12А, помещ. XVII, ком. 9.";
        public static string ts = "Transit city (MOO-1)";
    }

    public static class Skladohnay
    {
        public static int numstation = 10;

        public static List<string> _listPal = new List<string>();
        public static List<string> _listGM = new List<string>();
        public static List<string> _listMesh = new List<string>();
        public static List<string> _listCont = new List<string>();
        public static List<string> _listSave = new List<string>();
        public static List<string> _listZas = new List<string>();

        public static string oooinn = "ООО ИНН";
        public static string fio = "ФИО";
        public static string march = "Подольских Курсантов — Складочная";
        public static string phone = "Телефон";
        public static string dt = "Данные водителя";
        public static string auto1 = "Марка машины";
        public static string auto2 = "Номер машины";
        public static string autoplomb = "";

        public static string sdach = "г. Москва, ул.Складочная 1с6";
        public static string poluch = "г. Москва, вн. тер. г. муниципальный округ Черемушки, ул. Намёткина, д. 12А, помещ. XVII, ком. 9.";
        public static string ts = "Transit city (MSK-10)";
    }

    public static class Pererva
    {
        public static int numstation = 11;

        public static List<string> _listPal = new List<string>();
        public static List<string> _listGM = new List<string>();
        public static List<string> _listMesh = new List<string>();
        public static List<string> _listCont = new List<string>();
        public static List<string> _listSave = new List<string>();
        public static List<string> _listZas = new List<string>();

        public static string oooinn = "ООО ИНН";
        public static string fio = "ФИО";
        public static string march = "Подольских Курсантов — Перерва";
        public static string phone = "Телефон";
        public static string dt = "Данные водителя";
        public static string auto1 = "Марка машины";
        public static string auto2 = "Номер машины";
        public static string autoplomb = "";

        public static string sdach = "г. Москва,Перерва, 19с2";
        public static string poluch = "г. Москва, вн. тер. г. муниципальный округ Черемушки, ул. Намёткина, д. 12А, помещ. XVII, ком. 9.";
        public static string ts = "Transit city (HUB-10)";
    }

    public class BUhunskay
    {
        public static int numstation = 12;

        public static List<string> _listPal = new List<string>();
        public static List<string> _listGM = new List<string>();
        public static List<string> _listMesh = new List<string>();
        public static List<string> _listCont = new List<string>();
        public static List<string> _listSave = new List<string>();
        public static List<string> _listZas = new List<string>();

        public static string oooinn = "ООО ИНН";
        public static string fio = "ФИО";
        public static string march = "Подольских Курсантов — Большая Юшуньская";
        public static string phone = "Телефон";
        public static string dt = "Данные водителя";
        public static string auto1 = "Марка машины";
        public static string auto2 = "Номер машины";
        public static string autoplomb = "";

        public static string sdach = "ул Большая Юшуньская, д. 7";
        public static string poluch = "г. Москва, вн. тер. г. муниципальный округ Черемушки, ул. Намёткина, д. 12А, помещ. XVII, ком. 9.";
        public static string ts = "Transit city (HUB-12)";
    }
    public class Egorevsk
    {
        public static int numstation = 13;

        public static List<string> _listPal = new List<string>();
        public static List<string> _listGM = new List<string>();
        public static List<string> _listMesh = new List<string>();
        public static List<string> _listCont = new List<string>();
        public static List<string> _listSave = new List<string>();
        public static List<string> _listZas = new List<string>();

        public static string oooinn = "ООО ИНН";
        public static string fio = "ФИО";
        public static string march = "Подольских Курсантов — Егоревск";
        public static string phone = "Телефон";
        public static string dt = "Данные водителя";
        public static string auto1 = "Марка машины";
        public static string auto2 = "Номер машины";
        public static string autoplomb = "";

        public static string sdach = "г. Московская область, г.о. Егоревск г. Егоревск, ул Советская, 81";
        public static string poluch = "г. Москва, вн. тер. г. муниципальный округ Черемушки, ул. Намёткина, д. 12А, помещ. XVII, ком. 9.";
        public static string ts = "";
    }
}