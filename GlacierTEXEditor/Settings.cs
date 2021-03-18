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

        public GameVersion GameVersion
        {
            get;
            set;
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            CompressionLvl = configuration.GetCompressionLevel();
            UpdateZIPAutomatically = configuration.GetAutoUpdateZIPState();
            GameVersion = configuration.GetGameVersion();

            chkUpdateZipAutomatically.Checked = UpdateZIPAutomatically;

            if (CompressionLvl == CompressionLevel.Optimal)
            {
                rbOptimal.Checked = true;
            }
            else if (CompressionLvl == CompressionLevel.Fastest)
            {
                rbFastest.Checked = true;
            }

            switch (GameVersion)
            {
                case GameVersion.PC:
                    rbPC.Checked = true;
                    break;
                case GameVersion.PS2:
                    break;
                case GameVersion.PS3:
                    break;
                case GameVersion.PS4:
                    rbPS4.Checked = true;
                    break;
                case GameVersion.XBOX:
                    break;
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

            if (rbPC.Checked)
            {
                configuration.WriteGameVersion("PC");
            }
            else
            {
                configuration.WriteGameVersion("PS4");
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

        private void RbPC_CheckedChanged(object sender, EventArgs e)
        {
            GameVersion = GameVersion.PC;
        }

        private void RbPS4_CheckedChanged(object sender, EventArgs e)
        {
            GameVersion = GameVersion.PS4;
        }
    }
}
