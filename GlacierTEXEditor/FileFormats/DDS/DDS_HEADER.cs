using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlacierTEXEditor.FileFormats
{
    public struct DDS_HEADER
    {
        public char[] magic;
        public DDSURFACEDESC2 surfaceDesc;
    }
}
