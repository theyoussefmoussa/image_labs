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

namespace lab4_histogram_analysis
{
    public partial class Form1 : Form
    {
        IplImage image1;       // original color image loaded from disk
        Bitmap bitmap;
        IplImage grayImage;    // grayscale version used for histogram computation
        bool imageLoaded = false;
        bool grayImageReady = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e) { }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Images|*.jpg;*.jpeg;*.bmp;*.png";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // load image as BGR color (3 channels)
                    image1 = cvlib.CvLoadImage(openFileDialog1.FileName, cvlib.CV_LOAD_IMAGE_COLOR);

                    // resize to fit pictureBox1 dimensions
                    CvSize size = new CvSize(pictureBox1.Width, pictureBox1.Height);
                    IplImage resized = cvlib.CvCreateImage(size, image1.depth, image1.nChannels);
                    cvlib.CvResize(ref image1, ref resized, cvlib.CV_INTER_LINEAR);

                    pictureBox1.Image = cvlib.ToBitmap(resized, true);
                    imageLoaded = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void convertToGrayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!imageLoaded)
            {
                MessageBox.Show("Load image first");
                return;
            }

            // allocate gray image with same dimensions and channel count as source
            // (3 channels so all R=G=B = grayValue for display compatibility)
            IplImage gray = cvlib.CvCreateImage(
                new CvSize(image1.width, image1.height),
                image1.depth,
                image1.nChannels);

            int srcAdd = image1.imageData.ToInt32();
            int dstAdd = gray.imageData.ToInt32();

            unsafe
            {
                for (int r = 0; r < gray.height; r++)
                {
                    for (int c = 0; c < gray.width; c++)
                    {
                        // pixel byte offset: row * width * channels + col * channels
                        int index = (gray.width * r * gray.nChannels) + (c * gray.nChannels);

                        byte b = *(byte*)(srcAdd + index + 0);
                        byte g = *(byte*)(srcAdd + index + 1);
                        byte rC = *(byte*)(srcAdd + index + 2);

                        // simple average grayscale formula
                        byte grayValue = (byte)((rC + g + b) / 3);

                        // write same value to all 3 channels → neutral gray pixel
                        *(byte*)(dstAdd + index + 0) = grayValue;
                        *(byte*)(dstAdd + index + 1) = grayValue;
                        *(byte*)(dstAdd + index + 2) = grayValue;
                    }
                }
            }

            // BUG FIX: save the computed gray image so histogram can access it
            grayImage = gray;
            grayImageReady = true;

            // resize for display in pictureBox2
            CvSize size = new CvSize(pictureBox2.Width, pictureBox2.Height);
            IplImage resized = cvlib.CvCreateImage(size, gray.depth, gray.nChannels);
            cvlib.CvResize(ref gray, ref resized, cvlib.CV_INTER_LINEAR);

            pictureBox2.Image = cvlib.ToBitmap(resized, true);
        }

        private void histogramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // check grayImage specifically, not just imageLoaded
            if (!grayImageReady)
            {
                MessageBox.Show("Convert to grayscale first");
                return;
            }

            // frequency array: hist[i] = number of pixels with intensity i
            int[] hist = new int[256];
            int srcAdd = grayImage.imageData.ToInt32();

            unsafe
            {
                for (int r = 0; r < grayImage.height; r++)
                {
                    for (int c = 0; c < grayImage.width; c++)
                    {
                        int index = (grayImage.width * r * grayImage.nChannels) + (c * grayImage.nChannels);

                        // read only the blue channel (all 3 are equal in grayscale)
                        byte grayValue = *(byte*)(srcAdd + index);
                        hist[grayValue]++;
                    }
                }
            }

            // draw histogram as vertical bars normalized to pictureBox3 height
            int histWidth = 256;
            int histHeight = pictureBox3.Height;
            Bitmap histBitmap = new Bitmap(histWidth, histHeight);
            Graphics g = Graphics.FromImage(histBitmap);
            g.Clear(Color.White);

            int max = hist.Max();

            for (int i = 0; i < 256; i++)
            {
                // scale bar height proportionally to the peak frequency
                int barHeight = (int)(((double)hist[i] / max) * histHeight);

                g.DrawLine(
                    Pens.Black,
                    new Point(i, histHeight),           // bottom of bar
                    new Point(i, histHeight - barHeight) // top of bar
                );
            }

            pictureBox3.Image = histBitmap;
        }
    }
}