using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageFilters
{
   public class Filter
    {
        private int KernelSize;
        public int kX, kY;
        public double[,] KernelMatrix;
        public int Order = -2;
        public double multiplier = 1;
        public string name;
        public int OutofBoundValue;

        public Filter(double[,] matrix, string name)
        {
            KernelMatrix = matrix;
            kX = matrix.GetLength(0);
            kY = matrix.GetLength(1);
            this.name = name;
            OutofBoundValue = 0;     
        }

        public Filter(string name)
        {
            this.name = name;
        }

        public Filter(double[,] matrix, double val, string name)
        {
            KernelMatrix = matrix;
        
            kX = matrix.GetLength(0);
            kY = matrix.GetLength(1);
            KernelSize = kX;
            for (int i = 0; i < KernelSize; i++)
            {
                for (int j = 0; j < KernelSize; j++)
                {
                    KernelMatrix[i, j] *= val;
                }
            }

            this.name = name;
            OutofBoundValue = 0;
        }
       
        public Filter(int KernelSize, double val, string name)
        {
            this.name = name;
            kX = KernelSize;
            kY = KernelSize;
            this.KernelSize = KernelSize;
            KernelMatrix = new double[KernelSize, KernelSize];

            for(int i = 0; i < KernelSize; i++)
            {
                for(int j = 0; j < KernelSize; j++)
                {
                    KernelMatrix[i, j] = val;
                }
            }

            OutofBoundValue = 0;
        }

        public void randomUpdateFilter(int rowsCount, int columnsCount, double minVal, double maxVal, bool isGaussianBased)
        {
            kX = rowsCount;
            kY = columnsCount;
            Random random = new Random();
            KernelMatrix = new double[rowsCount, columnsCount];

            for (int r = 0; r < KernelMatrix.GetLength(0); r++)
            {
                for (int c = 0; c < KernelMatrix.GetLength(1); c++)
                {
                    if(!isGaussianBased)
                    {
                        KernelMatrix[r, c] = random.Next(0, 256);
                    }
                    else
                    {
                        double square = 1;
                        double v1 = 1;

                        while (square >= 1 || square <= 0)
                        {
                            v1 = 2 * random.NextDouble() - 1;
                            double v2 = 2 * random.NextDouble() - 1;
                            square = v1 * v1 + v2 * v2;
                        }

                        double normalStandard = Math.Sqrt(-2 * Math.Log(square) / square);
                        KernelMatrix[r, c] = v1 * normalStandard * maxVal + minVal;
                    }
                }
            }
        }
    }
}
