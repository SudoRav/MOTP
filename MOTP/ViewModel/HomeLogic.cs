using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;

namespace MOTP.Logic
{
    public static class HomeLogic
    {
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

        public static bool FormStr(string str, int typeIndex, bool isPlomb)
        {
            if (string.IsNullOrWhiteSpace(str))
                return false;

            if (str.Length < 3)
                return false;

            return true;
        }

        public static void ClearEntry(Action<string> setNacl, Action<string> setPlomb)
        {
            setNacl("");
            setPlomb("");
        }
    }
}
