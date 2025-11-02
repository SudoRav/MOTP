using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOTP.Model
{
    public class PatModManager
    {
        //public ObservableCollection<PatMod> _DataPatNacs { get; set; }
        //public ObservableCollection<PatMod> _DataPatPlbs { get; set; }

        public static ObservableCollection<PatMod> _DataPatNacs = new ObservableCollection<PatMod> {
            //new PatMod { PatName = "\\S",                          ActPal = true, ActGM = true, ActCont = true, ActMesh = true, ActSave = true, ActZas = true }
            new PatMod { PatName = "\\S",                          ActPal = false, ActGM = false, ActCont = false, ActMesh = false, ActSave = true, ActZas = true },
            new PatMod { PatName = "^(\\D+?)-(\\d+?)_L(\\d+?)\\b", ActPal = true, ActGM = true, ActCont = true, ActMesh = true, ActSave = false, ActZas = false },
            new PatMod { PatName = "^SHA_L(\\d+?)\\b",             ActPal = true, ActGM = true, ActCont = true, ActMesh = true, ActSave = false, ActZas = false },
            new PatMod { PatName = "стопка_паллет",                ActPal = true, ActGM = false, ActCont = false, ActMesh = false, ActSave = false, ActZas = false },
            new PatMod { PatName = "стопка_нестандартных_паллет",  ActPal = true, ActGM = false, ActCont = false, ActMesh = false, ActSave = false, ActZas = false },
            new PatMod { PatName = "стопка_сломанных_паллет",      ActPal = true, ActGM = false, ActCont = false, ActMesh = false, ActSave = false, ActZas = false },
            new PatMod { PatName = "стопка_контейнеров",           ActPal = true, ActGM = false, ActCont = false, ActMesh = false, ActSave = false, ActZas = false }
        };

        public static ObservableCollection<PatMod> _DataPatPlbs = new ObservableCollection<PatMod> {
            new PatMod { PatName = "\\S",           ActPal = false, ActGM = false, ActCont = false, ActMesh = false, ActSave = false, ActZas = false },
            new PatMod { PatName = "^\\d{12}\\b",   ActPal = false, ActGM = false, ActCont = true, ActMesh = true, ActSave = false, ActZas = false },
            new PatMod { PatName = "^MK\\d{10}\\b", ActPal = false, ActGM = false, ActCont = true, ActMesh = true, ActSave = false, ActZas = false }

        };

        public static ObservableCollection<PatMod> GetPatNacs() 
        { 
            return _DataPatNacs; 
        }

        public static ObservableCollection<PatMod> GetPatPlbs()
        {
            return _DataPatPlbs;
        }

        public static void AddPatNacs(PatMod patMod)
        {
            _DataPatNacs.Add(patMod);
        }

        public static void AddPatPlbs(PatMod patMod)
        {
            _DataPatPlbs.Add(patMod);
        }

        public static void RemPatNacs(int index)
        {
            _DataPatNacs.RemoveAt(index);
        }

        public static void RemPatPlbs(int index)
        {
            _DataPatPlbs.RemoveAt(index);
        }
    }
}
