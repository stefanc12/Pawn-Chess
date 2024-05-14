using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLogic
{
    public class Tabla
    {
        private readonly Piesa[,] piese = new Piesa[4, 4];

        public Piesa this[int rand, int col]
        {
            get { return piese[rand, col]; }
            set { piese[rand, col] = value; }
        }

        public Piesa this[Pozitie pos]
        {
            get { return this[pos.Rand, pos.Coloana]; }
            set { this[pos.Rand, pos.Coloana] = value; }
        }

        public static Tabla Initiala()
        {
            Tabla tabla = new Tabla();
            tabla.AddStartPieces();
            return tabla;
        }
        private void AddStartPieces()
        {
            this[0, 0] = new Pion(Jucator.Negru);
            this[0, 1] = new Pion(Jucator.Negru);
            this[0, 2] = new Pion(Jucator.Negru);
            this[0, 3] = new Pion(Jucator.Negru);

            this[3, 0] = new Pion(Jucator.Alb);
            this[3, 1] = new Pion(Jucator.Alb);
            this[3, 2] = new Pion(Jucator.Alb);
            this[3, 3] = new Pion(Jucator.Alb);
        }

        public static bool IsInside(Pozitie pos)
        {
            return pos.Rand >= 0 && pos.Rand < 4 && pos.Coloana >= 0 && pos.Coloana < 4;
        }

        public bool Liber(Pozitie pos)
        {
            return this[pos] == null;
        }
    }
}
