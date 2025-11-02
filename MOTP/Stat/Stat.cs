using System;
using System.Collections.Generic;

namespace Stat
{
    // -------------------------------------------------------------
    // Глобальные настройки
    // -------------------------------------------------------------
    public static class Settings
    {
        public static string Dol = "";
        public static string FIO = "";

        public static string timePRB = "";
        public static string timeOTB = "";

        public static bool AddRem = true;

        public static List<string>[][] arr =
        [
            [Himki._listPal, Himki._listGM, Himki._listMesh, Himki._listCont, Himki._listSave, Himki._listZas],
            [Marta._listPal, Marta._listGM, Marta._listMesh, Marta._listCont, Marta._listSave, Marta._listZas],
            [Puhkino._listPal, Puhkino._listGM, Puhkino._listMesh, Puhkino._listCont, Puhkino._listSave, Puhkino._listZas],
            [Privolnay._listPal, Privolnay._listGM, Privolnay._listMesh, Privolnay._listCont, Privolnay._listSave, Privolnay._listZas],
            [Vehki._listPal, Vehki._listGM, Vehki._listMesh, Vehki._listCont, Vehki._listSave, Vehki._listZas],
            [Rybinovay._listPal, Rybinovay._listGM, Rybinovay._listMesh, Rybinovay._listCont, Rybinovay._listSave, Rybinovay._listZas],
            [Sharapovo._listPal, Sharapovo._listGM, Sharapovo._listMesh, Sharapovo._listCont, Sharapovo._listSave, Sharapovo._listZas],
            [Helkovskay._listPal, Helkovskay._listGM, Helkovskay._listMesh, Helkovskay._listCont, Helkovskay._listSave, Helkovskay._listZas],
            [Odincovo._listPal, Odincovo._listGM, Odincovo._listMesh, Odincovo._listCont, Odincovo._listSave, Odincovo._listZas],
            [Skladohnay._listPal, Skladohnay._listGM, Skladohnay._listMesh, Skladohnay._listCont, Skladohnay._listSave, Skladohnay._listZas],
            [Pererva._listPal, Pererva._listGM, Pererva._listMesh, Pererva._listCont, Pererva._listSave, Pererva._listZas],
            [BUhunskay._listPal, BUhunskay._listGM, BUhunskay._listMesh, BUhunskay._listCont, BUhunskay._listSave, BUhunskay._listZas],
            [Egorevsk._listPal, Egorevsk._listGM, Egorevsk._listMesh, Egorevsk._listCont, Egorevsk._listSave, Egorevsk._listZas],
        ];

        public static DateTime timeEnd = DateTime.MinValue;
    }

    // -------------------------------------------------------------
    // Обобщённая структура данных для всех станций
    // -------------------------------------------------------------
    public class StationData
    {
        public int numstation;

        public List<string> _listPal = [];
        public List<string> _listGM = [];
        public List<string> _listMesh = [];
        public List<string> _listCont = [];
        public List<string> _listSave = [];
        public List<string> _listZas = [];

        public string oooinn = "ООО ИНН";
        public string fio = "ФИО";
        public string march = "";
        public string phone = "Телефон";
        public string dt = "Данные водителя";
        public string auto1 = "Марка машины";
        public string auto2 = "Номер машины";
        public string autoplomb = "";

        public string sdach = "";
        public string poluch = "г. Москва, вн. тер. г. муниципальный округ Черемушки, ул. Намёткина, д. 12А, помещ. XVII, ком. 9.";
        public string ts = "";
    }

    // -------------------------------------------------------------
    // Универсальный шаблон станции
    // -------------------------------------------------------------
    public static class StationTemplate
    {
        static void CopyLists(StationData source, StationData target)
        {
            target._listPal = source._listPal;
            target._listGM = source._listGM;
            target._listMesh = source._listMesh;
            target._listCont = source._listCont;
            target._listSave = source._listSave;
            target._listZas = source._listZas;
        }

        static void CopyData(StationData source, StationData target)
        {
            target.oooinn = source.oooinn;
            target.fio = source.fio;
            target.march = source.march;
            target.phone = source.phone;
            target.dt = source.dt;
            target.auto1 = source.auto1;
            target.auto2 = source.auto2;
            target.autoplomb = source.autoplomb;
            target.sdach = source.sdach;
            target.poluch = source.poluch;
            target.ts = source.ts;
        }
    }

