using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using GlacierTEXEditor.FileFormats;
using System.Windows.Forms;

namespace GlacierTEXEditor
{
    class TGAImage
    {
        private static byte[][] GetGrayScalePalette()
        {
            byte[][] palette = new byte[256][];

            for (int y = 0; y <= 255; y++)
            {
                palette[y] = new byte[4];

                for (int i = 0; i < 3; i++)
                {
                    palette[y][i] = (byte)y;
                }

                palette[y][3] = 0;
            }

            return palette;
        }

        public static void SaveToTGA32(byte[] buffer, int width, int height, string exportPath)
        {
            TGA tga = new TGA();

            tga.tgaHeader.idLength = 0;
            tga.tgaHeader.colorMapType = 0;
            tga.tgaHeader.imageType = 2;
            tga.tgaHeader.colorMapSpecification.firstEntryIndex = 0;
            tga.tgaHeader.colorMapSpecification.colorMapLength = 0;
            tga.tgaHeader.colorMapSpecification.colorMapEntrySize = 0;
            tga.tgaHeader.imageSpecification.imageXOrigin = 0;
            tga.tgaHeader.imageSpecification.imageYOrigin = 0;
            tga.tgaHeader.imageSpecification.imageWidth = (ushort)width;
            tga.tgaHeader.imageSpecification.imageHeight = (ushort)height;
            tga.tgaHeader.imageSpecification.pixelDepth = 32;
            tga.tgaHeader.imageSpecification.imageDescriptor = 8;

            tga.fileData = new byte[buffer.Length];

            Array.Copy(buffer, 0, tga.fileData, 0, buffer.Length);

            tga.tgaFooter.extensionAreaOffset = 0;
            tga.tgaFooter.developerDirectoryOffset = 0;

            string signature = "TRUEVISION-XFILE";
            tga.tgaFooter.signature = signature.ToCharArray();
            tga.tgaFooter.asciiCharacter = '.';
            tga.tgaFooter.binaryZeroStringTerminator = 0;

            try
            {
                using (FileStream fs = new FileStream(exportPath, FileMode.Create))
                {
                    using (BinaryWriter binaryWriter = new BinaryWriter(fs))
                    {
                        binaryWriter.Write(tga.tgaHeader.idLength);
                        binaryWriter.Write(tga.tgaHeader.colorMapType);
                        binaryWriter.Write(tga.tgaHeader.imageType);
                        binaryWriter.Write(tga.tgaHeader.colorMapSpecification.firstEntryIndex);
                        binaryWriter.Write(tga.tgaHeader.colorMapSpecification.colorMapLength);
                        binaryWriter.Write(tga.tgaHeader.colorMapSpecification.colorMapEntrySize);
                        binaryWriter.Write(tga.tgaHeader.imageSpecification.imageXOrigin);
                        binaryWriter.Write(tga.tgaHeader.imageSpecification.imageYOrigin);
                        binaryWriter.Write(tga.tgaHeader.imageSpecification.imageWidth);
                        binaryWriter.Write(tga.tgaHeader.imageSpecification.imageHeight);
                        binaryWriter.Write(tga.tgaHeader.imageSpecification.pixelDepth);
                        binaryWriter.Write(tga.tgaHeader.imageSpecification.imageDescriptor);

                        for (int i = 0; i < tga.fileData.Length; i++)
                        {
                            binaryWriter.Write(tga.fileData[i]);
                        }

                        binaryWriter.Write(tga.tgaFooter.extensionAreaOffset);
                        binaryWriter.Write(tga.tgaFooter.developerDirectoryOffset);
                        binaryWriter.Write(tga.tgaFooter.signature);
                        binaryWriter.Write(tga.tgaFooter.asciiCharacter);
                        binaryWriter.Write(tga.tgaFooter.binaryZeroStringTerminator);
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static void SaveToTGA8(byte[] data, int width, int height, int paletteSize, bool paletteAlpha, string exportPath)
        {
            TGA tga = new TGA();

            tga.tgaHeader.idLength = 0;
            tga.tgaHeader.colorMapType = 1;
            tga.tgaHeader.imageType = 1;
            tga.tgaHeader.colorMapSpecification.firstEntryIndex = 0;
            tga.tgaHeader.colorMapSpecification.colorMapLength = (ushort)paletteSize;

            if (paletteAlpha)
            {
                tga.tgaHeader.colorMapSpecification.colorMapEntrySize = 32;
            }
            else
            {
                tga.tgaHeader.colorMapSpecification.colorMapEntrySize = 24;
            }

            tga.tgaHeader.imageSpecification.imageXOrigin = 0;
            tga.tgaHeader.imageSpecification.imageYOrigin = 0;
            tga.tgaHeader.imageSpecification.imageWidth = (ushort)width;
            tga.tgaHeader.imageSpecification.imageHeight = (ushort)height;
            tga.tgaHeader.imageSpecification.pixelDepth = 8;
            tga.tgaHeader.imageSpecification.imageDescriptor = 0;

            int bufferSize;

            if (paletteAlpha)
            {
                bufferSize = paletteSize * 4;
            }
            else
            {
                bufferSize = paletteSize * 3;
            }

            byte[] buffer = new byte[bufferSize];
            byte[][] palette = GetGrayScalePalette();

            for (int x = 0; x < paletteSize; x++)
            {
                buffer[x * 3] = palette[x][2];
                buffer[x * 3 + 1] = palette[x][1];
                buffer[x * 3 + 2] = palette[x][0];
            }

            tga.colorMapData = new byte[bufferSize];
            Array.Copy(buffer, 0, tga.colorMapData, 0, buffer.Length);

            tga.fileData = new byte[data.Length];
            Array.Copy(data, 0, tga.fileData, 0, data.Length);

            tga.tgaFooter.extensionAreaOffset = 0;
            tga.tgaFooter.developerDirectoryOffset = 0;

            string signature = "TRUEVISION-XFILE";
            tga.tgaFooter.signature = signature.ToCharArray();
            tga.tgaFooter.asciiCharacter = '.';
            tga.tgaFooter.binaryZeroStringTerminator = 0;

            try
            {
                using (FileStream fs = new FileStream(exportPath, FileMode.Create))
                {
                    using (BinaryWriter binaryWriter = new BinaryWriter(fs))
                    {
                        binaryWriter.Write(tga.tgaHeader.idLength);
                        binaryWriter.Write(tga.tgaHeader.colorMapType);
                        binaryWriter.Write(tga.tgaHeader.imageType);
                        binaryWriter.Write(tga.tgaHeader.colorMapSpecification.firstEntryIndex);
                        binaryWriter.Write(tga.tgaHeader.colorMapSpecification.colorMapLength);
                        binaryWriter.Write(tga.tgaHeader.colorMapSpecification.colorMapEntrySize);
                        binaryWriter.Write(tga.tgaHeader.imageSpecification.imageXOrigin);
                        binaryWriter.Write(tga.tgaHeader.imageSpecification.imageYOrigin);
                        binaryWriter.Write(tga.tgaHeader.imageSpecification.imageWidth);
                        binaryWriter.Write(tga.tgaHeader.imageSpecification.imageHeight);
                        binaryWriter.Write(tga.tgaHeader.imageSpecification.pixelDepth);
                        binaryWriter.Write(tga.tgaHeader.imageSpecification.imageDescriptor);

                        binaryWriter.Write(tga.colorMapData);
                        binaryWriter.Write(tga.fileData);

                        binaryWriter.Write(tga.tgaFooter.extensionAreaOffset);
                        binaryWriter.Write(tga.tgaFooter.developerDirectoryOffset);
                        binaryWriter.Write(tga.tgaFooter.signature);
                        binaryWriter.Write(tga.tgaFooter.asciiCharacter);
                        binaryWriter.Write(tga.tgaFooter.binaryZeroStringTerminator);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static TGA LoadTGA32(string path)
        {
            TGA tga = new TGA();

            try
            {
                using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    using (BinaryReader binaryReader = new BinaryReader(fileStream))
                    {
                        tga.tgaHeader.idLength = binaryReader.ReadByte();
                        tga.tgaHeader.colorMapType = binaryReader.ReadByte();
                        tga.tgaHeader.imageType = binaryReader.ReadByte();
                        tga.tgaHeader.colorMapSpecification.firstEntryIndex = binaryReader.ReadUInt16();
                        tga.tgaHeader.colorMapSpecification.colorMapLength = binaryReader.ReadUInt16();
                        tga.tgaHeader.colorMapSpecification.colorMapEntrySize = binaryReader.ReadByte();
                        tga.tgaHeader.imageSpecification.imageXOrigin = binaryReader.ReadUInt16();
                        tga.tgaHeader.imageSpecification.imageYOrigin = binaryReader.ReadUInt16();
                        tga.tgaHeader.imageSpecification.imageWidth = binaryReader.ReadUInt16();
                        tga.tgaHeader.imageSpecification.imageHeight = binaryReader.ReadUInt16();
                        tga.tgaHeader.imageSpecification.pixelDepth = binaryReader.ReadByte();
                        tga.tgaHeader.imageSpecification.imageDescriptor = binaryReader.ReadByte();

                        if (tga.tgaHeader.idLength != 0 || tga.tgaHeader.colorMapType != 0 || tga.tgaHeader.imageType != 2
                            || tga.tgaHeader.imageSpecification.pixelDepth != 32)
                        {
                            MessageBox.Show("Source file is not an uncompressed Targa 32bpp (alpha channel) file");
                        }

                        int width = Convert.ToInt32(tga.tgaHeader.imageSpecification.imageWidth);
                        int height = Convert.ToInt32(tga.tgaHeader.imageSpecification.imageHeight);

                        tga.fileData = binaryReader.ReadBytes(width * height * 4);
                        tga.tgaFooter.extensionAreaOffset = binaryReader.ReadUInt64();
                        tga.tgaFooter.developerDirectoryOffset = binaryReader.ReadUInt64();
                        tga.tgaFooter.signature = binaryReader.ReadChars(16);
                        tga.tgaFooter.asciiCharacter = binaryReader.ReadChar();
                        tga.tgaFooter.binaryZeroStringTerminator = binaryReader.ReadByte();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return tga;
        }

        public static TGA LoadTGA8(string path, bool paletteAlpha)
        {
            TGA tga = new TGA();

            try
            {
                using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    using (BinaryReader binaryReader = new BinaryReader(fileStream))
                    {
                        tga.tgaHeader.idLength = binaryReader.ReadByte();
                        tga.tgaHeader.colorMapType = binaryReader.ReadByte();
                        tga.tgaHeader.imageType = binaryReader.ReadByte();
                        tga.tgaHeader.colorMapSpecification.firstEntryIndex = binaryReader.ReadUInt16();
                        tga.tgaHeader.colorMapSpecification.colorMapLength = binaryReader.ReadUInt16();
                        tga.tgaHeader.colorMapSpecification.colorMapEntrySize = binaryReader.ReadByte();
                        tga.tgaHeader.imageSpecification.imageXOrigin = binaryReader.ReadUInt16();
                        tga.tgaHeader.imageSpecification.imageYOrigin = binaryReader.ReadUInt16();
                        tga.tgaHeader.imageSpecification.imageWidth = binaryReader.ReadUInt16();
                        tga.tgaHeader.imageSpecification.imageHeight = binaryReader.ReadUInt16();
                        tga.tgaHeader.imageSpecification.pixelDepth = binaryReader.ReadByte();
                        tga.tgaHeader.imageSpecification.imageDescriptor = binaryReader.ReadByte();

                        if (tga.tgaHeader.idLength != 0 || tga.tgaHeader.colorMapType != 0 || tga.tgaHeader.imageType != 2
                            || tga.tgaHeader.imageSpecification.pixelDepth != 32)
                        {
                            MessageBox.Show("Source file is not an uncompressed Targa 32bpp (alpha channel) file");
                        }

                        int width = Convert.ToInt32(tga.tgaHeader.imageSpecification.imageWidth);
                        int height = Convert.ToInt32(tga.tgaHeader.imageSpecification.imageHeight);

                        int paletteSize = 256;
                        int colorMapDataLength;

                        if (paletteAlpha)
                        {
                            colorMapDataLength = paletteSize * 4;
                        }
                        else
                        {
                            colorMapDataLength = paletteSize * 3;
                        }

                        tga.colorMapData = binaryReader.ReadBytes(colorMapDataLength);
                        tga.fileData = binaryReader.ReadBytes(width * height * 4);
                        tga.tgaFooter.extensionAreaOffset = binaryReader.ReadUInt64();
                        tga.tgaFooter.developerDirectoryOffset = binaryReader.ReadUInt64();
                        tga.tgaFooter.signature = binaryReader.ReadChars(16);
                        tga.tgaFooter.asciiCharacter = binaryReader.ReadChar();
                        tga.tgaFooter.binaryZeroStringTerminator = binaryReader.ReadByte();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return tga;
        }
    }
}
