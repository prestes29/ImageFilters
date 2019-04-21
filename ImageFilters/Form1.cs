using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace ImageFilters
{
    public partial class Form1 : Form
    {
        Bitmap newBitmap;
        Image file;
        Boolean opened = false;
        int blurAmount = 1;
        int lastCol = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog f = new OpenFileDialog();
            f.Filter = "JPG(*.JPG)|*.jpg";

            if (f.ShowDialog() == DialogResult.OK)
            {
                file = Image.FromFile(f.FileName);
                newBitmap = new Bitmap(f.FileName);
                pictureBox1.Image = file;
                opened = true;
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            SaveFileDialog f = new SaveFileDialog();
            f.Filter = "JPG(*JPG)|*.jpg";

            if (f.ShowDialog() == DialogResult.OK)
            {
                file.Save(f.FileName);
            }
        }





        private void button3_Click(object sender, EventArgs e)
        {
            for (int x = 0; x < newBitmap.Width; x++)
            {
                for (int y = 0; y < newBitmap.Height; y++)
                {
                    Color originalColor = newBitmap.GetPixel(x, y);

                    int grayScale = (int)((originalColor.R * .3) + (originalColor.G * .59) + (originalColor.B * .11));

                    Color newColor = Color.FromArgb(grayScale, grayScale, grayScale);

                    newBitmap.SetPixel(x, y, newColor);
                }
            }
            pictureBox1.Image = newBitmap;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            for (int x = blurAmount; x <= newBitmap.Width - blurAmount; x++)
            {
                for (int y = blurAmount; y <= newBitmap.Height - blurAmount; y++)
                {
                    try
                    {
                        Color prevX = newBitmap.GetPixel(x - blurAmount, y);
                        Color nextX = newBitmap.GetPixel(x + blurAmount, y);
                        Color prevY = newBitmap.GetPixel(x, y - blurAmount);
                        Color nextY = newBitmap.GetPixel(x, y + blurAmount);

                        int avgR = (int)((prevX.R + nextX.R + prevY.R + nextY.R) / 4);
                        int avgG = (int)((prevX.G + nextX.G + prevY.G + nextY.G) / 4);
                        int avgB = (int)((prevX.B + nextX.B + prevY.B + nextY.B) / 4);

                        newBitmap.SetPixel(x, y, Color.FromArgb(avgR, avgG, avgB));
                    }
                    catch (Exception) { }
                }
            }
            pictureBox1.Image = newBitmap;
        }

        private void updateBlur(object sender, EventArgs e)
        {
            blurAmount = int.Parse(trackBar1.Value.ToString());
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //negative
            for (int y = 0; y < newBitmap.Height; y++)
            {
                for (int x = 0; x < newBitmap.Width; x++)
                {
                    //get pixel value
                    Color p = newBitmap.GetPixel(x, y);

                    //extract ARGB value from p
                    int a = p.A;
                    int r = p.R;
                    int g = p.G;
                    int b = p.B;

                    //find negative value
                    r = 255 - r;
                    g = 255 - g;
                    b = 255 - b;

                    //set new ARGB value in pixel
                    newBitmap.SetPixel(x, y, Color.FromArgb(a, r, g, b));
                }
            }
            pictureBox1.Image = newBitmap;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //color of pixel
            Color p;

            //sepia
            for (int y = 0; y < newBitmap.Height; y++)
            {
                for (int x = 0; x < newBitmap.Width; x++)
                {
                    //get pixel value
                    p = newBitmap.GetPixel(x, y);

                    //extract pixel component ARGB
                    int a = p.A;
                    int r = p.R;
                    int g = p.G;
                    int b = p.B;

                    //calculate temp value
                    int tr = (int)(0.393 * r + 0.769 * g + 0.189 * b);
                    int tg = (int)(0.349 * r + 0.686 * g + 0.168 * b);
                    int tb = (int)(0.272 * r + 0.534 * g + 0.131 * b);

                    //set new RGB value
                    if (tr > 255)
                    {
                        r = 255;
                    }
                    else
                    {
                        r = tr;
                    }

                    if (tg > 255)
                    {
                        g = 255;
                    }
                    else
                    {
                        g = tg;
                    }

                    if (tb > 255)
                    {
                        b = 255;
                    }
                    else
                    {
                        b = tb;
                    }

                    //set the new RGB value in image pixel
                    newBitmap.SetPixel(x, y, Color.FromArgb(a, r, g, b));
                }
            }

            //load sepia image in picturebox2
            pictureBox1.Image = newBitmap;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //3 bitmap for red green blue image
            Bitmap rbmp = new Bitmap(newBitmap);

            //red green blue image
            for (int y = 0; y < newBitmap.Height; y++)
            {
                for (int x = 0; x < newBitmap.Width; x++)
                {
                    //get pixel value
                    Color p = newBitmap.GetPixel(x, y);

                    //extract ARGB value from p
                    int a = p.A;
                    int r = p.R;
                    int g = p.G;
                    int b = p.B;

                    //set red image pixel
                    newBitmap.SetPixel(x, y, Color.FromArgb(a, r, 0, 0));

                }
            }
            //load red image in picturebox2
            pictureBox1.Image = newBitmap;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            for (int x = 0; x < newBitmap.Width; x++)
            {
                for (int y = 0; y < newBitmap.Height; y++)
                {
                    Color pixel = newBitmap.GetPixel(x, y);

                    int red = pixel.R;
                    int green = pixel.G;
                    int blue = pixel.B;

                    newBitmap.SetPixel(x, y, Color.FromArgb(255 - red, 255 - green, 255 - blue));                  
                }
            }
            pictureBox1.Image = newBitmap;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Bitmap nB = new Bitmap(newBitmap.Width, newBitmap.Height);

            for (int x = 1; x <= newBitmap.Width - 1; x++)
            {
                for (int y = 1; y <= newBitmap.Height - 1; y++)
                {
                    nB.SetPixel(x, y, Color.DarkGray);
                }
            }

            for (int x = 1; x <= newBitmap.Width - 1; x++)
            {
                for (int y = 1; y <= newBitmap.Height - 1; y++)
                {
                    try
                    {
                        Color pixel = newBitmap.GetPixel(x, y);

                        int colVal = (pixel.R + pixel.G + pixel.B);

                        if (lastCol == 0) lastCol = (pixel.R + pixel.G + pixel.B);

                        int diff;

                        if (colVal > lastCol) { diff = colVal - lastCol; } else { diff = lastCol - colVal; }

                        if (diff > 100)
                        {
                            nB.SetPixel(x, y, Color.Gray);
                            lastCol = colVal;
                        }


                    }
                    catch (Exception) { }
                }
            }

            for (int y = 1; y <= newBitmap.Height - 1; y++)
            {

                for (int x = 1; x <= newBitmap.Width - 1; x++)
                {
                    try
                    {
                        Color pixel = newBitmap.GetPixel(x, y);

                        int colVal = (pixel.R + pixel.G + pixel.B);

                        if (lastCol == 0) lastCol = (pixel.R + pixel.G + pixel.B);

                        int diff;

                        if (colVal > lastCol) { diff = colVal - lastCol; } else { diff = lastCol - colVal; }

                        if (diff > 100)
                        {
                            nB.SetPixel(x, y, Color.Gray);
                            lastCol = colVal;
                        }

                    }
                    catch (Exception) { }
                }

            }

            pictureBox1.Image = nB;
        }
    }
}

