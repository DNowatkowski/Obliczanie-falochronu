using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApplication1
{
    class fala_mr : fala_szablon  // klasa licząca ciśnieie metodą Miche'a - Rundgrena
    {
        public double A1;
        public double A3;
        
        public fala_mr(double glebokosc, double wysokosc_fali, double okres_fali, double wsp_odbicia) : base(glebokosc, wysokosc_fali, okres_fali,wsp_odbicia)
        {
            A1 = 1 + 3 / (4 * Math.Pow(Math.Sinh(k * h), 2)) - 1 / (4 * Math.Pow(Math.Cosh(k * h), 2));
            Kr = wsp_odbicia;
            h0 = Math.PI * Math.Pow(H, 2) / L / Math.Tanh(h * k) * A1;
            A3 = 1 - 1 / (4 * Math.Cosh(k * h)) - 2 * Math.Tanh(k * h) * Math.Sinh(k * h) + 0.75 * (Math.Cosh(k * h) / Math.Pow(Math.Sinh(k * h), 2) - 2 / Math.Cosh(k * h));
            
        }


        public override double  cisnieniePoziomSpokoju()
        {
            return (H + h0) / (h + h0 + H) * (h + H / Math.Cosh(k * h) + (Math.PI * Math.Pow(H, 2) / (L * Math.Sinh(k * h))) * A3);
        }

        public override double cisnieniePoziomDnaMorskiego(faza_fali faza)
        {
            if (faza == faza_fali.szczyt)
                return H / Math.Cosh(k * h) + (Math.PI * Math.Pow(H, 2) / (L * Math.Sinh(k * h))) * A3;
            else if (faza == faza_fali.dno)
                return -H / Math.Cosh(k * h) + Math.PI * Math.Pow(H, 2) / L / Math.Sinh(k * h) * A3;
            return 0;
        }

        public override double cisnieniePoziomDnaFali()
        {
            return -(H + h0);
        }

        public override double cisnienieCalkowite(faza_fali faza)
        {
            if (faza == faza_fali.szczyt)
                return base.cisnienieCalkowite(faza_fali.szczyt, cisnieniePoziomSpokoju(), cisnieniePoziomDnaMorskiego(faza_fali.szczyt));

            else if (faza == faza_fali.dno)
                return base.cisnienieCalkowite(faza_fali.dno, cisnieniePoziomDnaFali(), cisnieniePoziomDnaMorskiego(faza_fali.dno));

            return 0;
        }

    }

}
