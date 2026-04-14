using openCV;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace lab3_gray_scale_conversion
{
    public partial class Form1 : Form
    {
        IplImage image1; // Original image loaded using OpenCV
        Bitmap bmp;      // Bitmap used for pixel manipulation in .NET
        public Form1()
        {
            InitializeComponent();
        }

        private void convertToGrayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Create a Bitmap copy from the image displayed in PictureBox1
            // (avoids invalid casting from IplImage to Bitmap)
            bmp = new Bitmap(pictureBox1.Image);

            int width = bmp.Width;
            int height = bmp.Height;

            Color p;  // Stores pixel color

            // Iterate over every pixel in the image
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    // Get pixel color at (x, y)
                    p = bmp.GetPixel(x, y);

                    // Extract ARGB components
                    int a = p.A; // Alpha (transparency)
                    int r = p.R; // Red
                    int g = p.G; // Green
                    int b = p.B; // Blue

                    // Compute grayscale intensity using average method
                    int avg = (r + g + b) / 3;

                    // Set pixel to grayscale (R = G = B = avg)
                    bmp.SetPixel(x, y, Color.FromArgb(a, avg, avg, avg));
                }
            }
            // Display the processed grayscale image
            pictureBox2.Image = bmp;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = " ";
            openFileDialog1.Filter = "JPEG|*JPG|Bitmap|*.bmp|All|*.*-11";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Load image using OpenCV (BGR format)
                    image1 = cvlib.CvLoadImage(openFileDialog1.FileName, cvlib.CV_LOAD_IMAGE_COLOR);
                    
                    // Resize image to fit PictureBox1
                    CvSize size = new CvSize(pictureBox1.Width, pictureBox1.Height);
                    IplImage resized_image = cvlib.CvCreateImage(size, image1.depth, image1.nChannels);
                    cvlib.CvResize(ref image1, ref resized_image, cvlib.CV_INTER_LINEAR);

                    // Display resized image in PictureBox1
                    pictureBox1.Image = (System.Drawing.Image)resized_image;



                }
                catch (Exception ex)
                {
                    // Show error message if loading fails
                    MessageBox.Show(ex.Message);
                }
            }
            }
        private void pictureBox2_Click(object sender, EventArgs e) 
        { 
        }

    }
}
