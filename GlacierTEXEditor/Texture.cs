using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace GlacierTEXEditor
{
    public class Texture
    {
        public int Index
        {
            get;
            set;
        }

        public string FileName
        {
            get;
            set;
        }

        public int Offset
        {
            get;
            set;
        }

        public string Type1
        {
            get;
            set;
        }

        public string Type2
        {
            get;
            set;
        }

        public int FileSize
        {
            get;
            set;
        }

        public int Height
        {
            get;
            set;
        }

        public int Width
        {
            get;
            set;
        }

        public int NumOfMipMips
        {
            get;
            set;
        }

        public int Unk1
        {
            get;
            set;
        }

        public int Unk2
        {
            get;
            set;
        }

        public int Unk3
        {
            get;
            set;
        }

        public byte[][] Data
        {
            get;
            set;
        }

        public int[] MipMapLevelSizes
        {
            get;
            set;
        }

        public long[] DataOffsets
        {
            get;
            set;
        }

        public int PaletteSize
        {
            get;
            set;
        }

        public byte[] PalData
        {
            get;
            set;
        }

        public int IndicesCount
        {
            get;
            set;
        }

        public int[] Indices
        {
            get;
            set;
        }

        public long IndicesOffset
        {
            get;
            set;
        }

        public byte[] GetFileData()
        {
            int dataSize = 0;

            for (int i = 0; i < MipMapLevelSizes.Length; i++)
            {
                dataSize += MipMapLevelSizes[i];
            }

            byte[] data = new byte[dataSize];
            int n = 0;

            for (int i = 0; i < Data.Length; i++)
            {
                byte[] data1 = Data[i];

                for (int j = 0; j < data1.Length; j++)
                {
                    data[n++] = data1[j];
                }
            }

            return data;
        }

        public string GetFileName(string extension)
        {
            if (FileName.Equals(""))
            {
                return Index + extension;
            }

            string fileName = FileName.Replace("/", "_");
            fileName = Index + "_" + fileName + extension;

            return fileName;
        }
    }
}
