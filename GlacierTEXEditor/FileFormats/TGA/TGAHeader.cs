using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlacierTEXEditor.FileFormats
{
    struct TGAHeader
    {
        public byte idLength;
        public byte colorMapType;
        public byte imageType;
        public ColorMapSpecification colorMapSpecification;
        public ImageSpecification imageSpecification;
    }
}
