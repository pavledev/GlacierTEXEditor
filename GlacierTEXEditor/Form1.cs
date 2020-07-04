using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.IO.Compression;
using System.Drawing.Imaging;
using StbImageWriteSharp;
using GlacierTEXEditor.FileFormats;
using StbImageSharp;
using DirectXTexNet;

namespace GlacierTEXEditor
{
    public partial class Form1 : Form
    {
        private string selectedZipPath;
        private string filePath;
        private TEX tex = new TEX();
        private List<Texture> textures = new List<Texture>();
        private Dictionary<int, List<Texture>> texturesBackup = new Dictionary<int, List<Texture>>();
        private int countOfEmptyOffsets = 0;

        private int entriesCount = 0;
        private Dictionary<string, int> entryTypesCount = new Dictionary<string, int>();
        private int indexOfLastModifiedTexture = 0;
        private int indexOfCurrentBackup = 0;
        private bool importedFile = false;
        private bool savedChanges = false;
        private Configuration configuration = new Configuration();
        private bool autoUpdateZIP = false;
        private CompressionLevel compressionLevel;

        public Form1()
        {
            InitializeComponent();

            lvTexDetails.ContextMenuStrip = contextMenuStrip1;
            lvTexDetails.FullRowSelect = true;

            chkDXT1.Checked = true;
            chkDXT3.Checked = true;
            chkRGBA.Checked = true;
            chkPALN.Checked = true;
            chkI8.Checked = true;
            chkU8V8.Checked = true;

            tsmiOpenRecent.Enabled = false;
            tsmiUndo.Enabled = false;
            tsmiRedo.Enabled = false;
            tsmiImportFile.Enabled = false;
            tsmiExportFile.Enabled = false;
            tsmiExtractAllFiles.Enabled = false;
            tsmiSaveTEX.Enabled = false;

            cmsImportFile.Enabled = false;
            cmsExportFile.Enabled = false;
            cmsExtractAllFiles.Enabled = false;
            cmsSaveTEX.Enabled = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (!File.Exists("GlacierTEXEditor.ini"))
            {
                File.Create("GlacierTEXEditor.ini").Dispose();
            }

            string gamePath = configuration.GetGamePath();

            if (gamePath != null)
            {
                AddZipFiles(gamePath);
            }
            else
            {
                cbZipFiles.Text = "Select game path";

                if (!configuration.CheckIfGamePathEntryAdded())
                {
                    configuration.WriteGamePath("");
                }
            }

            SetCheckBoxesState();

            if (!configuration.CheckIfCompressionLvlExists())
            {
                configuration.WriteCompressionLevel("Fastest");
            }
            else
            {
                compressionLevel = configuration.GetCompressionLevel();
            }

            if (!configuration.CheckIfAutoUpdateZIPExists())
            {
                configuration.SetAutoUpdateZIP(false);
            }
            else
            {
                autoUpdateZIP = configuration.GetAutoUpdateZIPState();
            }

            if (configuration.GetRecentFilesCount() > 0)
            {
                tsmiOpenRecent.Enabled = true;
                AddRecentFilesToMenu();
            }
        }

        private void BtnChangeDirectory_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.ValidateNames = false;
            openFileDialog.CheckFileExists = false;
            openFileDialog.CheckPathExists = true;
            openFileDialog.FileName = "Folder Selection.";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string path = Path.GetDirectoryName(openFileDialog.FileName);
                AddZipFiles(path);

