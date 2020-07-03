using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlacierTEXEditor
{
    public class Option
    {
        public bool ExportAsSingleFile
        {
            get;
            set;
        }

        public Dictionary<string, Dictionary<int, string>> Extensions
        {
            get;
            set;
        } = new Dictionary<string, Dictionary<int, string>>();
    }
}
