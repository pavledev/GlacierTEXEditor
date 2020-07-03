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
    public partial class DisplayImage : Form
    {
        public DisplayImage()
        {
            InitializeComponent();
        }

        private void DisplayImage_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = Image;
            pictureBox1.Size = Image.Size;
            pictureBox1.Anchor = AnchorStyles.None;
            pictureBox1.Location = new Point((pictureBox1.Parent.ClientSize.Width / 2) - (pictureBox1.Width / 2),
                              (pictureBox1.Parent.ClientSize.Height / 2) - (pictureBox1.Height / 2));
        }

        public Bitmap Image
        {
            get;
            set;
        }
    }
}
