using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageFilters
{
    public partial class DisplayImage : Form
    {
        public DisplayImage(Bitmap image, Point Location, string name)
        {
            InitializeComponent();
            pictureBox1.Image = image;
            this.Show();
            this.Location = Location;
            this.Text = name;


            this.Width = image.Width + 15;
            
                Height = image.Height+39;

            pictureBox1.Location = new Point((int)(0), (0));

     
        }

     

        public void UpdateImage(Bitmap image, Point Location)
        {
            this.Location = Location;
            pictureBox1.Image = image;

           Width =  image.Width + 15;
            Height = image.Height + 39;
                pictureBox1.Location = new Point((int)(0),(0));

            
         
        }

        private void DisplayImage_Load(object sender, EventArgs e)
        {

        }
    }
}
