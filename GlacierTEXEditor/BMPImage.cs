using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace GlacierTEXEditor
{
    class BMPImage
    {
        private Bitmap bmp;
        private BitmapData bitmapData;
        private byte[] pixels;
        private int depth;
        private int bytesPerPixel;

        public int Width
        {
            get;
            set;
        }

        public int Height
        {
            get;
            set;
        }

        public BMPImage(Bitmap bmp)
        {
            this.bmp = bmp;
        }

        public BMPImage(string path)
        {
            bmp = new Bitmap(Image.FromFile(path));
        }

        public static Bitmap DataToBitmap(byte[] data, int width, int height, PixelFormat pixelFormat = PixelFormat.Format32bppArgb)
        {
            Bitmap bmp = new Bitmap(width, height, pixelFormat);

            if (pixelFormat == PixelFormat.Format8bppIndexed)
            {
                ColorPalette palette = bmp.Palette;

                for (int i = 0; i < 256; i++)
                {
                    Color color = Color.FromArgb((byte)i, (byte)i, (byte)i);
                    palette.Entries[i] = color;
                }

                bmp.Palette = palette;
            }

            BitmapData bmpData = bmp.LockBits(new Rectangle(Point.Empty, bmp.Size), ImageLockMode.WriteOnly, bmp.PixelFormat);

            Marshal.Copy(data, 0, bmpData.Scan0, bmpData.Stride * bmp.Height);
            bmp.UnlockBits(bmpData);

            return bmp;
        }

        public static byte[] BitmapToData(Bitmap bmp)
        {
            BitmapData bitmapData = bmp.LockBits(new Rectangle(Point.Empty, bmp.Size), ImageLockMode.ReadOnly, bmp.PixelFormat);
            int length = bitmapData.Stride * bitmapData.Height;

            byte[] data = new byte[length];

            Marshal.Copy(bitmapData.Scan0, data, 0, length);
            bmp.UnlockBits(bitmapData);

            return data;
        }

        public static Bitmap ReplaceTransparency(string file, Color background)
        {
            return ReplaceTransparency(Image.FromFile(file), background);
        }

        public static Bitmap ReplaceTransparency(Image image, Color background)
        {
            return ReplaceTransparency((Bitmap)image, background);
        }

        public static Bitmap ReplaceTransparency(Bitmap bitmap, Color background)
        {
            /* Important: you have to set the PixelFormat to remove the alpha channel.
             * Otherwise you'll still have a transparent image - just without transparent areas */
            Bitmap result = new Bitmap(bitmap.Size.Width, bitmap.Size.Height, PixelFormat.Format24bppRgb);

            using (Graphics graphics = Graphics.FromImage(result))
            {
                graphics.Clear(background);
                graphics.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;
                graphics.DrawImage(bitmap, 0, 0);
            }

            return result;
        }

        public void LockBits()
        {
            try
            {
                int pixelCount = bmp.Width * bmp.Height;
                depth = Image.GetPixelFormatSize(bmp.PixelFormat);

                if (depth != 8 && depth != 24 && depth != 32)
                {
                    MessageBox.Show("Only 8, 24 and 32 bpp images are supported.");

                    return;
                }

                bitmapData = bmp.LockBits(new Rectangle(Point.Empty, bmp.Size), ImageLockMode.ReadWrite, bmp.PixelFormat);

                bytesPerPixel = depth / 8;
                pixels = new byte[pixelCount * bytesPerPixel];

                Marshal.Copy(bitmapData.Scan0, pixels, 0, pixels.Length);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void UnlockBits()
        {
            try
            {
                Marshal.Copy(pixels, 0, bitmapData.Scan0, pixels.Length);
                bmp.UnlockBits(bitmapData);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public Color GetPixel(int x, int y)
        {
            Color color;
            int index = (y * bmp.Width + x) * bytesPerPixel;

            if (index > pixels.Length - bytesPerPixel)
            {
                throw new IndexOutOfRangeException();
            }

            if (depth == 32)
            {
                byte blue = pixels[index];
                byte green = pixels[index + 1];
                byte red = pixels[index + 2];
                byte alpha = pixels[index + 3];

                color = Color.FromArgb(alpha, red, green, blue);
            }
            else if (depth == 24)
            {
                byte blue = pixels[index];
                byte green = pixels[index + 1];
                byte red = pixels[index + 2];

                color = Color.FromArgb(red, green, blue);
            }
            else
            {
                byte pixel = pixels[index];

                color = Color.FromArgb(pixel, pixel, pixel);
            }

            return color;
        }

        public void SetPixel(int x, int y, Color color)
        {
            int index = (y * bmp.Width + x) * bytesPerPixel;

            if (depth == 32)
            {
                pixels[index] = color.B;
                pixels[index + 1] = color.G;
                pixels[index + 2] = color.R;
                pixels[index + 3] = color.A;
            }
            else if (depth == 24)
            {
                pixels[index] = color.B;
                pixels[index + 1] = color.G;
                pixels[index + 2] = color.R;
            }
            else
            {
                pixels[index] = color.B;
            }
        }
    }
}
