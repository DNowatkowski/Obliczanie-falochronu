using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace WindowsFormsApplication1
{

    public abstract class fala_szablon
    {
        const double g= 9.81;
        protected double H;
        public double wysokoscFali { get { return H; } set { H = value; } }
        protected double h;
        public double glebokosc 
        { 
            get { return h; } 
            set 
            {
                h = value; 
                ObliczDlugoscFali(h, T);               
            } 
        }
        protected double h0;
        public double wzniesieniePoziomu { get { return h0; } set { h0 = value; } }
        protected double L;
        protected double T;
        public double okres 
        { 
            get { return T; } 
            set 
            {
                T = value;
                ObliczDlugoscFali(h, T);
            } 
        }
        protected bool dysp = false;
        protected double k;
        protected double Kr;   
        private double Ld;
        public TextBox glebokoscWodyTextBox;
        public TextBox wysokoscFaliTextBox;
        public TextBox okresFaliTextBox;
        public TextBox wspolczynnikOdbiciaTextBox;
        public PictureBox myPictureBox;
        private Graphics graph;

        public fala_szablon(double glebokosc, double wysokosc_fali, double okres_fali,double wsp_odbicia)
        {
            H = wysokosc_fali;
            h = glebokosc;
            T = okres_fali;
            Kr = wsp_odbicia;
            L = 1;
            ObliczDlugoscFali(h,T);          
        }
        public void ObliczDlugoscFali(double h,  double T)
        {
            while (dysp == false)  //wyznaczanie długości fali ze związku dyspersyjnego
            {
                Ld = g * Math.Pow(T, 2) / (2 * Math.PI) * Math.Tanh(2 * Math.PI * h / L);
                if ((Math.Abs((L - Ld) / L)) < 0.01)
                {
                    dysp = true;
                }
                L = (Ld + L) / 2;
            }
            k = 2 * Math.PI / L;
            dysp = false;
        }

        public virtual double cisnienieCalkowite(faza_fali faza,double p2,double p3)
        {
            if (faza == faza_fali.szczyt)
                return 0.5 * p2 * (H + h0) + (p2 + p3) * h * 0.5;
            else if (faza == faza_fali.dno)
                return 0.5 * p2 * (H - h0) + (p2 + p3) * (h - (H - h0)) * 0.5;
            else
                return 0;
        }
           
        public virtual void rysujWykres(faza_fali faza, double p2,double p3, int skala1, int skala2)
        {
            myPictureBox.Image = new Bitmap(myPictureBox.Width, myPictureBox.Height);
            graph = Graphics.FromImage(myPictureBox.Image);
            graph.Clear(Color.White);

            int x1=0;                                                           //rysowanie poziomu spokoju
            int y1= myPictureBox.Height - Convert.ToInt16(h * skala1);
            int x2= myPictureBox.Width;
            int y2= myPictureBox.Height - Convert.ToInt16(h * skala1);
            graph.DrawLine(Pens.Black, x1,y1,x2,y2);
            
            x1 = 0;                                                                 //rysowanie wykresu ciśnienia
            if(faza== faza_fali.szczyt)
                y1 = myPictureBox.Height - Convert.ToInt16((h + h0 + H) * skala1);
            else if (faza == faza_fali.dno)
                y1 = myPictureBox.Height - Convert.ToInt16(h * skala1);
            x2 = Math.Abs(Convert.ToInt16(p2) * skala2);
            if (faza == faza_fali.szczyt)
                y2 = myPictureBox.Height - Convert.ToInt16(h * skala1);
            else if (faza == faza_fali.dno)
                y2 = myPictureBox.Height - Convert.ToInt16((h+h0-H) * skala1);
            graph.DrawLine(Pens.Red, x1, y1, x2, y2);

            x1 = x2;
            y1 = y2;    
            x2 = Math.Abs(Convert.ToInt16(p3)) * skala2;
            y2 = myPictureBox.Height;
            graph.DrawLine(Pens.Red, x1,y1,x2,y2);
            rysujFale(faza,skala1,graph);
        }

        public void rysujFale(faza_fali faza,int skala1,Graphics graph)
        {
            int x1 = 0;
            int y1;
            int x2 = 0;
            int y2;
            if (faza == faza_fali.szczyt)
            {
                while (x2 < myPictureBox.Width)
                {
                    x1 += 1;
                    y1 = myPictureBox.Height - Convert.ToInt16((H*Math.Sin(2 * Math.PI * x1 / (L*5) + Math.PI / 2) + h + h0) * skala1);
                    x2 = x1 + 1;
                    y2 = myPictureBox.Height - Convert.ToInt16((H*Math.Sin(2 * Math.PI * x2 / (L * 5) + Math.PI / 2) + h + h0) * skala1);
                    graph.DrawLine(Pens.Blue, x1, y1, x2, y2);
                }
            }
            else if (faza == faza_fali.dno)
            {
                while (x2 < myPictureBox.Width)
                {
                    x1 += 1;
                    y1 = myPictureBox.Height - Convert.ToInt16((H * Math.Sin(2 * Math.PI * x1 / (L * 5) - Math.PI / 2) + h + h0) * skala1);
                    x2 = x1 + 1;
                    y2 = myPictureBox.Height - Convert.ToInt16((H * Math.Sin(2 * Math.PI * x2 / (L * 5) - Math.PI / 2) + h + h0) * skala1);
                    graph.DrawLine(Pens.Blue, x1, y1, x2, y2);
                }
            }
        }
        public double srodekCiezkosci(double A, double p2, double p3)
        {
            double As = 0;
            double r = 0;
            while (As < A / 2)
            {
                r += h / 20;
                As = (p3 + (p3 + r * (p2 - p3) / h)) * r * 0.5;
            }
            return r;
        }
        public abstract double cisnieniePoziomSpokoju();
        public abstract double cisnieniePoziomDnaFali();
        public abstract double cisnieniePoziomDnaMorskiego(faza_fali faza);
        public abstract double cisnienieCalkowite(faza_fali faza);  

    }
}