    // -------------------------------------------------------------
    // Определения всех станций
    // -------------------------------------------------------------
    public static class Himki
    {
        public static StationData Data = new()
        {
            numstation = 1,
            march = "Подольских Курсантов — Химки",
            sdach = "г. Москва, Химки, проезд Коммунальный, д. 30а, стр. 1",
            ts = "Transit city (MOO-3)"
        };

        public static List<string> _listPal { get => Data._listPal; set => Data._listPal = value; }
        public static List<string> _listGM { get => Data._listGM; set => Data._listGM = value; }
        public static List<string> _listMesh { get => Data._listMesh; set => Data._listMesh = value; }
        public static List<string> _listCont { get => Data._listCont; set => Data._listCont = value; }
        public static List<string> _listSave { get => Data._listSave; set => Data._listSave = value; }
        public static List<string> _listZas { get => Data._listZas; set => Data._listZas = value; }

        public static int numstation { get => Data.numstation; set => Data.numstation = value; }
        public static string oooinn { get => Data.oooinn; set => Data.oooinn = value; }
        public static string fio { get => Data.fio; set => Data.fio = value; }
        public static string march { get => Data.march; set => Data.march = value; }
        public static string phone { get => Data.phone; set => Data.phone = value; }
        public static string dt { get => Data.dt; set => Data.dt = value; }
        public static string auto1 { get => Data.auto1; set => Data.auto1 = value; }
        public static string auto2 { get => Data.auto2; set => Data.auto2 = value; }
        public static string autoplomb { get => Data.autoplomb; set => Data.autoplomb = value; }
        public static string sdach { get => Data.sdach; set => Data.sdach = value; }
        public static string poluch { get => Data.poluch; set => Data.poluch = value; }
        public static string ts { get => Data.ts; set => Data.ts = value; }
    }

    public static class Marta
    {
        public static StationData Data = new()
        {
            numstation = 2,
            march = "Подольских Курсантов — 8-марта",
            sdach = "г. Москва, ул. 8 марта, д. 14 с. 1",
            ts = "Transit city (MOO-5)"
        };

        public static List<string> _listPal { get => Data._listPal; set => Data._listPal = value; }
        public static List<string> _listGM { get => Data._listGM; set => Data._listGM = value; }
        public static List<string> _listMesh { get => Data._listMesh; set => Data._listMesh = value; }
        public static List<string> _listCont { get => Data._listCont; set => Data._listCont = value; }
        public static List<string> _listSave { get => Data._listSave; set => Data._listSave = value; }
        public static List<string> _listZas { get => Data._listZas; set => Data._listZas = value; }

        public static int numstation { get => Data.numstation; set => Data.numstation = value; }
        public static string oooinn { get => Data.oooinn; set => Data.oooinn = value; }
        public static string fio { get => Data.fio; set => Data.fio = value; }
        public static string march { get => Data.march; set => Data.march = value; }
        public static string phone { get => Data.phone; set => Data.phone = value; }
        public static string dt { get => Data.dt; set => Data.dt = value; }
        public static string auto1 { get => Data.auto1; set => Data.auto1 = value; }
        public static string auto2 { get => Data.auto2; set => Data.auto2 = value; }
        public static string autoplomb { get => Data.autoplomb; set => Data.autoplomb = value; }
        public static string sdach { get => Data.sdach; set => Data.sdach = value; }
        public static string poluch { get => Data.poluch; set => Data.poluch = value; }
        public static string ts { get => Data.ts; set => Data.ts = value; }
    }

    public static class Puhkino
    {
        public static StationData Data = new()
        {
            numstation = 3,
            march = "Подольских Курсантов — Пушкино",
            sdach = "МО, г. Пушкино, Ярославское шоссе, 222",
            ts = "Transit city (MOO-4)"
        };

        public static List<string> _listPal { get => Data._listPal; set => Data._listPal = value; }
        public static List<string> _listGM { get => Data._listGM; set => Data._listGM = value; }
        public static List<string> _listMesh { get => Data._listMesh; set => Data._listMesh = value; }
        public static List<string> _listCont { get => Data._listCont; set => Data._listCont = value; }
        public static List<string> _listSave { get => Data._listSave; set => Data._listSave = value; }
        public static List<string> _listZas { get => Data._listZas; set => Data._listZas = value; }

