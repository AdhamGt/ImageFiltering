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
        public int brightness;
        public int contrast;
        public int saturation;

        public PreviewState(int s , Filter f , Bitmap OriginalImage,Bitmap bmp , Bitmap color , Bitmap gry, int[,] mat , bool iscol, int brightness, int contrast, int saturation)
        {
            state = s;
            filter = f;
            ResultImage = bmp;
            this.OriginalImage = OriginalImage;
            ColorisedImage = color;
            Mat = mat;
            GrayImage = gry;
            isColorised = iscol;
            this.brightness = brightness;
            this.contrast = contrast;
            this.saturation = saturation;
        }
    }
}
