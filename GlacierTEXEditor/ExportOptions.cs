using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GlacierTEXEditor
{
    public partial class ExportOptions : Form
    {
        private int previousIndex = 0;
        private readonly List<string> dimensions = new List<string>();

        public ExportOptions()
        {
            InitializeComponent();
        }

        public string FileName
        {
            get;
            set;
        }

        public Dictionary<int, string> SelectedDimensions { get; set; } = new Dictionary<int, string>();

        public string Extension
        {
            get;
            set;
        }

        public bool ExportAsSingleFile
        {
            get;
            set;
        }

        private void ExportOptions_Load(object sender, EventArgs e)
        {
            if (!Extension.Equals(".dds"))
            {
                chkExportAsSingleFile.Visible = false;
            }
            else
            {
                chkExportAsSingleFile.Checked = true;
            }

            CheckBox checkBox;

            for (int i = 0; i < dimensions.Count; i++)
            {
                checkBox = new CheckBox();
                checkBox.Tag = i.ToString();
                checkBox.Text = dimensions[i];
                checkBox.AutoSize = true;
                checkBox.Location = new Point(15, i * 30);
                checkBox.Font = new Font(checkBox.Font.FontFamily, 12.0f);

                if (Extension.Equals(".dds") || Extension.Equals(".dds"))
                {
                    checkBox.CheckedChanged += CbDimensions_CheckedChanged;
                }

                pnlDimensions.Controls.Add(checkBox);
            }
        }

        public void SetDimensions(int width, int height)
        {
            dimensions.Add(width + "x" + height);

            while (width > 1 && height > 1)
            {
                width /= 2;
                height /= 2;

                dimensions.Add(width + "x" + height);
            }
        }

        private void BtnExportImage_Click(object sender, EventArgs e)
        {
            foreach (Control control in pnlDimensions.Controls)
            {
                CheckBox checkBox = (CheckBox)control;

                if (checkBox.Checked)
                {
                    SelectedDimensions.Add(Convert.ToInt32(checkBox.Tag), checkBox.Text);
                }
            }

            if (SelectedDimensions.Count > 0)
            {
                DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("Please select dimensions.");
            }
        }

        private void CheckSelectedDimensions(CheckBox checkBox)
        {
            if (!checkBox.Checked)
            {
                List<int> indices = new List<int>();

                foreach (CheckBox checkBox2 in pnlDimensions.Controls)
                {
                    if (checkBox2.Checked)
                    {
                        indices.Add(Convert.ToInt32(checkBox2.Tag));
                    }
                }

                for (int i = 0; i < indices.Count - 1; i++)
                {
                    if (Math.Abs(indices[i] - indices[i + 1]) != 1)
                    {
                        int index = indices[i + 1];
                        ((CheckBox)pnlDimensions.Controls[index]).Checked = false;

                        previousIndex = indices[i];
                    }
                }

                return;
            }

            int count = 0;

            foreach (CheckBox checkBox2 in pnlDimensions.Controls)
            {
                if (checkBox2.Checked)
                {
                    count++;
                }
            }

            if (count == 1)
            {
                previousIndex = Convert.ToInt32(checkBox.Tag);

                return;
            }

            string previousCheckBoxText = pnlDimensions.Controls[previousIndex].Text;
            int widthOfPreviousCheckBox = Convert.ToInt32(previousCheckBoxText.Substring(0, previousCheckBoxText.IndexOf('x')));
            int heightOfPreviousCheckBox = Convert.ToInt32(previousCheckBoxText.Substring(previousCheckBoxText.IndexOf('x') + 1));

            string currentCheckBoxText = checkBox.Text;
            int widthOfCurrentCheckBox = Convert.ToInt32(currentCheckBoxText.Substring(0, currentCheckBoxText.IndexOf('x')));
            int heightOfCurrentCheckBox = Convert.ToInt32(currentCheckBoxText.Substring(currentCheckBoxText.IndexOf('x') + 1));

            if (!previousCheckBoxText.Equals(currentCheckBoxText))
            {
                if (widthOfPreviousCheckBox / 2 == widthOfCurrentCheckBox && heightOfPreviousCheckBox / 2 == heightOfCurrentCheckBox
                    || widthOfPreviousCheckBox * 2 == widthOfCurrentCheckBox && heightOfPreviousCheckBox * 2 == heightOfCurrentCheckBox)
                {
                    previousIndex = Convert.ToInt32(checkBox.Tag);
                    return;
                }

                if (previousIndex < Convert.ToInt32(checkBox.Tag))
                {
                    MessageBox.Show("Selected dimensions must be half of previous");

                    checkBox.Checked = false;
                }
                else
                {
                    MessageBox.Show("Selected dimensions must be twice as large as the previous one");

                    checkBox.Checked = false;
                }
            }
            else
            {
                previousIndex = Convert.ToInt32(checkBox.Tag);
            }
        }

        private void CbDimensions_CheckedChanged(object sender, EventArgs e)
        {
            if (chkExportAsSingleFile.Checked)
            {
                CheckBox checkBox = sender as CheckBox;

                CheckSelectedDimensions(checkBox);
            }
        }

        private void BtnSelectAll_Click(object sender, EventArgs e)
        {
            foreach (CheckBox checkBox in pnlDimensions.Controls)
            {
                if (!checkBox.Checked)
                {
                    checkBox.Checked = true;
                }
            }
        }

        private void ChkExportAsSingleFile_CheckedChanged(object sender, EventArgs e)
        {
            ExportAsSingleFile = chkExportAsSingleFile.Checked;
        }
    }
}
