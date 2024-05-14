using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLogic
{
    public class Rezultat
    {
        public Jucator Castigator {  get; }
        public EndReason Motiv { get; }

        public Rezultat(Jucator Castigator, EndReason motiv)
        {
            this.Castigator = Castigator;
            Motiv = motiv;
        }

        public static Rezultat Win(Jucator castigator)
        {
            return new Rezultat(castigator, EndReason.Capat);

        }
    }
}