                cbZipFiles.Text = "";
                configuration.WriteGamePath(path);
            }
        }

        private void CbZipFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            tsmiSaveTEX.Enabled = true;
            cmsSaveTEX.Enabled = true;

            tsmiExtractAllFiles.Enabled = true;
            cmsExtractAllFiles.Enabled = true;

            selectedZipPath = cbZipFiles.SelectedItem.ToString();
            filePath = Path.Combine(GetOutputPath(), Path.GetFileNameWithoutExtension(selectedZipPath) + ".TEX");

            ExtractZipFile(filePath);
            ReadTexFile();
        }

        private void ExtractZipFile(string extractPath)
        {
            try
            {
                using (ZipArchive archive = ZipFile.OpenRead(selectedZipPath))
                {
                    ZipArchiveEntry zipArchiveEntry = archive.Entries.Where(entry => entry.Name.EndsWith(".TEX")).FirstOrDefault();

                    if (File.Exists(extractPath))
                    {
                        string fileName = Path.GetFileName(extractPath);
                        string text = fileName + " already exists. Do you want to replace it?";

                        DialogResult dialogResult = MessageBox.Show(text,
                            "Glacier TEX Editor", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (dialogResult == DialogResult.Yes)
                        {
                            File.Delete(extractPath);
                        }
                        else
                        {
                            return;
                        }
                    }

                    zipArchiveEntry.ExtractToFile(extractPath);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Glacier TEX Editor", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ReadTexFile()
        {
            if (textures.Count > 0)
            {
                textures.Clear();
                lvTexDetails.Items.Clear();
                countOfEmptyOffsets = 0;
                entriesCount = 0;
                entryTypesCount.Clear();
            }

            using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                using (BinaryReader binaryReader = new BinaryReader(fileStream))
                {
                    tex.texHeader.table1Offset = binaryReader.ReadUInt32();
                    tex.texHeader.table2Offset = binaryReader.ReadUInt32();
                    tex.texHeader.unk1 = binaryReader.ReadUInt32();
                    tex.texHeader.unk2 = binaryReader.ReadUInt32();

                    fileStream.Seek(tex.texHeader.table1Offset, SeekOrigin.Begin);

                    tex.table1Offsets = new uint[2048];

                    for (int i = 0; i < tex.table1Offsets.Length; i++)
                    {
                        tex.table1Offsets[i] = binaryReader.ReadUInt32();
                    }

                    tex.entries = new List<TEXEntry>();

                    for (int i = 0; i < tex.table1Offsets.Length; i++)
                    {
                        if (tex.table1Offsets[i] != 0)
                        {
                            fileStream.Seek(tex.table1Offsets[i], SeekOrigin.Begin);

                            Texture texture = new Texture();

                            texture.Offset = (int)tex.table1Offsets[i];
                            texture.FileSize = binaryReader.ReadInt32();

                            string type1 = Encoding.Default.GetString(binaryReader.ReadBytes(4));
                            string type2 = Encoding.Default.GetString(binaryReader.ReadBytes(4));

                            texture.Type1 = ReverseTypeText(type1);
                            texture.Type2 = ReverseTypeText(type2);

                            texture.Index = binaryReader.ReadInt32();

                            texture.Height = binaryReader.ReadInt16();
                            texture.Width = binaryReader.ReadInt16();
                            texture.NumOfMipMips = binaryReader.ReadInt32();
                            texture.Data = new byte[texture.NumOfMipMips][];
                            texture.DataOffsets = new long[texture.NumOfMipMips];
                            texture.MipMapLevelSizes = new int[texture.NumOfMipMips];

                            texture.Unk1 = binaryReader.ReadInt32();
                            texture.Unk2 = binaryReader.ReadInt32();
                            texture.Unk3 = binaryReader.ReadInt32();

                            byte letter;

                            texture.FileName = "";

                            while ((letter = binaryReader.ReadByte()) != 0)
                            {
                                texture.FileName += Encoding.Default.GetString(new byte[] { letter });
                            }

                            for (int j = 0; j < texture.Data.Length; j++)
                            {
                                int size = binaryReader.ReadInt32();

                                texture.MipMapLevelSizes[j] = size;
                                texture.DataOffsets[j] = binaryReader.BaseStream.Position;
                                texture.Data[j] = binaryReader.ReadBytes(size);
                            }

                            if (texture.Type1.Equals("PALN"))
                            {
                                texture.PaletteSize = binaryReader.ReadInt32();
                                int dataSize = texture.PaletteSize * 4;
                                texture.PalData = binaryReader.ReadBytes(dataSize);
                            }

                            entriesCount += 1;

                            if (entryTypesCount.ContainsKey(texture.Type1))
                            {
                                entryTypesCount[texture.Type1] += 1;
                            }
                            else
                            {
                                entryTypesCount.Add(texture.Type1, 1);
                            }

                            textures.Add(texture);
                        }
                        else
                        {
                            if (textures.Count == 0)
                            {
                                countOfEmptyOffsets++;
                            }
                        }
                    }

                    fileStream.Seek(tex.texHeader.table2Offset, SeekOrigin.Begin);

                    tex.table2Offsets = new uint[2048];

                    for (int i = 0; i < tex.table2Offsets.Length; i++)
                    {
                        tex.table2Offsets[i] = binaryReader.ReadUInt32();
                        long position = binaryReader.BaseStream.Position;

                        if (tex.table2Offsets[i] != 0)
                        {
                            fileStream.Seek(tex.table2Offsets[i], SeekOrigin.Begin);

                            int indicesCount = binaryReader.ReadInt32();
                            int[] indices = new int[indicesCount];

                            for (int j = 0; j < indices.Length; j++)
                            {
                                indices[j] = binaryReader.ReadInt32();
                            }

                            int index = 0;

                            if (indices[indices.Length - 1] == 0)
                            {
                                for (int j = indices.Length - 1; j >= 0; j--)
                                {
                                    if (indices[j] > 0)
                                    {
                                        index = indices[j] - countOfEmptyOffsets;
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                index = indices[indices.Length - 1] - countOfEmptyOffsets;
                            }

                            textures[index].IndicesCount = indicesCount;
                            textures[index].Indices = new int[indicesCount];

                            Array.Copy(indices, 0, textures[index].Indices, 0, indicesCount);

                            fileStream.Seek(position, SeekOrigin.Begin);
                        }
                    }
                }
            }

            ShowTEXDetails();
        }

        private void ShowTEXDetails()
        {
            if (entryTypesCount.Count > 0)
            {
                if (entryTypesCount.ContainsKey("DXT1"))
                {
                    lblDXT1.Text = "DXT1: " + entryTypesCount["DXT1"].ToString();
                }
                else
                {
                    lblDXT1.Text = "DXT1: 0";
                }

                if (entryTypesCount.ContainsKey("DXT3"))
                {
                    lblDXT3.Text = "DXT3: " + entryTypesCount["DXT3"].ToString();
                }
                else
                {
                    lblDXT3.Text = "DXT3: 0";
                }

                if (entryTypesCount.ContainsKey("RGBA"))
                {
                    lblRGBA.Text = "RGBA: " + entryTypesCount["RGBA"].ToString();
                }
                else
                {
                    lblRGBA.Text = "RGBA: 0";
                }

                if (entryTypesCount.ContainsKey("PALN"))
                {
                    lblPALN.Text = "PALN: " + entryTypesCount["PALN"].ToString();
                }
                else
                {
                    lblPALN.Text = "PALN: 0";
                }

                if (entryTypesCount.ContainsKey("I8  "))
                {
                    lblI8.Text = "I8: " + entryTypesCount["I8  "].ToString();
                }
                else
                {
                    lblI8.Text = "I8: 0";
                }

                if (entryTypesCount.ContainsKey("U8V8"))
                {
                    lblU8V8.Text = "U8V8: " + entryTypesCount["U8V8"].ToString();
                }
                else
                {
                    lblU8V8.Text = "U8V8: 0";
                }

                lblAll.Text = "All: " + entriesCount.ToString();
            }

            FilterList();

            toolStripStatusLabel1.Text = "File parsed successfully.";
        }

        private void OpenTEXFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "TEX Files (*.TEX) | *.tex;";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                filePath = openFileDialog.FileName;

                if (configuration.GetRecentFilesCount() <= 20)
                {
                    if (!tsmiOpenRecent.Enabled)
                    {
                        tsmiOpenRecent.Enabled = true;
                    }

                    configuration.WriteRecentFilePath(filePath);
                }

                ReadTexFile();
            }
        }

        private void SaveTEXFile()
        {
            string title = "Save TEX File";
            string fileName= Path.GetFileName(filePath); ;
            string filter = "TEX Files (*.TEX) | *.tex;";

            string exportPath = ShowSaveFileDialog(title, fileName, filter);

            if (exportPath == null)
            {
                return;
            }

            ExtractZipFile(exportPath);
        }

        private void CreateBackupOfTexture(Texture texture)
        {
            Texture textureBackup = ObjectExtensions.Clone(texture);

            if (!texturesBackup.ContainsKey(texture.Index))
            {
                texturesBackup.Add(texture.Index, new List<Texture>());
            }

            texturesBackup[texture.Index].Add(textureBackup);
            indexOfCurrentBackup++;
        }

        private bool CheckDDSFileHeader(string path, string type)
        {
            try
            {
                using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    using (BinaryReader binaryReader = new BinaryReader(fileStream))
                    {
                        string magic = Encoding.Default.GetString(binaryReader.ReadBytes(4));
                        int size = binaryReader.ReadInt32();
                        int flags = binaryReader.ReadInt32();

                        if (!magic.Equals("DDS ") || size != 124)
                        {
                            MessageBox.Show("Source file is not a DDS file.", "Glacier TEX Editor", MessageBoxButtons.OK, MessageBoxIcon.Error);

                            return false;
                        }

                        uint expectedFlags = (uint)DDSSurfaceDescFlags.DDSD_CAPS | (uint)DDSSurfaceDescFlags.DDSD_HEIGHT
                            | (uint)DDSSurfaceDescFlags.DDSD_WIDTH | (uint)DDSSurfaceDescFlags.DDSD_PIXELFORMAT;

                        if (!type.Equals("A8B8G8R8") && !type.Equals("L8"))
                        {
                            expectedFlags |= (uint)DDSSurfaceDescFlags.DDSD_LINEARSIZE;
                        }
                        else
                        {
                            expectedFlags |= (uint)DDSSurfaceDescFlags.DDSD_PITCH;
                        }

                        if ((flags & expectedFlags) != expectedFlags)
                        {
                            MessageBox.Show("Bad flags in DDS file.", "Glacier TEX Editor", MessageBoxButtons.OK, MessageBoxIcon.Error);

                            return false;
                        }

                        int height = binaryReader.ReadInt32();
                        int width = binaryReader.ReadInt32();
                        int pitchOrLinearSize = binaryReader.ReadInt32();
                        int depth = binaryReader.ReadInt32();
                        int mipMapCount = binaryReader.ReadInt32();

                        if (mipMapCount > 1 && ((flags & (uint)DDSSurfaceDescFlags.DDSD_MIPMAPCOUNT) != (uint)DDSSurfaceDescFlags.DDSD_MIPMAPCOUNT))
                        {
                            MessageBox.Show("Missing MIPMAPCOUNT flag.", "Glacier TEX Editor", MessageBoxButtons.OK, MessageBoxIcon.Error);

                            return false;
                        }

                        for (int i = 0; i < 11; i++)
                        {
                            binaryReader.ReadInt32();
                        }

                        int size2 = binaryReader.ReadInt32();

                        if (size2 != 32)
                        {
                            MessageBox.Show("Bad flags in DDS Pixel Format.", "Glacier TEX Editor", MessageBoxButtons.OK, MessageBoxIcon.Error);

                            return false;
                        }

                        int flags2 = binaryReader.ReadInt32();
                        string fourCC = Encoding.Default.GetString(binaryReader.ReadBytes(4));
                        uint expectedFlags2;

                        if (type.Equals("A8B8G8R8"))
                        {
                            expectedFlags2 = (uint)DDSPixelFormatFlags.DDPF_RGB | (uint)DDSPixelFormatFlags.DDPF_ALPHAPIXELS;

                            if ((flags2 & expectedFlags2) != expectedFlags2)
                            {
                                MessageBox.Show("Bad flags in DDS Pixel Format.", "Glacier TEX Editor", MessageBoxButtons.OK, MessageBoxIcon.Error);

                                return false;
                            }

                            uint rgbBitCount = binaryReader.ReadUInt32();
                            uint dwRBitMask = binaryReader.ReadUInt32();
                            uint dwGBitMask = binaryReader.ReadUInt32();
                            uint dwBBitMask = binaryReader.ReadUInt32();
                            uint dwABitMask = binaryReader.ReadUInt32();

                            if (rgbBitCount != 32 || dwRBitMask != 0xFF0000 || dwGBitMask != 0xFF00 || dwBBitMask != 0xFF
                                || dwABitMask != 0xFF000000)
                            {
                                MessageBox.Show("Bad flags in DDS Pixel Format.", "Glacier TEX Editor", MessageBoxButtons.OK, MessageBoxIcon.Error);

                                return false;
                            }
                        }
                        else if (type.Equals("L8"))
                        {
                            expectedFlags2 = (uint)DDSPixelFormatFlags.DDPF_LUMINANCE;

                            if ((flags2 & expectedFlags2) != expectedFlags2)
                            {
                                MessageBox.Show("Bad flags in DDS Pixel Format.", "Glacier TEX Editor", MessageBoxButtons.OK, MessageBoxIcon.Error);

                                return false;
                            }

                            uint rgbBitCount = binaryReader.ReadUInt32();
                            uint dwRBitMask = binaryReader.ReadUInt32();
                            uint dwGBitMask = binaryReader.ReadUInt32();
                            uint dwBBitMask = binaryReader.ReadUInt32();
                            uint dwABitMask = binaryReader.ReadUInt32();

                            if (rgbBitCount != 8 || dwRBitMask != 0xFF || dwGBitMask != 0 || dwBBitMask != 0
                                || dwABitMask != 0)
                            {
                                MessageBox.Show("Bad flags in DDS Pixel Format.", "Glacier TEX Editor", MessageBoxButtons.OK, MessageBoxIcon.Error);

                                return false;
                            }
                        }
                        else
                        {
                            if (flags2 != (uint)DDSPixelFormatFlags.DDPF_FOURCC || !type.Equals(fourCC))
                            {
                                char dxtNum = char.Parse(type.Substring(type.Length - 1));
                                MessageBox.Show("DXT" + dxtNum + " compression expected in DDS file (incompatible format found).",
                                    "Glacier TEX Editor", MessageBoxButtons.OK, MessageBoxIcon.Error);

                                return false;
                            }

                            for (int i = 0; i < 5; i++)
                            {
                                binaryReader.ReadInt32();
                            }
                        }

                        uint caps1 = binaryReader.ReadUInt32();
                        uint expectedCaps = (uint)DDSHeaderCaps.DDSCAPS_TEXTURE;

                        if (mipMapCount > 1)
                        {
                            expectedCaps |= (uint)DDSHeaderCaps.DDSCAPS_COMPLEX | (uint)DDSHeaderCaps.DDSCAPS_MIPMAP;
                        }

                        if (caps1 != expectedCaps)
                        {
                            MessageBox.Show("Bad flags in Surface Format Header.", "Glacier TEX Editor", MessageBoxButtons.OK, MessageBoxIcon.Error);

                            return false;
                        }
                    }
                }
            }
            catch(IOException ex)
            {
                MessageBox.Show(ex.Message, "Glacier TEX Editor", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return true;
        }

        private bool ImportDDSFile(Texture texture, string path, string type)
        {
            if (!CheckDDSFileHeader(path, type))
            {
                return false;
            }

            try
            {
                using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    using (BinaryReader binaryReader = new BinaryReader(fileStream))
                    {
                        string magic = Encoding.Default.GetString(binaryReader.ReadBytes(4));
                        int size = binaryReader.ReadInt32();
                        int flags = binaryReader.ReadInt32();
                        int height = binaryReader.ReadInt32();
                        int width = binaryReader.ReadInt32();
                        int pitchOrLinearSize = binaryReader.ReadInt32();
                        int depth = binaryReader.ReadInt32();
                        int mipMapCount = binaryReader.ReadInt32();

                        for (int i = 0; i < 11; i++)
                        {
                            binaryReader.ReadInt32();
                        }

                        int size2 = binaryReader.ReadInt32();
                        int flags2 = binaryReader.ReadInt32();
                        string fourCC = Encoding.Default.GetString(binaryReader.ReadBytes(4));

                        for (int i = 0; i < 10; i++)
                        {
                            binaryReader.ReadInt32();
                        }

                        texture.Width = width;
                        texture.Height = height;

                        texture.NumOfMipMips = mipMapCount;
                        texture.MipMapLevelSizes = new int[mipMapCount];

                        int mainImageSize = CalculateMainImageSize(texture.Type1, width, height);

                        texture.MipMapLevelSizes[0] = mainImageSize;
                        int n = 0;

                        for (int i = 1; i < mipMapCount; i++)
                        {
                            int mipMapSize = texture.MipMapLevelSizes[i - 1] / 4;

                            if (texture.Type1.Equals("DXT1") && mipMapSize <= 8)
                            {
                                n = i;
                                break;
                            }
                            else if (texture.Type1.Equals("DXT3") && mipMapSize <= 16)
                            {
                                n = i;
                                break;
                            }

                            texture.MipMapLevelSizes[i] = mipMapSize;
                        }

                        if (texture.Type1.Equals("DXT1"))
                        {
                            while (n < mipMapCount)
                            {
                                texture.MipMapLevelSizes[n++] = 8;
                            }
                        }
                        else if (texture.Type1.Equals("DXT3"))
                        {
                            while (n < mipMapCount)
                            {
                                texture.MipMapLevelSizes[n++] = 16;
                            }
                        }

                        texture.Data = new byte[mipMapCount][];

                        for (int i = 0; i < mipMapCount; i++)
                        {
                            byte[] data = binaryReader.ReadBytes(texture.MipMapLevelSizes[i]);

                            if (type.Equals("A8B8G8R8"))
                            {
                                int currentWidth = width / (int)Math.Pow(2, i);
                                int currentHeight = height / (int)Math.Pow(2, i);

                                if (texture.Type1.Equals("PALN"))
                                {
                                    data = ColorsToPALNRefs(data);
                                }
                                else if (texture.Type1.Equals("RGBA"))
                                {
                                    ConvertBGRAToRGBA(data, currentWidth, currentHeight);
                                }
                            }

                            texture.Data[i] = data;
                        }

                        texture.FileSize = CalculateFileSize(texture);
                    }
                }
            }
            catch(IOException ex)
            {
                MessageBox.Show(ex.Message, "Glacier TEX Editor", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }

            return true;
        }

        private int CalculateMainImageSize(string type, int width, int height)
        {
            int mainImageSize;
            int bytesPerPixel;

            if (type.Equals("DXT1"))
            {
                mainImageSize = Math.Max(1, (width + 3) / 4) * Math.Max(1, (height + 3) / 4) * 8;
            }
            else if (type.Equals("DXT3"))
            {
                mainImageSize = Math.Max(1, (width + 3) / 4) * Math.Max(1, (height + 3) / 4) * 16;
            }
            else if (type.Equals("PALN") || type.Equals("I8  "))
            {
                bytesPerPixel = 1;
                mainImageSize = width * height * bytesPerPixel;
            }
            else if (type.Equals("U8V8"))
            {
                bytesPerPixel = 2;
                mainImageSize = width * height * bytesPerPixel;
            }
            else
            {
                bytesPerPixel = 4;
                mainImageSize = width * height * bytesPerPixel;
            }

            return mainImageSize;
        }

        private int CalculateNumOfMipMaps(int width, int height)
        {
            int numOfMipMaps = 0;

            while (width > 1 && height > 1)
            {
                width /= 2;
                height /= 2;

                numOfMipMaps++;
            }

            return numOfMipMaps;
        }

        private int CalculateFileSize(Texture texture)
        {
            int fileSize = texture.Type1.Length + texture.Type2.Length + sizeof(uint) + sizeof(ushort) + sizeof(ushort)
                + sizeof(uint) + sizeof(uint) + sizeof(uint) + sizeof(uint) + texture.FileName.Length + 1;

            for (int i = 0; i < texture.MipMapLevelSizes.Length; i++)
            {
                fileSize += texture.MipMapLevelSizes[i] + texture.Data[i].Length;
            }

            if (texture.Type1.Equals("PALN"))
            {
                fileSize += texture.PalData.Length;
            }

            if (texture.IndicesCount > 0)
            {
                fileSize += sizeof(uint) + texture.Indices.Length;
            }

            return fileSize;
        }

        private byte[][] GenerateMipMaps(Bitmap bitmap, int numOfMipMaps)
        {
            byte[][] mipMaps = new byte[numOfMipMaps - 1][];
            int n = 0;

            int width = bitmap.Width;
            int height = bitmap.Height;

            while (n < numOfMipMaps - 1)
            {
                width /= 2;
                height /= 2;

                Bitmap resized = new Bitmap(bitmap, new Size(width, height));
                byte[] data = BMPImage.BitmapToData(resized);

                mipMaps[n] = new byte[data.Length];
                Array.Copy(data, 0, mipMaps[n++], 0, data.Length);
            }

            return mipMaps;
        }

        private bool ImportDXTFile(Texture texture, string path)
        {
            CreateBackupOfTexture(texture);

            string extension = Path.GetExtension(path);
            bool imported;

            if (extension.Equals(".tga"))
            {
                using (ScratchImage image = TexHelper.Instance.LoadFromTGAFile(path))
                {
                    using (ScratchImage mipChain = image.GenerateMipMaps(TEX_FILTER_FLAGS.DEFAULT, 0))
                    {
                        DXGI_FORMAT dxgiFormat;

                        if (texture.Type1.Equals("DXT1"))
                        {
                            dxgiFormat = DXGI_FORMAT.BC1_UNORM;
                        }
                        else
                        {
                            dxgiFormat = DXGI_FORMAT.BC2_UNORM;
                        }

                        using (ScratchImage comp = mipChain.Compress(dxgiFormat, TEX_COMPRESS_FLAGS.PARALLEL, 0.5f))
                        {
                            path = Path.Combine(GetOutputPath(), "compressed.dds");
                            comp.SaveToDDSFile(DDS_FLAGS.NONE, path);
                        }
                    }
                }

                imported = ImportDDSFile(texture, path, texture.Type1);
                File.Delete(path);
            }
            else
            {
                imported = ImportDDSFile(texture, path, texture.Type1);
            }

            return imported;
        }

        private bool ImportRGBAFile(Texture texture, string path)
        {
            CreateBackupOfTexture(texture);

            string extension = Path.GetExtension(path);

            if (extension.Equals(".dds"))
            {
                return ImportDDSFile(texture, path, "A8B8G8R8");
            }
            else
            {
                byte[] data;
                int width, height;

                try
                {
                    using (var stream = File.OpenRead(path))
                    {
                        ImageResult image = ImageResult.FromStream(stream, StbImageSharp.ColorComponents.RedGreenBlueAlpha);

                        data = image.Data;
                        width = image.Width;
                        height = image.Height;
                    }
                }
                catch(IOException ex)
                {
                    MessageBox.Show(ex.Message, "Glacier TEX Editor", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return false;
                }

                Bitmap bitmap = BMPImage.DataToBitmap(data, width, height);

                int numOfMipMaps = CalculateNumOfMipMaps(width, height);

                texture.NumOfMipMips = numOfMipMaps;
                texture.MipMapLevelSizes = new int[numOfMipMaps];
                texture.MipMapLevelSizes[0] = data.Length;

                texture.Data = new byte[numOfMipMaps][];
                texture.Data[0] = new byte[data.Length];
                Array.Copy(data, 0, texture.Data[0], 0, data.Length);

                byte[][] mipMaps = GenerateMipMaps(bitmap, numOfMipMaps);

                for (int i = 0; i < mipMaps.Length; i++)
                {
                    byte[] mipMap = mipMaps[i];

                    texture.Data[i + 1] = new byte[mipMap.Length];
                    texture.MipMapLevelSizes[i + 1] = mipMap.Length;

                    Array.Copy(mipMap, 0, texture.Data[i + 1], 0, mipMap.Length);
                }

                texture.FileSize = CalculateFileSize(texture);
            }

            return true;
        }

        private string ShowOpenFileDialog(string title, string filter)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Title = title;
            openFileDialog.Filter = filter;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                return openFileDialog.FileName;
            }

            return "";
        }

        private void ImportFile()
        {
            foreach (ListViewItem lvItem in lvTexDetails.SelectedItems)
            {
                Texture texture = textures.Where(t => t.Index == Convert.ToInt32(lvItem.Text)).FirstOrDefault();
                string type = texture.Type1;
                string title;

                if (type.Contains("DXT"))
                {
                    char dxtNum = char.Parse(type.Substring(type.Length - 1));

                    title = "Select DDS (DXT" + dxtNum + ")/TGA to import...";
                }
                else
                {
                    title = "Select TGA/DDS to import...";
                }

                string filter = "Microsoft DirectDraw Surface (*.DDS)|*.dds";

                if (type.Equals("I8  "))
                {
                    filter += "|Truevision Targa 32bpp (*.TGA)|*.tga";
                }
                else
                {
                    filter += "|Truevision Targa 8bpp Grayscale (*.TGA)|*.tga";
                }

                filter += "|Bitmap (*.BMP)|*.bmp" +
                    "|Portable Network Graphics (*.PNG)|*.png";

                string path = ShowOpenFileDialog(title, filter);

                if (path.Equals(""))
                {
                    return;
                }

                bool imported = false;

                switch (type)
                {
                    case "DXT1":
                        imported = ImportDXTFile(texture, path);
                        break;
                    case "DXT3":
                        imported = ImportDXTFile(texture, path);
                        break;
                    case "RGBA":
                        imported = ImportRGBAFile(texture, path);
                        break;
                    case "PALN":
                        imported = ImportPALNFile(texture, path);
                        break;
                    case "I8  ":
                        imported = ImportI8File(texture, path);
                        break;
                    case "U8V8":
                        imported = ImportU8V8File(texture, path);
                        break;
                    default:
                        MessageBox.Show("Unknown texture type.", "Glacier TEX Editor", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }

                if (imported)
                {
                    toolStripStatusLabel1.Text = "Image(s) imported sucessfully.";

                    if (!tsmiUndo.Enabled)
                    {
                        tsmiUndo.Enabled = true;
                    }

                    importedFile = true;
                    savedChanges = false;

                    lvItem.BackColor = Color.Green;
                }
            }
        }

        private void CmsImportFile_Click(object sender, EventArgs e)
        {
            ImportFile();
        }

        private bool ExportDDSFile(Texture texture, string type, string exportPath)
        {
            DDS_HEADER ddsHeader = new DDS_HEADER();

            ddsHeader.magic = new char[] { 'D', 'D', 'S', ' ' };
            ddsHeader.surfaceDesc.size = 124;
            ddsHeader.surfaceDesc.flags = (uint)DDSSurfaceDescFlags.DDSD_CAPS | (uint)DDSSurfaceDescFlags.DDSD_HEIGHT
                | (uint)DDSSurfaceDescFlags.DDSD_WIDTH | (uint)DDSSurfaceDescFlags.DDSD_PIXELFORMAT;

            if (!type.Equals("A8B8G8R8") && !type.Equals("L8"))
            {
                ddsHeader.surfaceDesc.flags |= (uint)DDSSurfaceDescFlags.DDSD_LINEARSIZE;
            }
            else
            {
                ddsHeader.surfaceDesc.flags |= (uint)DDSSurfaceDescFlags.DDSD_PITCH;
            }

            if (texture.NumOfMipMips > 1)
            {
                ddsHeader.surfaceDesc.flags |= (uint)DDSSurfaceDescFlags.DDSD_MIPMAPCOUNT;
            }

            ddsHeader.surfaceDesc.height = (uint)texture.Height;
            ddsHeader.surfaceDesc.width = (uint)texture.Width;

            if (!type.Equals("A8B8G8R8") && !type.Equals("L8"))
            {
                ddsHeader.surfaceDesc.pitchOrLinearSize = (uint)texture.MipMapLevelSizes[0];
            }
            else
            {
                ddsHeader.surfaceDesc.pitchOrLinearSize = (uint)texture.Width * 4;
            }

            ddsHeader.surfaceDesc.depth = 0;
            ddsHeader.surfaceDesc.mipMapCount = (uint)texture.NumOfMipMips;
            ddsHeader.surfaceDesc.reserved1 = new uint[11];

            for (int i = 0; i < ddsHeader.surfaceDesc.reserved1.Length; i++)
            {
                ddsHeader.surfaceDesc.reserved1[i] = 0;
            }

            ddsHeader.surfaceDesc.ddspf.size = 32;

            if (type.Equals("A8B8G8R8"))
            {
                ddsHeader.surfaceDesc.ddspf.flags = (uint)DDSPixelFormatFlags.DDPF_RGB | (uint)DDSPixelFormatFlags.DDPF_ALPHAPIXELS;
                ddsHeader.surfaceDesc.ddspf.rgbBitCount = 32;
                ddsHeader.surfaceDesc.ddspf.dwRBitMask = 0xFF0000;
                ddsHeader.surfaceDesc.ddspf.dwGBitMask = 0xFF00;
                ddsHeader.surfaceDesc.ddspf.dwBBitMask = 0xFF;
                ddsHeader.surfaceDesc.ddspf.dwABitMask = 0xFF000000;
            }
            else if (type.Equals("L8"))
            {
                ddsHeader.surfaceDesc.ddspf.flags = (uint)DDSPixelFormatFlags.DDPF_LUMINANCE;
                ddsHeader.surfaceDesc.ddspf.rgbBitCount = 8;
                ddsHeader.surfaceDesc.ddspf.dwRBitMask = 0xFF;
                ddsHeader.surfaceDesc.ddspf.dwGBitMask = 0;
                ddsHeader.surfaceDesc.ddspf.dwBBitMask = 0;
                ddsHeader.surfaceDesc.ddspf.dwABitMask = 0;
            }
            else
            {
                ddsHeader.surfaceDesc.ddspf.flags = (uint)DDSPixelFormatFlags.DDPF_FOURCC;

                char dxtNum = char.Parse(texture.Type1.Substring(texture.Type1.Length - 1));

                ddsHeader.surfaceDesc.ddspf.fourCC = new char[] { 'D', 'X', 'T', dxtNum };
                ddsHeader.surfaceDesc.ddspf.rgbBitCount = 0;
                ddsHeader.surfaceDesc.ddspf.dwRBitMask = 0;
                ddsHeader.surfaceDesc.ddspf.dwGBitMask = 0;
                ddsHeader.surfaceDesc.ddspf.dwBBitMask = 0;
                ddsHeader.surfaceDesc.ddspf.dwABitMask = 0;
            }
            
            ddsHeader.surfaceDesc.caps.caps1 = (uint)DDSHeaderCaps.DDSCAPS_TEXTURE;

            if (texture.NumOfMipMips > 1)
            {
                ddsHeader.surfaceDesc.caps.caps1 |= (uint)DDSHeaderCaps.DDSCAPS_COMPLEX | (uint)DDSHeaderCaps.DDSCAPS_MIPMAP;
            }

            ddsHeader.surfaceDesc.caps.caps2 = 0;
            ddsHeader.surfaceDesc.caps.caps3 = 0;
            ddsHeader.surfaceDesc.caps.caps4 = 0;
            ddsHeader.surfaceDesc.reserved2 = 0;

            try
            {
                using (FileStream fs = new FileStream(exportPath, FileMode.Create))
                {
                    using (BinaryWriter binaryWriter = new BinaryWriter(fs))
                    {
                        binaryWriter.Write(ddsHeader.magic);
                        binaryWriter.Write(ddsHeader.surfaceDesc.size);
                        binaryWriter.Write(ddsHeader.surfaceDesc.flags);
                        binaryWriter.Write(ddsHeader.surfaceDesc.height);
                        binaryWriter.Write(ddsHeader.surfaceDesc.width);
                        binaryWriter.Write(ddsHeader.surfaceDesc.pitchOrLinearSize);
                        binaryWriter.Write(ddsHeader.surfaceDesc.depth);
                        binaryWriter.Write(ddsHeader.surfaceDesc.mipMapCount);

                        for (int i = 0; i < ddsHeader.surfaceDesc.reserved1.Length; i++)
                        {
                            binaryWriter.Write(ddsHeader.surfaceDesc.reserved1[i]);
                        }

                        binaryWriter.Write(ddsHeader.surfaceDesc.ddspf.size);
                        binaryWriter.Write(ddsHeader.surfaceDesc.ddspf.flags);

                        if (!type.Equals("A8B8G8R8") && !type.Equals("L8"))
                        {
                            binaryWriter.Write(ddsHeader.surfaceDesc.ddspf.fourCC);
                        }
                        else
                        {
                            binaryWriter.Write(0);
                        }

                        binaryWriter.Write(ddsHeader.surfaceDesc.ddspf.rgbBitCount);
                        binaryWriter.Write(ddsHeader.surfaceDesc.ddspf.dwRBitMask);
                        binaryWriter.Write(ddsHeader.surfaceDesc.ddspf.dwGBitMask);
                        binaryWriter.Write(ddsHeader.surfaceDesc.ddspf.dwBBitMask);
                        binaryWriter.Write(ddsHeader.surfaceDesc.ddspf.dwABitMask);
                        binaryWriter.Write(ddsHeader.surfaceDesc.caps.caps1);
                        binaryWriter.Write(ddsHeader.surfaceDesc.caps.caps2);
                        binaryWriter.Write(ddsHeader.surfaceDesc.caps.caps3);
                        binaryWriter.Write(ddsHeader.surfaceDesc.caps.caps4);
                        binaryWriter.Write(ddsHeader.surfaceDesc.reserved2);

                        for (int i = 0; i < texture.Data.Length; i++)
                        {
                            byte[] data = texture.Data[i];

                            binaryWriter.Write(data);
                        }
                    }
                }
            }
            catch(IOException ex)
            {
                MessageBox.Show(ex.Message, "Glacier TEX Editor", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }

            return true;
        }

        private bool ExportDXTFile(Texture texture, string exportPath, int index = 0)
        {
            byte[] data = texture.Data[index];

            Texture newTexture = ObjectExtensions.Clone(texture);

            newTexture.NumOfMipMips = 1;
            newTexture.Data = new byte[1][];
            newTexture.Data[0] = data;
            newTexture.MipMapLevelSizes = new int[1];
            newTexture.MipMapLevelSizes[0] = data.Length;

            if (index > 0)
            {
                newTexture.Width /= (int)Math.Pow(2, index);
                newTexture.Height /= (int)Math.Pow(2, index);
            }

            string extension = Path.GetExtension(exportPath);

            if (extension.Equals(".dds"))
            {
                bool exported = ExportDDSFile(newTexture, texture.Type1, exportPath);

                if (!exported)
                {
                    return false;
                } 
            }
            else
            {
                try
                {
                    byte[] data2 = ConvertDXTToRGBA(data, newTexture.Width, newTexture.Height, texture.Type1);
                    SaveToImage(data2, newTexture.Width, newTexture.Height, exportPath, extension);
                }
                catch
                {
                    return false;
                }
            }

            return true;
        }

        private bool ExportDXTFile(Texture texture)
        {
            string exportPath;
            string title = "Save DXT file...";
            string fileName = texture.GetFileName(".dds");
            string filter = "Microsoft DirectDraw Surface (*.DDS)|*.dds" +
                    "|Truevision Targa 32bpp (*.TGA)|*.tga" +
                    "|Bitmap (*.BMP)|*.bmp" +
                    "|Portable Network Graphics (*.PNG)|*.png" +
                    "|Joint Photographic Experts Group (*.JPG, *JPEG)|*.jpg;*.jpeg";

            exportPath = ShowSaveFileDialog(title, fileName, filter);

            if (exportPath == null)
            {
                return false;
            }

            string extension = Path.GetExtension(exportPath);

            if (texture.NumOfMipMips > 1)
            {
                using (ExportOptions exportOptions = new ExportOptions())
                {

                    exportOptions.FileName = "File Name: " + texture.FileName;
                    exportOptions.SetDimensions(texture.Width, texture.Height);
                    exportOptions.Extension = extension;

                    DialogResult dialogResult = exportOptions.ShowDialog();

                    if (dialogResult == DialogResult.OK)
                    {
                        bool exported = false;

                        if (!extension.Equals(".dds"))
                        {
                            exported = ExportDXTAsRGBA(texture, exportOptions.SelectedDimensions, extension, exportPath);
                        }
                        else
                        {
                            if (exportOptions.ExportAsSingleFile)
                            {
                                exported = ExportSelectedOptionsToDDS(texture, exportOptions.SelectedDimensions, texture.Type1, exportPath);
                            }
                            else
                            {
                                Dictionary<int, string> paths = GetExportPaths(exportOptions.SelectedDimensions, texture.Width, texture.Height, extension, exportPath);

                                foreach (KeyValuePair<int, string> entry in paths)
                                {
                                    ExportDXTFile(texture, entry.Value, entry.Key);
                                }
                            }
                        }

                        return exported;
                    }
                }
            }
            else
            {
                ExportDXTFile(texture, exportPath);
            }

            return true;
        }

        private bool ExportDXTAsRGBA(Texture texture, Dictionary<int, string> selectedDimensions, string extension, string exportPath)
        {
            Dictionary<int, string> paths = GetExportPaths(selectedDimensions, texture.Width, texture.Height, extension, exportPath);

            for (int i = 0; i < selectedDimensions.Count; i++)
            {
                int index = selectedDimensions.Keys.ElementAt(i);
                byte[] data = texture.Data[index];

                if (texture.Type1.Equals("U8V8"))
                {
                    data = ConvertU8V8ToBGRA(data);
                }

                string newExportPath = paths.Values.ElementAt(i);
                int width = texture.Width / (int)Math.Pow(2, index);
                int height = texture.Height / (int)Math.Pow(2, index);

                try
                {
                    byte[] data2 = ConvertDXTToRGBA(data, width, height, texture.Type1);
                    SaveToImage(data2, width, height, newExportPath, extension);
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, "Glacier TEX Editor", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return false;
                }
            }

            return true;
        }

        private byte[] ConvertDXTToRGBA(byte[] data, int width, int height, string type)
        {
            byte[] buffer;

            if (type.Equals("DXT1"))
            {
                buffer = DxtUtil.DecompressDxt1(data, width, height);
            }
            else
            {
                buffer = DxtUtil.DecompressDxt3(data, width, height);
            }

            return buffer;
        }

        private bool ExportRGBAFile(Texture texture, string exportPath, int index = 0)
        {
            int width = texture.Width / (int)Math.Pow(2, index);
            int height = texture.Height / (int)Math.Pow(2, index);

            int length = texture.Data[index].Length;
            byte[] data = new byte[length];
            Array.Copy(texture.Data[index], 0, data, 0, length);

            string extension = Path.GetExtension(exportPath);

            if (extension.Equals(".dds"))
            {
                ConvertRGBAToBGRA(data, width, height);

                Texture newTexture = ObjectExtensions.Clone(texture);

                newTexture.NumOfMipMips = 1;
                newTexture.Data = new byte[1][];
                newTexture.Data[0] = data;
                newTexture.MipMapLevelSizes = new int[1];
                newTexture.MipMapLevelSizes[0] = data.Length;

                return ExportDDSFile(newTexture, "A8B8G8R8", exportPath);
            }
            else
            {
                if (extension.Equals(".bmp"))
                {
                    ConvertRGBAToBGRA(data, width, height);
                    Bitmap bmp = BMPImage.DataToBitmap(data, width, height);

                    bmp.Save(exportPath, ImageFormat.Bmp);
                }
                else
                {
                    SaveToImage(data, width, height, exportPath, extension);
                }
            }

            return true;
        }

        private bool ExportRGBAFile(Texture texture)
        {
            string exportPath;
            string title = "Save RGBA file...";
            string fileName = texture.GetFileName(".dds");
            string filter = "Microsoft DirectDraw Surface (*.DDS)|*.dds" +
                    "|Truevision Targa 32bpp (*.TGA)|*.tga" +
                    "|Bitmap (*.BMP)|*.bmp" +
                    "|Portable Network Graphics (*.PNG)|*.png" +
                    "|Joint Photographic Experts Group (*.JPG, *JPEG)|*.jpg;*.jpeg";

            exportPath = ShowSaveFileDialog(title, fileName, filter);

            if (exportPath == null)
            {
                return false;
            }

            string extension = Path.GetExtension(exportPath);

            if (texture.NumOfMipMips > 1)
            {
                using (ExportOptions exportOptions = new ExportOptions())
                {

                    exportOptions.FileName = "File Name: " + texture.FileName;
                    exportOptions.SetDimensions(texture.Width, texture.Height);
                    exportOptions.Extension = extension;
                    DialogResult dialogResult = exportOptions.ShowDialog();

                    if (dialogResult == DialogResult.OK)
                    {
                        Dictionary<int, string> selectedDimensions = exportOptions.SelectedDimensions;

                        if (extension.Equals(".dds"))
                        {
                            if (exportOptions.ExportAsSingleFile)
                            {
                                bool exported = ExportSelectedOptionsToDDS(texture, selectedDimensions, "L8", exportPath);

                                return exported;
                            }
                            else
                            {
                                Dictionary<int, string> paths = GetExportPaths(exportOptions.SelectedDimensions, texture.Width, texture.Height, extension, exportPath);

                                foreach (KeyValuePair<int, string> entry in paths)
                                {
                                    ExportDXTFile(texture, entry.Value, entry.Key);
                                }
                            }
                        }
                        else
                        {
                            Dictionary<int, string> paths = GetExportPaths(selectedDimensions, texture.Width, texture.Height, extension, exportPath);

                            for (int i = 0; i < paths.Count; i++)
                            {
                                int index = paths.Keys.ElementAt(i);
                                string newExportPath = paths.Values.ElementAt(i);

                                ExportRGBAFile(texture, newExportPath, index);
                            }
                        }
                    }
                }
            }
            else
            {
                ExportRGBAFile(texture, exportPath);
            }

            return true;
        }

        private void SaveToImage(byte[] data, int width, int height, string exportPath, string extension, StbImageWriteSharp.ColorComponents colorComponents = StbImageWriteSharp.ColorComponents.RedGreenBlueAlpha)
        {
            using (Stream stream = File.OpenWrite(exportPath))
            {
                ImageWriter writer = new ImageWriter();

                switch (extension)
                {
                    case ".tga":
                        writer.WriteTga(data, width, height, colorComponents, stream);
                        break;
                    case ".bmp":
                        writer.WriteBmp(data, width, height, colorComponents, stream);
                        break;
                    case ".png":
                        writer.WritePng(data, width, height, colorComponents, stream);
                        break;
                    default:
                        writer.WriteJpg(data, width, height, colorComponents, stream, 100);
                        break;
                }
            }
        }

        private bool ExportSelectedOptionsToDDS(Texture texture, Dictionary<int, string> selectedDimensions, string type, string exportPath)
        {
            Texture newTexture = ObjectExtensions.Clone(texture);
            newTexture.NumOfMipMips = selectedDimensions.Keys.Count;

            string value = selectedDimensions.Values.ElementAt(0);

            newTexture.Width = Convert.ToInt32(value.Substring(0, value.IndexOf('x')));
            newTexture.Height = Convert.ToInt32(value.Substring(value.IndexOf('x') + 1));

            newTexture.Data = new byte[newTexture.NumOfMipMips][];
            newTexture.MipMapLevelSizes = new int[newTexture.NumOfMipMips];

            for (int i = 0; i < selectedDimensions.Count; i++)
            {
                int index = selectedDimensions.Keys.ElementAt(i);
                byte[] data = texture.Data[index];

                if (texture.Type1.Equals("U8V8"))
                {
                    data = ConvertU8V8ToBGRA(data);
                }

                newTexture.Data[i] = data;
                newTexture.MipMapLevelSizes[i] = data.Length;
            }

            return ExportDDSFile(newTexture, type, exportPath);
        }

        private Dictionary<int, string> GetExportPaths(Dictionary<int, string> selectedDimensions, int width, int height, string extension, string exportPath)
        {
            Dictionary<int, string> paths = new Dictionary<int, string>();
            string dimensions = width + "x" + height;

            if (selectedDimensions.Count > 0)
            {
                if (!(selectedDimensions.Count == 1 && selectedDimensions.Values.ElementAt(0).Equals(dimensions)))
                {
                    string path = Path.Combine(Path.GetDirectoryName(exportPath), Path.GetFileNameWithoutExtension(exportPath));

                    for (int i = 0; i < selectedDimensions.Count; i++)
                    {
                        string newPath = path + "_" + selectedDimensions.Values.ElementAt(i) + extension;

                        paths.Add(selectedDimensions.Keys.ElementAt(i), newPath);
                    }
                }
                else
                {
                    paths.Add(0, exportPath);
                }
            }

            return paths;
        }

        private byte[][] GetPALNPalette(Texture texture)
        {
            byte[][] colors = new byte[texture.PaletteSize][];

            for (int i = 0; i < texture.PaletteSize; i++)
            {
                colors[i] = new byte[4];

                colors[i][0] = texture.PalData[i * 4];
                colors[i][1] = texture.PalData[i * 4 + 1];
                colors[i][2] = texture.PalData[i * 4 + 2];
                colors[i][3] = texture.PalData[i * 4 + 3];
            }

            return colors;
        }

        private byte[] GetColorsFromPALNRefs(Texture texture, int index = 0)
        {
            int n = 0;
            byte[] data = new byte[texture.Width * texture.Height * 4];
            byte[][] palette = GetPALNPalette(texture);
            byte[] palnData = texture.Data[index];

            for (int i = 0; i < palnData.Length; i++)
            {
                byte[] colorBytes = palette[palnData[i]];

                data[n * 4] = colorBytes[0];
                data[n * 4 + 1] = colorBytes[1];
                data[n * 4 + 2] = colorBytes[2];
                data[n++ * 4 + 3] = colorBytes[3];
            }

            return data;
        }

        private byte[] FlipY(byte[] data, int width, int height)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (BinaryWriter binaryWriter = new BinaryWriter(memoryStream))
                {
                    using (BinaryReader binaryReader = new BinaryReader(new MemoryStream(data)))
                    {
                        byte[][] result = new byte[height][];

                        for (int i = height; i > 0; i--)
                        {
                            result[i - 1] = binaryReader.ReadBytes(width * 4);
                        }

                        for (int i = 0; i < height; i++)
                        {
                            binaryWriter.Write(result[i]);
                        }
                    }

                    return memoryStream.ToArray();
                }
            }
        }

        private byte[] FlipI8Y(byte[] data, int width, int height)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (BinaryWriter binaryWriter = new BinaryWriter(memoryStream))
                {
                    using (BinaryReader binaryReader = new BinaryReader(new MemoryStream(data)))
                    {
                        byte[][] result = new byte[height][];

                        for (int i = height; i > 0; i--)
                        {
                            result[i - 1] = binaryReader.ReadBytes(width);
                        }

                        for (int i = 0; i < height; i++)
                        {
                            binaryWriter.Write(result[i]);
                        }
                    }

                    return memoryStream.ToArray();
                }
            }
        }

        private bool ExportPALNFile(Texture texture, string exportPath, int index = 0)
        {
            int width = texture.Width / (int)Math.Pow(2, index);
            int height = texture.Height / (int)Math.Pow(2, index);

            byte[] data = GetColorsFromPALNRefs(texture, index);

            string extension = Path.GetExtension(exportPath);

            if (extension.Equals(".dds"))
            {
                Texture newTexture = ObjectExtensions.Clone(texture);

                newTexture.NumOfMipMips = 1;
                newTexture.Data = new byte[1][];
                newTexture.Data[0] = data;
                newTexture.MipMapLevelSizes = new int[1];
                newTexture.MipMapLevelSizes[0] = data.Length;

                return ExportDDSFile(newTexture, "A8B8G8R8", exportPath);
            }
            else if (extension.Equals(".tga"))
            {
                byte[] buffer = FlipY(data, width, height);

                TGAImage.SaveToTGA32(buffer, width, height, exportPath);
            }
            else if (extension.Equals(".bmp"))
            {
                byte[] data2 = new byte[data.Length];
                Array.Copy(data, 0, data2, 0, data.Length);

                Bitmap bmp = BMPImage.DataToBitmap(data, width, height);

                bmp.Save(exportPath, ImageFormat.Bmp);
            }
            else
            {
                SaveToImage(data, width, height, exportPath, extension);
            }

            return true;
        }

        private bool ExportPALNFile(Texture texture)
        {
            string exportPath;
            string title = "Save PALN file...";
            string fileName = texture.GetFileName(".dds");
            string filter = "Microsoft DirectDraw Surface (*.DDS)|*.dds" +
                    "|Truevision Targa 32bpp (*.TGA)|*.tga" +
                    "|Bitmap (*.BMP)|*.bmp" +
                    "|Portable Network Graphics (*.PNG)|*.png" +
                    "|Joint Photographic Experts Group (*.JPG, *JPEG)|*.jpg;*.jpeg";

            exportPath = ShowSaveFileDialog(title, fileName, filter);

            if (exportPath == null)
            {
                return false;
            }

            string extension = Path.GetExtension(exportPath);

            if (texture.NumOfMipMips > 1)
            {
                using (ExportOptions exportOptions = new ExportOptions())
                {

                    exportOptions.FileName = "File Name: " + texture.FileName;
                    exportOptions.SetDimensions(texture.Width, texture.Height);
                    exportOptions.Extension = extension;
                    DialogResult dialogResult = exportOptions.ShowDialog();

                    if (dialogResult == DialogResult.OK)
                    {
                        Dictionary<int, string> selectedDimensions = exportOptions.SelectedDimensions;

                        if (extension.Equals(".dds"))
                        {
                            if (exportOptions.ExportAsSingleFile)
                            {
                                bool exported = ExportSelectedOptionsToDDS(texture, selectedDimensions, "A8B8G8R8", exportPath);

                                return exported;
                            }
                            else
                            {
                                Dictionary<int, string> paths = GetExportPaths(exportOptions.SelectedDimensions, texture.Width, texture.Height, extension, exportPath);

                                foreach (KeyValuePair<int, string> entry in paths)
                                {
                                    ExportDXTFile(texture, entry.Value, entry.Key);
                                }
                            }
                        }
                        else
                        {
                            Dictionary<int, string> paths = GetExportPaths(selectedDimensions, texture.Width, texture.Height, extension, exportPath);

                            for (int i = 0; i < paths.Count; i++)
                            {
                                int index = paths.Keys.ElementAt(i);
                                string newExportPath = paths.Values.ElementAt(i);

                                ExportPALNFile(texture, newExportPath, index);
                            }
                        }
                    }
                }
            }
            else
            {
                ExportPALNFile(texture, exportPath);
            }

            return true;
        }

        private bool ExportI8File(Texture texture, string exportPath, int index = 0)
        {
            int width = texture.Width / (int)Math.Pow(2, index);
            int height = texture.Height / (int)Math.Pow(2, index);

            byte[] i8Data = texture.Data[index];
            string extension = Path.GetExtension(exportPath);
            
            if (extension.Equals(".dds"))
            {
                Texture newTexture = ObjectExtensions.Clone(texture);

                newTexture.NumOfMipMips = 1;
                newTexture.Data = new byte[1][];
                newTexture.Data[0] = i8Data;
                newTexture.MipMapLevelSizes = new int[1];
                newTexture.MipMapLevelSizes[0] = i8Data.Length;

                return ExportDDSFile(newTexture, "L8", exportPath);
            }

            if (extension.Equals(".tga"))
            {
                byte[] buffer = FlipI8Y(i8Data, width, height);

                TGAImage.SaveToTGA8(buffer, width, height, 256, false, exportPath);
            }
            else if (extension.Equals(".bmp"))
            {
                byte[] data = new byte[i8Data.Length];
                Array.Copy(i8Data, 0, data, 0, data.Length);

                Bitmap bmp = BMPImage.DataToBitmap(data, width, height, PixelFormat.Format8bppIndexed);

                bmp.Save(exportPath, ImageFormat.Bmp);
            }
            else
            {
                SaveToImage(texture.Data[0], width, height, exportPath, extension, StbImageWriteSharp.ColorComponents.Grey);
            }

            return true;
        }

        private bool ExportI8File(Texture texture)
        {
            string exportPath;
            string title = "Save I8 file...";
            string fileName = texture.GetFileName(".dds");
            string filter = "Microsoft DirectDraw Surface (*.DDS)|*.dds" +
                    "|Truevision Targa 8bpp Grayscale (*.TGA)|*.tga" +
                    "|Bitmap (*.BMP)|*.bmp" +
                    "|Portable Network Graphics (*.PNG)|*.png" +
                    "|Joint Photographic Experts Group (*.JPG, *JPEG)|*.jpg;*.jpeg";

            exportPath = ShowSaveFileDialog(title, fileName, filter);

            if (exportPath == null)
            {
                return false;
            }

            string extension = Path.GetExtension(exportPath);

            if (texture.NumOfMipMips > 1)
            {
                using (ExportOptions exportOptions = new ExportOptions())
                {

                    exportOptions.FileName = "File Name: " + texture.FileName;
                    exportOptions.SetDimensions(texture.Width, texture.Height);
                    exportOptions.Extension = extension;
                    DialogResult dialogResult = exportOptions.ShowDialog();

                    if (dialogResult == DialogResult.OK)
                    {
                        Dictionary<int, string> selectedDimensions = exportOptions.SelectedDimensions;

                        if (extension.Equals(".dds"))
                        {
                            if (exportOptions.ExportAsSingleFile)
                            {
                                bool exported = ExportSelectedOptionsToDDS(texture, selectedDimensions, "A8B8G8R8", exportPath);

                                return exported;
                            }
                            else
                            {
                                Dictionary<int, string> paths = GetExportPaths(exportOptions.SelectedDimensions, texture.Width, texture.Height, extension, exportPath);

                                foreach (KeyValuePair<int, string> entry in paths)
                                {
                                    ExportDXTFile(texture, entry.Value, entry.Key);
                                }
                            }
                        }
                        else
                        {
                            Dictionary<int, string> paths = GetExportPaths(selectedDimensions, texture.Width, texture.Height, extension, exportPath);

                            for (int i = 0; i < paths.Count; i++)
                            {
                                int index = paths.Keys.ElementAt(i);
                                string newExportPath = paths.Values.ElementAt(i);

                                ExportI8File(texture, newExportPath, index);
                            }
                        }
                    }
                }
            }
            else
            {
                ExportI8File(texture, exportPath);
            }

            return true;
        }

        private bool ExportU8V8File(Texture texture, string exportPath, int index = 0)
        {
            int width = texture.Width / (int)Math.Pow(2, index);
            int height = texture.Height / (int)Math.Pow(2, index);

            byte[] u8v8Data = texture.Data[index];
            byte[] colors = ConvertU8V8ToBGRA(u8v8Data);

            string extension = Path.GetExtension(exportPath);

            if (extension.Equals(".dds"))
            {
                Texture newTexture = ObjectExtensions.Clone(texture);

                newTexture.NumOfMipMips = 1;
                newTexture.Data = new byte[1][];
                newTexture.Data[0] = colors;
                newTexture.MipMapLevelSizes = new int[1];
                newTexture.MipMapLevelSizes[0] = colors.Length;

                return ExportDDSFile(texture, "A8B8G8R8", exportPath);
            }
            else if (extension.Equals(".tga"))
            {
                byte[] buffer = FlipY(colors, width, height);

                TGAImage.SaveToTGA32(buffer, width, height, exportPath);
            }
            else if (extension.Equals(".bmp"))
            {
                byte[] data = new byte[u8v8Data.Length];
                Array.Copy(u8v8Data, 0, data, 0, data.Length);

                Bitmap bmp = BMPImage.DataToBitmap(data, width, height);

                bmp.Save(exportPath, ImageFormat.Bmp);
            }
            else
            {
                SaveToImage(colors, width, height, exportPath, extension);
            }

            return true;
        }

        private bool ExportU8V8File(Texture texture)
        {
            string exportPath;
            string title = "Save U8V8 file...";
            string fileName = texture.GetFileName(".dds");
            string filter = "Microsoft DirectDraw Surface (*.DDS)|*.dds" +
                    "|Truevision Targa 32bpp (*.TGA)|*.tga" +
                    "|Bitmap (*.BMP)|*.bmp" +
                    "|Portable Network Graphics (*.PNG)|*.png" +
                    "|Joint Photographic Experts Group (*.JPG, *JPEG)|*.jpg;*.jpeg";

            exportPath = ShowSaveFileDialog(title, fileName, filter);

            if (exportPath == null)
            {
                return false;
            }

            string extension = Path.GetExtension(exportPath);

            if (texture.NumOfMipMips > 1)
            {
                using (ExportOptions exportOptions = new ExportOptions())
                {

                    exportOptions.FileName = "File Name: " + texture.FileName;
                    exportOptions.SetDimensions(texture.Width, texture.Height);
                    exportOptions.Extension = extension;
                    DialogResult dialogResult = exportOptions.ShowDialog();

                    if (dialogResult == DialogResult.OK)
                    {
                        Dictionary<int, string> selectedDimensions = exportOptions.SelectedDimensions;

                        if (extension.Equals(".dds"))
                        {
                            if (exportOptions.ExportAsSingleFile)
                            {
                                bool exported = ExportSelectedOptionsToDDS(texture, selectedDimensions, "A8B8G8R8", exportPath);

                                return exported;
                            }
                            else
                            {
                                Dictionary<int, string> paths = GetExportPaths(exportOptions.SelectedDimensions, texture.Width, texture.Height, extension, exportPath);

                                foreach (KeyValuePair<int, string> entry in paths)
                                {
                                    ExportDXTFile(texture, entry.Value, entry.Key);
                                }
                            }
                        }
                        else
                        {
                            Dictionary<int, string> paths = GetExportPaths(selectedDimensions, texture.Width, texture.Height, extension, exportPath);

                            for (int i = 0; i < paths.Count; i++)
                            {
                                int index = paths.Keys.ElementAt(i);
                                string newExportPath = paths.Values.ElementAt(i);

                                ExportU8V8File(texture, newExportPath, index);
                            }
                        }
                    }
                }
            }
            else
            {
                ExportU8V8File(texture, exportPath);
            }

            return true;
        }

        private byte[] ConvertU8V8ToRGBA(byte[] data)
        {
            byte[] output;

            using (BinaryReader binaryReader = new BinaryReader(new MemoryStream(data)))
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (BinaryWriter binaryWriter = new BinaryWriter(memoryStream))
                    {
                        for (int i = 0; i < data.Length / 2; i++)
                        {
                            byte red = binaryReader.ReadByte();
                            byte green = binaryReader.ReadByte();

                            binaryWriter.Write(new byte[] { red, green, 255, 255 });
                        }

                        output = memoryStream.ToArray();
                    }
                }
            }

            return output;
        }

        private byte[] ConvertU8V8ToBGRA(byte[] data)
        {
            byte[] output;

            using (BinaryReader binaryReader = new BinaryReader(new MemoryStream(data)))
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (BinaryWriter binaryWriter = new BinaryWriter(memoryStream))
                    {
                        for (int i = 0; i < data.Length / 2; i++)
                        {
                            byte red = binaryReader.ReadByte();
                            byte green = binaryReader.ReadByte();

                            binaryWriter.Write(new byte[] { 255, green, red, 255 });
                        }

                        output = memoryStream.ToArray();
                    }
                }
            }

            return output;
        }

        private byte[][] ConvertU8V8ToBGRA(Texture texture)
        {
            byte[][] data = new byte[texture.Data.Length][];

            for (int i = 0; i < texture.Data.Length; i++)
            {
                using (BinaryReader binaryReader = new BinaryReader(new MemoryStream(texture.Data[i])))
                {
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        using (BinaryWriter binaryWriter = new BinaryWriter(memoryStream))
                        {
                            for (int j = 0; j < texture.Data[i].Length / 2; j++)
                            {
                                byte red = binaryReader.ReadByte();
                                byte green = binaryReader.ReadByte();

                                binaryWriter.Write(new byte[] { 255, green, red, 255 });
                            }

                            data[i] = memoryStream.ToArray();
                        }
                    }
                }
            }

            return data;
        }

        private byte[] ColorsToPALNRefs(byte[] data)
        {
            List<Color> colors = new List<Color>();

            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (BinaryReader binaryReader = new BinaryReader(new MemoryStream(data)))
                {
                    for (int i = 0; i < data.Length / 4; i++)
                    {
                        byte[] colorBytes = binaryReader.ReadBytes(4);

                        Color color = Color.FromArgb(colorBytes[3], colorBytes[0], colorBytes[1], colorBytes[2]);
                        colors.Add(color);
                    }
                }
            }

            colors = colors.Distinct().OrderBy(c => c.A).ToList();

            while (colors.Count < 16)
            {
                Color color = Color.FromArgb(255, 255, 255, 255);
                colors.Add(color);
            }

            byte[] buffer = new byte[data.Length];
            int n = 0;

            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (BinaryReader binaryReader = new BinaryReader(new MemoryStream(data)))
                {
                    for (int i = 0; i < data.Length / 4; i++)
                    {
                        byte[] colorBytes = binaryReader.ReadBytes(4);

                        Color color = Color.FromArgb(colorBytes[3], colorBytes[0], colorBytes[1], colorBytes[2]);
                        int colorRef = colors.FindIndex(c => c == color);

                        buffer[n++] = (byte)colorRef;
                    }
                }
            }

            return buffer;
        }

        private byte[] ColorsToPALNRefs(Bitmap bitmap)
        {
            BMPImage bmpImage = new BMPImage(bitmap);
            List<Color> colors = new List<Color>();

            bmpImage.LockBits();

            for (int x = 0; x < bitmap.Width; x++)
            {
                for (int y = 0; y < bitmap.Height; y++)
                {
                    Color color = bmpImage.GetPixel(x, y);
                    colors.Add(color);
                }
            }

            bmpImage.UnlockBits();

            colors = colors.Distinct().OrderBy(c => c.A).ToList();

            while (colors.Count < 16)
            {
                Color color = Color.FromArgb(255, 255, 255, 255);
                colors.Add(color);
            }

            byte[] buffer = new byte[bitmap.Width * bitmap.Height];

            bmpImage.LockBits();

            for (int y = 0; y < bitmap.Height; y++)
            {
                for (int x = 0; x < bitmap.Width; x++)
                {
                    Color color = bmpImage.GetPixel(x, y);
                    int colorRef = colors.FindIndex(c => c == color);

                    buffer[y * bitmap.Width + x] = (byte)colorRef;
                }
            }

            bmpImage.UnlockBits();

            return buffer;
        }

        private bool ImportPALNFile(Texture texture, string path)
        {
            CreateBackupOfTexture(texture);

            string extension = Path.GetExtension(path);

            if (extension.Equals(".dds"))
            {
                return ImportDDSFile(texture, path, "A8B8G8R8");
            }

            Bitmap bitmap;
            byte[] data;
            int width, height;

            if (extension.Equals(".tga"))
            {
                bitmap = Paloma.TargaImage.LoadTargaImage(path);

                width = bitmap.Width;
                height = bitmap.Height;
            }
            else
            {
                try
                {
                    using (var stream = File.OpenRead(path))
                    {
                        ImageResult image = ImageResult.FromStream(stream, StbImageSharp.ColorComponents.RedGreenBlueAlpha);

                        data = image.Data;
                        width = image.Width;
                        height = image.Height;
                    }
                }
                catch(IOException ex)
                {
                    MessageBox.Show(ex.Message, "Glacier TEX Editor", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return false;
                }

                bitmap = BMPImage.DataToBitmap(data, width, height);
            }

            data = ColorsToPALNRefs(bitmap);

            int numOfMipMaps = CalculateNumOfMipMaps(width, height);

            texture.NumOfMipMips = numOfMipMaps;
            texture.MipMapLevelSizes = new int[numOfMipMaps];
            texture.MipMapLevelSizes[0] = data.Length;

            texture.Data = new byte[numOfMipMaps][];
            texture.Data[0] = new byte[data.Length];
            Array.Copy(data, 0, texture.Data[0], 0, data.Length);

            byte[][] mipMaps = GenerateMipMaps(bitmap, numOfMipMaps);

            for (int i = 0; i < mipMaps.Length; i++)
            {
                byte[] mipMap = mipMaps[i];

                texture.Data[i + 1] = new byte[mipMap.Length];
                texture.MipMapLevelSizes[i + 1] = mipMap.Length;

                Array.Copy(mipMap, 0, texture.Data[i + 1], 0, mipMap.Length);
            }

            texture.FileSize = CalculateFileSize(texture);

            return true;
        }

        private byte[][] GetGrayScalePalette()
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

        private byte[] GetI8ColorReferences(Bitmap bitmap)
        {
            BMPImage bmpImage = new BMPImage(bitmap);
            byte[] buffer = new byte[bitmap.Width * bitmap.Height];
            byte[][] palette = GetGrayScalePalette();

            bmpImage.LockBits();

            for (int y = 0; y < bitmap.Height; y++)
            {
                for (int x = 0; x < bitmap.Width; x++)
                {
                    Color color = bmpImage.GetPixel(x, y);
                    int colorRef = 0;

                    for (int i = 0; i < palette.Length; i++)
                    {
                        byte[] colorBytes = palette[i];
                        Color color2 = Color.FromArgb(255, colorBytes[0], colorBytes[0], colorBytes[0]);

                        if (color == color2)
                        {
                            colorRef = i;
                            break;
                        }
                    }

                    buffer[y * bitmap.Width + x] = (byte)colorRef;
                }
            }

            bmpImage.UnlockBits();

            return buffer;
        }

        private bool ImportI8File(Texture texture, string path)
        {
            CreateBackupOfTexture(texture);

            string extension = Path.GetExtension(path);

            if (extension.Equals(".dds"))
            {
                return ImportDDSFile(texture, path, "L8");
            }

            Bitmap bitmap;
            byte[] data;
            int width, height;

            if (extension.Equals(".tga"))
            {
                bitmap = Paloma.TargaImage.LoadTargaImage(path);
            }
            else
            {
                try
                {
                    using (var stream = File.OpenRead(path))
                    {
                        ImageResult image = ImageResult.FromStream(stream, StbImageSharp.ColorComponents.RedGreenBlueAlpha);

                        data = image.Data;
                        width = image.Width;
                        height = image.Height;
                    }
                }
                catch(IOException ex)
                {
                    MessageBox.Show(ex.Message, "Glacier TEX Editor", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return false;
                }

                bitmap = BMPImage.DataToBitmap(data, width, height);
            }

            byte[] buffer = GetI8ColorReferences(bitmap);

            int numOfMipMaps = CalculateNumOfMipMaps(bitmap.Width, bitmap.Height);

            texture.NumOfMipMips = numOfMipMaps;
            texture.MipMapLevelSizes = new int[numOfMipMaps];
            texture.MipMapLevelSizes[0] = buffer.Length;

            texture.Data = new byte[numOfMipMaps][];
            texture.Data[0] = new byte[buffer.Length];
            Array.Copy(buffer, 0, texture.Data[0], 0, buffer.Length);

            byte[][] mipMaps = GenerateMipMaps(bitmap, numOfMipMaps);

            for (int i = 0; i < mipMaps.Length; i++)
            {
                byte[] mipMap = mipMaps[i];

                texture.Data[i] = new byte[mipMap.Length];
                texture.MipMapLevelSizes[i] = mipMap.Length;

                Array.Copy(mipMap, 0, texture.Data[i], 0, mipMap.Length);
            }

            texture.FileSize = CalculateFileSize(texture);

            return true;
        }

        private bool ImportU8V8File(Texture texture, string path)
        {
            CreateBackupOfTexture(texture);

            string extension = Path.GetExtension(path);

            if (extension.Equals(".dds"))
            {
                return ImportDDSFile(texture, path, "A8B8G8R8");
            }

            Bitmap bitmap;
            byte[] buffer;
            int width, height;

            if (extension.Equals(".tga"))
            {
                bitmap = Paloma.TargaImage.LoadTargaImage(path);

                width = bitmap.Width;
                height = bitmap.Height;
            }
            else
            {
                try
                {
                    using (var stream = File.OpenRead(path))
                    {
                        ImageResult image = ImageResult.FromStream(stream, StbImageSharp.ColorComponents.RedGreenBlueAlpha);

                        buffer = image.Data;
                        width = image.Width;
                        height = image.Height;
                    }
                }
                catch (IOException ex)
                {
                    MessageBox.Show(ex.Message, "Glacier TEX Editor", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return false;
                }

                bitmap = BMPImage.DataToBitmap(buffer, width, height);
            }

            BMPImage bmpImage = new BMPImage(bitmap);
            List<byte> data = new List<byte>();

            bmpImage.LockBits();

            for (int x = 0; x < bitmap.Width; x++)
            {
                for (int y = 0; y < bitmap.Height; y++)
                {
                    Color color = bmpImage.GetPixel(x, y);

                    data.Add(color.R);
                    data.Add(color.G);
                }
            }

            bmpImage.UnlockBits();

            int numOfMipMaps = CalculateNumOfMipMaps(bitmap.Width, bitmap.Height);

            texture.NumOfMipMips = numOfMipMaps;
            texture.MipMapLevelSizes = new int[numOfMipMaps];
            texture.MipMapLevelSizes[0] = data.Count;

            texture.Data = new byte[numOfMipMaps][];
            texture.Data[0] = new byte[data.Count];
            Array.Copy(data.ToArray(), 0, texture.Data[0], 0, data.Count);

            int n = 0;

            while (n < numOfMipMaps - 1)
            {
                width /= 2;
                height /= 2;

                Bitmap resized = new Bitmap(bitmap, new Size(width, height));
                bmpImage = new BMPImage(resized);

                List<byte> data2 = new List<byte>();

                bmpImage.LockBits();

                for (int x = 0; x < resized.Width; x++)
                {
                    for (int y = 0; y < resized.Height; y++)
                    {
                        Color color = bmpImage.GetPixel(x, y);

                        data2.Add(color.R);
                        data2.Add(color.G);
                    }
                }

                bmpImage.UnlockBits();

                texture.Data[n] = new byte[data2.Count];
                Array.Copy(data2.ToArray(), 0, texture.Data[n++], 0, data2.Count);
            }

            return true;
        }


        private string ShowSaveFileDialog(string title, string fileName, string filter)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.Title = title;
            saveFileDialog.FileName = fileName;
            saveFileDialog.Filter = filter;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                return saveFileDialog.FileName;
            }

            return null;
        }

        private void ExportFile()
        {
            foreach (ListViewItem lvItem in lvTexDetails.SelectedItems)
            {
                Texture texture = textures.Where(t => t.Index == Convert.ToInt32(lvItem.Text)).FirstOrDefault();
                string type = texture.Type1;

                bool exported = false;

                switch (type)
                {
                    case "DXT1":
                        exported = ExportDXTFile(texture);
                        break;
                    case "DXT3":
                        exported = ExportDXTFile(texture);
                        break;
                    case "RGBA":
                        exported = ExportRGBAFile(texture);
                        break;
                    case "PALN":
                        exported = ExportPALNFile(texture);
                        break;
                    case "I8  ":
                        exported = ExportI8File(texture);
                        break;
                    case "U8V8":
                        exported = ExportU8V8File(texture);
                        break;
                    default:
                        MessageBox.Show("Unknown texture type.", "Glacier TEX Editor", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }

                if (exported)
                {
                    toolStripStatusLabel1.Text = "Image(s) exported sucessfully.";
                }
            }
        }

        private void ExportAllFiles()
        {
            toolStripStatusLabel1.Text = "Exporting all textures...";

            using (ExportAllTextures exportAllTextures = new ExportAllTextures())
            {
                exportAllTextures.Textures = textures;
                DialogResult dialogResult = exportAllTextures.ShowDialog();

                if (dialogResult == DialogResult.OK)
                {
                    Dictionary<int, Option> options = exportAllTextures.ExportOptions;

                    foreach (KeyValuePair<int, Option> option in options)
                    {
                        Texture texture = textures[option.Key];

                        foreach (var entry in option.Value.Extensions)
                        {
                            string extension = entry.Key;
                            string exportPath = Path.Combine(exportAllTextures.ExportPath, texture.GetFileName(extension));

                            if (extension.Equals(".dds"))
                            {
                                if (option.Value.ExportAsSingleFile)
                                {
                                    ExportSelectedOptionsToDDS(texture, entry.Value, texture.Type1, exportPath);
                                }
                                else
                                {
                                    Dictionary<int, string> paths = GetExportPaths(entry.Value, texture.Width, texture.Height, extension, exportPath);

                                    foreach (KeyValuePair<int, string> entry2 in paths)
                                    {
                                        switch (texture.Type1)
                                        {
                                            case "DXT1":
                                                ExportDXTFile(texture, entry2.Value, entry2.Key);
                                                break;
                                            case "DXT3":
                                                ExportDXTFile(texture, entry2.Value, entry2.Key);
                                                break;
                                            case "RGBA":
                                                ExportRGBAFile(texture, entry2.Value, entry2.Key);
                                                break;
                                            case "PALN":
                                                ExportPALNFile(texture, entry2.Value, entry2.Key);
                                                break;
                                            case "I8  ":
                                                ExportI8File(texture, entry2.Value, entry2.Key);
                                                break;
                                            case "U8V8":
                                                ExportU8V8File(texture, entry2.Value, entry2.Key);
                                                break;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                Dictionary<int, string> paths = GetExportPaths(entry.Value, texture.Width, texture.Height, extension, exportPath);

                                foreach (KeyValuePair<int, string> entry2 in paths)
                                {
                                    switch (texture.Type1)
                                    {
                                        case "DXT1":
                                            ExportDXTFile(texture, entry2.Value, entry2.Key);
                                            break;
                                        case "DXT3":
                                            ExportDXTFile(texture, entry2.Value, entry2.Key);
                                            break;
                                        case "RGBA":
                                            ExportRGBAFile(texture, entry2.Value, entry2.Key);
                                            break;
                                        case "PALN":
                                            ExportPALNFile(texture, entry2.Value, entry2.Key);
                                            break;
                                        case "I8  ":
                                            ExportI8File(texture, entry2.Value, entry2.Key);
                                            break;
                                        case "U8V8":
                                            ExportU8V8File(texture, entry2.Value, entry2.Key);
                                            break;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            toolStripStatusLabel1.Text = "All textures exported successfully.";
        }

        private string ReverseTypeText(string type)
        {
            char[] charArray = type.ToCharArray();
            Array.Reverse(charArray);

            return new string(charArray);
        }

        private bool CreateTEXFile()
        {
            TEX texture = new TEX();

            texture.texHeader.unk1 = tex.texHeader.unk1;
            texture.texHeader.unk2 = tex.texHeader.unk2;

            List<TEXEntry> entries = new List<TEXEntry>();

            foreach (Texture texture1 in textures)
            {
                TEXEntry texEntry = new TEXEntry();

                texEntry.fileSize = (uint)texture1.FileSize;
                texEntry.type1 = ReverseTypeText(texture1.Type1).ToCharArray();
                texEntry.type2 = ReverseTypeText(texture1.Type2).ToCharArray();
                texEntry.index = (uint)texture1.Index;
                texEntry.height = (ushort)texture1.Height;
                texEntry.width = (ushort)texture1.Width;
                texEntry.numOfMipMaps = (uint)texture1.NumOfMipMips;
                texEntry.unk1 = (uint)texture1.Unk1;
                texEntry.unk2 = (uint)texture1.Unk2;
                texEntry.unk3 = (uint)texture1.Unk3;

                byte[] data = Encoding.ASCII.GetBytes(texture1.FileName + char.MinValue);
                texEntry.fileName = data;
                texEntry.fileData = new List<FileData>();

                for (int i = 0; i < texture1.MipMapLevelSizes.Length; i++)
                {
                    FileData fileData = new FileData();

                    fileData.mipMapLevelSize = (uint)texture1.MipMapLevelSizes[i];
                    fileData.data = texture1.Data[i];

                    texEntry.fileData.Add(fileData);
                }

                if (texture1.Type1.Equals("PALN"))
                {
                    texEntry.palData = new byte[texture1.PalData.Length];

                    Array.Copy(texture1.PalData, 0, texEntry.palData, 0, texture1.PalData.Length);
                }

                if (texture1.IndicesCount > 0)
                {
                    texEntry.indicesCount = (uint)texture1.IndicesCount;
                    texEntry.indices = new uint[texture1.IndicesCount];

                    for (int i = 0; i < texture1.IndicesCount; i++)
                    {
                        texEntry.indices[i] = (uint)texture1.Indices[i];
                    }
                }

                entries.Add(texEntry);
            }

            texture.entries = entries;

            int table1Offset = 0;
            string extractPath = Path.Combine(GetOutputPath(), Path.GetFileNameWithoutExtension(filePath) + "_New.TEX");

            try
            {
                using (FileStream fs = new FileStream(extractPath, FileMode.Create))
                {
                    using (BinaryWriter binaryWriter = new BinaryWriter(fs))
                    {
                        binaryWriter.Write(0);
                        binaryWriter.Write(0);
                        binaryWriter.Write(texture.texHeader.unk1);
                        binaryWriter.Write(texture.texHeader.unk2);

                        for (int i = 0; i < texture.entries.Count; i++)
                        {
                            binaryWriter.Write(texture.entries[i].fileSize);
                            binaryWriter.Write(texture.entries[i].type1);
                            binaryWriter.Write(texture.entries[i].type2);
                            binaryWriter.Write(texture.entries[i].index);
                            binaryWriter.Write(texture.entries[i].height);
                            binaryWriter.Write(texture.entries[i].width);
                            binaryWriter.Write(texture.entries[i].numOfMipMaps);
                            binaryWriter.Write(texture.entries[i].unk1);
                            binaryWriter.Write(texture.entries[i].unk2);
                            binaryWriter.Write(texture.entries[i].unk3);

                            if (texture.entries[i].fileName == null)
                            {
                                binaryWriter.Write(0);
                            }
                            else
                            {
                                binaryWriter.Write(texture.entries[i].fileName);
                            }

                            for (int j = 0; j < texture.entries[i].fileData.Count; j++)
                            {
                                binaryWriter.Write(texture.entries[i].fileData[j].mipMapLevelSize);

                                for (int k = 0; k < texture.entries[i].fileData[j].data.Length; k++)
                                {
                                    binaryWriter.Write(texture.entries[i].fileData[j].data[k]);
                                }
                            }

                            string type = ReverseTypeText(new string(texture.entries[i].type1));

                            if (type.Equals("PALN"))
                            {
                                binaryWriter.Write(16);

                                for (int j = 0; j < texture.entries[i].palData.Length; j++)
                                {
                                    binaryWriter.Write(texture.entries[i].palData[j]);
                                }
                            }

                            Align(binaryWriter, 16);

                            if (texture.entries[i].indicesCount > 0)
                            {
                                textures[i].IndicesOffset = binaryWriter.BaseStream.Position;

                                binaryWriter.Write(texture.entries[i].indicesCount);

                                for (int j = 0; j < texture.entries[i].indices.Length; j++)
                                {
                                    binaryWriter.Write(texture.entries[i].indices[j]);
                                }

                                Align(binaryWriter, 16);
                            }

                            if (i < textures.Count - 1)
                            {
                                textures[i + 1].Offset = (int)binaryWriter.BaseStream.Position;
                            }
                            else
                            {
                                table1Offset = (int)binaryWriter.BaseStream.Position;
                            }
                        }

                        texture.texHeader.table1Offset = (uint)table1Offset;
                        texture.table1Offsets = new uint[tex.table1Offsets.Length];
                        texture.table2Offsets = new uint[tex.table2Offsets.Length];

                        for (int i = 0; i < textures.Count; i++)
                        {
                            texture.table1Offsets[i + countOfEmptyOffsets] = (uint)textures[i].Offset;
                        }

                        using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                        {
                            using (BinaryReader binaryReader = new BinaryReader(fileStream))
                            {
                                for (int i = 0; i < tex.table2Offsets.Length; i++)
                                {
                                    if (tex.table2Offsets[i] != 0)
                                    {
                                        fileStream.Seek(tex.table2Offsets[i], SeekOrigin.Begin);
                                        int indicesCount = binaryReader.ReadInt32();
                                        int[] indices = new int[indicesCount];

                                        for (int j = 0; j < indices.Length; j++)
                                        {
                                            indices[j] = binaryReader.ReadInt32();
                                        }

                                        int index = 0;

                                        if (indices[indices.Length - 1] == 0)
                                        {
                                            for (int j = indices.Length - 1; j >= 0; j--)
                                            {
                                                if (indices[j] > 0)
                                                {
                                                    index = indices[j] - countOfEmptyOffsets;
                                                    break;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            index = indices[indices.Length - 1] - countOfEmptyOffsets;
                                        }

                                        texture.table2Offsets[i] = (uint)textures[index].IndicesOffset;
                                    }
                                }
                            }
                        }

                        for (int i = 0; i < texture.table1Offsets.Length; i++)
                        {
                            binaryWriter.Write(texture.table1Offsets[i]);
                        }

                        texture.texHeader.table2Offset = (uint)binaryWriter.BaseStream.Position;

                        for (int i = 0; i < texture.table2Offsets.Length; i++)
                        {
                            binaryWriter.Write(texture.table2Offsets[i]);
                        }

                        fs.Seek(0, SeekOrigin.Begin);
                        binaryWriter.Write(texture.texHeader.table1Offset);
                        binaryWriter.Write(texture.texHeader.table2Offset);
                    }
                }
            }
            catch(IOException ex)
            {
                MessageBox.Show(ex.Message, "Glacier TEX Editor", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }

            return true;
        }

        private void Align(BinaryWriter binaryWriter, int alignment, byte padding = 0x00)
        {
            long position = binaryWriter.BaseStream.Position;

            if (position % alignment == 0)
            {
                return;
            }

            long number = alignment - position % alignment;

            for (long i = 0; i < number; i++)
            {
                binaryWriter.Write(padding);
            }
        }

        private void FilterList()
        {
            List<Texture> textures1 = new List<Texture>();

            lvTexDetails.Items.Clear();

            var checkedBoxes = grpTextureTypes.Controls.OfType<CheckBox>().Where(c => c.Checked);

            foreach (CheckBox chk in checkedBoxes)
            {
                textures1.AddRange(textures.Where(t => t.Type1.Equals(chk.Text)));
            }

            AddTexturesToList(textures1);
        }

        private void BtnRefreshList_Click(object sender, EventArgs e)
        {
            FilterList();

            var checkedBoxes = grpTextureTypes.Controls.OfType<CheckBox>();
            configuration.WriteCheckBoxesState(checkedBoxes);
        }

        private void AddTexturesToList(List<Texture> textures)
        {
            foreach (Texture texture in textures)
            {
                ListViewItem lvItem = new ListViewItem(texture.Index.ToString());

                lvItem.SubItems.Add(texture.FileName);
                lvItem.SubItems.Add(texture.Offset.ToString("X"));
                lvItem.SubItems.Add(texture.Type1);
                lvItem.SubItems.Add(texture.FileSize.ToString());

                string dimensions = texture.Width + "x" + texture.Height;
                lvItem.SubItems.Add(dimensions);
                lvItem.SubItems.Add(texture.NumOfMipMips.ToString());

                lvTexDetails.Items.Add(lvItem);
            }
        }

        private void TxtSearchName_TextChanged(object sender, EventArgs e)
        {
            lvTexDetails.Items.Clear();

            List<Texture> textures1 = new List<Texture>();
            textures1.AddRange(textures.Where(t => t.FileName.ToLower().Contains(txtSearchName.Text.ToLower())));

            AddTexturesToList(textures1);
        }

        private void LvTexDetails_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvTexDetails.SelectedItems.Count == 1)
            {
                int index = Convert.ToInt32(lvTexDetails.SelectedItems[0].Text);
                Texture texture = textures.Where(t => t.Index == index).FirstOrDefault();

                DisplayTexture(texture);
            }

            if (!tsmiImportFile.Enabled)
            {
                tsmiImportFile.Enabled = true;
                tsmiExportFile.Enabled = true;

                cmsImportFile.Enabled = true;
                cmsExportFile.Enabled = true;
            }
        }

        private string GetOutputPath()
        {
            string outputPath = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "Temp");

            if (!Directory.Exists(outputPath))
            {
                Directory.CreateDirectory(outputPath);
            }

            return outputPath;
        }

        private void DisplayTexture(Texture texture)
        {
            int index = 0;

            int width = texture.Width;
            int height = texture.Height;

            if (texture.Width > pbTexture.Width || texture.Height > pbTexture.Height)
            {
                while (width > pbTexture.Width || height > pbTexture.Height)
                {
                    width /= 2;
                    height /= 2;

                    index++;
                }

                width = texture.Width / (int)Math.Pow(2, index);
                height = texture.Height / (int)Math.Pow(2, index);
            }

            switch (texture.Type1)
            {
                case "DXT1":
                    DisplayDXTTexture(texture, index, width, height);
                    break;
                case "DXT3":
                    DisplayDXTTexture(texture, index, width, height);
                    break;
                case "RGBA":
                    DisplayRGBATexture(texture, index, width, height);
                    break;
                case "PALN":
                    DisplayPALNTexture(texture, index, width, height);
                    break;
                case "I8  ":
                    DisplayI8Texture(texture, index, width, height);
                    break;
                case "U8V8":
                    DisplayU8V8Texture(texture, index, width, height);
                    break;
                default:
                    MessageBox.Show("Unknown texture type.", "Glacier TEX Editor", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }
        }

        private void DisplayDXTTexture(Texture texture, int index, int width, int height)
        {
            byte[] data = ConvertDXTToRGBA(texture.Data[index], width, height, texture.Type1);

            byte[] data2 = new byte[data.Length];
            Array.Copy(data, 0, data2, 0, data.Length);

            ConvertRGBAToBGRA(data, width, height);
            Bitmap bmp = BMPImage.DataToBitmap(data, width, height);

            pbTexture.Image = bmp;
        }

        private void ConvertRGBAToBGRA(byte[] data, int width, int height)
        {
            for (int i = 0; i < width * height; i++)
            {
                byte r = data[i * 4];
                byte g = data[i * 4 + 1];
                byte b = data[i * 4 + 2];
                byte a = data[i * 4 + 3];

                data[i * 4] = b;
                data[i * 4 + 1] = g;
                data[i * 4 + 2] = r;
                data[i * 4 + 3] = a;
            }
        }

        private void ConvertBGRAToRGBA(byte[] data, int width, int height)
        {
            for (int i = 0; i < width * height; i++)
            {
                byte b = data[i * 4];
                byte g = data[i * 4 + 1];
                byte r = data[i * 4 + 2];
                byte a = data[i * 4 + 3];

                data[i * 4] = r;
                data[i * 4 + 1] = g;
                data[i * 4 + 2] = b;
                data[i * 4 + 3] = a;
            }
        }

        private void DisplayRGBATexture(Texture texture, int index, int width, int height)
        {
            int length = texture.Data[index].Length;
            byte[] data = new byte[length];
            Array.Copy(texture.Data[index], 0, data, 0, length);

            ConvertRGBAToBGRA(data, width, height);
            Bitmap bmp = BMPImage.DataToBitmap(data, width, height);

            pbTexture.Image = bmp;
        }

        private void DisplayPALNTexture(Texture texture, int index, int width, int height)
        {
            byte[] data = GetColorsFromPALNRefs(texture, index);
            ConvertRGBAToBGRA(data, width, height);

            Bitmap bmp = BMPImage.DataToBitmap(data, width, height);

            pbTexture.Image = BMPImage.ReplaceTransparency(bmp, Color.Black);
        }

        private void DisplayI8Texture(Texture texture, int index, int width, int height)
        {
            byte[] data = texture.Data[index];
            Bitmap bmp = BMPImage.DataToBitmap(data, width, height, PixelFormat.Format8bppIndexed);

            pbTexture.Image = bmp;
        }

        private void DisplayU8V8Texture(Texture texture, int index, int width, int height)
        {
            byte[] data = texture.Data[index];
            byte[] colors = ConvertU8V8ToBGRA(data);
            Bitmap bmp = BMPImage.DataToBitmap(colors, width, height);

            pbTexture.Image = bmp;
        }

        private void AddZipFiles(string path)
        {
            toolStripStatusLabel1.Text = "Adding ZIP files.";

            List<string> files = Directory.GetFiles(path, "*.zip*", SearchOption.AllDirectories).ToList();

            cbZipFiles.Items.Clear();

            foreach (string file in files)
            {
                cbZipFiles.Items.Add(file);
            }

            toolStripStatusLabel1.Text = "ZIP files added successfully.";
        }

        private void BtnCreateTEXFile_Click(object sender, EventArgs e)
        {
            if (filePath == null)
            {
                return;
            }

            toolStripStatusLabel1.Text = "Creating TEX file...";
            statusStrip1.Update();

            bool created = CreateTEXFile();

            if (created)
            {
                string fileName = Path.GetFileNameWithoutExtension(filePath);
                toolStripStatusLabel1.Text = fileName + " TEX file created sucessfully.";

                savedChanges = true;
                importedFile = false;

                if (autoUpdateZIP)
                {
                    string path = Path.Combine(GetOutputPath(), Path.GetFileNameWithoutExtension(filePath) + "_New.TEX");

                    if (UpdateZIPFile(path))
                    {
                        string zipFileName = Path.GetFileNameWithoutExtension(selectedZipPath);
                        toolStripStatusLabel1.Text = zipFileName + " ZIP file updated sucessfully.";

                        File.Delete(path);
                    }
                }
            }
        }

        private bool UpdateZIPFile(string path)
        {
            toolStripStatusLabel1.Text = "Updating ZIP file...";
            statusStrip1.Update();

            try
            {
                using (FileStream zipToOpen = new FileStream(selectedZipPath, FileMode.Open))
                {
                    using (ZipArchive archive = new ZipArchive(zipToOpen, ZipArchiveMode.Update))
                    {
                        ZipArchiveEntry entry = archive.GetEntry("Scenes/" + Path.GetFileName(filePath));

                        if (entry != null)
                        {
                            entry.Delete();
                        }

                        archive.CreateEntryFromFile(path, "Scenes/" + Path.GetFileName(filePath), compressionLevel);
                    }
                }
            }
            catch(IOException ex)
            {
                MessageBox.Show(ex.Message, "Glacier TEX Editor", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }

            return true;
        }

        private void BtnUpdateZipFile_Click(object sender, EventArgs e)
        {
            string path = Path.Combine(GetOutputPath(), Path.GetFileNameWithoutExtension(filePath) + "_New.TEX");

            if (!File.Exists(path) || selectedZipPath == null)
            {
                return;
            }

            if (UpdateZIPFile(path))
            {
                string zipFileName = Path.GetFileNameWithoutExtension(selectedZipPath);
                toolStripStatusLabel1.Text = zipFileName + " ZIP file updated sucessfully.";

                File.Delete(path);
            }
        }

        private string ShortenPath(string path)
        {
            int index = path.IndexOf('\\', path.Length / 5);
            string str1 = path.Substring(index + 1);
            string str2 = str1.Substring(0, str1.IndexOf('\\', 1));

            return path.Replace(str2, "...");
        }

        private void AddRecentFilesToMenu()
        {
            List<string> recentFiles = configuration.GetRecentFiles();
            ToolStripMenuItem recentFile;

            for (int i = 0; i < recentFiles.Count; i++)
            {
                string path = recentFiles[i];

                if (path.Length > 50 && File.Exists(path))
                {
                    path = ShortenPath(path);
                }

                recentFile = new ToolStripMenuItem(i + "  " + path, null, RecentFile_Click);
                tsmiOpenRecent.DropDownItems.Add(recentFile);
            }

            tsmiOpenRecent.DropDownItems.Add(new ToolStripSeparator());
            recentFile = new ToolStripMenuItem("Clear Recent File List", null, ClearRecentFileList_Click);
            tsmiOpenRecent.DropDownItems.Add(recentFile);
        }

        private void RecentFile_Click(object sender, EventArgs e)
        {
            List<string> recentFiles = configuration.GetRecentFiles();
            int index = Convert.ToInt32(sender.ToString().Substring(0, sender.ToString().IndexOf(' ')));
            filePath = recentFiles[index];

            ReadTexFile();
        }

        private void ClearRecentFileList_Click(object sender, EventArgs e)
        {
            tsmiOpenRecent.DropDownItems.Clear();

            configuration.ClearRecentFilesFromINI();
            tsmiOpenRecent.Enabled = false;
        }

        private void SetCheckBoxesState()
        {
            List<string> lines = File.ReadAllLines("GlacierTEXEditor.ini").ToList();
            int index = lines.FindIndex(l => l.StartsWith("CheckBoxes"));
            var checkedBoxes = grpTextureTypes.Controls.OfType<CheckBox>();

            if (index != -1)
            {
                int i = index + 1;

                while (!lines[i].StartsWith("CompressionLevel"))
                {
                    bool enabled = lines[i].Substring(lines[i].IndexOf('=') + 1).Equals("True");

                    string checkBoxName = lines[i].Substring(0, lines[i].IndexOf('='));
                    checkedBoxes.Where(c => c.Name.Equals(checkBoxName)).First().Checked = enabled;

                    i++;
                }
            }
            else
            {
                configuration.WriteCheckBoxesState(checkedBoxes);
            }
        }

        private byte[] GetDDSMainImageData(string path, string type, out int width, out int height)
        {
            using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                using (BinaryReader binaryReader = new BinaryReader(fileStream))
                {
                    for (int i = 0; i < 3; i++)
                    {
                        binaryReader.ReadInt32();
                    }

                    height = binaryReader.ReadInt32();
                    width = binaryReader.ReadInt32();

                    for (int i = 0; i < 27; i++)
                    {
                        binaryReader.ReadInt32();
                    }

                    int mainImageSize = CalculateMainImageSize(type, width, height);
                    byte[] data = binaryReader.ReadBytes(mainImageSize);

                    return data;
                }
            }
        }

        private void TsmiOpenTEX_Click(object sender, EventArgs e)
        {
            OpenTEXFile();
        }

        private void TsmiOpenImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "Microsoft DirectDraw Surface (*.DDS)|*.dds" +
                    "|Truevision TGA (*.TGA)|*.tga" +
                    "|Bitmap (*.BMP)|*.bmp" +
                    "|Portable Network Graphics (*.PNG)|*.png" +
                    "|Joint Photographic Experts Group (*.JPG, *JPEG)|*.jpg;*.jpeg";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string path = openFileDialog.FileName;
                string extension = Path.GetExtension(path);
                Bitmap bmp;

                if (extension.Equals(".tga"))
                {
                    bmp = Paloma.TargaImage.LoadTargaImage(path);
                }
                else if (extension.Equals(".dds"))
                {
                    using (SelectTextureType selectTextureType = new SelectTextureType())
                    {
                        DialogResult dialogResult = selectTextureType.ShowDialog();

                        if (dialogResult == DialogResult.OK)
                        {
                            string type = selectTextureType.TextureType;
                            byte[] data = GetDDSMainImageData(path, type, out int width, out int height);
                            byte[] data2 = ConvertDXTToRGBA(data, width, height, type);

                            bmp = BMPImage.DataToBitmap(data2, width, height);
                        }
                        else
                        {
                            return;
                        }
                    }
                }
                else
                {
                    bmp = new Bitmap(filePath);
                }

                using (DisplayImage displayImage = new DisplayImage())
                {
                    displayImage.Image = bmp;
                    displayImage.ShowDialog();
                }
            }
        }

        private void TsmiImportFile_Click(object sender, EventArgs e)
        {
            ImportFile();
        }

        private void TsmiExportFile_Click(object sender, EventArgs e)
        {
            ExportFile();
        }

        private void TsmiExtractAllFiles_Click(object sender, EventArgs e)
        {
            ExportAllFiles();
        }

        private void TsmiExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void TsmiUndo_Click(object sender, EventArgs e)
        {
            int count = texturesBackup.ElementAt(indexOfLastModifiedTexture).Value.Count;

            Texture backup = texturesBackup.ElementAt(indexOfLastModifiedTexture).Value[--indexOfCurrentBackup];
            textures[backup.Index - countOfEmptyOffsets] = backup;

            tsmiUndo.Enabled = indexOfCurrentBackup >= 1;
            tsmiRedo.Enabled = indexOfCurrentBackup < count;

            toolStripStatusLabel1.Text = "Changes to texture reverted successfully.";
        }

        private void TsmiRedo_Click(object sender, EventArgs e)
        {
            int count = texturesBackup.ElementAt(indexOfLastModifiedTexture).Value.Count;

            Texture backup = texturesBackup.ElementAt(indexOfLastModifiedTexture).Value[indexOfCurrentBackup++];
            textures[backup.Index - countOfEmptyOffsets] = backup;

            tsmiUndo.Enabled = indexOfCurrentBackup >= 1;
            tsmiRedo.Enabled = indexOfCurrentBackup < count;

            toolStripStatusLabel1.Text = "Texture successfully changed.";
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (importedFile && !savedChanges)
            {
                DialogResult dialogResult = MessageBox.Show("Do you want to save changes before exiting app?", "Glacier TEX Editor", 
                    MessageBoxButtons.YesNo);

                if (dialogResult == DialogResult.Yes)
                {
                    e.Cancel = true;

                    CreateTEXFile();
                }
            }
        }

        private void TsmiSaveTEX_Click(object sender, EventArgs e)
        {
            SaveTEXFile();
        }

        private void TsmiAbout_Click(object sender, EventArgs e)
        {
            using (About about = new About())
            {
                about.ShowDialog();
            }
        }

        private void TsmiSettings_Click(object sender, EventArgs e)
        {
            using (About about = new About())
            {
                Settings settings = new Settings();
                DialogResult dialogResult = settings.ShowDialog();

                if (dialogResult == DialogResult.OK)
                {
                    compressionLevel = settings.CompressionLvl;
                    autoUpdateZIP = settings.UpdateZIPAutomatically;
                }
            }
        }

        private void CmsOpenTEX_Click(object sender, EventArgs e)
        {
            OpenTEXFile();
        }

        private void CmsSaveTEX_Click(object sender, EventArgs e)
        {
            SaveTEXFile();
        }

        private void CmsExtractAllFiles_Click(object sender, EventArgs e)
        {
            ExportAllFiles();
        }

        private void CmsExportFile_Click(object sender, EventArgs e)
        {
            ExportFile();
        }
    }
}
