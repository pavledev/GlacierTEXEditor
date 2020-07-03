using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlacierTEXEditor.FileFormats
{
    struct TGAFooter
    {
        public ulong extensionAreaOffset;
        public ulong developerDirectoryOffset;
        public char[] signature;
        public char asciiCharacter;
        public byte binaryZeroStringTerminator;
    }
}
