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
        Filter Harmonic;
        Filter edge;
        Filter UnsharpennoDiag;
        Filter blur, blur2;
        Filter sobelh;
        Filter sobelv;
        Filter Emboss;
        Filter Pewitt;
        Filter Pewitt2;
        Filter saltAndPepper;
        Filter Median;
        Filter edge4s;
        Filter gaussianNoise;
        Filter avgNoiseReduction;
        Filter log;
        Filter squareroot, nthroot;
        Filter min;
        Filter max;
        Filter blur3;
        bool equalize = false;
        int interpolationMode = 0;
        double enlargmentscale = 2f;
        PreviewImage comparedImage;
        bool isColorised = true;
        PreviewImage img;
        string filterchosen = "";
        PreviewImage img2;
        public Dictionary<string, Filter> Filters = new Dictionary<string, Filter>();
        DisplayImage di;
        DisplayImage di2;
        Form2 customkernel;
        Filter cartoon;
        Filter Unsharpen2;
        Filter RobertCrossV, RobertCrossH;
        int brightness = 0;
        int contrast = 0;
        int saturation = 0;
        bool NoImage = true;
        Filter Unsharpen;
        double power = 0;
        double[] powers = new double[8] { 0.1f, 0.2f, 0.4f, 0.5f, 1, 2.5f, 5, 10 };
        double[] Qs = new double[5] {-2f, -1f, 0f, 1f, 2f };
        public Form1()
        {
            InitializeComponent();
            interpolationPanel.Hide();
            sharpen = new Filter(ImageProcessor.laplaciansharpendiagonal, "Laplacian Sharpening +D");
            edge4s = new Filter(ImageProcessor.laplacianedge, "Laplacian Edge");
            edge = new Filter(ImageProcessor.laplacianedgediagonal, "Laplacian Edge + D");
            blur2 = new Filter(ImageProcessor.gaussianBlur, "Gaussian Blur");
            Median = new Filter(3, 1, "median order");
            Pewitt = new Filter(ImageProcessor.previtHorizontal, "PrewittH");
            Pewitt2 = new Filter(ImageProcessor.previtVertical, "PrewittV");
            Harmonic = new Filter(5, 1, "harmonic");
            Emboss = new Filter(ImageProcessor.Emboss, "Emboss Filter");
            min = new Filter(3, 1, "minimum order");
            max = new Filter(3, 1, "maximum order");
            Unsharpen = new Filter(3, 1, "UnSharpen");
            cartoon = new Filter(3, 1, "Cartoon");
            UnsharpennoDiag = new Filter(ImageProcessor.laplaciansharpen, "Laplacian Sharpening");
            Unsharpen2 = new Filter(3, 1, "UnSharpen HighBoost");
            blur = new Filter(5, 0.04f, "Mean Filter (5x5)");
            blur3 = new Filter(3, 0.1111f, "Mean Filter (3x3)");
            sobelh = new Filter(ImageProcessor.sobelHorizontal, "SobelH");
            sobelv = new Filter(ImageProcessor.sobelVertical, "SobelV");
            saltAndPepper = new Filter("Salt and Pepper");
            RobertCrossH = new Filter(ImageProcessor.RobertCrossHorizontal, "RobertCrossH");
            RobertCrossV = new Filter(ImageProcessor.RobertCrossVertical, "RobertCrossV");
            log = new Filter("Log Operator");
            squareroot = new Filter("Square-root Operator");
            nthroot = new Filter("Nth-Power Operator");
            //gaussianNoise = new Filter("Gaussian Noise");
            //avgNoiseReduction = new Filter("Average Noise Reduction");
      
            Filters.Add(edge.name, edge);
            Filters.Add(edge4s.name, edge4s);
            Filters.Add("Sobel Operator", sobelh);
            Filters.Add("Prewitt Operator", Pewitt);
            Filters.Add("Robert Cross Operator", RobertCrossH);
            Filters.Add("Emboss", Emboss);
            Filters.Add(blur.name, blur);
            Filters.Add(blur2.name, blur2);
            Filters.Add("Median Filter", Median);
            Filters.Add("Minimum Filter", min);
            Filters.Add("Maximum Filter", max);
            Filters.Add(blur3.name, blur3);
            Filters.Add("ContraHarmonic Mean", Harmonic);
            Filters.Add(sharpen.name, sharpen);
            Filters.Add("Laplacian Sharpening", UnsharpennoDiag);
            Filters.Add("UnSharpen", Unsharpen);
            Filters.Add("UnSharpen HighBoost", Unsharpen);
            Filters.Add(saltAndPepper.name, saltAndPepper);
            Filters.Add(log.name, log);

           Filters.Add("Nth-Power Operator", nthroot);
            Filters.Add("Cartoon",cartoon);
            //Filters.Add(avgNoiseReduction.name, avgNoiseReduction);
            //Filters.Add(gaussianNoise.name, gaussianNoise);
            PopulateListbox();


            fourierButton.Hide();
            powerLabel.Hide();
            powerTrackBar.Hide();
            powerValLabel.Hide();
            trackBar1.Hide();
            label4.Hide();
            label2.Hide();
        }

       public void PopulateListbox()
        {
            filtersListBox.Items.Clear();
            foreach (string filter in Filters.Keys)
            {
                filtersListBox.Items.Add(filter);
            }
        }

        private void applyFilterButton_Click(object sender, EventArgs e)
        {
            //pictureBox1.Image = nw.returnGraytoImage();

            if (filterchosen != "" && !NoImage)
            {
                if (filterchosen.Contains("Sobel"))
                {
                    Bitmap imgtmp = ImageProcessor.CopyImage(img.ViewedImage);
                    PreviewImage tmp = new PreviewImage(imgtmp);

                    img.filterImage(sobelv);
               //     tmp.filterImage(sobelh);
                 //  img = img + tmp;

                }
                else if (filterchosen.Contains("Robert Cross"))
                {
                    Bitmap imgtmp = ImageProcessor.CopyImage(img.ViewedImage);
                    PreviewImage tmp = new PreviewImage(imgtmp);
                    img.filterImage(RobertCrossH);
                    tmp.filterImage(RobertCrossV);
                    img = img + tmp;
                }
                else if (filterchosen.Contains("UnSharpen"))
                {
                    Bitmap imgtmp = ImageProcessor.CopyImage(img.ViewedImage);
                    PreviewImage tmp = new PreviewImage(imgtmp);
                    Bitmap imgtmp2 = ImageProcessor.CopyImage(img.ViewedImage);
                    PreviewImage tmp2 = new PreviewImage(imgtmp2);
                    tmp.filterImage(blur);
                    tmp2 = tmp2 - tmp;
                    int k = 5;
                    if (!filterchosen.Contains("HighBoost"))
                    {
                        k = 1;
                    }
                    for (int i = 0; i < k; i++)
                    {
                        img = img + tmp2;
                    }
                }
                else if (filterchosen == "Average Noise Reduction")
                {
                    List<PreviewImage> noisyImages = new List<PreviewImage>();

                    ///fill list

                    Bitmap newImage = ImageProcessor.CopyImage(img.ViewedImage);

                    for (int r = 0; r < img.ViewedImage.Width; r++)
                    {
                        for (int c = 0; c < img.ViewedImage.Height; c++)
                        {
                            int redTotal = 0, greenTotal = 0, blueTotal = 0;

                            for (int i = 0; i < noisyImages.Count; i++)
                            {
                                Color color = noisyImages[i].ViewedImage.GetPixel(r, c);
                                redTotal += color.R;
                                greenTotal += color.G;
                                blueTotal += color.B;
                            }

                            redTotal /= noisyImages.Count;
                            greenTotal /= noisyImages.Count;
                            blueTotal /= noisyImages.Count;

                            newImage.SetPixel(r, c, Color.FromArgb(redTotal, greenTotal, blueTotal));
                        }
                    }

                    PreviewImage newPrev = new PreviewImage(newImage, img.isColorised);
                    img.ViewedImage = newPrev.ViewedImage;
                    img.Mat = newPrev.Mat;
                    img.GetViewedImage();
                    img.previewStages.Add(new PreviewState(img.stages, avgNoiseReduction, img.OriginalImage, img.ViewedImage, img.ColorisedImage, img.GrayscaleImage, img.MatOrigin, img.isColorised, img.brightness, img.contrast, img.saturation));
                }
                else if (filterchosen.Contains("Prewitt"))
                {
                    Bitmap imgtmp = ImageProcessor.CopyImage(img.ViewedImage);
                    PreviewImage tmp = new PreviewImage(imgtmp);
                    img.filterImage(Pewitt);
                    tmp.filterImage(Pewitt2);
                    img = img + tmp;
                }
                else if (filterchosen == "Log Operator")
                {
                    int[,] logMat = ImageProcessor.applyLogarithmicTransformation(img.Mat);
                    img.Mat = logMat;
                    img.GetViewedImage();
                    img.previewStages.Add(new PreviewState(img.stages, log, img.OriginalImage, img.ViewedImage, img.ColorisedImage, img.GrayscaleImage, img.CopyMat(), img.isColorised, img.brightness, img.contrast, img.saturation));
                }
              
                else if (filterchosen == "Nth-Power Operator")
                {
                    int[,] squarerootMat = ImageProcessor.applyNthRootOperator(img.Mat, power);
                    img.Mat = squarerootMat;
                    img.GetViewedImage();
                    img.previewStages.Add(new PreviewState(img.stages, log, img.OriginalImage, img.ViewedImage, img.ColorisedImage, img.GrayscaleImage, img.CopyMat(), img.isColorised, img.brightness, img.contrast, img.saturation));
                }
                else if (filterchosen.Contains("Cartoon"))
                {
                    Bitmap imgtmp = ImageProcessor.CopyImage(img.ViewedImage);
                    Bitmap imgtmp2 = ImageProcessor.CopyImage(img.ViewedImage);

                    PreviewImage tmp = new PreviewImage(imgtmp);

                    PreviewImage tmp2 = new PreviewImage(imgtmp2);
                    for (int i = 0; i < 2; i++)
                    {
                        tmp.filterImage(Median);
                        tmp2.filterImage(Median);
                    }

                    tmp.filterImage(sobelh);

                    tmp2.filterImage(sobelv);
                    tmp = tmp + tmp2;
                    for (int i = 0; i < 6; i++)
                    {
                        img.filterImage(blur);
                    }
                    img = img - tmp;
                    ViewImages();

                }
                else
                {
                    if (filterchosen == "Salt and Pepper")
                    {
                        saltAndPepper.randomUpdateFilter(img.ViewedImage.Width, img.ViewedImage.Height, 0, 256, false);
                    }
                    if (filterchosen == "Gaussian Noise")
                    {
                        gaussianNoise.randomUpdateFilter(img.ViewedImage.Width, img.ViewedImage.Height, 0, (0.1f * 0.5f), true);
                    }

                    img.filterImage(Filters[filterchosen]);
                }

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

                    if (filterchosen == "Nth-Power Operator")
                    {
                        powerLabel.Show();
                        powerTrackBar.Show();
                        powerValLabel.Show();
                    }
                    else
                    {
                        powerLabel.Hide();
                        powerTrackBar.Hide();
                        powerValLabel.Hide();
                    }
                    if (filterchosen == "ContraHarmonic Mean")
                    {
                        trackBar1.Show();
                        label4.Show();
                        label2.Show();
                    }
                    else
                    {
                        trackBar1.Hide();
                       label4.Hide();
                        label2.Hide();
                    }
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
            dialog.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png, *.gif) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png; *.gif;";

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
            multiplierLabel.Text = "x" + enlargmentscale;
        }

        private void filtersLabel_Click(object sender, EventArgs e)
        {
            filtersLabel.ForeColor = Color.Red;
             label.ForeColor = Color.Black;
            interpolationLabel.ForeColor = Color.Black;
            editingLabel.ForeColor = Color.Black;
            filtersPanel.Show();
            interpolationPanel.Hide();
            histogramPanel.Hide();
            imageEditingPanel.Hide();
        }

        private void histogramLabel_Click(object sender, EventArgs e)
        {
            filtersLabel.ForeColor = Color.Black;
            label.ForeColor = Color.Red;
            interpolationLabel.ForeColor = Color.Black;
            editingLabel.ForeColor = Color.Black;
            filtersPanel.Hide();
            interpolationPanel.Hide();
            histogramPanel.Show();
            imageEditingPanel.Hide();
        }

        private void editingLabel_Click(object sender, EventArgs e)
        {
            filtersLabel.ForeColor = Color.Black;
            label.ForeColor = Color.Black;
            interpolationLabel.ForeColor = Color.Black;
            editingLabel.ForeColor = Color.Red;
            filtersPanel.Hide();
            interpolationPanel.Hide();
            histogramPanel.Hide();
            imageEditingPanel.Show();
        }

        private void interpolationLabel_Click(object sender, EventArgs e)
        {
            filtersLabel.ForeColor = Color.Black;
            label.ForeColor = Color.Black;
            interpolationLabel.ForeColor = Color.Red;
            editingLabel.ForeColor = Color.Black;
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

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = openFileDialog1;
            dialog.Title = "Open Image";
            dialog.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                Bitmap img3 = (Bitmap)Image.FromFile(dialog.FileName);
                comparedImage = new PreviewImage((Bitmap)img3);
                if (img3.Width == img.ViewedImage.Width && img3.Height == img.ViewedImage.Height)
                {
                    Bitmap imgtmp = ImageProcessor.CopyImage(img3);
                    PreviewImage tmp = new PreviewImage(imgtmp);
                    Bitmap imgtmp3 = ImageProcessor.CopyImage(img3);
                    PreviewImage tmp3 = new PreviewImage(imgtmp3);
                    Bitmap imgtmp2 = ImageProcessor.CopyImage(img.ViewedImage);
                    PreviewImage tmp2 = new PreviewImage(imgtmp2);
                    tmp.filterImage(blur);
                    tmp2.filterImage(blur);
                    tmp3 = tmp3 - tmp;
                    img = tmp3 + tmp2;
                    ViewImages();
                }
            }
        }

        private void fourierButton_Click(object sender, EventArgs e)
        {
            img.applyFFT();
            ViewImages();
        }

        private void powerTrackBar_Scroll(object sender, EventArgs e)
        {
            power = powers[powerTrackBar.Value];
            powerValLabel.Text = (float)power + "";
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            Harmonic.Order = trackBar1.Value;
          label2.Text = (float)Harmonic.Order + "";
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if(customkernel != null)
            {
                customkernel.Dispose();
            }
            Form1 current = this;
          customkernel  = new Form2(current);
            customkernel.Show();
        }

        private void revertButton_Click(object sender, EventArgs e)
        {
            if (!NoImage)
            {
                img.revertState();
                img.UpdateImage(isColorised);
                img2.UpdateImage(isColorised);
                ViewImages();
                updateTrackBars();
            }
        }

        private void saturationTrackBar_Scroll(object sender, EventArgs e)
        {
            saturationValue.Text = "" + saturationTrackBar.Value;
            saturation = saturationTrackBar.Value;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = openFileDialog1;
            dialog.Title = "Open Image";
            dialog.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                Bitmap img3 = (Bitmap)Image.FromFile(dialog.FileName);
                comparedImage = new PreviewImage((Bitmap)img3);
                if (img3.Width == img.ViewedImage.Width && img3.Height == img.ViewedImage.Height)
                {
                    img.Difference = true;
                    img = img - comparedImage;
                    img.RangeDifference();
                    img.Difference = false;
                    ViewImages();
                }
            }
        }
    }
}
