using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Compression;

namespace GlacierTEXEditor
{
    public partial class Settings : Form
    {
        Configuration configuration = new Configuration();

        public Settings()
        {
            InitializeComponent();

            rbFastest.Checked = true;
        }

        public CompressionLevel CompressionLvl
        {
            get;
            set;
        }

        public bool UpdateZIPAutomatically
        {
            get;
            set;
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            CompressionLvl = configuration.GetCompressionLevel();
            UpdateZIPAutomatically = configuration.GetAutoUpdateZIPState();

            chkUpdateZipAutomatically.Checked = UpdateZIPAutomatically;

            if (CompressionLvl == CompressionLevel.Optimal)
            {
                rbOptimal.Checked = true;
            }
            else if (CompressionLvl == CompressionLevel.Fastest)
            {
                rbFastest.Checked = true;
            }
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            if (rbOptimal.Checked)
            {
                configuration.WriteCompressionLevel("Optimal");
            }
            else
            {
                configuration.WriteCompressionLevel("Fastest");
            }

            configuration.SetAutoUpdateZIP(chkUpdateZipAutomatically.Checked);

            DialogResult = DialogResult.OK;
        }

        private void RbOptimal_CheckedChanged(object sender, EventArgs e)
        {
            CompressionLvl = CompressionLevel.Optimal;
        }

        private void RbFastest_CheckedChanged(object sender, EventArgs e)
        {
            CompressionLvl = CompressionLevel.Fastest;
        }

        private void ChkUpdateZipAutomatically_CheckedChanged(object sender, EventArgs e)
        {
            UpdateZIPAutomatically = chkUpdateZipAutomatically.Checked;
        }
    }
}
