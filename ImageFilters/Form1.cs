using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageFilters
{
    public partial class Form1 : Form
    {
        Filter sharpen;
        Filter edge;
        Filter blur;
        Filter sobelh;
        Filter sobelv;
        bool equalize = false;
        int interpolationMode = 0;
        double enlargmentscale = 2f;
        bool isColorised = true;
        PreviewImage img;
        string filterchosen = "";
        PreviewImage img2;
        Dictionary<string, Filter> Filters = new Dictionary<string, ImageFilters.Filter>();
        DisplayImage di;
        DisplayImage di2;
        public Form1()
        {
            InitializeComponent();
            panel1.Hide();
            sharpen = new Filter(ImageProcessor.sharpen, "sharpening");
          edge = new Filter(ImageProcessor.edge, "edge");
            //   blur = new Filter(ImageProcessor.gaussianBlur, "blur");
            blur = new Filter(3, 0.1111f, "blur");
            sobelh = new Filter(ImageProcessor.sobelHorizontal, " hor ");
            KeyDown += Form1_KeyDown;
            sobelv = new Filter(ImageProcessor.sobelVertical, " hor ");
              img2 = new PreviewImage((Bitmap)pictureBox2.Image);
            img = new PreviewImage((Bitmap)pictureBox2.Image);
            Filters.Add(edge.name,edge);
            Filters.Add(blur.name, blur);
            Filters.Add(sharpen.name, sharpen);
            pictureBox2.Image = img2.OriginalImage;
            
            PopulateListbox();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
           
        }

        void PopulateListbox()
        {
          foreach (string filter in Filters.Keys)
            {
                FiltersListBox.Items.Add(filter);
            }
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
          //  img.FilterImage(sobelh);
          //  img2.FilterImage(sobelv);
           // PreviewImage nw = img2 + img;
            //pictureBox1.Image = nw.returnGraytoImage();
            if (filterchosen != "")
            {
            
             img.FilterImage(Filters[filterchosen]);
             ViewImages();
            }
        }

       void ViewImages()
        {
            di2.UpdateImage(img2.ViewedImage, new Point((Width + Location.X)));

            di.UpdateImage(img.ViewedImage, new Point(Width + Location.X, Location.Y + di2.Height));
        }
        void ViewImages( string name )
        {
            di2.UpdateImage(img2.ViewedImage, new Point((Width + Location.X)));

            di.UpdateImage(img.ViewedImage, new Point(Width + Location.X, Location.Y + di2.Height));
            di.Text = name;
        }
        void updateButton()
        {
            if (!isColorised)
            {

                button2.Text = "Convert to Color";
            }
            else
            {
                button2.Text = "Convert to GrayScale";

            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            isColorised = !isColorised;
            updateButton();
            img.UpdateImage(isColorised);
            img2.UpdateImage(isColorised);
            ViewImages();
        }

        private void FiltersListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (FiltersListBox.SelectedItem != null)
            {
                filterchosen = FiltersListBox.SelectedItem.ToString();
                if (filterchosen != "")
                {
                    label1.Text = filterchosen;
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            img.UndoState();
            img.UpdateImage(isColorised);
            img2.UpdateImage(isColorised);
            ViewImages();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            //ImageProcessor.interpolatePixel(Color.FromArgb(200, 200, 200), Color.FromArgb(120, 120, 120), Color.FromArgb(55, 55, 55), Color.FromArgb(28, 28, 28), 0.3, 1.3, 0, 1, 1, 2);
           img2.ViewedImage = ImageProcessor.BilinearInterpolation(img2.ViewedImage, new Size((int)(img.OriginalImage.Width * enlargmentscale), (int)(img.OriginalImage.Height * enlargmentscale)));
            ViewImages();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
          
            di2 = new DisplayImage(img2.ViewedImage, new Point((Width + Location.X)  , Location.Y), "Main Image");
            di = new DisplayImage(img.ViewedImage, new Point(Width + Location.X, Location.Y + di2.Height ), "Filtered Image");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            ApplyInterpolation();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            label6.ForeColor = Color.Blue;
            label7.ForeColor = Color.Black;
            label8.ForeColor = Color.Black; interpolationMode = 0;
        }

        private void label8_Click(object sender, EventArgs e)
        {
            label6.ForeColor = Color.Black;
            label7.ForeColor = Color.Black;
            label8.ForeColor = Color.Blue; interpolationMode = 2;
        }

        private void label7_Click(object sender, EventArgs e)
        {
            label6.ForeColor = Color.Black;
            label7.ForeColor = Color.Blue;
            label8.ForeColor = Color.Black;
            interpolationMode = 1;
        }
        void ApplyInterpolation()
        {
           switch(interpolationMode)
            {
                case 0:
                 img.SetOriginalImage(ImageProcessor.BilinearInterpolation(img.ViewedImage, new Size((int)(img.ViewedImage.Width * enlargmentscale), (int)(img.ViewedImage.Height * enlargmentscale))));
                    ViewImages();
                    break;
                case 1:
                    img.SetOriginalImage(ImageProcessor.NearestNeighborInterpolation(img.ViewedImage, new Size((int)(img.ViewedImage.Width * enlargmentscale), (int)(img.ViewedImage.Height * enlargmentscale))));
                    ViewImages();
                    break;
                case 2:
                    img.SetOriginalImage(ImageProcessor.NearestNeighborInterpolation(img.ViewedImage, new Size((int)(img.ViewedImage.Width * enlargmentscale), (int)(img.ViewedImage.Height * enlargmentscale))));
                    ViewImages();
                    break;
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            enlargmentscale = trackBar1.Value+1;
        }

        private void label2_Click(object sender, EventArgs e)
        {
            FiltersPanel.Show();
            panel1.Hide();
            panel2.Hide();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            FiltersPanel.Hide();
            panel1.Show();
            panel2.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            img.equalizeImage();
            ViewImages();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            FiltersPanel.Hide();
            panel2.Show();
            panel1.Hide();
        }
    }
}
