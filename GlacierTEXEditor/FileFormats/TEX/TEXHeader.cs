using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlacierTEXEditor.FileFormats
{
    public struct TEXHeader
    {
        public uint table1Offset;
        public uint table2Offset;
        public uint unk1;
        public uint unk2;
    }
}
