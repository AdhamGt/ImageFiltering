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
        public int multiplyer = 1;
        public string name;
        public int OutofBoudValue;
        public Filter(double[,] matrix , string name )
        {
            KernelMatrix = matrix;
            kX = matrix.GetLength(0);
            kY = matrix.GetLength(1);
            this.name = name;
            OutofBoudValue = 0;     
        }
        public Filter(double[,] matrix,int val ,string name)
        {
            KernelMatrix = matrix;
            kX = matrix.GetLength(0);
            kY = matrix.GetLength(1);
            for (int i = 0; i < KernelSize; i++)
            {
                for (int j = 0; j < KernelSize; j++)
                {
                    KernelMatrix[i, j] *= val;
                }
            }
            this.name = name;
            OutofBoudValue = 0;
        }
       
        public Filter(int KernelSize , double val , string name )
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

            OutofBoudValue = 0;
        }
        
    }
}
