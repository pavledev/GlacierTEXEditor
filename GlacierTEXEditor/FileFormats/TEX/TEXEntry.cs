using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlacierTEXEditor.FileFormats
{
    public struct TEXEntry
    {
        public uint fileSize;
        public char[] type1;
        public char[] type2;
        public uint index;
        public ushort height;
        public ushort width;
        public uint numOfMipMaps;
        public uint unk1;
        public uint unk2;
        public uint unk3;
        public byte[] fileName;
        public List<FileData> fileData;
        public byte[] palData;
        public uint indicesCount;
        public uint[] indices;
    }
}
