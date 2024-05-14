namespace ChessLogic
{
    public class Pozitie
    {
        public int Rand { get; }
        public int Coloana { get; }

        public Pozitie(int rand, int coloana)
        {
            Rand = rand;
            Coloana = coloana;
        }

        public Jucator SquareColor()
        {
            if ((Rand + Coloana) % 2 == 0)
            {
                return Jucator.Alb;
            }
            else
            {
                return Jucator.Negru;
            }
        }

        public override bool Equals(object obj)
        {
            return obj is Pozitie pozitie &&
                   Rand == pozitie.Rand &&
                   Coloana == pozitie.Coloana;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Rand, Coloana);
        }

        public static bool operator ==(Pozitie stanga, Pozitie dreapta)
        {
            return EqualityComparer<Pozitie>.Default.Equals(stanga, dreapta);
        }

        public static bool operator !=(Pozitie stanga, Pozitie dreapta)
        {
            return !(stanga == dreapta);
        }

        public static Pozitie operator +(Pozitie pos, Direction dir)
        {
            return new Pozitie(pos.Rand + dir.RowDelta, pos.Coloana + dir.ColumnDelta);
        }
    }
}
