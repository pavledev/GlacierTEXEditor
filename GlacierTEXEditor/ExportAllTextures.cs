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

namespace GlacierTEXEditor
{
    public partial class ExportAllTextures : Form
    {
        private Dictionary<int, Option> options = new Dictionary<int, Option>();
        private Dictionary<int, Option> options1 = new Dictionary<int, Option>();
        private bool checkSelectedDimensions = false;
        private int previousIndex = 0;
        private readonly List<string> extensions = new List<string>() { ".dds", ".tga", ".bmp", ".png", ".jpg" };

        public ExportAllTextures()
        {
            InitializeComponent();

            lvTextures.FullRowSelect = true;

            chkDXT1.Checked = true;
            chkDXT3.Checked = true;
            chkRGBA.Checked = true;
            chkPALN.Checked = true;
            chkI8.Checked = true;
            chkU8V8.Checked = true;

            lbExtensions.Items.Add("DDS");
            lbExtensions.Items.Add("TGA");
            lbExtensions.Items.Add("BMP");
            lbExtensions.Items.Add("PNG");
            lbExtensions.Items.Add("JPG");

            lbExtensions.Enabled = false;
            cklDimensions.Enabled = false;
            cklDimensions.CheckOnClick = true;

            chkDDS.Tag = 0;
            chkTGA.Tag = 1;
            chkBMP.Tag = 2;
            chkPNG.Tag = 3;
            chkJPG.Tag = 4;

            chkExportAsSingleFile.Visible = false;

            ExportPath = Path.GetDirectoryName(Application.ExecutablePath) + @"\";
        }

        public List<Texture> Textures
        {
            get;
            set;
        }

        public Dictionary<int, Option> ExportOptions
        {
            get;
            set;
        }

        public bool ExportAsSingleFile
        {
            get;
            set;
        }

        public string ExportPath
        {
            get;
            set;
        }

        private void ExportAllTextures_Load(object sender, EventArgs e)
        {
            foreach (Texture texture in Textures)
            {
                ListViewItem listViewItem = new ListViewItem(texture.Index.ToString());
                listViewItem.SubItems.Add(texture.FileName);
                listViewItem.SubItems.Add(texture.Type1);

                lvTextures.Items.Add(listViewItem);
            }
        }

        private void BtnRefreshList_Click(object sender, EventArgs e)
        {
            List<Texture> textures1 = new List<Texture>();

            lvTextures.Items.Clear();

            var checkedBoxes = grpTextureTypes.Controls.OfType<CheckBox>().Where(c => c.Checked);

            foreach (CheckBox chk in checkedBoxes)
            {
                textures1.AddRange(Textures.Where(t => t.Type1.Equals(chk.Text)));
            }

            foreach (Texture texture in textures1)
            {
                ListViewItem listViewItem = new ListViewItem(texture.Index.ToString());
                listViewItem.SubItems.Add(texture.FileName);
                listViewItem.SubItems.Add(texture.Type1);

                lvTextures.Items.Add(listViewItem);
            }
        }

