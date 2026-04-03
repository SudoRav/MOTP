using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;

namespace MOTP.Logic
{
    public static class HomeLogic
    {
        // === Автокириллизация ===
        public static string Cyrillify(string s)
        {
            if (string.IsNullOrEmpty(s)) return s;

            return s
                .Replace("A", "А").Replace("B", "В")
                .Replace("C", "С").Replace("E", "Е")
                .Replace("H", "Н").Replace("K", "К")
                .Replace("M", "М").Replace("O", "О")
                .Replace("P", "Р").Replace("T", "Т")
                .Replace("X", "Х");
        }

        // === Проверка корректности строки ===
        public static bool FormStr(string str, int typeIndex, bool isPlomb)
        {
            if (string.IsNullOrWhiteSpace(str))
                return false;

            // Твой реальный алгоритм проверки можно вставить сюда
            if (str.Length < 3)
                return false;

            return true;
        }

        // === Очистка двух полей ===
        public static void ClearEntry(Action<string> setNacl, Action<string> setPlomb)
        {
            setNacl("");
            setPlomb("");
        }
    }
}
