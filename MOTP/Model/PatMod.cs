using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOTP.Model
{
    public class PatMod
    {
        public string PatName { get; set; }
        public bool ActPal {  get; set; }
        public bool ActGM { get; set; }
        public bool ActMesh { get; set; }
        public bool ActCont { get; set; }
        public bool ActSave { get; set; }
        public bool ActZas { get; set; }
    }
}
