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

        public static PreviewImage operator +(PreviewImage a, PreviewImage b)
            {
            PreviewImage img = new PreviewImage(a.OriginalImage);
         for(int i =0; i < a.ViewedImage.Width; i++)
            {
                for (int j = 0; j< a.ViewedImage.Height; j++)
                {
                  img.Mat[i,j] = a.Mat[i, j] + b.Mat[i, j];
                    if (img.Mat[i,j] < 0)
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
            ConvertToGrayscale();
            GetViewedImage();
            previewStages.Add(new PreviewState(stages, null, OriginalImage, OriginalImage, GrayscaleImage, CopyMat(), isColorised));
        }
        public PreviewImage(Bitmap img )
        {
            OriginalImage = img;
            ViewedImage = img;
            ConvertToGrayscale();
            GetViewedImage();
            previewStages.Add(new PreviewState(stages, null, OriginalImage, OriginalImage, GrayscaleImage, CopyMat(), isColorised));
        }
        public void UndoState()
        {
            if (previewStages.Count > 1)
            {
                ViewedImage = previewStages[previewStages.Count - 2].ResultImage;
               CopyMat(ref Mat ,ref previewStages[previewStages.Count - 2].Mat);
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
       public void returntoColor( )
        {
            ColorisedImage = new Bitmap(OriginalImage.Width, OriginalImage.Height);
            for (int i = 0; i < OriginalImage.Width; i++)
            {
                for (int j = 0; j < OriginalImage.Height; j++)
                {
                    int r = 0;
                    int g = 0;
                    int b = 0;
                    Color c = OriginalImage.GetPixel(i, j);
                    if (MatOrigin[i, j] != 0)
                    {
                        r = c.R * Mat[i, j] / MatOrigin[i, j];
                        g = c.G * Mat[i, j] / MatOrigin[i, j];
                        b = c.B * Mat[i, j] / MatOrigin[i, j];
                    }
                    else
                    {
                       r = c.R* Mat[i, j];
                        g = c.G * Mat[i, j];
                       b = c.B * Mat[i, j];
                    }
                     if (r < 0 )
                    {
                        r = 0;

                    }
                    if (g < 0)
                    {
                        g = 0;
                    }
                    if ( b < 0 )
                    {
                       b = 0;
                    }
                    if (r > 255)
                    {
                        r = 255;

                    }
                    if (g > 255)
                    {
                        g = 255;
                    }
                    if (b > 255)
                    {
                        b = 255;
                    }
                    ColorisedImage.SetPixel(i, j,Color.FromArgb(r,g, b));
                }
            }
        }
        int[,] CopyMat()
        {
            int[,] MatOrigin = new int[OriginalImage.Width, OriginalImage.Height];
            for (int i = 0; i < OriginalImage.Width; i++)
            {
                for (int j = 0; j < OriginalImage.Height; j++)
                {
                    Color c = OriginalImage.GetPixel(i, j);
               
                    MatOrigin[i, j] = Mat[i, j];

                }
            }
            return MatOrigin;
        }
       void CopyMat(ref int[,] MatOrigin ,ref int[,] Mat)
        {
             MatOrigin = new int[OriginalImage.Width, OriginalImage.Height];
            for (int i = 0; i < OriginalImage.Width; i++)
            {
                for (int j = 0; j < OriginalImage.Height; j++)
                {
                    Color c = OriginalImage.GetPixel(i, j);

                    MatOrigin[i, j] = Mat[i, j];

                }
            }
          
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
      int[,] ConvertToGrayscale(int [,] mat)
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
        public void FilterImage(Filter f)
        {
            stages++;
           Mat = ImageProcessor.ApplyFilter(Mat, f);
            GetViewedImage();
            previewStages.Add(new PreviewState(stages, f, ViewedImage,ColorisedImage,GrayscaleImage, CopyMat(), isColorised));
        }
    }
}
