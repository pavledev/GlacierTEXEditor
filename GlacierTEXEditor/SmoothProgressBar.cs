using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GlacierTEXEditor
{
    public partial class SmoothProgressBar : UserControl
    {
        float min = 0;
        float max = 100;
        float val = 0;
        Color color = Color.Blue;
        Color textColor = Color.Black;

        public SmoothProgressBar()
        {
            InitializeComponent();
        }

        protected override void OnResize(EventArgs e)
        {
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            using (Graphics graphics = e.Graphics)
            {
                using (SolidBrush brush = new SolidBrush(color))
                {
                    float percent = (val - min) / (max - min);
                    Rectangle rect = ClientRectangle;

                    rect.Width = (int)(rect.Width * percent);

                    graphics.FillRectangle(brush, rect);

                    Draw3DBorder(graphics);
                }
            }
        }

        public float Minimum
        {
            get
            {
                return min;
            }

            set
            {
                if (value < 0)
                {
                    min = 0;
                }

                if (value > max)
                {
                    min = value;
                    min = value;
                }

                if (val < min)
                {
                    val = min;
                }

                Invalidate();
            }
        }

        public float Maximum
        {
            get
            {
                return max;
            }

            set
            {
                if (value < min)
                {
                    min = value;
                }

                max = value;

                if (val > max)
                {
                    val = max;
                }

                Invalidate();
            }
        }

        public float Value
        {
            get
            {
                return val;
            }

            set
            {
                float oldValue = val;

                if (value < min)
                {
                    val = min;
                }
                else if (value > max)
                {
                    val = max;
                }
                else
                {
                    val = value;
                }

                float percent;

                Rectangle newValueRect = ClientRectangle;
                Rectangle oldValueRect = ClientRectangle;

                percent = (val - min) / (max - min);
                newValueRect.Width = (int)(newValueRect.Width * percent);

                percent = (oldValue - min) / (max - min);
                oldValueRect.Width = (int)(oldValueRect.Width * percent);

                Rectangle updateRect = new Rectangle();

                if (newValueRect.Width > oldValueRect.Width)
                {
                    updateRect.X = oldValueRect.Size.Width;
                    updateRect.Width = newValueRect.Width - oldValueRect.Width;
                }
                else
                {
                    updateRect.X = newValueRect.Size.Width;
                    updateRect.Width = oldValueRect.Width - newValueRect.Width;
                }

                updateRect.Height = Height;

                Invalidate(updateRect);
            }
        }

        public Color ProgressBarBackColor
        {
            get
            {
                return color;
            }

            set
            {
                color = value;

                Invalidate();
            }
        }

        public Color TextColor
        {
            get
            {
                return textColor;
            }

            set
            {
                textColor = value;

                Invalidate();
            }
        }

        private void Draw3DBorder(Graphics g)
        {
            int penWidth = (int)Pens.White.Width;

            g.DrawLine(Pens.Silver,
            new Point(ClientRectangle.Left, ClientRectangle.Top),
            new Point(ClientRectangle.Width - penWidth, ClientRectangle.Top));

            g.DrawLine(Pens.Silver,
            new Point(ClientRectangle.Left, ClientRectangle.Top),
            new Point(ClientRectangle.Left, ClientRectangle.Height - penWidth));

            g.DrawLine(Pens.Silver,
            new Point(ClientRectangle.Left, ClientRectangle.Height - penWidth),
            new Point(ClientRectangle.Width - penWidth, ClientRectangle.Height - penWidth));

            g.DrawLine(Pens.Silver,
            new Point(ClientRectangle.Width - penWidth, ClientRectangle.Top),
            new Point(ClientRectangle.Width - penWidth, ClientRectangle.Height - penWidth));
        }
    }
}
