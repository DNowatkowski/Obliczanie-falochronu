using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApplication1
{
    class fala_sa:fala_szablon  // klasa licząca ciśnieie metodą Sainflou
    {
       
        public fala_sa(double glebokosc, double wysokosc_fali, double okres_fali,double wsp_odbicia) : base(glebokosc, wysokosc_fali, okres_fali,wsp_odbicia)
        {  
            dysp = false;         
            h0 = Math.Pow(H, 2) * Math.PI / (L * Math.Tanh(k * h));
            Kr = 1;
        }
        public override double cisnieniePoziomSpokoju()
        {                      
            return (H + h0) / (h + h0 + H) * (h + H / Math.Cosh(k * h));              
        }

        public override double cisnieniePoziomDnaMorskiego(faza_fali faza)
        {
            if(faza==faza_fali.szczyt)
                return H / Math.Cosh(k * h);
            else if (faza==faza_fali.dno)
                return -H / Math.Cosh(k * h);
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
                
            else if (faza==faza_fali.dno)
                return base.cisnienieCalkowite(faza_fali.dno, cisnieniePoziomDnaFali(), cisnieniePoziomDnaMorskiego(faza_fali.dno));
            
            return 0;
        }

    }
}