        public static int numstation { get => Data.numstation; set => Data.numstation = value; }
        public static string oooinn { get => Data.oooinn; set => Data.oooinn = value; }
        public static string fio { get => Data.fio; set => Data.fio = value; }
        public static string march { get => Data.march; set => Data.march = value; }
        public static string phone { get => Data.phone; set => Data.phone = value; }
        public static string dt { get => Data.dt; set => Data.dt = value; }
        public static string auto1 { get => Data.auto1; set => Data.auto1 = value; }
        public static string auto2 { get => Data.auto2; set => Data.auto2 = value; }
        public static string autoplomb { get => Data.autoplomb; set => Data.autoplomb = value; }
        public static string sdach { get => Data.sdach; set => Data.sdach = value; }
        public static string poluch { get => Data.poluch; set => Data.poluch = value; }
        public static string ts { get => Data.ts; set => Data.ts = value; }
    }

    public static class Privolnay
    {
        public static StationData Data = new()
        {
            numstation = 4,
            march = "Подольских Курсантов — Привольная",
            sdach = "Привольная улица, 8",
            ts = "Transit city (MSK-2)"
        };

        public static List<string> _listPal { get => Data._listPal; set => Data._listPal = value; }
        public static List<string> _listGM { get => Data._listGM; set => Data._listGM = value; }
        public static List<string> _listMesh { get => Data._listMesh; set => Data._listMesh = value; }
        public static List<string> _listCont { get => Data._listCont; set => Data._listCont = value; }
        public static List<string> _listSave { get => Data._listSave; set => Data._listSave = value; }
        public static List<string> _listZas { get => Data._listZas; set => Data._listZas = value; }

        public static int numstation { get => Data.numstation; set => Data.numstation = value; }
        public static string oooinn { get => Data.oooinn; set => Data.oooinn = value; }
        public static string fio { get => Data.fio; set => Data.fio = value; }
        public static string march { get => Data.march; set => Data.march = value; }
        public static string phone { get => Data.phone; set => Data.phone = value; }
        public static string dt { get => Data.dt; set => Data.dt = value; }
        public static string auto1 { get => Data.auto1; set => Data.auto1 = value; }
        public static string auto2 { get => Data.auto2; set => Data.auto2 = value; }
        public static string autoplomb { get => Data.autoplomb; set => Data.autoplomb = value; }
        public static string sdach { get => Data.sdach; set => Data.sdach = value; }
        public static string poluch { get => Data.poluch; set => Data.poluch = value; }
        public static string ts { get => Data.ts; set => Data.ts = value; }
    }

    public static class Vehki
    {
        public static StationData Data = new()
        {
            numstation = 5,
            march = "Подольских Курсантов — Вешки",
            sdach = "Мытищинский р-н, ш. Липкинское, 2-й км, территория ТПЗ \"Алтуфьево\" вл.1, стр.1Б.)",
            ts = "Transit city (MOO-10)"
        };

        public static List<string> _listPal { get => Data._listPal; set => Data._listPal = value; }
        public static List<string> _listGM { get => Data._listGM; set => Data._listGM = value; }
        public static List<string> _listMesh { get => Data._listMesh; set => Data._listMesh = value; }
        public static List<string> _listCont { get => Data._listCont; set => Data._listCont = value; }
        public static List<string> _listSave { get => Data._listSave; set => Data._listSave = value; }
        public static List<string> _listZas { get => Data._listZas; set => Data._listZas = value; }

        public static int numstation { get => Data.numstation; set => Data.numstation = value; }
        public static string oooinn { get => Data.oooinn; set => Data.oooinn = value; }
        public static string fio { get => Data.fio; set => Data.fio = value; }
        public static string march { get => Data.march; set => Data.march = value; }
        public static string phone { get => Data.phone; set => Data.phone = value; }
        public static string dt { get => Data.dt; set => Data.dt = value; }
        public static string auto1 { get => Data.auto1; set => Data.auto1 = value; }
        public static string auto2 { get => Data.auto2; set => Data.auto2 = value; }
        public static string autoplomb { get => Data.autoplomb; set => Data.autoplomb = value; }
        public static string sdach { get => Data.sdach; set => Data.sdach = value; }
        public static string poluch { get => Data.poluch; set => Data.poluch = value; }
        public static string ts { get => Data.ts; set => Data.ts = value; }
    }

    public static class Rybinovay
    {
        public static StationData Data = new()
        {
            numstation = 6,
            march = "Подольских Курсантов — Рябиновая",
            sdach = "г. Москва ул. Рябиновая 53 стр.2",
            ts = "Transit city (MSK-4)"
        };

