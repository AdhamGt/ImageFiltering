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
        public bool Difference = false;
        public bool isColorised = true;
        public int[,] Mat;
        public int[,] MatOrigin;
        public Bitmap ViewedImage;
        public Bitmap GrayscaleImage;
        public Bitmap ColorisedImage;
        public List<PreviewState> previewStages = new List<PreviewState>();
        public int[,] equalizedMat;
        public Bitmap equalizedImage;
        public int contrast = 0, brightness = 0, saturation = 0;

        void copyState(int index)
        {
            OriginalImage = previewStages[index].OriginalImage;
            ConvertToGrayscale();
            ViewedImage = previewStages[index].ResultImage;
            ImageProcessor.CopyMat(ref Mat, ref previewStages[index].Mat);
            isColorised = previewStages[index].isColorised;
            GrayscaleImage = previewStages[index].GrayImage;
            ColorisedImage = previewStages[index].ColorisedImage;
            brightness = previewStages[index].brightness;
            contrast = previewStages[index].contrast;
        }

        public void SetOriginalImage(Bitmap img)
        {
            OriginalImage = img;
            ViewedImage = img;

            ConvertToGrayscale();
            GetViewedImage();
            previewStages.Add(new PreviewState(stages, null, OriginalImage, OriginalImage, OriginalImage, GrayscaleImage, CopyMat(), isColorised, brightness, contrast, saturation));
        }

        public static PreviewImage operator +(PreviewImage a, PreviewImage b)
        {
            for (int i = 0; i < a.ViewedImage.Width; i++)
            {
                for (int j = 0; j < a.ViewedImage.Height; j++)
                {
                    a.Mat[i, j] = a.Mat[i, j] + b.Mat[i, j];

                    if (a.Mat[i, j] < 0)
                    {
                        a.Mat[i, j] = 0;
                    }
                    if (a.Mat[i, j] > 255)
                    {
                        a.Mat[i, j] = 255;
                    }
                }
            }
            a.returntoColor();
            a.GetViewedImage();

            a.previewStages.Add(new PreviewState(a.stages, null, a.OriginalImage, a.OriginalImage, a.OriginalImage, a.GrayscaleImage, a.CopyMat(), a.isColorised, a.brightness, a.contrast, a.saturation));

            return a;
        }
          public static PreviewImage operator -(PreviewImage a, PreviewImage b)
        {
            for (int i = 0; i < a.ViewedImage.Width; i++)
            {
                for (int j = 0; j < a.ViewedImage.Height; j++)
                {
                    a.Mat[i, j] = a.Mat[i, j] - b.Mat[i, j];

                    if (a.Difference)
                    {
                        if (a.Mat[i, j] < 0)
                        {
                            a.Mat[i, j] *= -1;
                        }
                    }
                    else
                    {
                        if (a.Mat[i, j] < 0)
                        {
                            a.Mat[i, j] = 0;
                        }
                    }
                    if (a.Mat[i, j] > 255)
                    {
                        a.Mat[i, j] = 255;
                    }
                }
            }
            a.returntoColor();
            a.GetViewedImage();
            a.previewStages.Add(new PreviewState(a.stages, null, a.OriginalImage, a.OriginalImage, a.OriginalImage, a.GrayscaleImage, a.CopyMat(), a.isColorised, a.brightness, a.contrast, a.saturation));

            return a;
        }

        public void RangeDifference()
        {
            for (int i = 0; i < Mat.GetLength(0); i++)
            {
                for (int j = 0; j < Mat.GetLength(1); j++)
                {
                    if (Mat[i, j] != 0)
                    {
                        Mat[i, j] = 255;
                    }
                }
            }
            isColorised = false;
            GetViewedImage();
        }

        public Bitmap ApplyBicubic(double ratio)
        {
            Bitmap bmp;
            if (isColorised)
            {


                int[,] r = new int[1, 1];
                int[,] g = new int[1, 1];
                int[,] b = new int[1, 1]; ;
                ImageProcessor.CreateMatrixfromImage(ColorisedImage, ref r, ref g, ref b);
                r = ImageProcessor.BiCubicInterpolation(r, ratio);
                g = ImageProcessor.BiCubicInterpolation(g, ratio);
                b = ImageProcessor.BiCubicInterpolation(b, ratio);
                bmp = ImageProcessor.CreateImageFromMatrix(r, g, b);
                SetOriginalImage(bmp);
            }
            else
            {
                Mat = ImageProcessor.BiCubicInterpolation(Mat, ratio);
                bmp = returnGraytoImage(Mat);
                SetOriginalImage(bmp);
            }
            return bmp;
        }

        public PreviewImage(Bitmap img, bool colorised)
        {
            OriginalImage = img;
            ViewedImage = img;
            this.isColorised = colorised;
            ConvertToGrayscale();
            GetViewedImage();
            previewStages.Add(new PreviewState(stages, null, OriginalImage, OriginalImage, OriginalImage, GrayscaleImage, CopyMat(), isColorised, brightness, contrast, saturation));
        }

        public PreviewImage(Bitmap img)
        {
            OriginalImage = img;
            ViewedImage = img;
            ConvertToGrayscale();
            GetViewedImage();
            previewStages.Add(new PreviewState(stages, null, OriginalImage, OriginalImage, OriginalImage, GrayscaleImage, CopyMat(), isColorised, brightness, contrast, saturation));
        }

        public void UndoState()
        {
            if (previewStages.Count > 1)
            {
                copyState(previewStages.Count - 2);
                previewStages.Remove(previewStages[previewStages.Count - 1]);
                stages--;
            }
        }

        public void revertState()
        {
            if (previewStages.Count > 1)
            {
                copyState(0);
                PreviewState firstState = previewStages[0];
                previewStages = new List<PreviewState>();
                previewStages.Add(firstState);
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

        public int[,] CopyMat()
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
            if (isColorised)
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
             
       
                Mat = invertedMat;
                GetViewedImage();
                previewStages.Add(new PreviewState(stages, null, OriginalImage, ViewedImage, ColorisedImage, GrayscaleImage, CopyMat(), isColorised, brightness, contrast, saturation));
            }
            else
            {
       
                Mat = invertedMat;
                GetViewedImage();
                previewStages.Add(new PreviewState(stages, null, OriginalImage, ViewedImage, ColorisedImage, GrayscaleImage, CopyMat(), isColorised, brightness, contrast, saturation));
            }

            return invertedImage;
        }

        public Bitmap updateImageContrast(int contrastValue)
        {
            contrast = contrastValue;
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

            PreviewImage contrastedColoredImage = new PreviewImage(contrastedImage, true);
            PreviewImage contrastedGreyImage = new PreviewImage(contrastedImage, false);

            if (isColorised)
            {
           
                Mat = contrastedColoredImage.Mat;
                GetViewedImage();
                previewStages.Add(new PreviewState(stages, null, OriginalImage, ViewedImage, ColorisedImage, GrayscaleImage, CopyMat(), isColorised, brightness, contrast, saturation));
            }
            else
            {
            
                Mat = contrastedGreyImage.Mat;
                GetViewedImage();
                previewStages.Add(new PreviewState(stages, null, OriginalImage, ViewedImage, ColorisedImage, GrayscaleImage, CopyMat(), isColorised, brightness, contrast, saturation));
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
                Mat = brightenedMat;
                GetViewedImage();
                previewStages.Add(new PreviewState(stages, null, OriginalImage, ViewedImage, ColorisedImage, GrayscaleImage, CopyMat(), isColorised, brightness, contrast, saturation));
            }
            else
            {
                Mat = brightenedMat;
                GetViewedImage();
                previewStages.Add(new PreviewState(stages, null, OriginalImage, ViewedImage, ColorisedImage, GrayscaleImage, CopyMat(), isColorised, brightness, contrast, saturation));
            }
            
            return brightenedImage;
        }

        public Bitmap updateImageSaturation(int saturation)
        {
            this.saturation = saturation;
            Bitmap saturatedImage = ImageProcessor.CopyImage(ColorisedImage);

            for (int r = 0; r < saturatedImage.Width; r++)
            {
                for (int c = 0; c < saturatedImage.Height; c++)
                {
                    int red = saturatedImage.GetPixel(r, c).R;
                    int green = saturatedImage.GetPixel(r, c).G;
                    int blue = saturatedImage.GetPixel(r, c).B;

                    double[] hsv = ImageProcessor.RGBToHSV(Color.FromArgb(red, green, blue));
                    hsv[1] += Math.Min(hsv[1], hsv[1] + saturation);
                    Color color = ImageProcessor.HSVToRGB(hsv);
                    saturatedImage.SetPixel(r, c, color);
                }
            }

            PreviewImage saturatedColoredImage = new PreviewImage(saturatedImage, true);
            PreviewImage saturatedGreyImage = new PreviewImage(saturatedImage, false);

            if (isColorised)
            {
             
                Mat = saturatedColoredImage.Mat;
                GetViewedImage();
                previewStages.Add(new PreviewState(stages, null, OriginalImage, ViewedImage, ColorisedImage, GrayscaleImage, CopyMat(), isColorised, brightness, contrast, saturation));
            }
            else
            {
        
                Mat = saturatedGreyImage.Mat;
                GetViewedImage();
                previewStages.Add(new PreviewState(stages, null, OriginalImage, ViewedImage, ColorisedImage, GrayscaleImage, CopyMat(), isColorised, brightness, contrast, saturation));
            }

            return saturatedImage;
        }

        public Bitmap equalizeImage()
        {
            equalizedMat = Histogram.getEqualized(Mat);

            if (isColorised)
            {
         
                Mat = equalizedMat;
                GetViewedImage();
                previewStages.Add(new PreviewState(stages, null, OriginalImage, ViewedImage, ColorisedImage, GrayscaleImage, CopyMat(), isColorised, brightness, contrast, saturation));
            }
            else
            {
                Mat = equalizedMat;
                GetViewedImage();
                previewStages.Add(new PreviewState(stages, null, OriginalImage, ViewedImage, ColorisedImage, GrayscaleImage, CopyMat(), isColorised, brightness, contrast, saturation));
            }

            return equalizedImage;
        }

        public void applyFFT()
        {
            //Mat = ImageProcessor.calculateFFT(Mat);

            GetViewedImage();
            previewStages.Add(new PreviewState(stages, null, OriginalImage, ViewedImage, ColorisedImage, GrayscaleImage, CopyMat(), isColorised, brightness, contrast, saturation));
        }

        public void filterImage(Filter f)
        {
            stages++;

            Mat = ImageProcessor.ApplyFilter(Mat, f);

            GetViewedImage();
            previewStages.Add(new PreviewState(stages, f, OriginalImage, ViewedImage, ColorisedImage, GrayscaleImage, CopyMat(), isColorised, brightness, contrast, saturation));
        }
    }
}
