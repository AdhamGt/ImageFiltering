using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageFilters
{
    public class ImageProcessor
    {
        private List<Filter> predefinedFilters = new List<Filter>();
        public static double[,] edge = new double[3, 3] { { -1, -1, -1 }, { -1, 8, -1 }, { -1, -1, -1 } };
     //   public static double[,] edge = new double[3, 3] { { -1, -1, -1 }, { -1, 8, -1 }, { -1, -1, -1 } };
        public static double[,] sharpen = new double[3, 3] { { 0, -1, 0 }, { -1,5, -1 }, { 0, -1, 0 } };
        public static double[,] gaussianBlur= { {0.0625, 0.125, 0.0625 },
  { 0.125, 0.25, 0.125 }, { 0.0625, 0.125, 0.0625} };
        public static double[,] sobelHorizontal = new double[3, 3] { { 1, 2, 1 }, { 0, 0, 0 }, { -1, -2, -1 } };
  public static double[,] sobelVertical =new double[3, 3] {{1, 0, -1}, {2, 0, -2}, {1, 0, -1} };
        public static int[,] ApplyFilter(int[,] Mat, Filter f)
            {
            int width = Mat.GetLength(0);
            int height = Mat.GetLength(1);

        //        // Apply Any Spatial Filter    
               int[,] mat2 = new int[Mat.GetLength(0), Mat.GetLength(1)];
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    mat2[i, j] = Mat[i, j];
                }
            }
                    for (int i = 0; i < width; i++)
               {
                    for (int j = 0; j < height; j++)
                  {

                       Mat[i, j] = ComputeMatrix(f, mat2, i, j);
                   }
               }
              return Mat;
            }




         static int ComputeMatrix(Filter f, int[,] Mat, int ip, int jp)
        {

            double Total = 0;
            double tr = 0, tg = 0, tb = 0;
            int i2 = 0, j2 = 0;
            int width = Mat.GetLength(0);
            int height = Mat.GetLength(1);
            for (int i = ip - (f.kX / 2); i < ip+ (f.kX / 2)+1; i++, i2++)
            {
                j2 = 0;
                for (int j = jp - (f.kY / 2); j < jp + (f.kY / 2)+1 ; j++, j2++)
                {
                    if (i < 0 || i >= width || j < 0 || j >= height)
                    {

                        Total += f.OutofBoudValue * f.KernelMatrix[i2, j2];
                    }
                    else
                    {

                        Total += Mat[i, j] * f.KernelMatrix[i2, j2];
                    }
                }
            }
            double Total2 = 0;
         
       
            if (Total > 255)
            {
                Total = 255;
            }
            if ( Total < 0)
            {
                Total = 0;
            }
            return (int)Total;
        }
     public   static Bitmap NearestNeighborInterpolation(Bitmap image , Size size)
        {
            Bitmap enlargedImage = new Bitmap(size.Width,size.Height);
            for(int i = 0; i < size.Width; i++)
            {
                for(int j = 0; j < size.Height; j++)
                {
                    int x = (image.Width * i) / size.Width;
                    int y = (image.Height * j) / size.Height;
                    enlargedImage.SetPixel(i, j, image.GetPixel(x,y));
                }
            }
            return enlargedImage;
        }
        static Bitmap CopyImage(Bitmap image)
        {
            Bitmap temp = new Bitmap(image.Width, image.Height);

            for (int i = 0; i < image.Width; i++)
            {
                for (int j = 0; j < image.Height; j++)
                {
                    temp.SetPixel(i, j, image.GetPixel(i, j));
                }
            }
            return temp;
        }
    }
 }
    