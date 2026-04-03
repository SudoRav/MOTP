using MOTP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOTP.ViewModel
{
    public class PatsVM
    {
        public IList<PatMod> ListPatNacs { get; set; }
        public IList<PatMod> ListPatPlbs { get; set; }

        public PatMod SelectedElement;
    }
}
