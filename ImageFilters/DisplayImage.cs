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
        public DisplayImage(Bitmap image , Point Location , string name)
        {
            InitializeComponent();
            pictureBox1.Image = image;
            this.Show();
            this.Location = Location;
            this.Text = name;
            Width = pictureBox1.Width * 2;
            Height = pictureBox1.Height * 2;
            pictureBox1.Location = new Point((int)(0.5 * pictureBox1.Width), (int)(0.5 * pictureBox1.Height));

        }
        public void UpdateImage(Bitmap image, Point Location)
        { 
           
        pictureBox1.Image = image;
        Width = pictureBox1.Width* 2;
            Height = pictureBox1.Height* 2;
            pictureBox1.Location = new Point((int)(0.5 * pictureBox1.Width), (int)(0.5 * pictureBox1.Height));
            this.Location = Location;
        }
    }
}
