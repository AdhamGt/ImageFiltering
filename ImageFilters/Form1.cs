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
        bool isColorised = true;
        PreviewImage img;
        string filterchosen = "";
        PreviewImage img2;
        Dictionary<string, Filter> Filters = new Dictionary<string, ImageFilters.Filter>();
        public Form1()
        {
            InitializeComponent();
          
            sharpen = new Filter(ImageProcessor.sharpen, "sharpening");
          edge = new Filter(ImageProcessor.edge, "edge");
            //   blur = new Filter(ImageProcessor.gaussianBlur, "blur");
            blur = new Filter(3, 0.1111f, "blur");
            sobelh = new Filter(ImageProcessor.sobelHorizontal, " hor ");
            KeyDown += Form1_KeyDown;
            sobelv = new Filter(ImageProcessor.sobelVertical, " hor ");
              img2 = new PreviewImage((Bitmap)pictureBox1.Image);
            img = new PreviewImage((Bitmap)pictureBox1.Image);
            Filters.Add(edge.name,edge);
            Filters.Add(blur.name, blur);
            Filters.Add(sharpen.name, sharpen);
            pictureBox2.Image = img2.OriginalImage;
            pictureBox1.Image = img.OriginalImage;
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
            img.FilterImage(sobelh);
            img2.FilterImage(sobelv);
            PreviewImage nw = img2 + img;
            pictureBox1.Image = nw.returnGraytoImage();
            if (filterchosen != "")
            {
            
             img.FilterImage(Filters[filterchosen]);
             ViewImages();
            }
        }

       void ViewImages()
        {
            pictureBox1.Image = img.ViewedImage;
            pictureBox2.Image = img2.ViewedImage;
           
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
      //      updateButton();
            ViewImages();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            pictureBox1.Image =   ImageProcessor.NearestNeighborInterpolation(img.OriginalImage, new Size(img.OriginalImage.Width * 2, img.OriginalImage.Height * 2));
        }
    }
}
