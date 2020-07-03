using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlacierTEXEditor.FileFormats
{
    public struct TEX
    {
        public TEXHeader texHeader;
        public List<TEXEntry> entries;
        public uint[] table1Offsets;
        public uint[] table2Offsets;
    }
}
