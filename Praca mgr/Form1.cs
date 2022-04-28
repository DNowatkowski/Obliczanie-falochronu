using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public double p1; // wartości ciśnienia na różnych poziomach
        public double p2;
        public double p3;
        fala_szablon fala;
        public double A;  // ciśnienie całkowite
        public double As;  // zmienna pozwalająca na obliczenie położenia środka ciężkości
        public double dz;
        public double r; // ramię wypadkowego ciśnienia liczone od dna
        public double M; //moment wywracający
        const int skala1=20; // skala wielkości geometr.
        const int skala2=50; //skala wielkości ciśnienia
        
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
                    radioButton2.Enabled = true;
                    break;
                case 1:
                    textBox5.Enabled = true;
                    radioButton2.Enabled = true;
                    break;
                case 2:
                    radioButton2.Enabled = true;
                    break;
                case 3:
                    radioButton2.Enabled = false;
                    radioButton1.Checked = true;
                    break;
            }
        }

        public void inputCheck(KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == ',')
                base.OnKeyPress(e);
            else
                e.Handled = true;
        }
        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            inputCheck(e);
        }
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            inputCheck(e);
        }
        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            inputCheck(e);
        }
        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            inputCheck(e);
        }
        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            inputCheck(e);
        }
        public void aktualizujOpisy(fala_szablon fala)
        {
            label13.Text = Math.Round(fala.wzniesieniePoziomu, 2).ToString(); // wzniesienie poziomu falowania
            label14.Text = Math.Round(p2, 2).ToString(); // ciśnienie w poziomie spokoju/dna fali
            label15.Text = Math.Round(p3, 2).ToString(); // ciśnienie w poziomie dna morskiego
            label37.Text = Math.Round(A, 2).ToString(); // ciśnienie całkowite
            label38.Text =Math.Round(A * r, 2).ToString(); // moment wywracający
            if (!(fala is fala_g))
            {
                label46.Text = Math.Round(fala.wysokoscFali / (9.81 * fala.okres * fala.okres), 3).ToString();
                label47.Text = Math.Round(fala.glebokosc / (9.81 * fala.okres * fala.okres), 3).ToString();
            }
            else
            {
                label46.Text = "-";
                label47.Text = "-";
            }
        }

        private void button1_Click(object sender, EventArgs e)  // obliczanie rozkładu ciśnienia na ścianie
        {
            switch (comboBox1.SelectedIndex)
            {
                case 0: fala = new fala_sa(Convert.ToDouble(textBox1.Text), Convert.ToDouble(textBox2.Text), Convert.ToDouble(textBox4.Text), Convert.ToDouble(textBox5.Text)); break;
                case 1: fala = new fala_mr(Convert.ToDouble(textBox1.Text), Convert.ToDouble(textBox2.Text), Convert.ToDouble(textBox4.Text), Convert.ToDouble(textBox5.Text)); break;
                case 2: fala = new fala_st(Convert.ToDouble(textBox1.Text), Convert.ToDouble(textBox2.Text), Convert.ToDouble(textBox4.Text), Convert.ToDouble(textBox5.Text)); break;
                case 3: fala = new fala_g(Convert.ToDouble(textBox1.Text), Convert.ToDouble(textBox2.Text), Convert.ToDouble(textBox4.Text), Convert.ToDouble(textBox5.Text));break;
            }
            p1 = 0;
            fala.myPictureBox = pictureBox1;
            if (radioButton1.Checked)   //faza szczytu fali na ścianie fali na ścianie
            {                             
                p2 = fala.cisnieniePoziomSpokoju();
                p3 = fala.cisnieniePoziomDnaMorskiego(faza_fali.szczyt);
                A = fala.cisnienieCalkowite(faza_fali.szczyt);
                fala.rysujWykres(faza_fali.szczyt,p2,p3, skala1, skala2);
            }
            else if (radioButton2.Checked) //faza dna fali na ścianie
            {
                p2 = fala.cisnieniePoziomDnaFali();
                p3 = fala.cisnieniePoziomDnaMorskiego(faza_fali.dno);
                A = fala.cisnienieCalkowite(faza_fali.dno);
                fala.rysujWykres(faza_fali.dno, p2, p3, skala1, skala2);
            }
            r = fala.srodekCiezkosci(A, p2, p3);
            aktualizujOpisy(fala);

            if (radioButton1.Checked)            
                labelPoziomSpokoju.Text = "Wysokość ciśnienia w poziomie spokoju [m]:";            
            else           
                labelPoziomSpokoju.Text = "Wysokość ciśnienia w poziomie dna fali [m]:";                
        }
      
        private void textBox12_KeyPress(object sender, KeyPressEventArgs e)
        {
            inputCheck(e);
        }
        private void textBox11_KeyPress(object sender, KeyPressEventArgs e)
        {
            inputCheck(e);
        }
        private void textBox9_KeyPress(object sender, KeyPressEventArgs e)
        {
            inputCheck(e);
        }
        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            inputCheck(e);
        }
        private void textBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            inputCheck(e);
        }

        public void ustawOpisy(ComboBox box, int i)
        {
            switch (box.SelectedIndex)
            {
                case 0:
                    chart1.Series[i].LegendText = "-";
                    break;
                case 1:
                    chart1.Series[i].LegendText = "Sainflou";
                    break;
                case 2:
                    chart1.Series[i].LegendText = "Miche-Rundgren";
                    break;
                case 3:
                    chart1.Series[i].LegendText = "Stokes";
                    break;
                case 4:
                    chart1.Series[i].LegendText = "Goda";
                    break;
            }
        }
        private void button2_Click(object sender, EventArgs e)   //analiza parametryczna
        {            
            chart1.Series[0].Font = new Font("Arial Regular", 12);
            
            foreach (var series in chart1.Series)
            {
                series.Points.Clear();
            }
            ustawOpisy(comboBox2, 0);
            ustawOpisy(comboBox3, 1);
            ustawOpisy(comboBox5, 2);           

            if(glebokoscRadioButton.Checked)   
                chart1.ChartAreas[0].AxisX.Title = "głębokość wody [m]";
            else if(wysokoscRadioButton.Checked)
                chart1.ChartAreas[0].AxisX.Title = "wysokość fali podchodzącej [m]";
            else if (okresRadioButton.Checked)
                chart1.ChartAreas[0].AxisX.Title = "okres fali [s]";
    
            chart1.ChartAreas[0].AxisY.Title = "wysokość ciśnienia hydrodynamicznego [m]";
            chart1.Images.Clear();

            porownaj(comboBox2,0);
            porownaj(comboBox3,1);
            porownaj(comboBox5,2);
        }
        
        public void porownaj(ComboBox combo, int index)           
        {
            double dh= (Convert.ToDouble(glebokoscMax.Text) - Convert.ToDouble(glebokoscMin.Text)) / 20;
            double dH = (Convert.ToDouble(wysokoscMax.Text) - Convert.ToDouble(wysokoscMin.Text)) / 20;
            double dT = (Convert.ToDouble(okresMax.Text) - Convert.ToDouble(okresMin.Text)) / 20;

            switch (combo.SelectedIndex)
            {
                case 0: fala = null;break;
                case 1: fala = new fala_sa(Convert.ToDouble(glebokoscMin.Text), Convert.ToDouble(wysokoscMin.Text), Convert.ToDouble(okresMin.Text), Convert.ToDouble(textBox3.Text)); break;
                case 2: fala = new fala_mr(Convert.ToDouble(glebokoscMin.Text), Convert.ToDouble(wysokoscMin.Text), Convert.ToDouble(okresMin.Text), Convert.ToDouble(textBox3.Text)); break;
                case 3: fala = new fala_st(Convert.ToDouble(glebokoscMin.Text), Convert.ToDouble(wysokoscMin.Text), Convert.ToDouble(okresMin.Text), Convert.ToDouble(textBox3.Text)); break;
                case 4: fala = new fala_g(Convert.ToDouble(glebokoscMin.Text), Convert.ToDouble(wysokoscMin.Text), Convert.ToDouble(okresMin.Text), Convert.ToDouble(textBox3.Text)); break;
            }

            if (fala != null)
            {

                if (radioButton1.Checked)   //faza szczytu fali na ścianie fali na ścianie             
                    A = fala.cisnienieCalkowite(faza_fali.szczyt);
                else if (radioButton2.Checked) //faza dna fali na ścianie
                    A = fala.cisnienieCalkowite(faza_fali.dno);

                if (glebokoscRadioButton.Checked)
                {
                    if (radioButton4.Checked)//szczyt
                    {
                        for (int i = 0; i <= 20; i++)
                        {
                            chart1.Series[index].Points.AddXY(fala.glebokosc, A);
                            fala.glebokosc += dh;
                            A = fala.cisnienieCalkowite(faza_fali.szczyt);
                        }
                    }
                    else if (radioButton3.Checked) //dno
                        for (int i = 0; i <= 20; i++)
                        {
                            chart1.Series[index].Points.AddXY(fala.glebokosc, A);
                            fala.glebokosc += dh;
                            A = fala.cisnienieCalkowite(faza_fali.dno);
                        }
                }
                else if (wysokoscRadioButton.Checked)
                {
                    if (radioButton4.Checked)//szczyt
                    {
                        for (int i = 0; i <= 20; i++)
                        {
                            chart1.Series[index].Points.AddXY(fala.wysokoscFali, A);
                            fala.wysokoscFali += dH;
                            A = fala.cisnienieCalkowite(faza_fali.szczyt);
                        }
                    }
                    else if (radioButton3.Checked) //dno
                        for (int i = 0; i <= 20; i++)
                        {
                            chart1.Series[index].Points.AddXY(fala.wysokoscFali, A);
                            fala.wysokoscFali += dH;
                            A = fala.cisnienieCalkowite(faza_fali.dno);
                        }
                }
                else if (okresRadioButton.Checked)
                {
                    if (radioButton4.Checked)//szczyt
                    {
                        for (int i = 0; i <= 20; i++)
                        {
                            chart1.Series[index].Points.AddXY(fala.okres, A);
                            fala.okres += dT;
                            A = fala.cisnienieCalkowite(faza_fali.szczyt);
                        }
                    }
                    else if (radioButton3.Checked) //dno
                        for (int i = 0; i <= 20; i++)
                        {
                            chart1.Series[index].Points.AddXY(fala.okres, A);
                            fala.okres += dT;
                            A = fala.cisnienieCalkowite(faza_fali.dno);
                        }
                }
            }   
        }
        private void wysokoscRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            glebokoscMax.Enabled = false;
            wysokoscMax.Enabled = true;
            okresMax.Enabled = false;
        }
        private void glebokoscRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            glebokoscMax.Enabled = true;
            wysokoscMax.Enabled = false;
            okresMax.Enabled = false;
        }
        private void okresRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            glebokoscMax.Enabled = false;
            wysokoscMax.Enabled = false;
            okresMax.Enabled = true;  
        }
    }

}