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
    public partial class Form2 : Form
    {
        List<NumericUpDown> btns = new List<NumericUpDown>();
        int width, height;
        int minsize = 2;
        int kernelvalue = 0;
        Form1 parentPage;
        public Form2(Form1 f)
        {
            InitializeComponent();
            parentPage = f;
        }
        void CreateNewFilter()
        {
            int j = 0;
            int ct = 0;
            double[,] Mat = new double[kernelvalue, kernelvalue];
            for(int i = 0; i < btns.Count; i++,ct++)
            {
                if (ct == kernelvalue)
                {
                    ct = 0;
                    j++;
                }
            
                Mat[ct, j] = (double)btns[i].Value;
            
            }
            if(textBox3.Text == "")
            {
                textBox3.Text = "DefaultKernel";
            }
           double Multiplier = 1;
            double val;
            if (double.TryParse(textBox1.Text, out val) && val > 0)
            {
                Multiplier = val;
            }
                Filter f = new Filter(Mat,Multiplier,textBox3.Text);
            parentPage.Filters.Add(f.name, f);
            parentPage.PopulateListbox();
            MessageBox.Show("Filter Added");
    }
        void GenerateButtons(int val)
        {
            foreach(NumericUpDown np in btns)
            {
                np.Dispose();
            }
            btns.Clear();
            int scalex = 40;
            int scaley = 20;
            int startx = 100, starty =100;
            int incx = startx;
            int incy = starty;
            for(int i = 0; i < val; i++)
            {
               
                for(int j = 0; j < val; j++)
                {
                    NumericUpDown np = new NumericUpDown();
                    np.Name = "bt" + i + j;
                    np.Parent = this;
                    np.Size = new Size(scalex,scaley);
                    np.Minimum = -999;
                    np.DecimalPlaces = 1;
                    np.Location = new Point(incx, incy);
                    np.Show();
                    btns.Add(np);
                    incx += scalex;
    
                }
                incx = startx;
                incy += scaley ;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CreateNewFilter();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            TextBox recieved = (TextBox)sender;
            int val;
            if (int.TryParse(recieved.Text, out val) && val > 1 && val < 10)
             {
                GenerateButtons(val);
                kernelvalue = val;
            }
        }
    }
}