        public static List<string> _listPal { get => Data._listPal; set => Data._listPal = value; }
        public static List<string> _listGM { get => Data._listGM; set => Data._listGM = value; }
        public static List<string> _listMesh { get => Data._listMesh; set => Data._listMesh = value; }
        public static List<string> _listCont { get => Data._listCont; set => Data._listCont = value; }
        public static List<string> _listSave { get => Data._listSave; set => Data._listSave = value; }
        public static List<string> _listZas { get => Data._listZas; set => Data._listZas = value; }

        public static int numstation { get => Data.numstation; set => Data.numstation = value; }
        public static string oooinn { get => Data.oooinn; set => Data.oooinn = value; }
        public static string fio { get => Data.fio; set => Data.fio = value; }
        public static string march { get => Data.march; set => Data.march = value; }
        public static string phone { get => Data.phone; set => Data.phone = value; }
        public static string dt { get => Data.dt; set => Data.dt = value; }
        public static string auto1 { get => Data.auto1; set => Data.auto1 = value; }
        public static string auto2 { get => Data.auto2; set => Data.auto2 = value; }
        public static string autoplomb { get => Data.autoplomb; set => Data.autoplomb = value; }
        public static string sdach { get => Data.sdach; set => Data.sdach = value; }
        public static string poluch { get => Data.poluch; set => Data.poluch = value; }
        public static string ts { get => Data.ts; set => Data.ts = value; }
    }

    public static class Sharapovo
    {
        public static StationData Data = new()
        {
            numstation = 7,
            march = "Подольских Курсантов — Шарапово",
            sdach = "г. Москва, сельское поселение Марушкинское, д. Шарапово, ул.124 Придорожная, стр. 7А,стр1",
            ts = "Transit (SHA)"
        };

        public static List<string> _listPal { get => Data._listPal; set => Data._listPal = value; }
        public static List<string> _listGM { get => Data._listGM; set => Data._listGM = value; }
        public static List<string> _listMesh { get => Data._listMesh; set => Data._listMesh = value; }
        public static List<string> _listCont { get => Data._listCont; set => Data._listCont = value; }
        public static List<string> _listSave { get => Data._listSave; set => Data._listSave = value; }
        public static List<string> _listZas { get => Data._listZas; set => Data._listZas = value; }

        public static int numstation { get => Data.numstation; set => Data.numstation = value; }
        public static string oooinn { get => Data.oooinn; set => Data.oooinn = value; }
        public static string fio { get => Data.fio; set => Data.fio = value; }
        public static string march { get => Data.march; set => Data.march = value; }
        public static string phone { get => Data.phone; set => Data.phone = value; }
        public static string dt { get => Data.dt; set => Data.dt = value; }
        public static string auto1 { get => Data.auto1; set => Data.auto1 = value; }
        public static string auto2 { get => Data.auto2; set => Data.auto2 = value; }
        public static string autoplomb { get => Data.autoplomb; set => Data.autoplomb = value; }
        public static string sdach { get => Data.sdach; set => Data.sdach = value; }
        public static string poluch { get => Data.poluch; set => Data.poluch = value; }
        public static string ts { get => Data.ts; set => Data.ts = value; }
    }

    public static class Helkovskay
    {
        public static StationData Data = new()
        {
            numstation = 8,
            march = "Подольских Курсантов — Щёлковская",
            sdach = "Щелковское шоссе 100к100",
            ts = "Transit city (MSK-9)"
        };

        public static List<string> _listPal { get => Data._listPal; set => Data._listPal = value; }
        public static List<string> _listGM { get => Data._listGM; set => Data._listGM = value; }
        public static List<string> _listMesh { get => Data._listMesh; set => Data._listMesh = value; }
        public static List<string> _listCont { get => Data._listCont; set => Data._listCont = value; }
        public static List<string> _listSave { get => Data._listSave; set => Data._listSave = value; }
        public static List<string> _listZas { get => Data._listZas; set => Data._listZas = value; }

        public static int numstation { get => Data.numstation; set => Data.numstation = value; }
        public static string oooinn { get => Data.oooinn; set => Data.oooinn = value; }
        public static string fio { get => Data.fio; set => Data.fio = value; }
        public static string march { get => Data.march; set => Data.march = value; }
        public static string phone { get => Data.phone; set => Data.phone = value; }
        public static string dt { get => Data.dt; set => Data.dt = value; }
        public static string auto1 { get => Data.auto1; set => Data.auto1 = value; }
        public static string auto2 { get => Data.auto2; set => Data.auto2 = value; }
        public static string autoplomb { get => Data.autoplomb; set => Data.autoplomb = value; }
        public static string sdach { get => Data.sdach; set => Data.sdach = value; }
        public static string poluch { get => Data.poluch; set => Data.poluch = value; }
        public static string ts { get => Data.ts; set => Data.ts = value; }
    }

