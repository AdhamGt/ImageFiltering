using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageFilters
{
    public class Histogram
    {
        static void calculateFrequencies(int mn, int[,] mat, ref float[] intensityFrequencies)
        {
            for (int r = 0; r < mat.GetLength(0); r++)
            {
                for (int c = 0; c < mat.GetLength(1); c++)
                {
                    intensityFrequencies[mat[r, c]]++;
                }
            }
            for (int i = 0; i < intensityFrequencies.Length; i++)
            {
                intensityFrequencies[i] /= mn;
            }
        }

        static void buildEqualized(float[] intensityFrequencies, ref int[,] equalizedHistogram)
        {
            int[] transformationFunction = getTransformationFunction(intensityFrequencies);

            for (int r = 0; r < equalizedHistogram.GetLength(0); r++)
            {
                for (int c = 0; c < equalizedHistogram.GetLength(1); c++)
                {
                    equalizedHistogram[r, c] = transformationFunction[equalizedHistogram[r, c]];
                }
            }
        }

        static int[] getTransformationFunction(float[] frequencies)
        {
            int l = frequencies.Length - 1;
            int[] transformationFunction = new int[frequencies.Length];

            for (int i = 0; i < transformationFunction.Length; i++)
            {
                float total = 0;

                for (int u = 0; u <= i; u++)
                {
                    total += frequencies[u];
                }

                transformationFunction[i] = (int)Math.Round(total * l);
            }

            return transformationFunction;
        }

        public static int[,] getEqualized(int[,] mat)
        {
            int mn = mat.GetLength(0) * mat.GetLength(1);
            float[] intensityFrequencies = new float[256];
            int[,] equalizedHistogram = new int[mat.GetLength(0), mat.GetLength(1)];
            ImageProcessor.CopyMat(ref equalizedHistogram, ref mat);
            calculateFrequencies(mn, mat, ref intensityFrequencies);
            buildEqualized(intensityFrequencies, ref equalizedHistogram);
            return equalizedHistogram;
        }
    }
}