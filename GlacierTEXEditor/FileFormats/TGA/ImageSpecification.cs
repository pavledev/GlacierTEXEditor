using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlacierTEXEditor.FileFormats
{
    struct ImageSpecification
    {
        public ushort imageXOrigin;
        public ushort imageYOrigin;
        public ushort imageWidth;
        public ushort imageHeight;
        public byte pixelDepth;
        public byte imageDescriptor;
    }
}
