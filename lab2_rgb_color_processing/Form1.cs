using openCV;
using System;
using System.Windows.Forms;

namespace lab2_rgb_color_processing
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent(); // Initialize UI components
        }

        IplImage image1; // Original loaded image
        IplImage img;    // Processed image (after color filtering)

        private void redToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Create a new image with same size and properties as original
            img = cvlib.CvCreateImage(new CvSize(image1.width, image1.height), image1.depth, image1.nChannels);

            // Get memory addresses of source and destination images
            int srcAdd = image1.imageData.ToInt32();
            int dstAdd = img.imageData.ToInt32();

            unsafe
            {
                int srcIndex, dstIndex;

                // Loop through each pixel
                for (int r = 0; r < img.height; r++)
                    for (int c = 0; c < img.width; c++)
                    {
                        // Calculate pixel index (BGR format)
                        srcIndex = dstIndex = (img.width * r * img.nChannels) + (c * img.nChannels);

                        // Keep RED channel only, set Blue and Green to 0
                        *(byte*)(dstAdd + dstIndex + 0) = 0; // Blue
                        *(byte*)(dstAdd + dstIndex + 1) = 0; // Green
                        *(byte*)(dstAdd + dstIndex + 2) = *(byte*)(srcAdd + srcIndex + 2); // Red
                    }
            }

            // Resize result to fit PictureBox
            CvSize size = new CvSize(pictureBox2.Width, pictureBox2.Height);
            IplImage resized_image = cvlib.CvCreateImage(size, img.depth, img.nChannels);
            cvlib.CvResize(ref img, ref resized_image, cvlib.CV_INTER_LINEAR);

            // Display processed image
            pictureBox2.BackgroundImage = (System.Drawing.Image)resized_image;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = " ";
            openFileDialog1.Filter = "JPEG|*JPG|Bitmap|*.bmp|All|*.*-11";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Load image in color mode (BGR)
                    image1 = cvlib.CvLoadImage(openFileDialog1.FileName, cvlib.CV_LOAD_IMAGE_COLOR);

                    // Resize to fit PictureBox
                    CvSize size = new CvSize(pictureBox1.Width, pictureBox1.Height);
                    IplImage resized_image = cvlib.CvCreateImage(size, image1.depth, image1.nChannels);
                    cvlib.CvResize(ref image1, ref resized_image, cvlib.CV_INTER_LINEAR);

                    // Display original image
                    pictureBox1.BackgroundImage = (System.Drawing.Image)resized_image;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message); // Show error if loading fails
                }
            }
        }

        private void greenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            img = cvlib.CvCreateImage(new CvSize(image1.width, image1.height), image1.depth, image1.nChannels);

            int srcAdd = image1.imageData.ToInt32();
            int dstAdd = img.imageData.ToInt32();

            unsafe
            {
                int srcIndex, dstIndex;

                for (int r = 0; r < img.height; r++)
                    for (int c = 0; c < img.width; c++)
                    {
                        srcIndex = dstIndex = (img.width * r * img.nChannels) + (c * img.nChannels);

                        // Keep GREEN channel only
                        *(byte*)(dstAdd + dstIndex + 0) = 0; // Blue
                        *(byte*)(dstAdd + dstIndex + 1) = *(byte*)(srcAdd + srcIndex + 1); // Green
                        *(byte*)(dstAdd + dstIndex + 2) = 0; // Red
                    }
            }

            CvSize size = new CvSize(pictureBox2.Width, pictureBox2.Height);
            IplImage resized_image = cvlib.CvCreateImage(size, img.depth, img.nChannels);
            cvlib.CvResize(ref img, ref resized_image, cvlib.CV_INTER_LINEAR);

            pictureBox2.BackgroundImage = (System.Drawing.Image)resized_image;
        }

        private void blueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            img = cvlib.CvCreateImage(new CvSize(image1.width, image1.height), image1.depth, image1.nChannels);

            int srcAdd = image1.imageData.ToInt32();
            int dstAdd = img.imageData.ToInt32();

            unsafe
            {
                int srcIndex, dstIndex;

                for (int r = 0; r < img.height; r++)
                    for (int c = 0; c < img.width; c++)
                    {
                        srcIndex = dstIndex = (img.width * r * img.nChannels) + (c * img.nChannels);

                        // Keep BLUE channel only
                        *(byte*)(dstAdd + dstIndex + 0) = *(byte*)(srcAdd + srcIndex + 0); // Blue
                        *(byte*)(dstAdd + dstIndex + 1) = 0; // Green
                        *(byte*)(dstAdd + dstIndex + 2) = 0; // Red
                    }
            }

            CvSize size = new CvSize(pictureBox2.Width, pictureBox2.Height);
            IplImage resized_image = cvlib.CvCreateImage(size, img.depth, img.nChannels);
            cvlib.CvResize(ref img, ref resized_image, cvlib.CV_INTER_LINEAR);

            pictureBox2.BackgroundImage = (System.Drawing.Image)resized_image;
        }
        private void pictureBox2_Click(object sender, EventArgs e) { }
        private void pictureBox1_Click(object sender, EventArgs e) { }
    }
}