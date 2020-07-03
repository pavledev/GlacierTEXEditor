using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlacierTEXEditor.FileFormats
{
    struct ColorMapSpecification
    {
        public ushort firstEntryIndex;
        public ushort colorMapLength;
        public byte colorMapEntrySize;
    }
}
