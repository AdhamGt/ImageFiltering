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
        //public static double[,] edge = new double[3, 3] { { -1, -1, -1 }, { -1, 8, -1 }, { -1, -1, -1 } };
        public static double[,] sharpen = new double[3, 3] { { 0, -1, 0 }, { -1,5, -1 }, { 0, -1, 0 } };
        public static double[,] gaussianBlur = { {0.0625, 0.125, 0.0625 }, { 0.125, 0.25, 0.125 }, { 0.0625, 0.125, 0.0625} };
        public static double[,] sobelHorizontal = new double[3, 3] { { 1, 2, 1 }, { 0, 0, 0 }, { -1, -2, -1 } };
        public static double[,] sobelVertical = new double[3, 3] { {1, 0, -1}, {2, 0, -2}, {1, 0, -1} };
        private double a00, a01, a02, a03;
        private double a10, a11, a12, a13;
        private double a20, a21, a22, a23;
        private double a30, a31, a32, a33;
        public static Bitmap BicubicInterpolation(int[,] Mat ,double factor )
        {
            Bitmap enlargedImage = new Bitmap(Mat.GetLength(0), Mat.GetLength(1));
            double ivf = 1 / factor;
            for (int i = 0; i < Mat.GetLength(0); i++)
            {
                for (int j = 0; j < Mat.GetLength(1); j++)
                {

                   
                }
            }
            return enlargedImage;
        }
        public void computePolonomyalCoff(double[,] Mat, int x, int y)
        {
            a00 = Mat[x + 1, 1 + y];
            a01 = -.5 * Mat[x + 1, 0 + y] + .5 * Mat[x + 1, 2 + y];
            a02 = Mat[x + 1, 0 + y] - 2.5 * Mat[x + 1, 1 + y] + 2 * Mat[x + 1, 2 + y] - .5 * Mat[x + 1, 3 + y];
            a03 = -.5 * Mat[x + 1, 0 + y] + 1.5 * Mat[x + 1, 1 + y] - 1.5 * Mat[x + 1, 2 + y] + .5 * Mat[x + 1, 3 + y];
            a10 = -.5 * Mat[x + 0, 1 + y] + .5 * Mat[x + 2, 1 + y];
            a11 = .25 * Mat[x + 0, 0 + y] - .25 * Mat[x + 0, 2 + y] - .25 * Mat[x + 2, 0 + y] + .25 * Mat[x + 2, 2 + y];
            a12 = -.5 * Mat[x + 0, 0 + y] + 1.25 * Mat[x + 0, 1 + y] - Mat[x + 0, 2 + y] + .25 * Mat[x + 0, 3 + y] + .5 * Mat[x + 2, 0 + y] - 1.25 * Mat[x + 2, 1 + y] + Mat[x + 2, 2 + y] - .25 * Mat[x + 2, 3 + y];
            a13 = .25 * Mat[x + 0, 0 + y] - .75 * Mat[x + 0, 1 + y] + .75 * Mat[x + 0, 2 + y] - .25 * Mat[x + 0, 3 + y] - .25 * Mat[x + 2, 0 + y] + .75 * Mat[x + 2, 1 + y] - .75 * Mat[x + 2, 2 + y] + .25 * Mat[x + 2, 3 + y];
            a20 = Mat[x + 0, 1 + y] - 2.5 * Mat[x + 1, 1 + y] + 2 * Mat[x + 2, 1 + y] - .5 * Mat[x + 3, 1 + y];
            a21 = -.5 * Mat[x + 0, 0 + y] + .5 * Mat[x + 0, 2 + y] + 1.25 * Mat[x + 1, 0 + y] - 1.25 * Mat[x + 1, 2 + y] - Mat[x + 2, 0 + y] + Mat[x + 2, 2 + y] + .25 * Mat[x + 3, 0 + y] - .25 * Mat[x + 3, 2 + y];
            a22 = Mat[x + 0, 0 + y] - 2.5 * Mat[x + 0, 1 + y] + 2 * Mat[x + 0, 2 + y] - .5 * Mat[x + 0, 3 + y] - 2.5 * Mat[x + 1, 0 + y] + 6.25 * Mat[x + 1, 1 + y] - 5 * Mat[x + 1, 2 + y] + 1.25 * Mat[x + 1, 3 + y] + 2 * Mat[x + 2, 0 + y] - 5 * Mat[x + 2, 1 + y] + 4 * Mat[x + 2, 2 + y] - Mat[x + 2, 3 + y] - .5 * Mat[x + 3, 0 + y] + 1.25 * Mat[x + 3, 1 + y] - Mat[x + 3, 2 + y] + .25 * Mat[x + 3, 3 + y];
            a23 = -.5 * Mat[x + 0, 0 + y] + 1.5 * Mat[x + 0, 1 + y] - 1.5 * Mat[x + 0, 2 + y] + .5 * Mat[x + 0, 3 + y] + 1.25 * Mat[x + 1, 0 + y] - 3.75 * Mat[x + 1, 1 + y] + 3.75 * Mat[x + 1, 2 + y] - 1.25 * Mat[x + 1, 3 + y] - Mat[x + 2, 0 + y] + 3 * Mat[x + 2, 1 + y] - 3 * Mat[x + 2, 2 + y] + Mat[x + 2, 3 + y] + .25 * Mat[x + 3, 0 + y] - .75 * Mat[x + 3, 1 + y] + .75 * Mat[x + 3, 2 + y] - .25 * Mat[x + 3, 3 + y];
            a30 = -.5 * Mat[x + 0, 1 + y] + 1.5 * Mat[x + 1, 1 + y] - 1.5 * Mat[x + 2, 1 + y] + .5 * Mat[x + 3, 1 + y];
            a31 = .25 * Mat[x + 0, 0 + y] - .25 * Mat[x + 0, 2 + y] - .75 * Mat[x + 1, 0 + y] + .75 * Mat[x + 1, 2 + y] + .75 * Mat[x + 2, 0 + y] - .75 * Mat[x + 2, 2 + y] - .25 * Mat[x + 3, 0 + y] + .25 * Mat[x + 3, 2 + y];
            a32 = -.5 * Mat[x + 0, 0 + y] + 1.25 * Mat[x + 0, 1 + y] - Mat[x + 0, 2 + y] + .25 * Mat[x + 0, 3 + y] + 1.5 * Mat[x + 1, 0 + y] - 3.75 * Mat[x + 1, 1 + y] + 3 * Mat[x + 1, 2 + y] - .75 * Mat[x + 1, 3 + y] - 1.5 * Mat[x + 2, 0 + y] + 3.75 * Mat[x + 2, 1 + y] - 3 * Mat[x + 2, 2 + y] + .75 * Mat[x + 2, 3 + y] + .5 * Mat[x + 3, 0 + y] - 1.25 * Mat[x + 3, 1 + y] + Mat[x + 3, 2 + y] - .25 * Mat[x + 3, 3 + y];
            a33 = .25 * Mat[x + 0, 0 + y] - .75 * Mat[x + 0, 1 + y] + .75 * Mat[x + 0, 2 + y] - .25 * Mat[x + 0, 3 + y] - .75 * Mat[x + 1, 0 + y] + 2.25 * Mat[x + 1, 1 + y] - 2.25 * Mat[x + 1, 2 + y] + .75 * Mat[x + 1, 3 + y] + .75 * Mat[x + 2, 0 + y] - 2.25 * Mat[x + 2, 1 + y] + 2.25 * Mat[x + 2, 2 + y] - .75 * Mat[x + 2, 3 + y] - .25 * Mat[x + 3, 0 + y] + .75 * Mat[x + 3, 1 + y] - .75 * Mat[x + 3, 2 + y] + .25 * Mat[x + 3, 3 + y];
        }

        public double getpixelValue(double x, double y)
        {
            double x2 = x * x;
            double x3 = x2 * x;
            double y2 = y * y;
            double y3 = y2 * y;

            return (a00 + a01 * y + a02 * y2 + a03 * y3) +
                   (a10 + a11 * y + a12 * y2 + a13 * y3) * x +
                   (a20 + a21 * y + a22 * y2 + a23 * y3) * x2 +
                   (a30 + a31 * y + a32 * y2 + a33 * y3) * x3;
        }
        public static int[,] ApplyFilter(int[,] Mat, Filter f)
        {
            int width = Mat.GetLength(0);
            int height = Mat.GetLength(1);

            // Apply Any Spatial Filter
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
        public static Bitmap NearestNeighborInterpolation(Bitmap image, Size size)
        {
            Bitmap enlargedImage = new Bitmap(size.Width, size.Height);
            for (int i = 0; i < size.Width; i++)
            {
                for (int j = 0; j < size.Height; j++)
                {
                    int x = (image.Width * i) / size.Width;
                    int y = (image.Height * j) / size.Height;
                    enlargedImage.SetPixel(i, j, image.GetPixel(x, y));
                }
            }
            return enlargedImage;
        }
        public static Bitmap BilinearInterpolation(Bitmap image, Size size)
        {
            Bitmap enlargedImage = new Bitmap(size.Width, size.Height);
            for (int i = 0; i < size.Width; i++)
            {
                for (int j = 0; j < size.Height; j++)
                {
                    double x = ((double)(image.Width - 1) * (double)i) / (double)size.Width;
                    double y = ((double)(image.Height - 1) * (double)j) / (double)size.Height;
                    int x1 = (int)Math.Floor(x);
                    int x2 = (int)x1+1;
                    int y1 = (int)Math.Floor(y);
                    int y2 = (int)y1+1;
                    if (x2 == x1 && y2 == y1)
                    {
                        enlargedImage.SetPixel(i, j, image.GetPixel((int)x, (int)y));
                    }

                    if (y2 >= image.Height)
                    {
                        y2 = image.Height - 1;
                    }
                    if (x2 >= image.Width)
                    {
                        x2 = image.Width - 1;
                    }
                    Color c11 = image.GetPixel(x1, y1);
                    Color c21 = image.GetPixel(x2, y1);
                    Color c12 = image.GetPixel(x1, y2);
                    Color c22 = image.GetPixel(x2, y2);
                    enlargedImage.SetPixel(i, j, interpolatePixel(c11, c21, c12, c22, x, y, x1, x2, y1, y2));

                }
            }
            return enlargedImage;
        }
        static void RangeColors(ref double r1, ref double g1, ref double b1)
        {
            if (r1 > 255)
            {
                r1 = 255;
            }
            if (b1 > 255)
            {
                b1 = 255;
            }
            if (g1 > 255)
            {
                g1 = 255;
            }
            if (r1 < 0)
            {
                r1 = 0;
            }
            if (b1 < 0)
            {
                b1 = 0;
            }
            if (g1 < 0)
            {
                g1 = 0;
            }
        }
        public static Color interpolatePixel(Color f11, Color f21, Color f12, Color f22, double x, double y, double x1, double x2, double y1, double y2)
        {
            // interpolate X
            double dx1 = (x2 - x) / (x2 - x1);
            double dx2 = (x - x1) / (x2 - x1);
            double r1;
            double g1;
            double b1;
            double r2;
            double g2;
            double b2;
            if (x1 != x2)
            {
                r1 = (int)(dx1 * f11.R + dx2 * f21.R);
                g1 = (int)(dx1 * f11.G + dx2 * f21.G);
                b1 = (int)(dx1 * f11.B + dx2 * f21.B);
                r2 = (int)(dx1 * f12.R + dx2 * f22.R);
                g2 = (int)(dx1 * f12.G + dx2 * f22.G);
                b2 = (int)(dx1 * f12.B + dx2 * f22.B);

            }
            else
            {
                r1 = (int)(f11.R);
                g1 = (int)(f11.G);
                b1 = (int)(f11.B);
                r2 = (int)(f12.R);
                g2 = (int)(f12.G);
                b2 = (int)(f12.B);
            }
            RangeColors(ref r1, ref b1, ref g1);
            RangeColors(ref r2, ref b2, ref g2);
            // interplolate Y
            double dy1;
            double dy2;
            double rf;
            double gf;
            double bf;
            if (y1 == y2)
            {

                rf = (int)(r1);
                gf = (int)(g1);
                bf = (int)(b1);
            }
            else
            {
                dy1 = (y2 - y) / (y2 - y1);
                dy2 = (y - y1) / (y2 - y1);
                rf = (int)(dx1 * r1 + dx2 * r2);
                gf = (int)(dx1 * g1 + dx2 * g2);
                bf = (int)(dx1 * b1 + dx2 * b2);
            }
            RangeColors(ref rf, ref gf, ref bf);

            return Color.FromArgb((int)rf, (int)gf, (int)bf);
        }
        static int ComputeMatrix(Filter f, int[,] Mat, int ip, int jp)
         {
            double Total = 0;
           
            double tr = 0, tg = 0, tb = 0;
            int i2 = 0, j2 = 0;
            int width = Mat.GetLength(0);
            int height = Mat.GetLength(1);

            for (int i = ip - (f.kX / 2); i < ip + (f.kX / 2) + 1; i++, i2++)
            {
                j2 = 0;

                for (int j = jp - (f.kY / 2); j < jp + (f.kY / 2) + 1; j++, j2++)
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

        public static void CopyMat(ref int[,] Mat, ref int[,] MatOrigin)
        {
            Mat = new int[MatOrigin.GetLength(0), MatOrigin.GetLength(1)];

            for (int i = 0; i < MatOrigin.GetLength(0); i++)
            {
                for (int j = 0; j < MatOrigin.GetLength(1); j++)
                {
                    Mat[i, j] = MatOrigin[i, j];
                }
            }
        }
    }
 }
    