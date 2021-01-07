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
        public static double[,] laplacianedge = new double[3, 3] { { 0, -1, 0 }, { -1, 4, -1 }, { 0, -1, 0 } };
        public static double[,] laplacianedgediagonal = new double[3, 3] { { -1, -1, -1 }, { -1, 8, -1 }, { -1, -1, -1 } };
        public static double[,] laplaciansharpen = new double[3, 3] { { 0, -1, 0 }, { -1, 5, -1 }, { 0, -1, 0 } };
        public static double[,] laplaciansharpendiagonal = new double[3, 3] { { -1, -1, -1 }, { -1, 9, -1 }, { -1, -1, -1 } };
        public static double[,] previtHorizontal = { { 1, 1, 1 }, { 0, 0, 0 }, { -1, -1, -1 } };
        public static double[,] previtVertical = { { 1, 0, -1 }, { 1, 0, -1 }, { 1, 0, -1 }};
        public static double[,] Emboss  = { { 0,-1, 0 }, { 0, 0, 0 }, { 0, 1, 0 } };
        public static double[,] gaussianBlur = { { 0.0625, 0.125, 0.0625 }, { 0.125, 0.25, 0.125 }, { 0.0625, 0.125, 0.0625 } };
        public static double[,] sobelHorizontal = new double[3, 3] { { 1, 2, 1 }, { 0, 0, 0 }, { -1, -2, -1 } };
        public static double[,] sobelVertical = new double[3, 3] { { 1, 0, -1 }, { 2, 0, -2 }, { 1, 0, -1 } };
        public static double[,] RobertCrossHorizontal = new double[2, 2] { { 1, 0 }, { 0, -1 } };
        public static double[,] RobertCrossVertical = new double[2, 2] { { 0, 1 }, { -1, 0 } };
        public static double[,] MeanFilter = { { 0.1111f, 0.1111f, 0.1111f }, { 0.1111f, 0.1111f, 0.1111f }, { 0.1111f, 0.1111f, 0.1111f } };

        //public static int[,] calculateFFT(int[,] mat)
        //{
        //    //theta = 2 pi (km/m+ln/n)
        //    ComplexNumber fourier = new ComplexNumber(0, 0);
        //    double u = 0;
        //    double v = (18 * Math.PI) / 256;

        //    for (int r = 0; r < mat.GetLength(0); r++)
        //    {
        //        for (int c = 0; c < mat.GetLength(1); c++)
        //            double theta1 = (2 * Math.PI / mat.GetLength(0)) * u * r;
        //            double theta2 = (2 * Math.PI / mat.GetLength(1)) * v * c;

        //            ComplexNumber e1 = new ComplexNumber(Math.Cos(theta1), -Math.Sin(theta1));
        //            ComplexNumber e2 = new ComplexNumber(Math.Cos(theta2), -Math.Sin(theta2));

        //            fourier += mat[r, c] * e1 * e2;

        //            //double v1 = (u * r) / fourierMat.GetLength(0);
        //            //double v2 = (v * c) / fourierMat.GetLength(1);
        //            //double theta = 2 * Math.PI * (v1 + v2);
        //            //double cos = Math.Cos(theta);
        //            //double sin = Math.Sin(theta);
        //            //ComplexNumber e = new ComplexNumber(cos, -sin);
        //            ////tot += e * mat[r, c];

        //            ////fourierMat[r, c] = tot / Math.Sqrt(mat.GetLength(0) * mat.GetLength(1));
        //            ////fourierMat[r, c] = tot;
        //        }
        //    }

        //    //return applyInverseFourier(fourier);
        //    //return applyLogarithmicTransformation(amplitude);
        //}

        //static int[,] applyInverseFourier(ComplexNumber fourier, int rLen, int cLen)
        //{
        //    int[,] resultMat = new int[rLen, cLen];

        //    for (int r = 0; r < rLen; r++)
        //    {
        //        for (int c = 0; c < cLen; c++)
        //        {
                    
        //        }
        //    }
        //}

        public static double calcRadian(double theta)
        {
            return theta * Math.PI / 180;

        public static double calcTheta(double radian)
        {
            return radian * 180 / Math.PI;

        static int[,] applyLogarithmicTransformation(double[,] mat)
        {
            int[,] resultMat = new int[mat.GetLength(0), mat.GetLength(1)];
            double magnitude = Math.Sqrt(Math.Pow(mat.GetLength(0), 2) + Math.Pow(mat.GetLength(1), 2));
            double constant = 255 / Math.Log(1 + Math.Abs(magnitude));

            for (int r = 0; r < mat.GetLength(0); r++)
                for (int c = 0; c < mat.GetLength(1); c++)
                {
                    resultMat[r, c] = (int)(constant * Math.Log(1 + Math.Abs(mat[r, c])));

                    if (resultMat[r, c] < 0)
                    {
                        resultMat[r, c] = 0;
                    }
                    if (resultMat[r, c] > 255)
                    {
                        resultMat[r, c] = 255;
                    }
                }
            }

            return resultMat;
        }

        static int[,] applySaltAndPepper(int[,] mat, double[,] kernel)
        {
            float ratio = 15;

            for (int r = 0; r < mat.GetLength(0); r++)
            {
                for (int c = 0; c < mat.GetLength(1); c++)
                {
                    if (kernel[r, c] >= 240)
                    {
                        mat[r, c] = (int)kernel[r, c];
                    }
                    else if (kernel[r, c] <= 15)
                    {
                        mat[r, c] = (int)kernel[r, c];
                    }
                }
            }

            return mat;
        }

        public static int[,] ApplyFilter(int[,] Mat, Filter f)
        {
            if (f.name == "Salt and Pepper")
            {
                return applySaltAndPepper(Mat, f.KernelMatrix);
            }

            int width = Mat.GetLength(0);
            int height = Mat.GetLength(1);

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

        public static double[,] PadImage(int[,] Mat)
        {
            double[,] Matpad = new double[Mat.GetLength(0) + 4, Mat.GetLength(1) + 4];
            for (int i = 0; i < Matpad.GetLength(0); i++)
            {
                for (int j = 0; j < Matpad.GetLength(1); j++)
                {
                    Matpad[i, j] = 0;
                }
            }
            for (int i = 1; i < Mat.GetLength(0) + 1; i++)
            {
                for (int j = 1; j < Mat.GetLength(1) + 1; j++)
                {
                    Matpad[i, j] = Mat[i - 1, j - 1];
                }

            }

            return Matpad;
        }

        public static int[,] imgRevered(int[,] Mat)
        {
            int[,] Matpad = new int[Mat.GetLength(1), Mat.GetLength(0)];
            for (int i = 0; i < Matpad.GetLength(0); i++)
            {
                for (int j = 0; j < Matpad.GetLength(1); j++)
                {
                    Matpad[i, j] = Mat[j, i];
                }
            }

            return Matpad;
        }

        public static int[,] BiCubicInterpolation(int[,] Mat, double ratio)
        {
            int rn = (int)Math.Floor((double)(Mat.GetLength(0) * ratio));
            int cn = (int)Math.Floor((double)(Mat.GetLength(1) * ratio));

            int[,] MatImage = new int[rn, cn];
            double[,] matpad = PadImage(Mat);

            for (int i = 0; i < rn; i++)
            {
                double x1 = Math.Ceiling((double)i / (double)ratio);

                double x2 = x1 + 1;
                double x3 = x2 + 1;
                int p = (int)x1;

                double m1 = Math.Ceiling(ratio * ((double)x1 - 1));
                double m2 = Math.Ceiling(ratio * ((double)x1));
                double m3 = Math.Ceiling(ratio * ((double)x2));
                double m4 = Math.Ceiling(ratio * ((double)x3));

                double u1 = ((i - m2) * (i - m3) * (i - m4) / ((m1 - m2) * (m1 - m3) * (m1 - m4)));
                double u2 = (i - m1) * (i - m3) * (i - m4) / ((m2 - m1) * (m2 - m3) * (m2 - m4));
                double u3 = (i - m1) * (i - m2) * (i - m4) / ((m3 - m1) * (m3 - m2) * (m3 - m4));
                double u4 = (i - m1) * (i - m2) * (i - m3) / ((m4 - m1) * (m4 - m2) * (m4 - m3));
                double[,] X = new double[1, 4] { { u1, u2, u3, u4 } };

                for (int j = 0; j < cn; j++)
                {

                    double y1 = (double)Math.Ceiling((double)j / ratio);

                    double y2 = y1 + 1;
                    double y3 = y2 + 1;
                    int q = (int)y1;

                    double n1 = Math.Ceiling(ratio * ((double)y1 - 1));
                    double n2 = Math.Ceiling(ratio * ((double)y1));
                    double n3 = Math.Ceiling(ratio * ((double)y2));
                    double n4 = Math.Ceiling(ratio * ((double)y3));
                    double b1 = ((j - n2) * (j - n3) * (j - n4)) / ((n1 - n2) * (n1 - n3) * (n1 - n4));
                    double b2 = ((j - n1) * (j - n3) * (j - n4)) / ((n2 - n1) * (n2 - n3) * (n2 - n4));
                    double b3 = ((j - n1) * (j - n2) * (j - n4)) / ((n3 - n1) * (n3 - n2) * (n3 - n4));
                    double b4 = ((j - n1) * (j - n2) * (j - n3)) / ((n4 - n1) * (n4 - n2) * (n4 - n3));
                    double[,] Y = new double[4, 1] { { b1 }, { b2 }, { b3 }, { b4 } };


                    double[,] neighbors = new double[4, 4];
                    CopyMat(ref neighbors, ref matpad, p, p + 3, q, q + 3);
                    double[,] mat = matrixMultiplication(X, neighbors);

                    mat = matrixMultiplication(mat, Y);


                    MatImage[i, j] = (int)(mat[0, 0]);
                    if (MatImage[i, j] > 255)
                    {
                        MatImage[i, j] = 255;
                    }
                    if (MatImage[i, j] < 0)
                    {
                        MatImage[i, j] = 0;
                    }
                }
            }

            return MatImage;
        }

        public static double[,] matrixMultiplication(double[,] matrix1, double[,] matrix2)
        {
            if (matrix1.GetLength(1) == matrix2.GetLength(0))
            {
                double[,] resultMatrix = new double[matrix1.GetLength(0), matrix2.GetLength(1)];

                for (int r = 0; r < resultMatrix.GetLength(0); r++)
                {
                    for (int c = 0; c < resultMatrix.GetLength(1); c++)
                    {
                        resultMatrix[r, c] = 0;

                        for (int i = 0; i < matrix1.GetLength(1); i++)
                        {
                            resultMatrix[r, c] += matrix1[r, i] * matrix2[i, c];
                        }
                    }
                }

                return resultMatrix;
            }
            else
            {
                return null;
            }
        }

        public static Bitmap NearestNeighborInterpolation(Bitmap image, Size size)
        {
            Bitmap enlargedImage = new Bitmap(size.Width, size.Height);

            for (int i = 0; i < size.Width; i++)
            {
                for (int j = 0; j < size.Height; j++)
                {
                    int x = (int)Math.Floor(((double)(image.Width) * (double)i) / (double)size.Width);
                    int y = (int)Math.Floor(((double)(image.Height) * (double)j) / (double)size.Height);

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

                    float gx = ((float)i) * (image.Width - 1) / (size.Width - 1);
                    float gy = ((float)j) * (image.Height - 1) / (size.Height - 1);
                    int x1 = (int)Math.Floor(gx);
                    int y1 = (int)Math.Floor(gy);

                    int x2 = (int)Math.Ceiling(gx);

                    int y2 = (int)Math.Ceiling(gy);


                    Color c11 = image.GetPixel(x1, y1);
                    Color c21 = image.GetPixel(x2, y1);
                    Color c12 = image.GetPixel(x1, y2);
                    Color c22 = image.GetPixel(x2, y2);
                    enlargedImage.SetPixel(i, j, interpolatePixel(c11, c21, c12, c22, gx, gy, x1, x2, y1, y2));

                    double x = ((double)(image.Width - 1) * (double)i) / (double)size.Width;
                    double y = ((double)(image.Height - 1) * (double)j) / (double)size.Height;

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


                }
            }

            return enlargedImage;
        }

        public static void RangeColors(ref double r1, ref double g1, ref double b1)
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

            double r1;
            double g1;
            double b1;
            double r2;
            double g2;
            double b2;

            if (x1 != x2)
            {
                double dx1 = (x2 - x) / (x2 - x1);
                double dx2 = (x - x1) / (x2 - x1);
                r1 = (int)((dx1 * f11.R) + (dx2 * f21.R));
                g1 = (int)((dx1 * f11.G) + (dx2 * f21.G));
                b1 = (int)((dx1 * f11.B) + (dx2 * f21.B));
                r2 = (int)((dx1 * f12.R) + (dx2 * f22.R));
                g2 = (int)((dx1 * f12.G) + (dx2 * f22.G));
                b2 = (int)((dx1 * f12.B) + (dx2 * f22.B));

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

            //RangeColors(ref r1, ref b1, ref g1);
            // RangeColors(ref r2, ref b2, ref g2);
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
                if (x1 != x2)
                {

                    rf = (int)((dy1 * r1) + (dy2 * r2));
                    gf = (int)((dy1 * g1) + (dy2 * g2));
                    bf = (int)((dy1 * b1) + (dy2 * b2));
                }
                else
                {
                    rf = (int)((dy1 * r1) + (dy2 * r2));
                    gf = (int)((dy1 * g1) + (dy2 * g2));
                    bf = (int)((dy1 * b1) + (dy2 * b2));
                }
            }

            RangeColors(ref rf, ref gf, ref bf);

            return Color.FromArgb((int)rf, (int)gf, (int)bf);
        }
        
        static int ComputeHarmonic(Filter f , int[,] Mat , int ip , int jp )
        {
            double Total = 0;
            double tr = 0, tg = 0, tb = 0;
            int i2 = 0, j2 = 0;
            int width = Mat.GetLength(0);
            int height = Mat.GetLength(1);
            double SecondTotal = 0;
            int[] L = new int[9];
            int evenodd = 1;
            if (f.kY % 2 == 0)
            {
                evenodd = 0;
            }
            if (f.kY % 2 == 0)
                for (int j = jp - (f.kY / 2); j < jp + (f.kY / 2) + evenodd; j++, j2++)

                    {
            if (Total != 0)
            }
            }

        static int ComputeMatrix(Filter f, int[,] Mat, int ip, int jp)
        {
            double Total = 0;
            double tr = 0, tg = 0, tb = 0;
            int i2 = 0, j2 = 0;
            int order = f.Order;
            int width = Mat.GetLength(0);
            double SecondTotal = 0;
            int height = Mat.GetLength(1);
            int[] L = new int[9];
            int evenodd = 1;
            if (f.kY % 2 == 0)
            {
                evenodd = 0;
            }
            if (f.kY % 2 == 0)
            {
                evenodd = 0;
            }
            if (!f.name.Contains("order"))
            {
                for (int i = ip - (f.kX / 2); i < ip + (f.kX / 2) + evenodd; i++, i2++)
                {
                    j2 = 0;

                    for (int j = jp - (f.kY / 2); j < jp + (f.kY / 2) + evenodd; j++, j2++)
                    {

                        if (f.name != "harmonic")
                        {
                            if (i < 0 || i >= width || j < 0 || j >= height)
                            {
                                Total += f.OutofBoundValue * f.KernelMatrix[i2, j2];
                            }
                            else
                            {
                                double val = Mat[i, j] * f.KernelMatrix[i2, j2];

                                Total += val;
                            }
                        }
                        else
                        {
                            if (i < 0 || i >= width || j < 0 || j >= height)
                            {
                                double val = Math.Pow(f.OutofBoundValue, order - 1) * f.KernelMatrix[i2, j2];
                                double val2 = Math.Pow(f.OutofBoundValue, order) * f.KernelMatrix[i2, j2];
                                Total += val;
                                SecondTotal += val2;
                            }
                            else
                            {
                                double val = Math.Pow(Mat[i, j], order - 1) * f.KernelMatrix[i2, j2];
                                double val2 = Math.Pow(Mat[i, j], order) * f.KernelMatrix[i2, j2];
                                Total += val;
                                SecondTotal += val2;

                            }
                        }
                    }
                }
                if (f.name == "harmonic")
                {
                    if (Total != 0)
                    {
                        Total = SecondTotal / Total;
                    }
                }
            }
            else
            {
                int k = 0;

                for (int i = ip - (f.kX / 2); i < ip + (f.kX / 2) + evenodd; i++, i2++)
                {
                    for (int j = jp - (f.kY / 2); j < jp + (f.kY / 2) + evenodd; j++, j2++, k++)
                    {
                        if (i < 0 || i >= width || j < 0 || j >= height)
                        {
                            L[k] = 0;
                        }
                        else
                        {
                            L[k] = Mat[i, j];
                        }
                    }
                }

                Sorted(ref L);

                    if (f.name.Contains("minimum"))
                if (f.name.Contains("minimum"))
                {
                    Total = L[0];
                }
                else if (f.name.Contains("maximum"))
                {
                    Total = L[L.GetLength(0) - 1];
                }
                else if (f.name.Contains("median"))
                {
                    Total = L[(L.GetLength(0) - 1) / 2];
                }

            }

            double Total2 = 0;

            if (f.name.Contains("Sobel") || f.name.Contains("RobertCross"))
            {

                if (Total < 0)
                {
                    Total *= -1;
                }
            }
            if (Total < 0)
            {
                Total = 0;
            }
            if (Total > 255)
            {
                Total = 255;
            }

            return (int)Total;
        }

        static void Sorted(ref int[] L)
        {
            for (int i = 0; i < L.Count(); i++)
            {
                for (int k = 1; k < L.Count(); k++)
                {
                    if (L[k] < L[k - 1])
                    {
                        int tmp = L[k];
                        L[k] = L[k - 1];
                        L[k - 1] = tmp;
                    }
                }
            }

        }

        public static void CreateMatrixfromImage(Bitmap img, ref int[,] r, ref int[,] g, ref int[,] b)
        {
            r = new int[img.Width, img.Height];
            g = new int[img.Width, img.Height];
            b = new int[img.Width, img.Height];
            for (int i = 0; i < img.Width; i++)
            {
                for (int j = 0; j < img.Height; j++)
                {
                    r[i, j] = img.GetPixel(i, j).R;
                    g[i, j] = img.GetPixel(i, j).G;
                    b[i, j] = img.GetPixel(i, j).B;
                }
            }
        }

        public static Bitmap CreateImageFromMatrix(int[,] r, int[,] g, int[,] b)
        {
            Bitmap img = new Bitmap(r.GetLength(0), r.GetLength(1));
            for (int i = 0; i < img.Width; i++)
            {
                for (int j = 0; j < img.Height; j++)
                {
                    img.SetPixel(i, j, Color.FromArgb(r[i, j], g[i, j], b[i, j]));
                }

            }
            return img;
        }

        public static int[,,] CreateMatrixfromImage(Bitmap img)
        {
            int[,,] ImgMat = new int[img.Width, img.Height, 3];

            for (int i = 0; i < img.Width; i++)
            {
                for (int j = 0; j < img.Height; j++)
                {
                    ImgMat[i, j, 0] = img.GetPixel(i, j).R;
                    ImgMat[i, j, 1] = img.GetPixel(i, j).G;
                    ImgMat[i, j, 2] = img.GetPixel(i, j).B;
                }

            }
            return ImgMat;
        }

        public static Bitmap CreateImageFromMatrix(int[,,] ImgMat)
        {
            Bitmap img = new Bitmap(ImgMat.GetLength(0), ImgMat.GetLength(1));
            for (int i = 0; i < img.Width; i++)
            {
                for (int j = 0; j < img.Height; j++)
                {
                    img.SetPixel(i, j, Color.FromArgb(ImgMat[i, j, 0], ImgMat[i, j, 1], ImgMat[i, j, 2]));
                }

            }
            return img;
        }

        public static Bitmap CopyImage(Bitmap image)
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

        public static void CopyMat(ref double[,] Mat, ref int[,] MatOrigin)
        {
            Mat = new double[MatOrigin.GetLength(0), MatOrigin.GetLength(1)];

            for (int i = 0; i < MatOrigin.GetLength(0); i++)
            {
                for (int j = 0; j < MatOrigin.GetLength(1); j++)
                {
                    Mat[i, j] = (double)MatOrigin[i, j];
                }
            }
        }

        public static void CopyMat(ref double[,] Mat, ref double[,] MatOrigin, int xr, int xe, int yr, int ye)
        {
            int i2 = 0;
            int j2 = 0;

            for (int i = xr; i < xe + 1; i++, i2++)
            {
                j2 = 0;

                for (int j = yr; j < ye + 1; j++, j2++)
                {
                    Mat[i2, j2] = (double)MatOrigin[i, j];
                }
            }
        }

        public static double[] RGBToHSV(Color c)
        {
            int r = c.R / 255;
            int g = c.G / 255;
            int b = c.B / 255;
            int min = Math.Min(r, g);
            min = Math.Min(min, b);
            int max = Math.Max(r, g);
            max = Math.Max(max, b);
            int difference = max - min;
            double s = 0, h = 0;

            if (max != 0)
            {
                s = difference / max;
            }
            if (difference != 0)
            {
                if (r == max)
                {
                    h = 60 * (((g - b) / difference) % 6);
                }
                else if (g == max)
                {
                    h = 60 * (((b - r) / difference) + 2);
                }
                else
                {
                    h = 60 * (((r - g) / difference) + 4);
                }
            }

            return new double[3] { h, s, max };
        }

        public static Color HSVToRGB(double[] hsv)
        {
            double r = 0;
            double g = 0;
            double b = 0;
            double h = hsv[0];
            double s = hsv[1];
            double v = hsv[2];

            if (s == 0)
            {
                r = v;
                g = v;
                b = v;
            }

            double c = v * s;
            double x = c * (1 - Math.Abs((h / 60) % (2 - 1)));
            double m = v - c;

            if (h >= 0 && h < 60)
            {
                r = c;
                g = x;
                b = 0;
            }
            if (h >= 60 && h < 120)
            {
                r = x;
                g = c;
                b = 0;
            }
            if (h >= 120 && h < 180)
            {
                r = 0;
                g = c;
                b = x;
            }
            if (h >= 180 && h < 240)
            {
                r = 0;
                g = x;
                b = c;
            }
            if (h >= 240 && h < 300)
            {
                r = x;
                g = 0;
                b = c;
            }
            if (h >= 300 && h < 360)
            {
                r = c;
                g = 0;
                b = x;
            }

            return Color.FromArgb((int)((r + m) * 255), (int)((g + m) * 255), (int)((b + m) * 255));

        }
    }
 }
    