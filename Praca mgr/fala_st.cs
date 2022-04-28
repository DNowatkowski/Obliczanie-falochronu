using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    class fala_st:fala_szablon  // klasa licząca ciśnieie metodą Stokesa
    {

        private Graphics graph;
        public fala_st(double glebokosc, double wysokosc_fali, double okres_fali, double wsp_odbicia) : base(glebokosc, wysokosc_fali, okres_fali,wsp_odbicia)
        {
            h0 = k * Math.Pow(H, 2) / 8 * (3 + Math.Pow(Math.Tanh(k * h), 4)) / Math.Pow(Math.Tanh(k * h), 3);                             
        }

        public double[,] rozkladCisnienia(faza_fali faza)
        {         
            double[,] rozklad = new double[2, 9];
            Array.Clear(rozklad, 0,rozklad.Length);
            if (faza == faza_fali.szczyt)
            {
                for (int j = 0; j < rozklad.GetLength(1); j++)
                {
                    rozklad[0,j] = (double)-j / (rozklad.GetLength(1) - 1) * h;
                    rozklad[1,j] = H * Math.Cosh(k * (h + rozklad[0,j])) / Math.Cosh(k * h) - k * Math.Pow(H, 2) / 8 * (4 * Math.Tanh(k * h) + 3 * (Math.Pow(Math.Tanh(k * h), 4) - 1) / Math.Pow(Math.Tanh(k * h), 3) * Math.Cosh(2 * k * (h + rozklad[0,j])) / (Math.Cosh(2 * k * h))); //57                
                }
                
            }
            else if (faza == faza_fali.dno)
            {
                for (int j = 0; j < rozklad.GetLength(1); j++)
                {
                    rozklad[0, j] = (double)-j / (rozklad.GetLength(1) - 1) * (h + h0 - H);
                    rozklad[1, j] = -(H * Math.Cosh(k * (h + rozklad[0,j])) / Math.Cosh(k * h)) - k * Math.Pow(H, 2) / 8 * (4 * Math.Tanh(k * h) + 3 * (Math.Pow(Math.Tanh(k * h), 4) - 1) / Math.Pow(Math.Tanh(k * h), 3) * Math.Cosh(2 * k * (h + rozklad[0,j])) / (Math.Cosh(2 * k * h)));                  
                }
            }
                return rozklad;
        }

        public override double cisnieniePoziomDnaMorskiego(faza_fali faza)
        {
            return rozkladCisnienia(faza)[1, 8];
        }
        public override double cisnieniePoziomSpokoju()
        {
            return rozkladCisnienia(faza_fali.szczyt)[1, 0];
        }

        public override double cisnieniePoziomDnaFali()
        {
            return rozkladCisnienia(faza_fali.dno)[1, 0];
        }

        public override double cisnienieCalkowite(faza_fali faza)
        {
            double dz;
            double A=0;           
            double[,] rozklad = rozkladCisnienia(faza);
            if (faza == faza_fali.szczyt)
            {                                
                A = (H + h0) * rozklad[1,0] * 0.5;
                for (int k = 0; k < rozklad.GetLength(1) - 1; k++)
                {
                    dz = Math.Abs(rozklad[0, 0] - rozklad[0, 1]);
                    A += dz * (rozklad[1, k] + rozklad[1, k + 1]) / 2;
                }
            }
                   //pole powierzchni rozkładu ciśnienia
            else if (faza == faza_fali.dno) 
            {
                A = (H - h0) * (H - h0) * 0.5;
                for (int k = 0; k < rozklad.GetLength(1) - 1; k++)
                {
                    dz = Math.Abs(rozklad[0, 0] - rozklad[0, 1]);
                    A += dz * (rozklad[1, k] + rozklad[1, k + 1]) / 2;
                }
            }

            return A;
        }

        public double srodekCiezkosci(faza_fali faza)
        {
            double As = 0;
            double dz;
            double r=0;
            double[,] rozklad = rozkladCisnienia(faza);
            while (As < Math.Abs(cisnienieCalkowite(faza) / 2))       //położenie środka ciężkości
            {
                for (int l = rozklad.GetLength(1) - 1; l > 0; l--)
                {
                    dz = Math.Abs(rozklad[0, 0] - rozklad[0, 1]);
                    As += dz * Math.Abs((rozklad[1, l-1] + rozklad[1, l]) / 2);
                    r += dz;
                }
            }
            return r;
        }

        public override void rysujWykres(faza_fali faza, double p2, double p3, int skala1, int skala2)
        {
            myPictureBox.Image = new Bitmap(myPictureBox.Width, myPictureBox.Height);
            graph = Graphics.FromImage(myPictureBox.Image);
            graph.Clear(Color.White);
            double[,] rozklad = rozkladCisnienia(faza);
            Point p_1 = new Point();
            Point p_2 = new Point();

            if (faza == faza_fali.szczyt)
            {
                for (int m = 0; m < rozklad.GetLength(1) - 1; m++)
                {
                    p_1.Y = myPictureBox.Height - Convert.ToInt16(skala1 *( h + rozklad[0,m]));  
                    p_1.X = Convert.ToInt16(skala2 * rozklad[1, m]);
                    p_2.Y = myPictureBox.Height - Convert.ToInt16(skala1 * (h + rozklad[0, m+1]));
                    p_2.X = Convert.ToInt16(skala2 * rozklad[1, m+1]);
                    graph.DrawLine(Pens.Red, p_1, p_2);
                   
                }
                graph.DrawLine(Pens.Black, 0, myPictureBox.Height - Convert.ToInt16(h * skala1), myPictureBox.Width, myPictureBox.Height - Convert.ToInt16(h * skala1));
                graph.DrawLine(Pens.Red, 0, myPictureBox.Height - Convert.ToInt16((h + h0 + H) * skala1), Convert.ToInt16(rozklad[1, 0] * skala2), myPictureBox.Height - Convert.ToInt16(h * skala1));
            }

            else if(faza == faza_fali.dno)
            {
                for (int m = 0; m < rozklad.GetLength(1) - 1; m++)  
                {
                    p_1.Y = myPictureBox.Height - Convert.ToInt16(skala1 * (h + h0 - H + rozklad[0, m]));
                    p_1.X = -Convert.ToInt32(skala2 * rozklad[1, m]);
                    p_2.Y = myPictureBox.Height - Convert.ToInt16(skala1 * (h + h0 - H + rozklad[0, m+1]));
                    p_2.X = -Convert.ToInt32(skala2 * rozklad[1, m+1]);

                graph.DrawLine(Pens.Red, p_1, p_2);
                }

                graph.DrawLine(Pens.Black, 0, myPictureBox.Height - Convert.ToInt16(h * skala1), myPictureBox.Width, myPictureBox.Height - Convert.ToInt16((h) * skala1));
                graph.DrawLine(Pens.Red, 0, myPictureBox.Height - Convert.ToInt16((h) * skala1), -Convert.ToInt16(rozklad[1, 0] * skala2), myPictureBox.Height - Convert.ToInt16((h + h0 - H) * skala1));
            }
            rysujFale(faza, skala1,graph);
        }

    }


}
