using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApplication1  
{
    class fala_g:fala_szablon  // klasa licząca ciśnieie metodą Gody
    {
        private double a1;
        private double a2;
        private double a3;
        private double n;

        public fala_g(double glebokosc, double wysokosc_fali, double okres_fali, double wsp_odbicia):base(glebokosc, wysokosc_fali, okres_fali,wsp_odbicia)
        {
            a1 = 0.6 + 0.5 * Math.Pow(((4 * Math.PI * h /L)/ (Math.Sinh(4 * Math.PI * h/L))),2);
            a2 = 0;
            a3 = 1 - 1*(1 - 1 / Math.Cosh(2 * Math.PI * h / L));
            n = 0.75 * (1 + 1) * H;
        }

        public override double cisnieniePoziomSpokoju()
        {
            return 0.5 * (1 + 1) * (a1 + a2) * H;
        }

        public double cisnieniePoziomDnaMorskiego()
        {      
            return a3 * cisnieniePoziomSpokoju();
        }

        public override double cisnieniePoziomDnaMorskiego(faza_fali faza)
        {
            return cisnieniePoziomDnaMorskiego();
        }

        public override double cisnieniePoziomDnaFali()
        {
            return 0;
        }

        public double cisnienieCalkowite()
        {
            return 0.5 * cisnieniePoziomSpokoju() * (n) + (cisnieniePoziomSpokoju() + cisnieniePoziomDnaMorskiego()) * h * 0.5;
        }

        public override double cisnienieCalkowite(faza_fali faza)
        {
            return cisnienieCalkowite();
        }
        public double srodekCiezkosci()
        {
            double As = 0;
            double r = 0;
            while (As < cisnienieCalkowite() / 2)                  //położenie środka ciężkości
            {
                r += + h / 20;
                As = (cisnieniePoziomDnaMorskiego() + (cisnieniePoziomDnaMorskiego() + r * (cisnieniePoziomSpokoju() - cisnieniePoziomDnaMorskiego()) / h)) * r * 0.5;
            }
            return r;
        }
    }
}