        private void BtnSelectAllDimensions_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < cklDimensions.Items.Count; i++)
            {
                cklDimensions.SetItemChecked(i, true);
            }
        }

        private void BtnClearAllDimensions_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < cklDimensions.Items.Count; i++)
            {
                cklDimensions.SetItemChecked(i, false);
            }
        }

        private void BtnExportAllTextures_Click(object sender, EventArgs e)
        {
            if (chkDDS.Checked || chkTGA.Checked || chkBMP.Checked || chkPNG.Checked || chkJPG.Checked)
            {
                foreach (CheckBox checkBox in grpExportAllAs.Controls.OfType<CheckBox>())
                {
                    if (checkBox.Checked)
                    {
                        for (int i = 0; i < Textures.Count; i++)
                        {
                            if (!options1.ContainsKey(i))
                            {
                                options1.Add(i, new Option());
                            }

                            string extension = extensions[Convert.ToInt32(checkBox.Tag)];

                            if (extension.Equals(".dds"))
                            {
                                options1.ElementAt(i).Value.ExportAsSingleFile = true;
                            }

                            var dictionary = options1.ElementAt(i).Value.Extensions;
                            dictionary.Add(extension, new Dictionary<int, string>());

                            List<string> dimensions = GetDimensions(i);

                            for (int j = 0; j < dimensions.Count; j++)
                            {
                                int width = Convert.ToInt32(dimensions[j].Substring(0, dimensions[j].IndexOf('x')));
                                dictionary.ElementAt(dictionary.Count - 1).Value.Add(Textures[i].Width / width, dimensions[j]);
                            }
                        }
                    }
                }

                ExportOptions = options1;
            }
            else
            {
                ExportOptions = options;

                for (int i = 0; i < Textures.Count; i++)
                {
                    if (!ExportOptions.ContainsKey(i))
                    {
                        ExportOptions.Add(i, new Option());

                        ExportOptions.ElementAt(i).Value.ExportAsSingleFile = true;
                        ExportOptions.ElementAt(i).Value.Extensions.Add("DDS", new Dictionary<int, string>());

                        List<string> dimensions = GetDimensions(i);

                        for (int j = 0; j < dimensions.Count; j++)
                        {
                            int width = Convert.ToInt32(dimensions[j].Substring(0, dimensions[j].IndexOf('x')));
                            ExportOptions.ElementAt(i).Value.Extensions.ElementAt(0).Value.Add(Textures[i].Width / width, dimensions[j]);
                        }
                    }
                }
            }

            DialogResult = DialogResult.OK;
        }

        private void AddDimensionsToList()
        {
            int textureIndex = Textures.FindIndex(t => t.Index == Convert.ToInt32(lvTextures.SelectedItems[0].Text));
            List<string> dimensions = GetDimensions(textureIndex);

            cklDimensions.Items.Clear();

            for (int i = 0; i < dimensions.Count; i++)
            {
                cklDimensions.Items.Add(dimensions[i]);
            }
        }

        private List<string> GetDimensions(int textureIndex)
        {
            List<string> dimensions = new List<string>();

            int width = Textures[textureIndex].Width;
            int height = Textures[textureIndex].Height;

            dimensions.Add(width + "x" + height);

            while (width > 1 && height > 1)
            {
                width /= 2;
                height /= 2;

                dimensions.Add(width + "x" + height);
            }

            return dimensions;
        }

        private void LvTextures_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvTextures.SelectedItems.Count == 1)
            {
                AddDimensionsToList();
            }

            if (!lbExtensions.Enabled)
            {
                lbExtensions.Enabled = true;
            }
        }

        private bool CheckDimensions(int selectedIndex)
        {
            bool checkBoxChcked = cklDimensions.GetItemChecked(selectedIndex);

            if (!checkBoxChcked)
            {
                List<int> indices = new List<int>();

                for (int i = 0; i < cklDimensions.Items.Count; i++)
                {
                    bool itemChecked = cklDimensions.GetItemChecked(i);

                    if (itemChecked)
                    {
                        indices.Add(i);
                    }
                }

                for (int i = 0; i < indices.Count - 1; i++)
                {
                    if (Math.Abs(indices[i] - indices[i + 1]) != 1)
                    {
                        int index = indices[i + 1];
                        cklDimensions.SetItemChecked(index, false);

                        previousIndex = indices[i];
                    }
                }

                return true;
            }

            int count = 0;

            for (int i = 0; i < cklDimensions.Items.Count; i++)
            {
                bool itemChecked = cklDimensions.GetItemChecked(i);

                if (itemChecked)
                {
                    count++;
                }
            }

            if (count == 1)
            {
                previousIndex = selectedIndex;

                return true;
            }

            string previousCheckBoxText = cklDimensions.Items[previousIndex].ToString();
            int widthOfPreviousCheckBox = Convert.ToInt32(previousCheckBoxText.Substring(0, previousCheckBoxText.IndexOf('x')));
            int heightOfPreviousCheckBox = Convert.ToInt32(previousCheckBoxText.Substring(previousCheckBoxText.IndexOf('x') + 1));

            string currentCheckBoxText = cklDimensions.Items[selectedIndex].ToString();
            int widthOfCurrentCheckBox = Convert.ToInt32(currentCheckBoxText.Substring(0, currentCheckBoxText.IndexOf('x')));
            int heightOfCurrentCheckBox = Convert.ToInt32(currentCheckBoxText.Substring(currentCheckBoxText.IndexOf('x') + 1));

            if (!previousCheckBoxText.Equals(currentCheckBoxText))
            {
                if (widthOfPreviousCheckBox / 2 == widthOfCurrentCheckBox && heightOfPreviousCheckBox / 2 == heightOfCurrentCheckBox
                    || widthOfPreviousCheckBox * 2 == widthOfCurrentCheckBox && heightOfPreviousCheckBox * 2 == heightOfCurrentCheckBox)
                {
                    previousIndex = selectedIndex;
                    return true;
                }

                if (previousIndex < selectedIndex)
                {
                    MessageBox.Show("Selected dimensions must be half of previous");

                    cklDimensions.SetItemChecked(selectedIndex, false);

                    return false;
                }
                else
                {
                    MessageBox.Show("Selected dimensions must be twice as large as the previous one");

                    cklDimensions.SetItemChecked(selectedIndex, false);

                    return false;
                }
            }
            else
            {
                previousIndex = selectedIndex;
            }

            return true;
        }

        private void SetSelectedCheckBoxes(int textureIndex, string extension)
        {
            for (int i = 0; i < cklDimensions.Items.Count; i++)
            {
                cklDimensions.SetItemChecked(i, false);
            }

            var dimensions = options[textureIndex].Extensions[extension].Values;

            if (dimensions.Count > 0)
            {
                List<string> list = dimensions.Select(d => d).ToList();
                List<int> indices = new List<int>();

                for (int i = 0; i < list.Count; i++)
                {
                    for (int j = 0; j < cklDimensions.Items.Count; j++)
                    {
                        if (cklDimensions.Items[j].ToString().Equals(list[i]))
                        {
                            cklDimensions.SetItemChecked(j, true);
                        }
                    }
                }

                for (int i = 0; i < indices.Count; i++)
                {
                    cklDimensions.SetItemChecked(indices[i], false);
                }
            }
        }

        private void CklDimensions_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = cklDimensions.SelectedIndex;

            if (checkSelectedDimensions && !CheckDimensions(index))
            {
                return;
            }

            int textureIndex = Textures.FindIndex(s => s.Index == Convert.ToInt32(lvTextures.SelectedItems[0].Text));
            string extension = extensions[lbExtensions.SelectedIndex];
            bool itemChecked = cklDimensions.GetItemChecked(cklDimensions.SelectedIndex);

            string dimensions = cklDimensions.SelectedItem.ToString();

            if (itemChecked)
            {
                options[textureIndex].Extensions[extension].Add(index, dimensions);
            }
            else
            {
                var item = options[textureIndex].Extensions[extension].First(d => d.Value.Equals(dimensions));
                options[textureIndex].Extensions[extension].Remove(item.Key);
            }
        }

        private void CklExtensions_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cklDimensions.SelectedIndex >= 0)
            {
                cklDimensions.ClearSelected();
            }

            int index = Textures.FindIndex(s => s.Index == Convert.ToInt32(lvTextures.SelectedItems[0].Text));
            SetSelectedCheckBoxes(index, lbExtensions.SelectedItem.ToString());
        }

        private void BtnSelectAllExtensionTypes_Click(object sender, EventArgs e)
        {
            chkDDS.Checked = true;
            chkTGA.Checked = true;
            chkBMP.Checked = true;
            chkPNG.Checked = true;
            chkJPG.Checked = true;
        }

        private void BtnChangeExportPath_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.ValidateNames = false;
            openFileDialog.CheckFileExists = false;
            openFileDialog.CheckPathExists = true;
            openFileDialog.FileName = "Folder Selection.";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                ExportPath = Path.GetDirectoryName(openFileDialog.FileName);
                txtExportPath.Text = ExportPath;
            }
        }

        private void CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;

            lbExtensions.Enabled = !(chkDDS.Checked || chkTGA.Checked || chkBMP.Checked || chkPNG.Checked || chkJPG.Checked);
            cklDimensions.Enabled = !(chkDDS.Checked || chkTGA.Checked || chkBMP.Checked || chkPNG.Checked || chkJPG.Checked);

            int textureIndex = Convert.ToInt32(lvTextures.SelectedItems[0].Text);

            if (!options1.ContainsKey(textureIndex))
            {
                options1.Add(textureIndex, new Option());
            }

            int extensionIndex = Convert.ToInt32(checkBox.Tag);
            string extension = extensions[extensionIndex];

            if (checkBox.Checked)
            {
                options1[textureIndex].Extensions.Add(extension, new Dictionary<int, string>());

                for (int i = 0; i < cklDimensions.Items.Count; i++)
                {
                    string dimensions = cklDimensions.Items[i].ToString();
                    int width = Convert.ToInt32(dimensions.Substring(0, dimensions.IndexOf('x')));

                    options1[textureIndex].Extensions.ElementAt(extensionIndex).Value.Add(Textures[textureIndex].Width / width, dimensions);
                }
            }
            else
            {
                options[textureIndex].Extensions.Remove(extension);
            }
        }

        private void ChkExportAsSingleFile_CheckedChanged(object sender, EventArgs e)
        {
            int textureIndex = Textures.FindIndex(s => s.Index == Convert.ToInt32(lvTextures.SelectedItems[0].Text));
            options[textureIndex].ExportAsSingleFile = chkExportAsSingleFile.Checked;
        }

        private void LbExtensions_SelectedIndexChanged(object sender, EventArgs e)
        {
            cklDimensions.Enabled = lbExtensions.SelectedItems.Count > 0;

            int index = Textures.FindIndex(s => s.Index == Convert.ToInt32(lvTextures.SelectedItems[0].Text));
            string extension = extensions[lbExtensions.SelectedIndex];

            chkExportAsSingleFile.Visible = extension.Equals(".dds");

            if (!options.ContainsKey(index))
            {
                options.Add(index, new Option());
            }

            if (!options[index].Extensions.ContainsKey(extension))
            {
                options[index].Extensions.Add(extension, new Dictionary<int, string>());
            }

            if (extension.Equals(".dds"))
            {
                checkSelectedDimensions = true;
            }

            SetSelectedCheckBoxes(index, extension);
        }
    }
}
