using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {

        public double p1;
        public double p2;
        public double p3;
        fala_sa fala_sa;
        fala_mr fala_mr;
        fala_st fala_st;
        fala_g fala_g;
        public double A;
        public double As;
        public double dz;
        public double r; // ramię wypadkowego ciśnienia liczone od dna
        public double M; //moment wywracający
        public int skala1;
        public int skala2;

        public Form1()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    textBox5.Text = "1";
                    textBox5.Enabled = false;
                    break;
                case 1:
                    textBox5.Enabled = true;
                    break;

            }
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }


        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == ',')
                base.OnKeyPress(e);
            else
                e.Handled = true;
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == ',')
                base.OnKeyPress(e);
            else
                e.Handled = true;
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == ',')
                base.OnKeyPress(e);
            else
                e.Handled = true;
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == ',')
                base.OnKeyPress(e);
            else
                e.Handled = true;
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == ',')
                base.OnKeyPress(e);
            else
                e.Handled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            pictureBox2.Image = Image.FromFile(@"C:\Users\Dom\Documents\Visual Studio 2010\Projects\Praca mgr\Praca mgr\ściana.png");
            pictureBox3.Image = Image.FromFile(@"C:\Users\Dom\Documents\Visual Studio 2010\Projects\Praca mgr\Praca mgr\dno.png");

            Graphics graph;
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            graph = Graphics.FromImage(pictureBox1.Image);
            graph.Clear(Color.White);
            skala1 = 20;  // skala wielkości geometr.
            skala2 = 50;  //skala wielkości ciśnienia

            label29.Text="Wzniesienie poziomu falowania [m]:";

            switch (comboBox1.SelectedIndex)
            {
                case 0: //Sainflou

                    fala_sa = new fala_sa(Convert.ToDouble(textBox1.Text), Convert.ToDouble(textBox2.Text), Convert.ToDouble(textBox4.Text));
                    if (radioButton1.Checked == true)   //szczyt
                    {
                        p1 = 0; //szczyt fali
                        p2 = (fala_sa.H + fala_sa.h0) / (fala_sa.h + fala_sa.h0 + fala_sa.H) * (fala_sa.h + fala_sa.H / Math.Cosh(fala_sa.k * fala_sa.h));  //poziom spokoju
                        p3 = fala_sa.H / Math.Cosh(fala_sa.k * fala_sa.h);  //dno
                        A = 0.5 * p2 * (fala_sa.H + fala_sa.h0) + (p2 + p3) * fala_sa.h * 0.5;
                        graph.DrawLine(Pens.Black, 0, pictureBox1.Height - Convert.ToInt16(fala_sa.h*skala1), pictureBox1.Width, pictureBox1.Height - Convert.ToInt16(fala_sa.h*skala1));
                        graph.DrawLine(Pens.Red, 0, pictureBox1.Height - Convert.ToInt16((fala_sa.h+fala_sa.h0+fala_sa.H) * skala1), Convert.ToInt16(p2)*skala2, pictureBox1.Height - Convert.ToInt16(fala_sa.h * skala1));
                        graph.DrawLine(Pens.Red, Convert.ToInt16(p2) * skala2, pictureBox1.Height - Convert.ToInt16(fala_sa.h * skala1), Convert.ToInt16(p3) * skala2, pictureBox1.Height);


                    }
                    else  //dno
                    {
                        p1 = 0;
                        p2 = -fala_sa.H + fala_sa.h0;
                        p3 = -fala_sa.H / Math.Cosh(fala_sa.k * fala_sa.h);
                        A = 0.5 * p2 * (fala_sa.H - fala_sa.h0) + (p2 + p3) * (fala_sa.h - (fala_sa.H - fala_sa.h0)) * 0.5;

                        graph.DrawLine(Pens.Black, 0, pictureBox1.Height - Convert.ToInt16(fala_sa.h * skala1), pictureBox1.Width, pictureBox1.Height - Convert.ToInt16(fala_sa.h * skala1));
                        graph.DrawLine(Pens.Red, 0, pictureBox1.Height - Convert.ToInt16((fala_sa.h) * skala1), -Convert.ToInt16(p2) * skala2, pictureBox1.Height - Convert.ToInt16((fala_sa.h+fala_sa.h0-fala_sa.H )* skala1));
                        graph.DrawLine(Pens.Red, -Convert.ToInt16(p2) * skala2, pictureBox1.Height - Convert.ToInt16((fala_sa.h + fala_sa.h0 - fala_sa.H) * skala1), -Convert.ToInt16(p3) * skala2, pictureBox1.Height);

                    }
  

                    while (As < A / 2)                                  //położenie środka ciężkości
                    {
                        r = r + fala_sa.h / 20;
                        As = (p3 + (p3 + r * (p2 - p3) / fala_sa.h)) * r * 0.5;
                    
                    }

                    label13.Text = Convert.ToString(Math.Round(fala_sa.h0,2));
                    label14.Text = Convert.ToString(Math.Round(p2,2));
                    label15.Text = Convert.ToString(Math.Round(p3,2));
                    label37.Text = Convert.ToString(Math.Round(A, 2));
                    label38.Text = Convert.ToString(Math.Round(A * r, 2));
                    break;

                case 1: //Miche-Rundgren

                    fala_mr = new fala_mr(Convert.ToDouble(textBox1.Text), Convert.ToDouble(textBox2.Text), Convert.ToDouble(textBox4.Text), Convert.ToDouble(textBox5.Text));
                    if (radioButton1.Checked == true)  //szczyt
                    {
                        p1 = 0;
                        p2 = (fala_mr.H + fala_mr.h0) / (fala_mr.h + fala_mr.h0 + fala_mr.H) * (fala_mr.h + fala_mr.H / Math.Cosh(fala_mr.k * fala_mr.h) + (Math.PI * Math.Pow(fala_mr.H, 2) / (fala_mr.L * Math.Sinh(fala_mr.k * fala_mr.h))) * fala_mr.A3);
                        p3 = fala_mr.H / Math.Cosh(fala_mr.k * fala_mr.h) + (Math.PI * Math.Pow(fala_mr.H, 2) / (fala_mr.L * Math.Sinh(fala_mr.k * fala_mr.h))) * fala_mr.A3;
                        A = 0.5 * p2 * (fala_mr.H + fala_mr.h0) + (p2 + p3) * fala_mr.h * 0.5;

                        graph.DrawLine(Pens.Black, 0, pictureBox1.Height - Convert.ToInt16(fala_mr.h * skala1), pictureBox1.Width, pictureBox1.Height - Convert.ToInt16(fala_mr.h * skala1));
                        graph.DrawLine(Pens.Red, 0, pictureBox1.Height - Convert.ToInt16((fala_mr.h + fala_mr.h0 + fala_mr.H) * skala1), Convert.ToInt16(p2) * skala2, pictureBox1.Height - Convert.ToInt16(fala_mr.h * skala1));
                        graph.DrawLine(Pens.Red, Convert.ToInt16(p2) * skala2, pictureBox1.Height - Convert.ToInt16(fala_mr.h * skala1), Convert.ToInt16(p3) * skala2, pictureBox1.Height);
                    }
                    else     //dno
                    {
                        p1 = 0;
                        p2 = -fala_mr.H + fala_mr.h0;
                        p3 = -fala_mr.H / Math.Cosh(fala_mr.k * fala_mr.h) + Math.PI * Math.Pow(fala_mr.H, 2) / fala_mr.L / Math.Sinh(fala_mr.k * fala_mr.h) * fala_mr.A3;
                        A = 0.5 * p2 * (fala_mr.H - fala_mr.h0) + (p2 + p3) * (fala_mr.h - (fala_mr.H - fala_mr.h0)) * 0.5;

                        graph.DrawLine(Pens.Black, 0, pictureBox1.Height - Convert.ToInt16(fala_mr.h * skala1), pictureBox1.Width, pictureBox1.Height - Convert.ToInt16(fala_mr.h * skala1));
                        graph.DrawLine(Pens.Red, 0, pictureBox1.Height - Convert.ToInt16((fala_mr.h) * skala1), -Convert.ToInt16(p2) * skala2, pictureBox1.Height - Convert.ToInt16((fala_mr.h + fala_mr.h0 -fala_mr.H) * skala1));
                        graph.DrawLine(Pens.Red, -Convert.ToInt16(p2) * skala2, pictureBox1.Height - Convert.ToInt16((fala_mr.h + fala_mr.h0 - fala_mr.H) * skala1), -Convert.ToInt16(p3) * skala2, pictureBox1.Height);
                    }



                    while (As < A / 2)                  //położenie środka ciężkości
                    {
                        r = r + fala_mr.h / 20;
                        As = (p3 + (p3 + r * (p2 - p3) / fala_mr.h)) * r * 0.5;

                    }

                    label13.Text = Convert.ToString(Math.Round(fala_mr.h0,2));
                    label14.Text = Convert.ToString(Math.Round(p2,2));
                    label15.Text = Convert.ToString(Math.Round(p3,2));
                    label37.Text = Convert.ToString(Math.Round(A, 2));
                    label38.Text = Convert.ToString(Math.Round(A * r, 2));
                    label40.Text = Convert.ToString(Math.Round(fala_mr.L));
                    break;

                case 2: //Stokes
                    fala_st = new fala_st(Convert.ToDouble(textBox1.Text), Convert.ToDouble(textBox2.Text), Convert.ToDouble(textBox4.Text), Convert.ToDouble(textBox5.Text));
                    double[] p = new double[9];
                    double[] z = new double[9];
                    Point p_1 = new Point();
                    Point p_2 = new Point();


                    if (radioButton1.Checked == true)  //szczyt
                    {

                        for (int i = 0; i < z.Length; i++)
                        {
                            double a = i;
                            z[i] = -a / (z.Length - 1) * fala_st.h;

                        }
                        for (int j = 0; j < p.Length; j++)
                        {
                            p[j] = fala_st.H * Math.Cosh(fala_st.k * (fala_st.h + z[j])) / Math.Cosh(fala_st.k * fala_st.h) - fala_st.k * Math.Pow(fala_st.H, 2) / 8 * (4 * Math.Tanh(fala_st.k * fala_st.h) + 3 * (Math.Pow(Math.Tanh(fala_st.k * fala_st.h), 4) - 1) / Math.Pow(Math.Tanh(fala_st.k * fala_st.h), 3) * Math.Cosh(2 * fala_st.k * (fala_st.h + z[j])) / (Math.Cosh(2 * fala_st.k * fala_st.h))); //57
                        }

                        A = (fala_st.H + fala_st.h0) * p[0] * 0.5;   //pole powierzchni rozkładu ciśnienia

                        for (int k = 0; k < z.Length - 1; k++)
                        {
                            dz = Math.Abs(z[1] - z[2]);
                            A = A + dz * (p[k] + p[k + 1]) / 2;
                        }

                        while (As < A / 2)                      //położenie środka ciężkości
                        {
                            for (int l = z.Length - 1; l > 0; l--)
                            {
                                dz = Math.Abs(z[1] - z[2]);
                                As = As + dz * (p[l - 1] + p[l]) / 2;
                                r = r + dz;
                            }

                        }
                            
                        

                        for (int m=0;m<z.Length-1; m++)
                        {

                            

                            p_1.Y = pictureBox1.Height - skala1*Convert.ToInt16(fala_st.h+z[m]);
                            p_1.X = Convert.ToInt16(skala2 * p[m]);
                            p_2.Y = pictureBox1.Height - skala1 * Convert.ToInt16(fala_st.h + z[m+1]);
                            p_2.X = Convert.ToInt16(skala2 * p[m + 1]);

                            graph.DrawLine(Pens.Red, p_1, p_2);
                        }

                        graph.DrawLine(Pens.Black, 0, pictureBox1.Height - Convert.ToInt16(fala_st.h * skala1), pictureBox1.Width, pictureBox1.Height - Convert.ToInt16(fala_st.h * skala1));
                        graph.DrawLine(Pens.Red, 0, pictureBox1.Height - Convert.ToInt16((fala_st.h + fala_st.h0 + fala_st.H) * skala1), Convert.ToInt16(p[0]* skala2) , pictureBox1.Height - Convert.ToInt16(fala_st.h * skala1));
                        //graph.DrawLine(Pens.Red, Convert.ToInt16(p2) * skala2, pictureBox1.Height - Convert.ToInt16(fala_mr.h * skala1), Convert.ToInt16(p3) * skala2, pictureBox1.Height);
                    }
                    else   //dno
                    {
                        for (int i = 0; i < z.Length; i++)
                        {
                            double a = i;
                            z[i] = -a / (z.Length-1) * (fala_st.h+fala_st.h0-fala_st.H);
                            //z[i] = -(a / (z.Length - 1) * (fala_st.h + (fala_st.H - fala_st.h0)) - (fala_st.H - fala_st.h0));

                        }
                        for (int j = 0; j < p.Length; j++)
                        {
                            p[j] = -(fala_st.H * Math.Cosh(fala_st.k * (fala_st.h + z[j])) / Math.Cosh(fala_st.k * fala_st.h)) - fala_st.k * Math.Pow(fala_st.H, 2) / 8 * (4 * Math.Tanh(fala_st.k * fala_st.h) + 3 * (Math.Pow(Math.Tanh(fala_st.k * fala_st.h), 4) - 1) / Math.Pow(Math.Tanh(fala_st.k * fala_st.h), 3) * Math.Cosh(2 * fala_st.k * (fala_st.h + z[j])) / (Math.Cosh(2 * fala_st.k * fala_st.h))); //57                                
                        }

                        A = (fala_st.H - fala_st.h0) * (fala_st.H - fala_st.h0) * 0.5;   //pole powierzchni rozkładu ciśnienia

                        for (int k = 0; k < p.Length - 1; k++)
                        {
                            dz = Math.Abs(z[1] - z[2]);
                            A = A + dz * (p[k] + p[k + 1]) / 2;
                        }



                        for (int m = 0; m < z.Length - 1; m++)  //rysowanie krzywej 
                        {

                            p_1.Y = pictureBox1.Height -  Convert.ToInt16(skala1 *(fala_st.h+fala_st.h0-fala_st.H+ z[m]));
                            p_1.X = -Convert.ToInt16(skala2 * p[m]);
                            p_2.Y = pictureBox1.Height - Convert.ToInt16(skala1 * (fala_st.h + fala_st.h0-fala_st.H + z[m + 1]));
                            p_2.X = -Convert.ToInt16(skala2 * p[m + 1]);

                            graph.DrawLine(Pens.Red, p_1, p_2);
                        }

                        graph.DrawLine(Pens.Black, 0, pictureBox1.Height - Convert.ToInt16(fala_st.h * skala1), pictureBox1.Width, pictureBox1.Height - Convert.ToInt16((fala_st.h) * skala1));
                        graph.DrawLine(Pens.Red, 0, pictureBox1.Height - Convert.ToInt16((fala_st.h) * skala1), -Convert.ToInt16(p[0] * skala2), pictureBox1.Height - Convert.ToInt16((fala_st.h + fala_st.h0 - fala_st.H) * skala1));

                 
                        
                        while (As < A / 2)                      //położenie środka ciężkości
                        {
                            for (int l = z.Length - 1; l > 0; l--)
                            {
                                dz = Math.Abs(z[1] - z[2]);
                                As = As + dz * (p[l - 1] + p[l]) / 2;
                                r = r + dz;
                            }
                        }

                    }


                        label13.Text = Convert.ToString(Math.Round(fala_st.h0,2));
                        label14.Text = Convert.ToString(Math.Round(p[0],2));
                        label15.Text = Convert.ToString(Math.Round(p[8], 2));
                        label37.Text = Convert.ToString(Math.Round(A, 2));
                        label38.Text = Convert.ToString(Math.Round(A*r, 2));

                        break;



                case 3:    //Goda
                        fala_g = new fala_g(Convert.ToDouble(textBox1.Text), Convert.ToDouble(textBox2.Text), Convert.ToDouble(textBox4.Text));
                        p1 = 0.5 * (1 + 1) * (fala_g.a1 + fala_g.a2) * fala_g.H;
                        p2 = fala_g.a3 * p1;
                        graph.DrawLine(Pens.Black, 0, pictureBox1.Height - Convert.ToInt16(fala_g.h * skala1), pictureBox1.Width, pictureBox1.Height - Convert.ToInt16(fala_g.h * skala1));
                        graph.DrawLine(Pens.Red, 0, pictureBox1.Height - Convert.ToInt16((fala_g.h + fala_g.H) * skala1), Convert.ToInt16(p1) * skala2, pictureBox1.Height - Convert.ToInt16(fala_g.h * skala1));
                        graph.DrawLine(Pens.Red, Convert.ToInt16(p1) * skala2, pictureBox1.Height - Convert.ToInt16(fala_g.h * skala1), Convert.ToInt16(p2) * skala2, pictureBox1.Height);

                        A = 0.5 * p1 * (fala_g.H) + (p1 + p2) * fala_g.h * 0.5;

                        while (As < A / 2)                  //położenie środka ciężkości
                        {
                            r = r + fala_g.h / 20;
                            As = (p2 + (p2 + r * (p1 - p2) / fala_g.h)) * r * 0.5;

                        }

                        
                        label14.Text = Convert.ToString(Math.Round(p1,2));
                        label15.Text = Convert.ToString(Math.Round(p2,2));
                        label37.Text = Convert.ToString(Math.Round(A, 2));
                        label38.Text = Convert.ToString(Math.Round(A * r, 2));
                        label40.Text = Convert.ToString(Math.Round(fala_g.L));

                        break;
                   
            }

                        if (radioButton1.Checked == true)
                        {
                            label34.Text = "Wysokość ciśnienia w poziomie spokoju [m]:";
                        }
                        else
                        {
                            label34.Text = "Wysokość ciśnienia w poziomie dna fali [m]:";
                        }
                        label35.Text = "Wysokość ciśnienia w poziomie dna [m]:";
                        label36.Text = "Wysokość ciśnienia całkowitego []:";
                        label39.Text = "Moment wywracający:";
            
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox4.SelectedIndex)
            {
                case 0:
                    textBox9.Enabled = true;
                    textBox11.Enabled = true;
                    textBox12.Enabled = false;
                    label25.Text = "[m]";
                    label28.Text = "[m]";
                    break;


                case 1:
                    textBox9.Enabled = true;
                    textBox11.Enabled = false;
                    textBox12.Enabled = true;
                    label25.Text = "[m]";
                    label28.Text = "[m]";
                    break;


                case 2:
                    textBox9.Enabled = false;
                    textBox11.Enabled = true;
                    textBox12.Enabled = true;
                    label25.Text = "[s]";
                    label28.Text = "[s]";
                    break;
            }


        }

        private void textBox12_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == ',')
                base.OnKeyPress(e);
            else
                e.Handled = true;
        }

        private void textBox11_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == ',')
                base.OnKeyPress(e);
            else
                e.Handled = true;
        }

        private void textBox9_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == ',')
                base.OnKeyPress(e);
            else
                e.Handled = true;
        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == ',')
                base.OnKeyPress(e);
            else
                e.Handled = true;
        }

        private void textBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == ',')
                base.OnKeyPress(e);
            else
                e.Handled = true;
        }



        private void button2_Click(object sender, EventArgs e)   //analiza parametryczna
        {

            foreach (var series in chart1.Series)
            {
                series.Points.Clear();
            }
            switch (comboBox2.SelectedIndex)
            {
                case 0:
                    chart1.Series[0].LegendText = "Sainflou";
                    break;
                case 1:
                    chart1.Series[0].LegendText = "Miche-Rundgren";
                    break;
                case 2:
                    chart1.Series[0].LegendText = "Stokes (2. rząd)";
                    break;
                case 3:
                    chart1.Series[0].LegendText = "Goda";
                    break;

            }

            switch (comboBox3.SelectedIndex)
            {
                case 0:
                    chart1.Series[1].LegendText = "Sainflou";
                    break;
                case 1:
                    chart1.Series[1].LegendText = "Miche-Rundgren";
                    break;
                case 2:
                    chart1.Series[1].LegendText = "Stokes (2. rząd)";
                    break;
                case 3:
                    chart1.Series[1].LegendText = "Goda";
                    break;
            }


            switch (comboBox4.SelectedIndex)
            {
                case 0:
                    chart1.ChartAreas[0].AxisX.Title = "głębokość wody [m]";
                    break;
                case 1:
                    chart1.ChartAreas[0].AxisX.Title = "wysokość fali podchodzącej [m]";
                    break;
                case 2:
                    chart1.ChartAreas[0].AxisX.Title = "okres fali [s]";
                    break;
            }

            chart1.ChartAreas[0].AxisY.Title = "wysokość ciśnieniea hydrodynamicznego [m]";

            chart1.Images.Clear();

            switch (comboBox2.SelectedIndex)       /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            {
                case 0: //Sainflou


                    if (radioButton4.Checked == true)   //szczyt
                    {
                                     
                        switch (comboBox4.SelectedIndex)
                        {
                            case 0:            // h zmienne
                                
                                double dh;
                                

                                fala_sa = new fala_sa(Convert.ToDouble(textBox6.Text), Convert.ToDouble(textBox11.Text), Convert.ToDouble(textBox9.Text));                       
                                
                                for (int i = 0; i <= 19; i++)
                                {
                                    
                                    dh = (Convert.ToDouble(textBox7.Text) - Convert.ToDouble(textBox6.Text)) / 20;
                                    p1 = 0;     //szczyt fali
                                    p2 = (fala_sa.H + fala_sa.h0) / (fala_sa.h + fala_sa.h0 + fala_sa.H) * (fala_sa.h + fala_sa.H / Math.Cosh(fala_sa.k * fala_sa.h));  //poziom spokoju
                                    p3 = fala_sa.H / Math.Cosh(fala_sa.k * fala_sa.h);      //dno
                                    A = 0.5 * p2 * (fala_sa.H + fala_sa.h0) + (p2 + p3) * fala_sa.h * 0.5;
                                    chart1.Series[0].Points.AddXY(fala_sa.h, A);
                                    fala_sa.h = fala_sa.h + dh;
                                    fala_sa.przelicz(fala_sa.h, fala_sa.H, fala_sa.T);
                                    
                                }
                                fala_sa.h = Convert.ToDouble(textBox12.Text);
                                fala_sa.przelicz(fala_sa.h, fala_sa.H, fala_sa.T);
                                break;


                            case 1:           //H zmienne

                                double dH;
                                
                                fala_sa = new fala_sa(Convert.ToDouble(textBox12.Text), Convert.ToDouble(textBox6.Text), Convert.ToDouble(textBox9.Text));                       
                                
                                for (int i = 0; i <= 19; i++)
                                {
                                    
                                    dH = (Convert.ToDouble(textBox7.Text) - Convert.ToDouble(textBox6.Text)) / 20;
                                    p1 = 0;     //szczyt fali
                                    p2 = (fala_sa.H + fala_sa.h0) / (fala_sa.h + fala_sa.h0 + fala_sa.H) * (fala_sa.h + fala_sa.H / Math.Cosh(fala_sa.k * fala_sa.h));  //poziom spokoju
                                    p3 = fala_sa.H / Math.Cosh(fala_sa.k * fala_sa.h);      //dno
                                    A = 0.5 * p2 * (fala_sa.H + fala_sa.h0) + (p2 + p3) * fala_sa.h * 0.5;
                                    chart1.Series[0].Points.AddXY(fala_sa.H, A);
                                    fala_sa.H = fala_sa.H + dH;
                                    fala_sa.przelicz(fala_sa.h, fala_sa.H, fala_sa.T);
                                    
                                }
                                
                                fala_sa.H = Convert.ToDouble(textBox11.Text);
                                fala_sa.przelicz(fala_sa.h, fala_sa.H, fala_sa.T);
                                break;

                            case 2:   //T zmienne


                                double dT;
                                
                                fala_sa = new fala_sa(Convert.ToDouble(textBox12.Text), Convert.ToDouble(textBox11.Text), Convert.ToDouble(textBox6.Text));                       
                                
                                for (int i = 0; i <= 19; i++)
                                {
                                    
                                    dT = (Convert.ToDouble(textBox7.Text) - Convert.ToDouble(textBox6.Text)) / 20;
                                    p1 = 0;     //szczyt fali
                                    p2 = (fala_sa.H + fala_sa.h0) / (fala_sa.h + fala_sa.h0 + fala_sa.H) * (fala_sa.h + fala_sa.H / Math.Cosh(fala_sa.k * fala_sa.h));  //poziom spokoju
                                    p3 = fala_sa.H / Math.Cosh(fala_sa.k * fala_sa.h);      //dno
                                    A = 0.5 * p2 * (fala_sa.H + fala_sa.h0) + (p2 + p3) * fala_sa.h * 0.5;
                                    chart1.Series[0].Points.AddXY(fala_sa.T, A);
                                    fala_sa.T = fala_sa.T + dT;
                                    fala_sa.przelicz(fala_sa.h,fala_sa.H, fala_sa.T);
                                    
                                }
                                fala_sa.T = Convert.ToDouble(textBox9.Text);
                                fala_sa.przelicz(fala_sa.h, fala_sa.H, fala_sa.T);                                

                                break;
                        }
                    



         
                    }
                    else  //dno
                    {
                        switch (comboBox4.SelectedIndex)
                        {
                            case 0:            // h zmienne

                                double dh;

                                fala_sa = new fala_sa(Convert.ToDouble(textBox6.Text), Convert.ToDouble(textBox11.Text), Convert.ToDouble(textBox9.Text));

                                for (int i = 0; i <= 19; i++)
                                {

                                    dh = (Convert.ToDouble(textBox7.Text) - Convert.ToDouble(textBox6.Text)) / 20;

                                    p1 = 0;
                                    p2 = -fala_sa.H + fala_sa.h0;
                                    p3 = -fala_sa.H / Math.Cosh(fala_sa.k * fala_sa.h);
                                    A = 0.5 * p2 * (fala_sa.H - fala_sa.h0) + (p2 + p3) * (fala_sa.h - (fala_sa.H - fala_sa.h0)) * 0.5;
                                    chart1.Series[0].Points.AddXY(fala_sa.h, A);
                                    fala_sa.h = fala_sa.h + dh;
                                    fala_sa.przelicz(fala_sa.h, fala_sa.H, fala_sa.T);

                                }

                                break;


                            case 1:           //H zmienne

                                double dH;

                                fala_sa = new fala_sa(Convert.ToDouble(textBox12.Text), Convert.ToDouble(textBox6.Text), Convert.ToDouble(textBox9.Text));

                                for (int i = 0; i <= 19; i++)
                                {

                                    dH = (Convert.ToDouble(textBox7.Text) - Convert.ToDouble(textBox6.Text)) / 20;
                                    p1 = 0;
                                    p2 = -fala_sa.H + fala_sa.h0;
                                    p3 = -fala_sa.H / Math.Cosh(fala_sa.k * fala_sa.h);
                                    A = 0.5 * p2 * (fala_sa.H - fala_sa.h0) + (p2 + p3) * (fala_sa.h - (fala_sa.H - fala_sa.h0)) * 0.5;
                                    chart1.Series[0].Points.AddXY(fala_sa.H, A);
                                    fala_sa.H = fala_sa.H + dH;
                                    fala_sa.przelicz(fala_sa.h, fala_sa.H, fala_sa.T);

                                }
                                break;


                            case 2:   //T zmienne


                                double dT;

                                fala_sa = new fala_sa(Convert.ToDouble(textBox12.Text), Convert.ToDouble(textBox11.Text), Convert.ToDouble(textBox6.Text));

                                for (int i = 0; i <= 19; i++)
                                {

                                    dT = (Convert.ToDouble(textBox7.Text) - Convert.ToDouble(textBox6.Text)) / 20;
                                    p1 = 0;
                                    p2 = -fala_sa.H + fala_sa.h0;
                                    p3 = -fala_sa.H / Math.Cosh(fala_sa.k * fala_sa.h);
                                    A = 0.5 * p2 * (fala_sa.H - fala_sa.h0) + (p2 + p3) * (fala_sa.h - (fala_sa.H - fala_sa.h0)) * 0.5;
                                    chart1.Series[0].Points.AddXY(fala_sa.T, A);
                                    fala_sa.T = fala_sa.T + dT;
                                    fala_sa.przelicz(fala_sa.h, fala_sa.H, fala_sa.T);

                                }


                                break;
                        }


                    }






                    while (As < A / 2)                                  //położenie środka ciężkości
                    {
                        r = r + fala_sa.h / 20;
                        As = (p3 + (p3 + r * (p2 - p3) / fala_sa.h)) * r * 0.5;

                    }

           
                    break;


                case 1:  //Miche-Rundgren

                    if (radioButton4.Checked == true)   //szczyt
                    {
                                     
                        switch (comboBox4.SelectedIndex)
                        {
                            case 0:            // h zmienne
                                
                                double dh;
                                

                                fala_mr = new fala_mr(Convert.ToDouble(textBox6.Text), Convert.ToDouble(textBox11.Text), Convert.ToDouble(textBox9.Text), Convert.ToDouble(textBox3.Text));                       
                                
                                for (int i = 0; i <= 19; i++)
                                {
                                    
                                    dh = (Convert.ToDouble(textBox7.Text) - Convert.ToDouble(textBox6.Text)) / 20;
                                    p1 = 0;
                                    p2 = (fala_mr.H + fala_mr.h0) / (fala_mr.h + fala_mr.h0 + fala_mr.H) * (fala_mr.h + fala_mr.H / Math.Cosh(fala_mr.k * fala_mr.h) + (Math.PI * Math.Pow(fala_mr.H, 2) / (fala_mr.L * Math.Sinh(fala_mr.k * fala_mr.h))) * fala_mr.A3);
                                    p3 = fala_mr.H / Math.Cosh(fala_mr.k * fala_mr.h) + (Math.PI * Math.Pow(fala_mr.H, 2) / (fala_mr.L * Math.Sinh(fala_mr.k * fala_mr.h))) * fala_mr.A3;
                                    A = 0.5 * p2 * (fala_mr.H + fala_mr.h0) + (p2 + p3) * fala_mr.h * 0.5;
                                    chart1.Series[0].Points.AddXY(fala_mr.h, A);
                                    fala_mr.h = fala_mr.h + dh;
                                    fala_mr.przelicz(fala_mr.h, fala_mr.H, fala_mr.T);
                                    
                                }

                                break;


                            case 1:           //H zmienne

                                double dH;
                                
                                fala_mr = new fala_mr(Convert.ToDouble(textBox12.Text), Convert.ToDouble(textBox6.Text), Convert.ToDouble(textBox9.Text), Convert.ToDouble(textBox3.Text));                       
                                
                                for (int i = 0; i <= 19; i++)
                                {
                                    
                                    dH = (Convert.ToDouble(textBox7.Text) - Convert.ToDouble(textBox6.Text)) / 20;
                                    p1 = 0;
                                    p2 = (fala_mr.H + fala_mr.h0) / (fala_mr.h + fala_mr.h0 + fala_mr.H) * (fala_mr.h + fala_mr.H / Math.Cosh(fala_mr.k * fala_mr.h) + (Math.PI * Math.Pow(fala_mr.H, 2) / (fala_mr.L * Math.Sinh(fala_mr.k * fala_mr.h))) * fala_mr.A3);
                                    p3 = fala_mr.H / Math.Cosh(fala_mr.k * fala_mr.h) + (Math.PI * Math.Pow(fala_mr.H, 2) / (fala_mr.L * Math.Sinh(fala_mr.k * fala_mr.h))) * fala_mr.A3;
                                    A = 0.5 * p2 * (fala_mr.H + fala_mr.h0) + (p2 + p3) * fala_mr.h * 0.5;
                                    chart1.Series[0].Points.AddXY(fala_mr.H, A);
                                    fala_mr.H = fala_mr.H + dH;
                                    fala_mr.przelicz(fala_mr.h, fala_mr.H, fala_mr.T);
                                    
                                }
                                
 
                                break;

                            case 2:   //T zmienne


                                double dT;
                                
                                fala_mr = new fala_mr(Convert.ToDouble(textBox12.Text), Convert.ToDouble(textBox11.Text), Convert.ToDouble(textBox6.Text), Convert.ToDouble(textBox3.Text));                       
                                
                                for (int i = 0; i <= 19; i++)
                                {
                                    
                                    dT = (Convert.ToDouble(textBox7.Text) - Convert.ToDouble(textBox6.Text)) / 20;
                                    p1 = 0;
                                    p2 = (fala_mr.H + fala_mr.h0) / (fala_mr.h + fala_mr.h0 + fala_mr.H) * (fala_mr.h + fala_mr.H / Math.Cosh(fala_mr.k * fala_mr.h) + (Math.PI * Math.Pow(fala_mr.H, 2) / (fala_mr.L * Math.Sinh(fala_mr.k * fala_mr.h))) * fala_mr.A3);
                                    p3 = fala_mr.H / Math.Cosh(fala_mr.k * fala_mr.h) + (Math.PI * Math.Pow(fala_mr.H, 2) / (fala_mr.L * Math.Sinh(fala_mr.k * fala_mr.h))) * fala_mr.A3;
                                    A = 0.5 * p2 * (fala_mr.H + fala_mr.h0) + (p2 + p3) * fala_mr.h * 0.5;
                                    chart1.Series[0].Points.AddXY(fala_mr.T, A);
                                    fala_mr.T = fala_mr.T + dT;
                                    fala_mr.przelicz(fala_mr.h,fala_mr.H, fala_mr.T);
                                    
                                }
                                

                                break;
                        }
                    



         
                    }
                    else  //dno
                    {
                        switch (comboBox4.SelectedIndex)
                        {
                            case 0:            // h zmienne

                                double dh;

                                fala_mr = new fala_mr(Convert.ToDouble(textBox6.Text), Convert.ToDouble(textBox11.Text), Convert.ToDouble(textBox9.Text), Convert.ToDouble(textBox3.Text));

                                for (int i = 0; i <= 19; i++)
                                {

                                    dh = (Convert.ToDouble(textBox7.Text) - Convert.ToDouble(textBox6.Text)) / 20;

                                    p1 = 0;
                                    p2 = -fala_mr.H + fala_mr.h0;
                                    p3 = -fala_mr.H / Math.Cosh(fala_mr.k * fala_mr.h) + Math.PI * Math.Pow(fala_mr.H, 2) / fala_mr.L / Math.Sinh(fala_mr.k * fala_mr.h) * fala_mr.A3;
                                    A = 0.5 * p2 * (fala_mr.H - fala_mr.h0) + (p2 + p3) * (fala_mr.h - (fala_mr.H - fala_mr.h0)) * 0.5;
                                    chart1.Series[0].Points.AddXY(fala_mr.h, A);
                                    fala_mr.h = fala_mr.h + dh;
                                    fala_mr.przelicz(fala_mr.h, fala_mr.H, fala_mr.T);

                                }

                                break;


                            case 1:           //H zmienne

                                double dH;

                                fala_mr = new fala_mr(Convert.ToDouble(textBox12.Text), Convert.ToDouble(textBox6.Text), Convert.ToDouble(textBox9.Text), Convert.ToDouble(textBox3.Text));

                                for (int i = 0; i <= 19; i++)
                                {

                                    dH = (Convert.ToDouble(textBox7.Text) - Convert.ToDouble(textBox6.Text)) / 20;
                                    p1 = 0;
                                    p2 = -fala_mr.H + fala_mr.h0;
                                    p3 = -fala_mr.H / Math.Cosh(fala_mr.k * fala_mr.h) + Math.PI * Math.Pow(fala_mr.H, 2) / fala_mr.L / Math.Sinh(fala_mr.k * fala_mr.h) * fala_mr.A3;
                                    A = 0.5 * p2 * (fala_mr.H - fala_mr.h0) + (p2 + p3) * (fala_mr.h - (fala_mr.H - fala_mr.h0)) * 0.5;
                                    chart1.Series[0].Points.AddXY(fala_mr.H, A);
                                    fala_mr.H = fala_mr.H + dH;
                                    fala_mr.przelicz(fala_mr.h, fala_mr.H, fala_mr.T);

                                }
                                break;


                            case 2:   //T zmienne


                                double dT;

                                fala_mr = new fala_mr(Convert.ToDouble(textBox12.Text), Convert.ToDouble(textBox11.Text), Convert.ToDouble(textBox6.Text), Convert.ToDouble(textBox3.Text));

                                for (int i = 0; i <= 19; i++)
                                {

                                    dT = (Convert.ToDouble(textBox7.Text) - Convert.ToDouble(textBox6.Text)) / 20;
                                    p1 = 0;
                                    p2 = -fala_mr.H + fala_mr.h0;
                                    p3 = -fala_mr.H / Math.Cosh(fala_mr.k * fala_mr.h) + Math.PI * Math.Pow(fala_mr.H, 2) / fala_mr.L / Math.Sinh(fala_mr.k * fala_mr.h) * fala_mr.A3;
                                    A = 0.5 * p2 * (fala_mr.H - fala_mr.h0) + (p2 + p3) * (fala_mr.h - (fala_mr.H - fala_mr.h0)) * 0.5;
                                    chart1.Series[0].Points.AddXY(fala_mr.T, A);
                                    fala_mr.T = fala_mr.T + dT;
                                    fala_mr.przelicz(fala_mr.h, fala_mr.H, fala_mr.T);

                                }


                                break;
                        }


                    }






                    while (As < A / 2)                                  //położenie środka ciężkości
                    {
                        r = r + fala_mr.h / 20;
                        As = (p3 + (p3 + r * (p2 - p3) / fala_mr.h)) * r * 0.5;

                    }

           
                    break;

                case 2: //Stokes

                   double[] p = new double[9];
                   double[] z = new double[9]; 

                   if (radioButton4.Checked == true)   //szczyt
                    {
                                     
                        switch (comboBox4.SelectedIndex)
                        {
                            case 0:            // h zmienne
                                
                                double dh;
                                

                                fala_st = new fala_st(Convert.ToDouble(textBox6.Text), Convert.ToDouble(textBox2.Text), Convert.ToDouble(textBox4.Text), Convert.ToDouble(textBox5.Text));
                      
                                
                                for (int i = 0; i <= 19; i++)
                                {
                                    
                                    dh = (Convert.ToDouble(textBox7.Text) - Convert.ToDouble(textBox6.Text)) / 20;
                                   

                                    for (int k = 0; k < z.Length; k++)
                                        {
                                            double a = k;
                                            z[k] = -a / (z.Length - 1) * fala_st.h;

                                        }
                                        for (int j = 0; j < p.Length; j++)
                                        {
                                            p[j] = fala_st.H * Math.Cosh(fala_st.k * (fala_st.h + z[j])) / Math.Cosh(fala_st.k * fala_st.h) - fala_st.k * Math.Pow(fala_st.H, 2) / 8 * (4 * Math.Tanh(fala_st.k * fala_st.h) + 3 * (Math.Pow(Math.Tanh(fala_st.k * fala_st.h), 4) - 1) / Math.Pow(Math.Tanh(fala_st.k * fala_st.h), 3) * Math.Cosh(2 * fala_st.k * (fala_st.h + z[j])) / (Math.Cosh(2 * fala_st.k * fala_st.h))); //57
                                        }

                                        A = (fala_st.H + fala_st.h0) * p[0] * 0.5;   //pole powierzchni rozkładu ciśnienia

                                        for (int k = 0; k < z.Length - 1; k++)
                                        {
                                            dz = Math.Abs(z[1] - z[2]);
                                            A = A + dz * (p[k] + p[k + 1]) / 2;
                                        }



                                    chart1.Series[0].Points.AddXY(fala_st.h, A);
                                    fala_st.h = fala_st.h + dh;
                                    fala_st.przelicz(fala_st.h, fala_st.H, fala_st.T);
                                    
                                }
                                //
                                break;


                            case 1:           //H zmienne

                                double dH;
                                
                                fala_st = new fala_st(Convert.ToDouble(textBox12.Text), Convert.ToDouble(textBox6.Text), Convert.ToDouble(textBox4.Text), Convert.ToDouble(textBox5.Text));
 
                                for (int i = 0; i <= 19; i++)
                                {
                                    
                                    dH = (Convert.ToDouble(textBox7.Text) - Convert.ToDouble(textBox6.Text)) / 20;
                                    for (int k = 0; k < z.Length; k++)
                                    {
                                        double a = k;
                                        z[k] = -a / (z.Length - 1) * fala_st.h;

                                    }
                                    for (int j = 0; j < p.Length; j++)
                                    {
                                        p[j] = fala_st.H * Math.Cosh(fala_st.k * (fala_st.h + z[j])) / Math.Cosh(fala_st.k * fala_st.h) - fala_st.k * Math.Pow(fala_st.H, 2) / 8 * (4 * Math.Tanh(fala_st.k * fala_st.h) + 3 * (Math.Pow(Math.Tanh(fala_st.k * fala_st.h), 4) - 1) / Math.Pow(Math.Tanh(fala_st.k * fala_st.h), 3) * Math.Cosh(2 * fala_st.k * (fala_st.h + z[j])) / (Math.Cosh(2 * fala_st.k * fala_st.h))); //57
                                    }

                                    A = (fala_st.H + fala_st.h0) * p[0] * 0.5;   //pole powierzchni rozkładu ciśnienia

                                    for (int k = 0; k < z.Length - 1; k++)
                                    {
                                        dz = Math.Abs(z[1] - z[2]);
                                        A = A + dz * (p[k] + p[k + 1]) / 2;
                                    }
                                    chart1.Series[0].Points.AddXY(fala_st.H, A);
                                    fala_st.H = fala_st.H + dH;
                                    fala_st.przelicz(fala_st.h, fala_st.H, fala_st.T);
                                    
                                }
                                

                                break;

                            case 2:   //T zmienne


                                double dT;
                                
                                fala_st = new fala_st(Convert.ToDouble(textBox12.Text), Convert.ToDouble(textBox11.Text), Convert.ToDouble(textBox6.Text), Convert.ToDouble(textBox3.Text));                       
                                
                                for (int i = 0; i <= 19; i++)
                                {
                                    
                                    dT = (Convert.ToDouble(textBox7.Text) - Convert.ToDouble(textBox6.Text)) / 20;
                                    for (int k = 0; k < z.Length; k++)
                                    {
                                        double a = k;
                                        z[k] = -a / (z.Length - 1) * fala_st.h;

                                    }
                                    for (int j = 0; j < p.Length; j++)
                                    {
                                        p[j] = fala_st.H * Math.Cosh(fala_st.k * (fala_st.h + z[j])) / Math.Cosh(fala_st.k * fala_st.h) - fala_st.k * Math.Pow(fala_st.H, 2) / 8 * (4 * Math.Tanh(fala_st.k * fala_st.h) + 3 * (Math.Pow(Math.Tanh(fala_st.k * fala_st.h), 4) - 1) / Math.Pow(Math.Tanh(fala_st.k * fala_st.h), 3) * Math.Cosh(2 * fala_st.k * (fala_st.h + z[j])) / (Math.Cosh(2 * fala_st.k * fala_st.h))); //57
                                    }

                                    A = (fala_st.H + fala_st.h0) * p[0] * 0.5;   //pole powierzchni rozkładu ciśnienia

                                    for (int k = 0; k < z.Length - 1; k++)
                                    {
                                        dz = Math.Abs(z[1] - z[2]);
                                        A = A + dz * (p[k] + p[k + 1]) / 2;
                                    }
                                    chart1.Series[0].Points.AddXY(fala_st.T, A);
                                    fala_st.T = fala_st.T + dT;
                                    fala_st.przelicz(fala_st.h,fala_st.H, fala_st.T);
                                    
                                }
                           

                                break;
                        }
                    



         
                    }
                    else  //dno
                    {
                        switch (comboBox4.SelectedIndex)
                        {
                            case 0:            // h zmienne

                                double dh;

                                fala_st = new fala_st(Convert.ToDouble(textBox6.Text), Convert.ToDouble(textBox11.Text), Convert.ToDouble(textBox9.Text), Convert.ToDouble(textBox3.Text));

                                for (int i = 0; i <= 19; i++)
                                {

                                    dh = (Convert.ToDouble(textBox7.Text) - Convert.ToDouble(textBox6.Text)) / 20;

                                    for (int k = 0; k < z.Length; k++)
                                    {
                                        double a = k;
                                        z[k] = -(a / (z.Length - 1) * (fala_st.h + (fala_st.H - fala_st.h0)) - (fala_st.H - fala_st.h0));

                                    }
                                    for (int j = 0; j < p.Length; j++)
                                    {
                                        p[j] = -(fala_st.H * Math.Cosh(fala_st.k * (fala_st.h + z[j])) / Math.Cosh(fala_st.k * fala_st.h)) - fala_st.k * Math.Pow(fala_st.H, 2) / 8 * (4 * Math.Tanh(fala_st.k * fala_st.h) + 3 * (Math.Pow(Math.Tanh(fala_st.k * fala_st.h), 4) - 1) / Math.Pow(Math.Tanh(fala_st.k * fala_st.h), 3) * Math.Cosh(2 * fala_st.k * (fala_st.h + z[j])) / (Math.Cosh(2 * fala_st.k * fala_st.h))); //57                                
                                    }

                                    A = (fala_st.H - fala_st.h0) * (fala_st.H - fala_st.h0) * 0.5;   //pole powierzchni rozkładu ciśnienia

                                    for (int k = 0; k < p.Length - 1; k++)
                                    {
                                        dz = Math.Abs(z[1] - z[2]);
                                        A = A + dz * (p[k] + p[k + 1]) / 2;
                                    }

                                    chart1.Series[0].Points.AddXY(fala_st.h, A);
                                    fala_st.h = fala_st.h + dh;
                                    fala_st.przelicz(fala_st.h, fala_st.H, fala_st.T);

                                }

                                break;


                            case 1:           //H zmienne

                                double dH;

                                fala_st = new fala_st(Convert.ToDouble(textBox12.Text), Convert.ToDouble(textBox6.Text), Convert.ToDouble(textBox9.Text), Convert.ToDouble(textBox3.Text));

                                for (int i = 0; i <= 19; i++)
                                {

                                    dH = (Convert.ToDouble(textBox7.Text) - Convert.ToDouble(textBox6.Text)) / 20;
                                    for (int k = 0; k < z.Length; k++)
                                    {
                                        double a = k;
                                        z[k] = -(a / (z.Length - 1) * (fala_st.h + (fala_st.H - fala_st.h0)) - (fala_st.H - fala_st.h0));

                                    }
                                    for (int j = 0; j < p.Length; j++)
                                    {
                                        p[j] = -(fala_st.H * Math.Cosh(fala_st.k * (fala_st.h + z[j])) / Math.Cosh(fala_st.k * fala_st.h)) - fala_st.k * Math.Pow(fala_st.H, 2) / 8 * (4 * Math.Tanh(fala_st.k * fala_st.h) + 3 * (Math.Pow(Math.Tanh(fala_st.k * fala_st.h), 4) - 1) / Math.Pow(Math.Tanh(fala_st.k * fala_st.h), 3) * Math.Cosh(2 * fala_st.k * (fala_st.h + z[j])) / (Math.Cosh(2 * fala_st.k * fala_st.h))); //57                                
                                    }

                                    A = (fala_st.H - fala_st.h0) * (fala_st.H - fala_st.h0) * 0.5;   //pole powierzchni rozkładu ciśnienia

                                    for (int k = 0; k < p.Length - 1; k++)
                                    {
                                        dz = Math.Abs(z[1] - z[2]);
                                        A = A + dz * (p[k] + p[k + 1]) / 2;
                                    }
                                    chart1.Series[0].Points.AddXY(fala_st.H, A);
                                    fala_st.H = fala_st.H + dH;
                                    fala_st.przelicz(fala_st.h, fala_st.H, fala_st.T);

                                }
                                break;


                            case 2:   //T zmienne


                                double dT;

                                fala_st = new fala_st(Convert.ToDouble(textBox12.Text), Convert.ToDouble(textBox11.Text), Convert.ToDouble(textBox6.Text), Convert.ToDouble(textBox3.Text));

                                for (int i = 0; i <= 19; i++)
                                {

                                    dT = (Convert.ToDouble(textBox7.Text) - Convert.ToDouble(textBox6.Text)) / 20;
                                    for (int k = 0; k < z.Length; k++)
                                    {
                                        double a = k;
                                        z[k] = -(a / (z.Length - 1) * (fala_st.h + (fala_st.H - fala_st.h0)) - (fala_st.H - fala_st.h0));

                                    }
                                    for (int j = 0; j < p.Length; j++)
                                    {
                                        p[j] = -(fala_st.H * Math.Cosh(fala_st.k * (fala_st.h + z[j])) / Math.Cosh(fala_st.k * fala_st.h)) - fala_st.k * Math.Pow(fala_st.H, 2) / 8 * (4 * Math.Tanh(fala_st.k * fala_st.h) + 3 * (Math.Pow(Math.Tanh(fala_st.k * fala_st.h), 4) - 1) / Math.Pow(Math.Tanh(fala_st.k * fala_st.h), 3) * Math.Cosh(2 * fala_st.k * (fala_st.h + z[j])) / (Math.Cosh(2 * fala_st.k * fala_st.h))); //57                                
                                    }

                                    A = (fala_st.H - fala_st.h0) * (fala_st.H - fala_st.h0) * 0.5;   //pole powierzchni rozkładu ciśnienia

                                    for (int k = 0; k < p.Length - 1; k++)
                                    {
                                        dz = Math.Abs(z[1] - z[2]);
                                        A = A + dz * (p[k] + p[k + 1]) / 2;
                                    }
                                    chart1.Series[0].Points.AddXY(fala_st.T, A);
                                    fala_st.T = fala_st.T + dT;
                                    fala_st.przelicz(fala_st.h, fala_st.H, fala_st.T);

                                }


                                break;
                        }


                    }


                    break;

                case 3: // Goda
                    if (radioButton4.Checked == true)   //szczyt
                    {
                                     
                        switch (comboBox4.SelectedIndex)
                        {
                            case 0:            // h zmienne
                                
                                double dh;
                                

                                fala_g = new fala_g(Convert.ToDouble(textBox6.Text), Convert.ToDouble(textBox2.Text), Convert.ToDouble(textBox4.Text));
                             
                                
                                for (int i = 0; i <= 19; i++)
                                {
                                    
                                    dh = (Convert.ToDouble(textBox7.Text) - Convert.ToDouble(textBox6.Text)) / 20;
                                    p1 = 0;
                                    p2 = 0.5 * (1 + 1) * (fala_g.a1 + fala_g.a2) * fala_g.H;
                                    p3 = fala_g.a3 * p2;
                                    A = 0.5 * p2 * (fala_g.n ) + (p2 + p3) * fala_g.h * 0.5;
                                    chart1.Series[0].Points.AddXY(fala_g.h, A);
                                    fala_g.h = fala_g.h + dh;
                                    fala_g.przelicz(fala_g.h, fala_g.H, fala_g.T);
                                    
                                }

                                break;


                            case 1:           //H zmienne

                                double dH;

                                fala_g = new fala_g(Convert.ToDouble(textBox1.Text), Convert.ToDouble(textBox6.Text), Convert.ToDouble(textBox4.Text));                     
                                
                                for (int i = 0; i <= 19; i++)
                                {
                                    
                                    dH = (Convert.ToDouble(textBox7.Text) - Convert.ToDouble(textBox6.Text)) / 20;
                                    p1 = 0;
                                    p2 = 0.5 * (1 + 1) * (fala_g.a1 + fala_g.a2) * fala_g.H;
                                    p3 = fala_g.a3 * p2;
                                    A = 0.5 * p2 * (fala_g.n) + (p2 + p3) * fala_g.h * 0.5;
                                    chart1.Series[0].Points.AddXY(fala_g.H, A);
                                    fala_g.H = fala_g.H + dH;
                                    fala_g.przelicz(fala_g.h, fala_g.H, fala_g.T);
                                    
                                }
                                
 
                                break;

                            case 2:   //T zmienne


                                double dT;

                                fala_g = new fala_g(Convert.ToDouble(textBox1.Text), Convert.ToDouble(textBox2.Text), Convert.ToDouble(textBox6.Text));                       
                                
                                for (int i = 0; i <= 19; i++)
                                {
                                    
                                    dT = (Convert.ToDouble(textBox7.Text) - Convert.ToDouble(textBox6.Text)) / 20;
                                    p1 = 0;
                                    p2 = 0.5 * (1 + 1) * (fala_g.a1 + fala_g.a2) * fala_g.H;
                                    p3 = fala_g.a3 * p2;
                                    A = 0.5 * p2 * (fala_g.n) + (p2 + p3) * fala_g.h * 0.5;
                                    chart1.Series[0].Points.AddXY(fala_g.T, A);
                                    fala_g.T = fala_g.T + dT;
                                    fala_g.przelicz(fala_g.h,fala_g.H, fala_g.T);
                                    
                                }
                                
                                break;
                        }
                    
        
                    }

     
                    
                    break;
                    
                    
            }

            switch (comboBox3.SelectedIndex)   ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            {
                case 0: //Sainflou


                    if (radioButton4.Checked == true)   //szczyt
                    {

                        switch (comboBox4.SelectedIndex)
                        {
                            case 0:            // h zmienne

                                double dh;


                                fala_sa = new fala_sa(Convert.ToDouble(textBox6.Text), Convert.ToDouble(textBox11.Text), Convert.ToDouble(textBox9.Text));

                                for (int i = 0; i <= 19; i++)
                                {

                                    dh = (Convert.ToDouble(textBox7.Text) - Convert.ToDouble(textBox6.Text)) / 20;
                                    p1 = 0;     //szczyt fali
                                    p2 = (fala_sa.H + fala_sa.h0) / (fala_sa.h + fala_sa.h0 + fala_sa.H) * (fala_sa.h + fala_sa.H / Math.Cosh(fala_sa.k * fala_sa.h));  //poziom spokoju
                                    p3 = fala_sa.H / Math.Cosh(fala_sa.k * fala_sa.h);      //dno
                                    A = 0.5 * p2 * (fala_sa.H + fala_sa.h0) + (p2 + p3) * fala_sa.h * 0.5;
                                    chart1.Series[0].Points.AddXY(fala_sa.h, A);
                                    fala_sa.h = fala_sa.h + dh;
                                    fala_sa.przelicz(fala_sa.h, fala_sa.H, fala_sa.T);

                                }
                                fala_sa.h = Convert.ToDouble(textBox12.Text);
                                fala_sa.przelicz(fala_sa.h, fala_sa.H, fala_sa.T);
                                break;


                            case 1:           //H zmienne

                                double dH;

                                fala_sa = new fala_sa(Convert.ToDouble(textBox12.Text), Convert.ToDouble(textBox6.Text), Convert.ToDouble(textBox9.Text));

                                for (int i = 0; i <= 19; i++)
                                {

                                    dH = (Convert.ToDouble(textBox7.Text) - Convert.ToDouble(textBox6.Text)) / 20;
                                    p1 = 0;     //szczyt fali
                                    p2 = (fala_sa.H + fala_sa.h0) / (fala_sa.h + fala_sa.h0 + fala_sa.H) * (fala_sa.h + fala_sa.H / Math.Cosh(fala_sa.k * fala_sa.h));  //poziom spokoju
                                    p3 = fala_sa.H / Math.Cosh(fala_sa.k * fala_sa.h);      //dno
                                    A = 0.5 * p2 * (fala_sa.H + fala_sa.h0) + (p2 + p3) * fala_sa.h * 0.5;
                                    chart1.Series[0].Points.AddXY(fala_sa.H, A);
                                    fala_sa.H = fala_sa.H + dH;
                                    fala_sa.przelicz(fala_sa.h, fala_sa.H, fala_sa.T);

                                }

                                fala_sa.H = Convert.ToDouble(textBox11.Text);
                                fala_sa.przelicz(fala_sa.h, fala_sa.H, fala_sa.T);
                                break;

                            case 2:   //T zmienne


                                double dT;

                                fala_sa = new fala_sa(Convert.ToDouble(textBox12.Text), Convert.ToDouble(textBox11.Text), Convert.ToDouble(textBox6.Text));

                                for (int i = 0; i <= 19; i++)
                                {

                                    dT = (Convert.ToDouble(textBox7.Text) - Convert.ToDouble(textBox6.Text)) / 20;
                                    p1 = 0;     //szczyt fali
                                    p2 = (fala_sa.H + fala_sa.h0) / (fala_sa.h + fala_sa.h0 + fala_sa.H) * (fala_sa.h + fala_sa.H / Math.Cosh(fala_sa.k * fala_sa.h));  //poziom spokoju
                                    p3 = fala_sa.H / Math.Cosh(fala_sa.k * fala_sa.h);      //dno
                                    A = 0.5 * p2 * (fala_sa.H + fala_sa.h0) + (p2 + p3) * fala_sa.h * 0.5;
                                    chart1.Series[0].Points.AddXY(fala_sa.T, A);
                                    fala_sa.T = fala_sa.T + dT;
                                    fala_sa.przelicz(fala_sa.h, fala_sa.H, fala_sa.T);

                                }
                                fala_sa.T = Convert.ToDouble(textBox9.Text);
                                fala_sa.przelicz(fala_sa.h, fala_sa.H, fala_sa.T);

                                break;
                        }





                    }
                    else  //dno
                    {
                        switch (comboBox4.SelectedIndex)
                        {
                            case 0:            // h zmienne

                                double dh;

                                fala_sa = new fala_sa(Convert.ToDouble(textBox6.Text), Convert.ToDouble(textBox11.Text), Convert.ToDouble(textBox9.Text));

                                for (int i = 0; i <= 19; i++)
                                {

                                    dh = (Convert.ToDouble(textBox7.Text) - Convert.ToDouble(textBox6.Text)) / 20;

                                    p1 = 0;
                                    p2 = -fala_sa.H + fala_sa.h0;
                                    p3 = -fala_sa.H / Math.Cosh(fala_sa.k * fala_sa.h);
                                    A = 0.5 * p2 * (fala_sa.H - fala_sa.h0) + (p2 + p3) * (fala_sa.h - (fala_sa.H - fala_sa.h0)) * 0.5;
                                    chart1.Series[0].Points.AddXY(fala_sa.h, A);
                                    fala_sa.h = fala_sa.h + dh;
                                    fala_sa.przelicz(fala_sa.h, fala_sa.H, fala_sa.T);

                                }

                                break;


                            case 1:           //H zmienne

                                double dH;

                                fala_sa = new fala_sa(Convert.ToDouble(textBox12.Text), Convert.ToDouble(textBox6.Text), Convert.ToDouble(textBox9.Text));

                                for (int i = 0; i <= 19; i++)
                                {

                                    dH = (Convert.ToDouble(textBox7.Text) - Convert.ToDouble(textBox6.Text)) / 20;
                                    p1 = 0;
                                    p2 = -fala_sa.H + fala_sa.h0;
                                    p3 = -fala_sa.H / Math.Cosh(fala_sa.k * fala_sa.h);
                                    A = 0.5 * p2 * (fala_sa.H - fala_sa.h0) + (p2 + p3) * (fala_sa.h - (fala_sa.H - fala_sa.h0)) * 0.5;
                                    chart1.Series[0].Points.AddXY(fala_sa.H, A);
                                    fala_sa.H = fala_sa.H + dH;
                                    fala_sa.przelicz(fala_sa.h, fala_sa.H, fala_sa.T);

                                }
                                break;


                            case 2:   //T zmienne


                                double dT;

                                fala_sa = new fala_sa(Convert.ToDouble(textBox12.Text), Convert.ToDouble(textBox11.Text), Convert.ToDouble(textBox6.Text));

                                for (int i = 0; i <= 19; i++)
                                {

                                    dT = (Convert.ToDouble(textBox7.Text) - Convert.ToDouble(textBox6.Text)) / 20;
                                    p1 = 0;
                                    p2 = -fala_sa.H + fala_sa.h0;
                                    p3 = -fala_sa.H / Math.Cosh(fala_sa.k * fala_sa.h);
                                    A = 0.5 * p2 * (fala_sa.H - fala_sa.h0) + (p2 + p3) * (fala_sa.h - (fala_sa.H - fala_sa.h0)) * 0.5;
                                    chart1.Series[0].Points.AddXY(fala_sa.T, A);
                                    fala_sa.T = fala_sa.T + dT;
                                    fala_sa.przelicz(fala_sa.h, fala_sa.H, fala_sa.T);

                                }


                                break;
                        }


                    }






                    while (As < A / 2)                                  //położenie środka ciężkości
                    {
                        r = r + fala_sa.h / 20;
                        As = (p3 + (p3 + r * (p2 - p3) / fala_sa.h)) * r * 0.5;

                    }


                    break;


                case 1:  //Miche-Rundgren

                    if (radioButton4.Checked == true)   //szczyt
                    {

                        switch (comboBox4.SelectedIndex)
                        {
                            case 0:            // h zmienne

                                double dh;


                                fala_mr = new fala_mr(Convert.ToDouble(textBox6.Text), Convert.ToDouble(textBox11.Text), Convert.ToDouble(textBox9.Text), Convert.ToDouble(textBox3.Text));

                                for (int i = 0; i <= 19; i++)
                                {

                                    dh = (Convert.ToDouble(textBox7.Text) - Convert.ToDouble(textBox6.Text)) / 20;
                                    p1 = 0;
                                    p2 = (fala_mr.H + fala_mr.h0) / (fala_mr.h + fala_mr.h0 + fala_mr.H) * (fala_mr.h + fala_mr.H / Math.Cosh(fala_mr.k * fala_mr.h) + (Math.PI * Math.Pow(fala_mr.H, 2) / (fala_mr.L * Math.Sinh(fala_mr.k * fala_mr.h))) * fala_mr.A3);
                                    p3 = fala_mr.H / Math.Cosh(fala_mr.k * fala_mr.h) + (Math.PI * Math.Pow(fala_mr.H, 2) / (fala_mr.L * Math.Sinh(fala_mr.k * fala_mr.h))) * fala_mr.A3;
                                    A = 0.5 * p2 * (fala_mr.H + fala_mr.h0) + (p2 + p3) * fala_mr.h * 0.5;
                                    chart1.Series[0].Points.AddXY(fala_mr.h, A);
                                    fala_mr.h = fala_mr.h + dh;
                                    fala_mr.przelicz(fala_mr.h, fala_mr.H, fala_mr.T);

                                }

                                break;


                            case 1:           //H zmienne

                                double dH;

                                fala_mr = new fala_mr(Convert.ToDouble(textBox12.Text), Convert.ToDouble(textBox6.Text), Convert.ToDouble(textBox9.Text), Convert.ToDouble(textBox3.Text));

                                for (int i = 0; i <= 19; i++)
                                {

                                    dH = (Convert.ToDouble(textBox7.Text) - Convert.ToDouble(textBox6.Text)) / 20;
                                    p1 = 0;
                                    p2 = (fala_mr.H + fala_mr.h0) / (fala_mr.h + fala_mr.h0 + fala_mr.H) * (fala_mr.h + fala_mr.H / Math.Cosh(fala_mr.k * fala_mr.h) + (Math.PI * Math.Pow(fala_mr.H, 2) / (fala_mr.L * Math.Sinh(fala_mr.k * fala_mr.h))) * fala_mr.A3);
                                    p3 = fala_mr.H / Math.Cosh(fala_mr.k * fala_mr.h) + (Math.PI * Math.Pow(fala_mr.H, 2) / (fala_mr.L * Math.Sinh(fala_mr.k * fala_mr.h))) * fala_mr.A3;
                                    A = 0.5 * p2 * (fala_mr.H + fala_mr.h0) + (p2 + p3) * fala_mr.h * 0.5;
                                    chart1.Series[0].Points.AddXY(fala_mr.H, A);
                                    fala_mr.H = fala_mr.H + dH;
                                    fala_mr.przelicz(fala_mr.h, fala_mr.H, fala_mr.T);

                                }


                                break;

                            case 2:   //T zmienne


                                double dT;

                                fala_mr = new fala_mr(Convert.ToDouble(textBox12.Text), Convert.ToDouble(textBox11.Text), Convert.ToDouble(textBox6.Text), Convert.ToDouble(textBox3.Text));

                                for (int i = 0; i <= 19; i++)
                                {

                                    dT = (Convert.ToDouble(textBox7.Text) - Convert.ToDouble(textBox6.Text)) / 20;
                                    p1 = 0;
                                    p2 = (fala_mr.H + fala_mr.h0) / (fala_mr.h + fala_mr.h0 + fala_mr.H) * (fala_mr.h + fala_mr.H / Math.Cosh(fala_mr.k * fala_mr.h) + (Math.PI * Math.Pow(fala_mr.H, 2) / (fala_mr.L * Math.Sinh(fala_mr.k * fala_mr.h))) * fala_mr.A3);
                                    p3 = fala_mr.H / Math.Cosh(fala_mr.k * fala_mr.h) + (Math.PI * Math.Pow(fala_mr.H, 2) / (fala_mr.L * Math.Sinh(fala_mr.k * fala_mr.h))) * fala_mr.A3;
                                    A = 0.5 * p2 * (fala_mr.H + fala_mr.h0) + (p2 + p3) * fala_mr.h * 0.5;
                                    chart1.Series[0].Points.AddXY(fala_mr.T, A);
                                    fala_mr.T = fala_mr.T + dT;
                                    fala_mr.przelicz(fala_mr.h, fala_mr.H, fala_mr.T);

                                }


                                break;
                        }





                    }
                    else  //dno
                    {
                        switch (comboBox4.SelectedIndex)
                        {
                            case 0:            // h zmienne

                                double dh;

                                fala_mr = new fala_mr(Convert.ToDouble(textBox6.Text), Convert.ToDouble(textBox11.Text), Convert.ToDouble(textBox9.Text), Convert.ToDouble(textBox3.Text));

                                for (int i = 0; i <= 19; i++)
                                {

                                    dh = (Convert.ToDouble(textBox7.Text) - Convert.ToDouble(textBox6.Text)) / 20;

                                    p1 = 0;
                                    p2 = -fala_mr.H + fala_mr.h0;
                                    p3 = -fala_mr.H / Math.Cosh(fala_mr.k * fala_mr.h) + Math.PI * Math.Pow(fala_mr.H, 2) / fala_mr.L / Math.Sinh(fala_mr.k * fala_mr.h) * fala_mr.A3;
                                    A = 0.5 * p2 * (fala_mr.H - fala_mr.h0) + (p2 + p3) * (fala_mr.h - (fala_mr.H - fala_mr.h0)) * 0.5;
                                    chart1.Series[0].Points.AddXY(fala_mr.h, A);
                                    fala_mr.h = fala_mr.h + dh;
                                    fala_mr.przelicz(fala_mr.h, fala_mr.H, fala_mr.T);

                                }

                                break;


                            case 1:           //H zmienne

                                double dH;

                                fala_mr = new fala_mr(Convert.ToDouble(textBox12.Text), Convert.ToDouble(textBox6.Text), Convert.ToDouble(textBox9.Text), Convert.ToDouble(textBox3.Text));

                                for (int i = 0; i <= 19; i++)
                                {

                                    dH = (Convert.ToDouble(textBox7.Text) - Convert.ToDouble(textBox6.Text)) / 20;
                                    p1 = 0;
                                    p2 = -fala_mr.H + fala_mr.h0;
                                    p3 = -fala_mr.H / Math.Cosh(fala_mr.k * fala_mr.h) + Math.PI * Math.Pow(fala_mr.H, 2) / fala_mr.L / Math.Sinh(fala_mr.k * fala_mr.h) * fala_mr.A3;
                                    A = 0.5 * p2 * (fala_mr.H - fala_mr.h0) + (p2 + p3) * (fala_mr.h - (fala_mr.H - fala_mr.h0)) * 0.5;
                                    chart1.Series[0].Points.AddXY(fala_mr.H, A);
                                    fala_mr.H = fala_mr.H + dH;
                                    fala_mr.przelicz(fala_mr.h, fala_mr.H, fala_mr.T);

                                }
                                break;


                            case 2:   //T zmienne


                                double dT;

                                fala_mr = new fala_mr(Convert.ToDouble(textBox12.Text), Convert.ToDouble(textBox11.Text), Convert.ToDouble(textBox6.Text), Convert.ToDouble(textBox3.Text));

                                for (int i = 0; i <= 19; i++)
                                {

                                    dT = (Convert.ToDouble(textBox7.Text) - Convert.ToDouble(textBox6.Text)) / 20;
                                    p1 = 0;
                                    p2 = -fala_mr.H + fala_mr.h0;
                                    p3 = -fala_mr.H / Math.Cosh(fala_mr.k * fala_mr.h) + Math.PI * Math.Pow(fala_mr.H, 2) / fala_mr.L / Math.Sinh(fala_mr.k * fala_mr.h) * fala_mr.A3;
                                    A = 0.5 * p2 * (fala_mr.H - fala_mr.h0) + (p2 + p3) * (fala_mr.h - (fala_mr.H - fala_mr.h0)) * 0.5;
                                    chart1.Series[0].Points.AddXY(fala_mr.T, A);
                                    fala_mr.T = fala_mr.T + dT;
                                    fala_mr.przelicz(fala_mr.h, fala_mr.H, fala_mr.T);

                                }


                                break;
                        }


                    }






                    while (As < A / 2)                                  //położenie środka ciężkości
                    {
                        r = r + fala_mr.h / 20;
                        As = (p3 + (p3 + r * (p2 - p3) / fala_mr.h)) * r * 0.5;

                    }


                    break;

                case 2: //Stokes

                    double[] p = new double[9];
                    double[] z = new double[9];

                    if (radioButton4.Checked == true)   //szczyt
                    {

                        switch (comboBox4.SelectedIndex)
                        {
                            case 0:            // h zmienne

                                double dh;


                                fala_st = new fala_st(Convert.ToDouble(textBox6.Text), Convert.ToDouble(textBox2.Text), Convert.ToDouble(textBox4.Text), Convert.ToDouble(textBox5.Text));


                                for (int i = 0; i <= 19; i++)
                                {

                                    dh = (Convert.ToDouble(textBox7.Text) - Convert.ToDouble(textBox6.Text)) / 20;


                                    for (int k = 0; k < z.Length; k++)
                                    {
                                        double a = k;
                                        z[k] = -a / (z.Length - 1) * fala_st.h;

                                    }
                                    for (int j = 0; j < p.Length; j++)
                                    {
                                        p[j] = fala_st.H * Math.Cosh(fala_st.k * (fala_st.h + z[j])) / Math.Cosh(fala_st.k * fala_st.h) - fala_st.k * Math.Pow(fala_st.H, 2) / 8 * (4 * Math.Tanh(fala_st.k * fala_st.h) + 3 * (Math.Pow(Math.Tanh(fala_st.k * fala_st.h), 4) - 1) / Math.Pow(Math.Tanh(fala_st.k * fala_st.h), 3) * Math.Cosh(2 * fala_st.k * (fala_st.h + z[j])) / (Math.Cosh(2 * fala_st.k * fala_st.h))); //57
                                    }

                                    A = (fala_st.H + fala_st.h0) * p[0] * 0.5;   //pole powierzchni rozkładu ciśnienia

                                    for (int k = 0; k < z.Length - 1; k++)
                                    {
                                        dz = Math.Abs(z[1] - z[2]);
                                        A = A + dz * (p[k] + p[k + 1]) / 2;
                                    }



                                    chart1.Series[0].Points.AddXY(fala_st.h, A);
                                    fala_st.h = fala_st.h + dh;
                                    fala_st.przelicz(fala_st.h, fala_st.H, fala_st.T);

                                }
                                //
                                break;


                            case 1:           //H zmienne

                                double dH;

                                fala_st = new fala_st(Convert.ToDouble(textBox12.Text), Convert.ToDouble(textBox6.Text), Convert.ToDouble(textBox4.Text), Convert.ToDouble(textBox5.Text));

                                for (int i = 0; i <= 19; i++)
                                {

                                    dH = (Convert.ToDouble(textBox7.Text) - Convert.ToDouble(textBox6.Text)) / 20;
                                    for (int k = 0; k < z.Length; k++)
                                    {
                                        double a = k;
                                        z[k] = -a / (z.Length - 1) * fala_st.h;

                                    }
                                    for (int j = 0; j < p.Length; j++)
                                    {
                                        p[j] = fala_st.H * Math.Cosh(fala_st.k * (fala_st.h + z[j])) / Math.Cosh(fala_st.k * fala_st.h) - fala_st.k * Math.Pow(fala_st.H, 2) / 8 * (4 * Math.Tanh(fala_st.k * fala_st.h) + 3 * (Math.Pow(Math.Tanh(fala_st.k * fala_st.h), 4) - 1) / Math.Pow(Math.Tanh(fala_st.k * fala_st.h), 3) * Math.Cosh(2 * fala_st.k * (fala_st.h + z[j])) / (Math.Cosh(2 * fala_st.k * fala_st.h))); //57
                                    }

                                    A = (fala_st.H + fala_st.h0) * p[0] * 0.5;   //pole powierzchni rozkładu ciśnienia

                                    for (int k = 0; k < z.Length - 1; k++)
                                    {
                                        dz = Math.Abs(z[1] - z[2]);
                                        A = A + dz * (p[k] + p[k + 1]) / 2;
                                    }
                                    chart1.Series[0].Points.AddXY(fala_st.H, A);
                                    fala_st.H = fala_st.H + dH;
                                    fala_st.przelicz(fala_st.h, fala_st.H, fala_st.T);

                                }


                                break;

                            case 2:   //T zmienne


                                double dT;

                                fala_st = new fala_st(Convert.ToDouble(textBox12.Text), Convert.ToDouble(textBox11.Text), Convert.ToDouble(textBox6.Text), Convert.ToDouble(textBox3.Text));

                                for (int i = 0; i <= 19; i++)
                                {

                                    dT = (Convert.ToDouble(textBox7.Text) - Convert.ToDouble(textBox6.Text)) / 20;
                                    for (int k = 0; k < z.Length; k++)
                                    {
                                        double a = k;
                                        z[k] = -a / (z.Length - 1) * fala_st.h;

                                    }
                                    for (int j = 0; j < p.Length; j++)
                                    {
                                        p[j] = fala_st.H * Math.Cosh(fala_st.k * (fala_st.h + z[j])) / Math.Cosh(fala_st.k * fala_st.h) - fala_st.k * Math.Pow(fala_st.H, 2) / 8 * (4 * Math.Tanh(fala_st.k * fala_st.h) + 3 * (Math.Pow(Math.Tanh(fala_st.k * fala_st.h), 4) - 1) / Math.Pow(Math.Tanh(fala_st.k * fala_st.h), 3) * Math.Cosh(2 * fala_st.k * (fala_st.h + z[j])) / (Math.Cosh(2 * fala_st.k * fala_st.h))); //57
                                    }

                                    A = (fala_st.H + fala_st.h0) * p[0] * 0.5;   //pole powierzchni rozkładu ciśnienia

                                    for (int k = 0; k < z.Length - 1; k++)
                                    {
                                        dz = Math.Abs(z[1] - z[2]);
                                        A = A + dz * (p[k] + p[k + 1]) / 2;
                                    }
                                    chart1.Series[0].Points.AddXY(fala_st.T, A);
                                    fala_st.T = fala_st.T + dT;
                                    fala_st.przelicz(fala_st.h, fala_st.H, fala_st.T);

                                }


                                break;
                        }





                    }
                    else  //dno
                    {
                        switch (comboBox4.SelectedIndex)
                        {
                            case 0:            // h zmienne

                                double dh;

                                fala_st = new fala_st(Convert.ToDouble(textBox6.Text), Convert.ToDouble(textBox11.Text), Convert.ToDouble(textBox9.Text), Convert.ToDouble(textBox3.Text));

                                for (int i = 0; i <= 19; i++)
                                {

                                    dh = (Convert.ToDouble(textBox7.Text) - Convert.ToDouble(textBox6.Text)) / 20;

                                    for (int k = 0; k < z.Length; k++)
                                    {
                                        double a = k;
                                        z[k] = -(a / (z.Length - 1) * (fala_st.h + (fala_st.H - fala_st.h0)) - (fala_st.H - fala_st.h0));

                                    }
                                    for (int j = 0; j < p.Length; j++)
                                    {
                                        p[j] = -(fala_st.H * Math.Cosh(fala_st.k * (fala_st.h + z[j])) / Math.Cosh(fala_st.k * fala_st.h)) - fala_st.k * Math.Pow(fala_st.H, 2) / 8 * (4 * Math.Tanh(fala_st.k * fala_st.h) + 3 * (Math.Pow(Math.Tanh(fala_st.k * fala_st.h), 4) - 1) / Math.Pow(Math.Tanh(fala_st.k * fala_st.h), 3) * Math.Cosh(2 * fala_st.k * (fala_st.h + z[j])) / (Math.Cosh(2 * fala_st.k * fala_st.h))); //57                                
                                    }

                                    A = (fala_st.H - fala_st.h0) * (fala_st.H - fala_st.h0) * 0.5;   //pole powierzchni rozkładu ciśnienia

                                    for (int k = 0; k < p.Length - 1; k++)
                                    {
                                        dz = Math.Abs(z[1] - z[2]);
                                        A = A + dz * (p[k] + p[k + 1]) / 2;
                                    }

                                    chart1.Series[0].Points.AddXY(fala_st.h, A);
                                    fala_st.h = fala_st.h + dh;
                                    fala_st.przelicz(fala_st.h, fala_st.H, fala_st.T);

                                }

                                break;


                            case 1:           //H zmienne

                                double dH;

                                fala_st = new fala_st(Convert.ToDouble(textBox12.Text), Convert.ToDouble(textBox6.Text), Convert.ToDouble(textBox9.Text), Convert.ToDouble(textBox3.Text));

                                for (int i = 0; i <= 19; i++)
                                {

                                    dH = (Convert.ToDouble(textBox7.Text) - Convert.ToDouble(textBox6.Text)) / 20;
                                    for (int k = 0; k < z.Length; k++)
                                    {
                                        double a = k;
                                        z[k] = -(a / (z.Length - 1) * (fala_st.h + (fala_st.H - fala_st.h0)) - (fala_st.H - fala_st.h0));

                                    }
                                    for (int j = 0; j < p.Length; j++)
                                    {
                                        p[j] = -(fala_st.H * Math.Cosh(fala_st.k * (fala_st.h + z[j])) / Math.Cosh(fala_st.k * fala_st.h)) - fala_st.k * Math.Pow(fala_st.H, 2) / 8 * (4 * Math.Tanh(fala_st.k * fala_st.h) + 3 * (Math.Pow(Math.Tanh(fala_st.k * fala_st.h), 4) - 1) / Math.Pow(Math.Tanh(fala_st.k * fala_st.h), 3) * Math.Cosh(2 * fala_st.k * (fala_st.h + z[j])) / (Math.Cosh(2 * fala_st.k * fala_st.h))); //57                                
                                    }

                                    A = (fala_st.H - fala_st.h0) * (fala_st.H - fala_st.h0) * 0.5;   //pole powierzchni rozkładu ciśnienia

                                    for (int k = 0; k < p.Length - 1; k++)
                                    {
                                        dz = Math.Abs(z[1] - z[2]);
                                        A = A + dz * (p[k] + p[k + 1]) / 2;
                                    }
                                    chart1.Series[0].Points.AddXY(fala_st.H, A);
                                    fala_st.H = fala_st.H + dH;
                                    fala_st.przelicz(fala_st.h, fala_st.H, fala_st.T);

                                }
                                break;


                            case 2:   //T zmienne


                                double dT;

                                fala_st = new fala_st(Convert.ToDouble(textBox12.Text), Convert.ToDouble(textBox11.Text), Convert.ToDouble(textBox6.Text), Convert.ToDouble(textBox3.Text));

                                for (int i = 0; i <= 19; i++)
                                {

                                    dT = (Convert.ToDouble(textBox7.Text) - Convert.ToDouble(textBox6.Text)) / 20;
                                    for (int k = 0; k < z.Length; k++)
                                    {
                                        double a = k;
                                        z[k] = -(a / (z.Length - 1) * (fala_st.h + (fala_st.H - fala_st.h0)) - (fala_st.H - fala_st.h0));

                                    }
                                    for (int j = 0; j < p.Length; j++)
                                    {
                                        p[j] = -(fala_st.H * Math.Cosh(fala_st.k * (fala_st.h + z[j])) / Math.Cosh(fala_st.k * fala_st.h)) - fala_st.k * Math.Pow(fala_st.H, 2) / 8 * (4 * Math.Tanh(fala_st.k * fala_st.h) + 3 * (Math.Pow(Math.Tanh(fala_st.k * fala_st.h), 4) - 1) / Math.Pow(Math.Tanh(fala_st.k * fala_st.h), 3) * Math.Cosh(2 * fala_st.k * (fala_st.h + z[j])) / (Math.Cosh(2 * fala_st.k * fala_st.h))); //57                                
                                    }

                                    A = (fala_st.H - fala_st.h0) * (fala_st.H - fala_st.h0) * 0.5;   //pole powierzchni rozkładu ciśnienia

                                    for (int k = 0; k < p.Length - 1; k++)
                                    {
                                        dz = Math.Abs(z[1] - z[2]);
                                        A = A + dz * (p[k] + p[k + 1]) / 2;
                                    }
                                    chart1.Series[0].Points.AddXY(fala_st.T, A);
                                    fala_st.T = fala_st.T + dT;
                                    fala_st.przelicz(fala_st.h, fala_st.H, fala_st.T);

                                }


                                break;
                        }


                    }


                    break;

                case 3: // Goda
                    if (radioButton4.Checked == true)   //szczyt
                    {

                        switch (comboBox4.SelectedIndex)
                        {
                            case 0:            // h zmienne

                                double dh;


                                fala_g = new fala_g(Convert.ToDouble(textBox6.Text), Convert.ToDouble(textBox2.Text), Convert.ToDouble(textBox4.Text));


                                for (int i = 0; i <= 19; i++)
                                {

                                    dh = (Convert.ToDouble(textBox7.Text) - Convert.ToDouble(textBox6.Text)) / 20;
                                    p1 = 0;
                                    p2 = 0.5 * (1 + 1) * (fala_g.a1 + fala_g.a2) * fala_g.H;
                                    p3 = fala_g.a3 * p2;
                                    A = 0.5 * p2 * (fala_g.n) + (p2 + p3) * fala_g.h * 0.5;
                                    chart1.Series[0].Points.AddXY(fala_g.h, A);
                                    fala_g.h = fala_g.h + dh;
                                    fala_g.przelicz(fala_g.h, fala_g.H, fala_g.T);

                                }

                                break;


                            case 1:           //H zmienne

                                double dH;

                                fala_g = new fala_g(Convert.ToDouble(textBox1.Text), Convert.ToDouble(textBox6.Text), Convert.ToDouble(textBox4.Text));

                                for (int i = 0; i <= 19; i++)
                                {

                                    dH = (Convert.ToDouble(textBox7.Text) - Convert.ToDouble(textBox6.Text)) / 20;
                                    p1 = 0;
                                    p2 = 0.5 * (1 + 1) * (fala_g.a1 + fala_g.a2) * fala_g.H;
                                    p3 = fala_g.a3 * p2;
                                    A = 0.5 * p2 * (fala_g.n) + (p2 + p3) * fala_g.h * 0.5;
                                    chart1.Series[0].Points.AddXY(fala_g.H, A);
                                    fala_g.H = fala_g.H + dH;
                                    fala_g.przelicz(fala_g.h, fala_g.H, fala_g.T);

                                }


                                break;

                            case 2:   //T zmienne


                                double dT;

                                fala_g = new fala_g(Convert.ToDouble(textBox1.Text), Convert.ToDouble(textBox2.Text), Convert.ToDouble(textBox6.Text));

                                for (int i = 0; i <= 19; i++)
                                {

                                    dT = (Convert.ToDouble(textBox7.Text) - Convert.ToDouble(textBox6.Text)) / 20;
                                    p1 = 0;
                                    p2 = 0.5 * (1 + 1) * (fala_g.a1 + fala_g.a2) * fala_g.H;
                                    p3 = fala_g.a3 * p2;
                                    A = 0.5 * p2 * (fala_g.n) + (p2 + p3) * fala_g.h * 0.5;
                                    chart1.Series[0].Points.AddXY(fala_g.T, A);
                                    fala_g.T = fala_g.T + dT;
                                    fala_g.przelicz(fala_g.h, fala_g.H, fala_g.T);

                                }

                                break;
                        }


                    }



                    break;


            }
        
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }



    }

}