    public static class Odincovo
    {
        public static StationData Data = new()
        {
            numstation = 9,
            march = "Подольских Курсантов — Щёлковская",
            sdach = "г. Одинцово, Ул Зеленая 10",
            ts = "Transit city (MOO-1)"
        };

        public static List<string> _listPal { get => Data._listPal; set => Data._listPal = value; }
        public static List<string> _listGM { get => Data._listGM; set => Data._listGM = value; }
        public static List<string> _listMesh { get => Data._listMesh; set => Data._listMesh = value; }
        public static List<string> _listCont { get => Data._listCont; set => Data._listCont = value; }
        public static List<string> _listSave { get => Data._listSave; set => Data._listSave = value; }
        public static List<string> _listZas { get => Data._listZas; set => Data._listZas = value; }

        public static int numstation { get => Data.numstation; set => Data.numstation = value; }
        public static string oooinn { get => Data.oooinn; set => Data.oooinn = value; }
        public static string fio { get => Data.fio; set => Data.fio = value; }
        public static string march { get => Data.march; set => Data.march = value; }
        public static string phone { get => Data.phone; set => Data.phone = value; }
        public static string dt { get => Data.dt; set => Data.dt = value; }
        public static string auto1 { get => Data.auto1; set => Data.auto1 = value; }
        public static string auto2 { get => Data.auto2; set => Data.auto2 = value; }
        public static string autoplomb { get => Data.autoplomb; set => Data.autoplomb = value; }
        public static string sdach { get => Data.sdach; set => Data.sdach = value; }
        public static string poluch { get => Data.poluch; set => Data.poluch = value; }
        public static string ts { get => Data.ts; set => Data.ts = value; }
    }

    public static class Skladohnay
    {
        public static StationData Data = new()
        {
            numstation = 10,
            march = "Подольских Курсантов — Складочная",
            sdach = "г. Москва, ул.Складочная 1с6",
            ts = "Transit city (MSK-10)"
        };

        public static List<string> _listPal { get => Data._listPal; set => Data._listPal = value; }
        public static List<string> _listGM { get => Data._listGM; set => Data._listGM = value; }
        public static List<string> _listMesh { get => Data._listMesh; set => Data._listMesh = value; }
        public static List<string> _listCont { get => Data._listCont; set => Data._listCont = value; }
        public static List<string> _listSave { get => Data._listSave; set => Data._listSave = value; }
        public static List<string> _listZas { get => Data._listZas; set => Data._listZas = value; }

        public static int numstation { get => Data.numstation; set => Data.numstation = value; }
        public static string oooinn { get => Data.oooinn; set => Data.oooinn = value; }
        public static string fio { get => Data.fio; set => Data.fio = value; }
        public static string march { get => Data.march; set => Data.march = value; }
        public static string phone { get => Data.phone; set => Data.phone = value; }
        public static string dt { get => Data.dt; set => Data.dt = value; }
        public static string auto1 { get => Data.auto1; set => Data.auto1 = value; }
        public static string auto2 { get => Data.auto2; set => Data.auto2 = value; }
        public static string autoplomb { get => Data.autoplomb; set => Data.autoplomb = value; }
        public static string sdach { get => Data.sdach; set => Data.sdach = value; }
        public static string poluch { get => Data.poluch; set => Data.poluch = value; }
        public static string ts { get => Data.ts; set => Data.ts = value; }
    }

    public static class Pererva
    {
        public static StationData Data = new()
        {
            numstation = 11,
            march = "Подольских Курсантов — Перерва",
            sdach = "г. Москва,Перерва, 19с2",
            ts = "Transit city (HUB-10)"
        };

        public static List<string> _listPal { get => Data._listPal; set => Data._listPal = value; }
        public static List<string> _listGM { get => Data._listGM; set => Data._listGM = value; }
        public static List<string> _listMesh { get => Data._listMesh; set => Data._listMesh = value; }
        public static List<string> _listCont { get => Data._listCont; set => Data._listCont = value; }
        public static List<string> _listSave { get => Data._listSave; set => Data._listSave = value; }
        public static List<string> _listZas { get => Data._listZas; set => Data._listZas = value; }

