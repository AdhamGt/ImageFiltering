using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageFilters
{
    public class PreviewImage 
    {
        public int stages;
        public Bitmap OriginalImage;
        public bool isColorised = true;
        public int[,] Mat;
        private int[,] MatOrigin;
        public Bitmap ViewedImage;
        public Bitmap GrayscaleImage;
        public Bitmap ColorisedImage;
        public List<PreviewState> previewStages = new List<PreviewState>();
        public int[,] equalizedMat;
        public Bitmap equalizedImage;
        int contrast = 0, brightness = 0;

        public void SetOriginalImage(Bitmap img)
        {
            OriginalImage = img;
            ViewedImage = img;
            
            ConvertToGrayscale();
            GetViewedImage();
            previewStages.Add(new PreviewState(stages, null, OriginalImage, OriginalImage, OriginalImage, GrayscaleImage, CopyMat(), isColorised));
        }

        public static PreviewImage operator +(PreviewImage a, PreviewImage b)
        {
            PreviewImage img = new PreviewImage(a.OriginalImage);

            for(int i = 0; i < a.ViewedImage.Width; i++)
            {
                for (int j = 0; j < a.ViewedImage.Height; j++)
                {
                    img.Mat[i, j] = a.Mat[i, j] + b.Mat[i, j];

                    if (img.Mat[i, j] < 0)
                    {
                        img.Mat[i, j] = 0;
                    }
                    if (img.Mat[i, j] > 255)
                    {
                        img.Mat[i, j] = 255;
                    }
                }
            }

            img.returnGraytoImage();
            img.returntoColor();
            return img;
        }

        public PreviewImage(Bitmap img, bool colorised)
        {
            OriginalImage = img;
            ViewedImage = img;
            this.isColorised = colorised;
            ConvertToGrayscale();
            GetViewedImage();
            previewStages.Add(new PreviewState(stages, null, OriginalImage, OriginalImage, OriginalImage, GrayscaleImage, CopyMat(), isColorised));
        }

        public PreviewImage(Bitmap img)
        {
            OriginalImage = img;
            ViewedImage = img;
            ConvertToGrayscale();
            GetViewedImage();
            previewStages.Add(new PreviewState(stages, null, OriginalImage, OriginalImage, OriginalImage, GrayscaleImage, CopyMat(), isColorised));
        }

        public void UndoState()
        {
            if (previewStages.Count > 1)
            {
                OriginalImage = previewStages[previewStages.Count - 2].OriginalImage;
                ConvertToGrayscale();
                ViewedImage = previewStages[previewStages.Count - 2].ResultImage;
                ImageProcessor.CopyMat(ref Mat, ref previewStages[previewStages.Count - 2].Mat);
                isColorised = previewStages[previewStages.Count - 2].isColorised;
                GrayscaleImage = previewStages[previewStages.Count - 2].GrayImage;
                ColorisedImage = previewStages[previewStages.Count - 2].ColorisedImage;
                previewStages.Remove(previewStages[previewStages.Count - 1]);
                stages--;
            }
        }

        public void UpdateImage(bool color)
        {
            isColorised = color;
            GetViewedImage();
        }

        public void returntoColor()
        {
            ColorisedImage = new Bitmap(OriginalImage.Width, OriginalImage.Height);

            for (int i = 0; i < OriginalImage.Width; i++)
            {
                for (int j = 0; j < OriginalImage.Height; j++)
                {
                    double r = 0;
                    double g = 0;
                    double b = 0;
                    Color c = OriginalImage.GetPixel(i, j);

                    if (MatOrigin[i, j] != 0)
                    {
                        r = c.R * Mat[i, j] / MatOrigin[i, j];
                        g = c.G * Mat[i, j] / MatOrigin[i, j];
                        b = c.B * Mat[i, j] / MatOrigin[i, j];
                    }
                    else
                    {
                        r = c.R * Mat[i, j];
                        g = c.G * Mat[i, j];
                        b = c.B * Mat[i, j];
                    }

                    ImageProcessor.RangeColors(ref r, ref g, ref b);
                    ColorisedImage.SetPixel(i, j, Color.FromArgb((int)r, (int)g, (int)b));
                }
            }
        }

        public Bitmap returntoColor(int[,] Mat, int[,] MatOrigin, Bitmap OriginalImage)
        {
            Bitmap ColorisedImage = new Bitmap(Mat.GetLength(0), Mat.GetLength(1));

            for (int i = 0; i < Mat.GetLength(0); i++)
            {
                for (int j = 0; j < Mat.GetLength(1); j++)
                {
                    double r = 0;
                    double g = 0;
                    double b = 0;
                    Color c = OriginalImage.GetPixel(i, j);

                    if (MatOrigin[i, j] != 0)
                    {
                        r = c.R * Mat[i, j] / MatOrigin[i, j];
                        g = c.G * Mat[i, j] / MatOrigin[i, j];
                        b = c.B * Mat[i, j] / MatOrigin[i, j];
                    }
                    else
                    {
                        r = c.R * Mat[i, j];
                        g = c.G * Mat[i, j];
                        b = c.B * Mat[i, j];
                    }

                    ImageProcessor.RangeColors(ref r, ref g, ref b);
                    ColorisedImage.SetPixel(i, j, Color.FromArgb((int)r, (int)g, (int)b));
                }
            }

            return ColorisedImage;
        }

        int[,] CopyMat()
        {
            int[,] MatOrigin = new int[OriginalImage.Width, OriginalImage.Height];

            for (int i = 0; i < OriginalImage.Width; i++)
            {
                for (int j = 0; j < OriginalImage.Height; j++)
                {
                    MatOrigin[i, j] = Mat[i, j];
                }
            }

            return MatOrigin;
        }

        public void ConvertToGrayscale()
        {
            Mat = new int[OriginalImage.Width, OriginalImage.Height];
            MatOrigin = new int[OriginalImage.Width, OriginalImage.Height];

            for (int i = 0; i < OriginalImage.Width; i++)
            {
                for (int j = 0; j < OriginalImage.Height; j++)
                {
                    Color c = OriginalImage.GetPixel(i, j);
                    Mat[i, j] = (int)((0.3 * c.R) + (0.59 * c.G) + (0.11 * c.B));
                    MatOrigin[i, j] = (int)((0.3 * c.R) + (0.59 * c.G) + (0.11 * c.B));
                }
            }

            ViewedImage = returnGraytoImage();
        }

        int[,] ConvertToGrayscale(int[,] mat)
        {
            Mat = new int[OriginalImage.Width, OriginalImage.Height];

            for (int i = 0; i < OriginalImage.Width; i++)
            {
                for (int j = 0; j < OriginalImage.Height; j++)
                {
                    Color c = OriginalImage.GetPixel(i, j);
                    mat[i, j] = (int)((0.3 * c.R) + (0.59 * c.G) + (0.11 * c.B));
                }
            }

            return mat;
        }

        public Bitmap returnGraytoImage()
        {
            Bitmap temp = new Bitmap(OriginalImage.Width, OriginalImage.Height);

            for (int i = 0; i < OriginalImage.Width; i++)
            {
                for (int j = 0; j < OriginalImage.Height; j++)
                {
                    Color c = Color.FromArgb(Mat[i, j], Mat[i, j], Mat[i, j]);
                    temp.SetPixel(i, j, c);
                }
            }

            return temp;
        }

        public Bitmap returnGraytoImage(int[,] Mat)
        {
            Bitmap temp = new Bitmap(Mat.GetLength(0), Mat.GetLength(1));

            for (int i = 0; i < Mat.GetLength(0); i++)
            {
                for (int j = 0; j < Mat.GetLength(1); j++)
                {
                    Color c = Color.FromArgb(Mat[i, j], Mat[i, j], Mat[i, j]);
                    temp.SetPixel(i, j, c);
                }
            }

            return temp;
        }

        public void returnGrayImage()
        {
            GrayscaleImage = new Bitmap(OriginalImage.Width, OriginalImage.Height);

            for (int i = 0; i < OriginalImage.Width; i++)
            {
                for (int j = 0; j < OriginalImage.Height; j++)
                {
                    Color c = Color.FromArgb(Mat[i, j], Mat[i, j], Mat[i, j]);
                    GrayscaleImage.SetPixel(i, j, c);
                }
            }
        }

        public void GetViewedImage()
        {
            if(isColorised)
            {
                returntoColor();
                ViewedImage = ColorisedImage;
            }
            else
            {
                returnGrayImage();
                ViewedImage = GrayscaleImage;
            }
        }

        public Bitmap invertImage()
        {
            int[,] invertedMat = new int[Mat.GetLength(0), Mat.GetLength(1)];
            ImageProcessor.CopyMat(ref invertedMat, ref Mat);

            for (int r = 0; r < invertedMat.GetLength(0); r++)
            {
                for (int c = 0; c < invertedMat.GetLength(1); c++)
                {
                    invertedMat[r, c] = 255 - invertedMat[r, c];
                }
            }

            Bitmap invertedImage = ImageProcessor.CopyImage(ViewedImage);

            if (isColorised)
            {
                invertedImage = returntoColor(invertedMat, Mat, ColorisedImage);
            }
            else
            {
                invertedImage = returnGraytoImage(invertedMat);
            }

            return invertedImage;
        }

        public Bitmap updateImageContrast(int contrastValue)
        {
            this.contrast = contrastValue;
            Bitmap contrastedImage = ImageProcessor.CopyImage(ColorisedImage);
            double factor = (double)(259 * (contrastValue + 255)) / (double)(255 * (259 - contrastValue));

            for (int r = 0; r < contrastedImage.Width; r++)
            {
                for (int c = 0; c < contrastedImage.Height; c++)
                {
                    double red = contrastedImage.GetPixel(r, c).R;
                    double green = contrastedImage.GetPixel(r, c).G;
                    double blue = contrastedImage.GetPixel(r, c).B;
                    red = (int)(factor * (red - 128) + 128);
                    green = (int)(factor * (green - 128) + 128);
                    blue = (int)(factor * (blue - 128) + 128);
                    ImageProcessor.RangeColors(ref red, ref green, ref blue);
                    contrastedImage.SetPixel(r, c, Color.FromArgb(contrastedImage.GetPixel(r, c).A, (int)red, (int)green, (int)blue));
                }
            }

            if (!isColorised)
            {
                PreviewImage contrast = new PreviewImage(contrastedImage, false);
                contrastedImage = contrast.GrayscaleImage;
            }

            return contrastedImage;
        }

        public Bitmap updateImageBrightness(int brightness)
        {
            this.brightness = brightness;
            double brightnessFactor = 1 + ((double)brightness / (double)100.0);
            int[,] brightenedMat = new int[Mat.GetLength(0), Mat.GetLength(1)];
            ImageProcessor.CopyMat(ref brightenedMat, ref Mat);

            for (int r = 0; r < brightenedMat.GetLength(0); r++)
            {
                for (int c = 0; c < brightenedMat.GetLength(1); c++)
                {
                    brightenedMat[r, c] = (int)Math.Min(255, brightenedMat[r, c] * brightnessFactor);
                }
            }

            Bitmap brightenedImage = ImageProcessor.CopyImage(ViewedImage);

            if (isColorised)
            {
                brightenedImage = returntoColor(brightenedMat, Mat, ColorisedImage);
            }
            else
            {
                brightenedImage = returnGraytoImage(brightenedMat);
            }

            return brightenedImage;
        }

        public Bitmap equalizeImage()
        {
            equalizedMat = Histogram.getEqualized(Mat);

            if (isColorised)
            {
                equalizedImage = returntoColor(equalizedMat, Mat, ColorisedImage);
                ViewedImage = equalizedImage;
                ColorisedImage = equalizedImage;
                Mat = equalizedMat;
                previewStages.Add(new PreviewState(stages, null, OriginalImage, ViewedImage, ColorisedImage, GrayscaleImage, CopyMat(), isColorised));

            }
            else
            {
                equalizedImage = returnGraytoImage(equalizedMat);
                Mat = equalizedMat;
                GrayscaleImage = equalizedImage;
                ViewedImage = equalizedImage;
                previewStages.Add(new PreviewState(stages, null, OriginalImage, ViewedImage, ColorisedImage, GrayscaleImage, CopyMat(), isColorised));

            }

            return equalizedImage;
        }

        public void FilterImage(Filter f)
        {
            stages++;
            Mat = ImageProcessor.ApplyFilter(Mat, f);

            GetViewedImage();
            previewStages.Add(new PreviewState(stages, f, OriginalImage ,ViewedImage,ColorisedImage,GrayscaleImage, CopyMat(), isColorised));
        }
    }
}
