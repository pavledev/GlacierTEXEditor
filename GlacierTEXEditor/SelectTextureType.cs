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
    public partial class SelectTextureType : Form
    {
        public SelectTextureType()
        {
            InitializeComponent();

            rbDXT1.Tag = "DXT1";
            rbDXT3.Tag = "DXT3";
            rbRGBA.Tag = "RGBA";
            rbPALN.Tag = "PALN";
            rbI8.Tag = "I8";
            rbU8V8.Tag = "U8V8";

            rbDXT1.Checked = true;
        }

        public string TextureType
        {
            get;
            set;
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;

            TextureType = radioButton.Tag.ToString();
        }
    }
}
