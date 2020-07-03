using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlacierTEXEditor.FileFormats
{
    public struct DDSURFACEDESC2
    {
        public uint size;
        public uint flags;
        public uint height;
        public uint width;
        public uint pitchOrLinearSize;
        public uint depth;
        public uint mipMapCount;
        public uint[] reserved1;
        public DDS_PIXELFORMAT ddspf;
        public DDSCAPS2 caps;
        public uint reserved2;
    }
}
