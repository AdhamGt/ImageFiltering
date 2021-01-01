using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageFilters
{
   public class PreviewState
    {
        public int state;
        public Filter filter;
        public bool isColorised;
        public Bitmap ResultImage;
        public Bitmap OriginalImage;
        public Bitmap ColorisedImage;
        public Bitmap GrayImage;
        public int[,] Mat;
        public PreviewState(int s , Filter f , Bitmap OriginalImage,Bitmap bmp , Bitmap color , Bitmap gry, int[,] mat , bool iscol)
        {
            state = s;
            filter = f;
            ResultImage = bmp;
            this.OriginalImage = OriginalImage;
            ColorisedImage = color;
            Mat = mat;
            GrayImage = gry;
            isColorised = iscol;
        }
    }
}
