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
        int brightness = 0;
        int contrast = 0;

        public Form1()
        {
            InitializeComponent();
            interpolationPanel.Hide();
            sharpen = new Filter(ImageProcessor.sharpen, "sharpening");
            edge = new Filter(ImageProcessor.edge, "edge");
            //blur = new Filter(ImageProcessor.gaussianBlur, "blur");
            blur = new Filter(3, 0.1111f, "blur");
            sobelh = new Filter(ImageProcessor.sobelHorizontal, " hor ");
            sobelv = new Filter(ImageProcessor.sobelVertical, " hor ");
            img2 = new PreviewImage((Bitmap)pictureBox2.Image);
            img = new PreviewImage((Bitmap)pictureBox2.Image);
            Filters.Add(edge.name,edge);
            Filters.Add(blur.name, blur);
            Filters.Add(sharpen.name, sharpen);
            pictureBox2.Image = img2.OriginalImage;
            
            PopulateListbox();
        }

        void PopulateListbox()
        {
            foreach (string filter in Filters.Keys)
            {
                filtersListBox.Items.Add(filter);
            }
        }

        private void applyFilterButton_Click(object sender, EventArgs e)
        {
            //img.FilterImage(sobelh);
            //img2.FilterImage(sobelv);
            //PreviewImage nw = img2 + img;
            //pictureBox1.Image = nw.returnGraytoImage();

            if (filterchosen != "")
            {
                img.filterImage(Filters[filterchosen]);
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
                grayScaleButton.Text = "Convert to Color";
            }
            else
            {
                grayScaleButton.Text = "Convert to GrayScale";
            }
        }

        private void grayScaleButton_Click(object sender, EventArgs e)
        {
            isColorised = !isColorised;
            updateButton();
            img.UpdateImage(isColorised);
            img2.UpdateImage(isColorised);
            ViewImages();
        }

        private void FiltersListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (filtersListBox.SelectedItem != null)
            {
                filterchosen = filtersListBox.SelectedItem.ToString();

                if (filterchosen != "")
                {
                    label1.Text = filterchosen;
                }
            }
        }

        private void undoButton_Click(object sender, EventArgs e)
        {
            img.UndoState();
            img.UpdateImage(isColorised);
            img2.UpdateImage(isColorised);
            ViewImages();
            updateTrackBars();
        }

        private void updateTrackBars()
        {
            brightness = img.brightness;
            contrast = img.contrast;
            brightnessValue.Text = "" + brightness;
            contrastValue.Text = "" + contrast;
            BrightnessTrackBar.Value = brightness;
            contrastTrackBar.Value = contrast;
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

        private void histogramButton_Click(object sender, EventArgs e)
        {
            img.equalizeImage();
            ViewImages();
        }

        private void interpolationButton_Click(object sender, EventArgs e)
        {
            ApplyInterpolation();
        }

        private void bilinearLabel_Click(object sender, EventArgs e)
        {
            bilinearLabel.ForeColor = Color.Blue;
            nearestNeighborLabel.ForeColor = Color.Black;
            bicubicLabel.ForeColor = Color.Black;
            interpolationMode = 0;
        }

        private void bicubicLabel_Click(object sender, EventArgs e)
        {
            bilinearLabel.ForeColor = Color.Black;
            nearestNeighborLabel.ForeColor = Color.Black;
            bicubicLabel.ForeColor = Color.Blue;
            interpolationMode = 2;
        }

        private void nearestNeighborLabel_Click(object sender, EventArgs e)
        {
            bilinearLabel.ForeColor = Color.Black;
            nearestNeighborLabel.ForeColor = Color.Blue;
            bicubicLabel.ForeColor = Color.Black;
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

        private void interpolationTrackBar_Scroll(object sender, EventArgs e)
        {
            enlargmentscale = interpolationTrackBar.Value + 1;
        }

        private void filtersLabel_Click(object sender, EventArgs e)
        {
            filtersPanel.Show();
            interpolationPanel.Hide();
            histogramPanel.Hide();
            imageEditingPanel.Hide();
        }

        private void histogramLabel_Click(object sender, EventArgs e)
        {
            filtersPanel.Hide();
            interpolationPanel.Hide();
            histogramPanel.Show();
            imageEditingPanel.Hide();
        }

        private void editingLabel_Click(object sender, EventArgs e)
        {
            filtersPanel.Hide();
            interpolationPanel.Hide();
            histogramPanel.Hide();
            imageEditingPanel.Show();
        }

        private void interpolationLabel_Click(object sender, EventArgs e)
        {
            filtersPanel.Hide();
            interpolationPanel.Show();
            histogramPanel.Hide();
            imageEditingPanel.Hide();
        }

        private void colorInvertingButton_Click(object sender, EventArgs e)
        {
            img.invertImage();
            ViewImages();
        }

        private void contrastTrackBar_Scroll(object sender, EventArgs e)
        {
            contrastValue.Text = "" + contrastTrackBar.Value;
            contrast = contrastTrackBar.Value;
        }

        private void BrightnessTrackBar_Scroll(object sender, EventArgs e)
        {
            brightnessValue.Text = "" + BrightnessTrackBar.Value;
            brightness = BrightnessTrackBar.Value;
        }

        private void applyEditButton_Click(object sender, EventArgs e)
        {
            if (img.brightness != brightness)
            {
                img.updateImageBrightness(brightness);
            }
            if (img.contrast != contrast)
            {
                img.updateImageContrast(contrast);
            }

            ViewImages();
        }
    }
}
