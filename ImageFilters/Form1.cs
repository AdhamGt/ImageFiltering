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
        Filter saltAndPepper;
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
        int saturation = 0;
        bool NoImage = true;

        public Form1()
        {
            InitializeComponent();
            interpolationPanel.Hide();
            sharpen = new Filter(ImageProcessor.sharpen, "Sharpening");
            edge = new Filter(ImageProcessor.laplacianedge, "Edge");
            //blur = new Filter(ImageProcessor.gaussianBlur, "blur");
            blur = new Filter(3, 0.1111f, "Blur");
            sobelh = new Filter(ImageProcessor.sobelHorizontal, "Hor");
            sobelv = new Filter(ImageProcessor.sobelVertical, "Ver");
            saltAndPepper = new Filter("Salt and Pepper");
      
            Filters.Add(edge.name,edge);
            Filters.Add(blur.name, blur);
            Filters.Add(sharpen.name, sharpen);
            Filters.Add(saltAndPepper.name, saltAndPepper);

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

            if (filterchosen != "" && !NoImage)
            {
                img.filterImage(Filters[filterchosen]);
                ViewImages();
            }
        }

        void ViewImages()
        {
            di2.UpdateImage(img2.ViewedImage, new Point((Width + Location.X), Location.Y));
            di.UpdateImage(img.ViewedImage, new Point(Width + Location.X, Location.Y + di2.Height));
        }

        void ViewImages(string name)
        {
            di2.UpdateImage(img2.ViewedImage, new Point((Width + Location.X), Location.Y));
            di.UpdateImage(img.ViewedImage, new Point(Width + Location.X, Location.Y + di2.Height));
            di.Text = name;
        }

        void updateButton()
        {
            if (!NoImage)
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
        }

        private void grayScaleButton_Click(object sender, EventArgs e)
        {
            if (!NoImage)
            {
                isColorised = !isColorised;
                updateButton();
                img.UpdateImage(isColorised);
                img2.UpdateImage(isColorised);
                ViewImages();
            }
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
            if (!NoImage)
            {
                img.UndoState();
                img.UpdateImage(isColorised);
                img2.UpdateImage(isColorised);
                ViewImages();
                updateTrackBars();
            }
        }

        private void updateTrackBars()
        {
            brightness = img.brightness;
            contrast = img.contrast;
            saturation = img.saturation;

            brightnessValue.Text = "" + brightness;
            contrastValue.Text = "" + contrast;
            saturationValue.Text = "" + saturation;

            brightnessTrackBar.Value = brightness;
            contrastTrackBar.Value = contrast;
            saturationTrackBar.Value = saturation;
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog dialog = openFileDialog1;
            dialog.Title = "Open Image";
            dialog.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                Bitmap img3 = (Bitmap)Image.FromFile(dialog.FileName);
                img2 = new PreviewImage((Bitmap)img3);
                img = new PreviewImage((Bitmap)img3);

                if (di != null)
                {
                    di.Close();
                    di2.Close();
                }

                di2 = new DisplayImage(img2.ViewedImage, new Point((Width + Location.X), Location.Y), "Main Image");
                di = new DisplayImage(img.ViewedImage, new Point(Width + Location.X, Location.Y + di2.Height), "Filtered Image");
                NoImage = false;
                saltAndPepper.updateFilter(img.ViewedImage.Width, img.ViewedImage.Height, 0, 256);

                updateTrackBars();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
        }

        private void histogramButton_Click(object sender, EventArgs e)
        {
            if (!NoImage)
            {
                img.equalizeImage();
                ViewImages();
            }

        }

        private void interpolationButton_Click(object sender, EventArgs e)
        {
            if (!NoImage)
            {
                ApplyInterpolation();
            }
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
            if (!NoImage)
            {
                switch (interpolationMode)
                {
                    case 0:
                        img.SetOriginalImage(ImageProcessor.BilinearInterpolation(img.ViewedImage, new Size((int)(img.ViewedImage.Width * enlargmentscale), (int)(img.ViewedImage.Height * enlargmentscale))));
                        //img.SetOriginalImage(ImageProcessor.Scale(img.ViewedImage, 3, 3));
                        ViewImages();

                        break;

                    case 1:
                        img.SetOriginalImage(ImageProcessor.NearestNeighborInterpolation(img.ViewedImage, new Size((int)(img.ViewedImage.Width * enlargmentscale), (int)(img.ViewedImage.Height * enlargmentscale))));
                        ViewImages();

                        break;

                    case 2:
                        img.ViewedImage = img.ApplyBicubic(enlargmentscale);
                        ViewImages();

                        break;
                }
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
            if (!NoImage)
            {
                img.invertImage();
                ViewImages();
            }
        }

        private void contrastTrackBar_Scroll(object sender, EventArgs e)
        {
            contrastValue.Text = "" + contrastTrackBar.Value;
            contrast = contrastTrackBar.Value;
        }

        private void BrightnessTrackBar_Scroll(object sender, EventArgs e)
        {
            brightnessValue.Text = "" + brightnessTrackBar.Value;
            brightness = brightnessTrackBar.Value;
        }

        private void applyEditButton_Click(object sender, EventArgs e)
        {
            if (!NoImage)
            {
                if (img.brightness != brightness)
                {
                    img.updateImageBrightness(brightness);
                }
                if (img.contrast != contrast)
                {
                    img.updateImageContrast(contrast);
                }
                if (img.saturation != saturation)
                {
                    img.updateImageSaturation(saturation);
                }

                ViewImages();
            }
        }

        private void saturationTrackBar_Scroll(object sender, EventArgs e)
        {
            saturationValue.Text = "" + saturationTrackBar.Value;
            saturation = saturationTrackBar.Value;
        }
    }
}
