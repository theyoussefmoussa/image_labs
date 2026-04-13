using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using openCV;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab1
{
    public partial class Form1 : Form
    {
        // Holds the original loaded image in OpenCV format (IplImage)
        IplImage image1;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Form initialization logic (currently not used)
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Reset file dialog path
            openFileDialog1.FileName = " ";

            // Set allowed image formats
            openFileDialog1.Filter = "JPEG|*JPG|Bitmap|*.bmp|All|*.*";

            // Open file dialog and check if user selected a file
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Load image using OpenCV library (color mode)
                    image1 = cvlib.CvLoadImage(
                        openFileDialog1.FileName,
                        cvlib.CV_LOAD_IMAGE_COLOR
                    );

                    // Define target size based on PictureBox dimensions
                    CvSize size = new CvSize(
                        pictureBox1.Width,
                        pictureBox1.Height
                    );

                    // Create a resized image container
                    IplImage resized_image = cvlib.CvCreateImage(
                        size,
                        image1.depth,
                        image1.nChannels
                    );

                    // Resize original image into the new container
                    cvlib.CvResize(
                        ref image1,
                        ref resized_image,
                        cvlib.CV_INTER_LINEAR
                    );

                    // Display resized image in PictureBox
                    pictureBox1.BackgroundImage = (Image)resized_image;
                }
                catch (Exception ex)
                {
                    // Show error message if image loading fails
                    MessageBox.Show(ex.Message);
                }
            }
        }


    }
}