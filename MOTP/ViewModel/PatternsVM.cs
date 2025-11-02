using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MOTP.Model;

namespace MOTP.ViewModel
{
    public class PatternsVM
    {
        public ObservableCollection<PatMod> PatNacs { get; set; }
        public ObservableCollection<PatMod> PatPlbs { get; set; }

        public PatternsVM()
        {
            PatNacs = PatModManager.GetPatNacs();
            PatPlbs = PatModManager.GetPatPlbs();
        }
    }
}
