using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlacierTEXEditor.FileFormats
{
    struct TGA
    {
        public TGAHeader tgaHeader;
        public byte[] colorMapData;
        public byte[] fileData;
        public TGAFooter tgaFooter;
    }
}