        public static int numstation { get => Data.numstation; set => Data.numstation = value; }
        public static string oooinn { get => Data.oooinn; set => Data.oooinn = value; }
        public static string fio { get => Data.fio; set => Data.fio = value; }
        public static string march { get => Data.march; set => Data.march = value; }
        public static string phone { get => Data.phone; set => Data.phone = value; }
        public static string dt { get => Data.dt; set => Data.dt = value; }
        public static string auto1 { get => Data.auto1; set => Data.auto1 = value; }
        public static string auto2 { get => Data.auto2; set => Data.auto2 = value; }
        public static string autoplomb { get => Data.autoplomb; set => Data.autoplomb = value; }
        public static string sdach { get => Data.sdach; set => Data.sdach = value; }
        public static string poluch { get => Data.poluch; set => Data.poluch = value; }
        public static string ts { get => Data.ts; set => Data.ts = value; }
    }

    public static class BUhunskay
    {
        public static StationData Data = new()
        {
            numstation = 12,
            march = "Подольских Курсантов — Большая Юшуньская",
            sdach = "ул Большая Юшуньская, д. 7",
            ts = "Transit city (HUB-12)"
        };

        public static List<string> _listPal { get => Data._listPal; set => Data._listPal = value; }
        public static List<string> _listGM { get => Data._listGM; set => Data._listGM = value; }
        public static List<string> _listMesh { get => Data._listMesh; set => Data._listMesh = value; }
        public static List<string> _listCont { get => Data._listCont; set => Data._listCont = value; }
        public static List<string> _listSave { get => Data._listSave; set => Data._listSave = value; }
        public static List<string> _listZas { get => Data._listZas; set => Data._listZas = value; }

        public static int numstation { get => Data.numstation; set => Data.numstation = value; }
        public static string oooinn { get => Data.oooinn; set => Data.oooinn = value; }
        public static string fio { get => Data.fio; set => Data.fio = value; }
        public static string march { get => Data.march; set => Data.march = value; }
        public static string phone { get => Data.phone; set => Data.phone = value; }
        public static string dt { get => Data.dt; set => Data.dt = value; }
        public static string auto1 { get => Data.auto1; set => Data.auto1 = value; }
        public static string auto2 { get => Data.auto2; set => Data.auto2 = value; }
        public static string autoplomb { get => Data.autoplomb; set => Data.autoplomb = value; }
        public static string sdach { get => Data.sdach; set => Data.sdach = value; }
        public static string poluch { get => Data.poluch; set => Data.poluch = value; }
        public static string ts { get => Data.ts; set => Data.ts = value; }
    }

    public static class Egorevsk
    {
        public static StationData Data = new()
        {
            numstation = 13,
            march = "Подольских Курсантов — Егоревск",
            sdach = "г. Московская область, г.о. Егоревск г. Егоревск, ул Советская, 81",
            ts = ""
        };

        public static List<string> _listPal { get => Data._listPal; set => Data._listPal = value; }
        public static List<string> _listGM { get => Data._listGM; set => Data._listGM = value; }
        public static List<string> _listMesh { get => Data._listMesh; set => Data._listMesh = value; }
        public static List<string> _listCont { get => Data._listCont; set => Data._listCont = value; }
        public static List<string> _listSave { get => Data._listSave; set => Data._listSave = value; }
        public static List<string> _listZas { get => Data._listZas; set => Data._listZas = value; }

        public static int numstation { get => Data.numstation; set => Data.numstation = value; }
        public static string oooinn { get => Data.oooinn; set => Data.oooinn = value; }
        public static string fio { get => Data.fio; set => Data.fio = value; }
        public static string march { get => Data.march; set => Data.march = value; }
        public static string phone { get => Data.phone; set => Data.phone = value; }
        public static string dt { get => Data.dt; set => Data.dt = value; }
        public static string auto1 { get => Data.auto1; set => Data.auto1 = value; }
        public static string auto2 { get => Data.auto2; set => Data.auto2 = value; }
        public static string autoplomb { get => Data.autoplomb; set => Data.autoplomb = value; }
        public static string sdach { get => Data.sdach; set => Data.sdach = value; }
        public static string poluch { get => Data.poluch; set => Data.poluch = value; }
        public static string ts { get => Data.ts; set => Data.ts = value; }
    }
}
