using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace ChessLogic
{
    public class GameState
    {
        public Tabla Tabla {  get; }
        public Jucator CurrentPlayer { get; private set; }
        public Rezultat Rezultat { get; private set; }
        public static bool PoateFiMutat {  get; private set; }
        public bool PoateCaptura {  get; private set; }
        public Pion pion;
        public Pozitie pozitie;




        public GameState(Jucator player, Tabla tabla)
        {
            CurrentPlayer = player;
            Tabla = tabla;
        }

        public IEnumerable<Move> MutariLegale(Pozitie pos)
        {
            if(Tabla.Liber(pos) || Tabla[pos].Culoare !=CurrentPlayer)
            {
                return Enumerable.Empty<Move>();
            }

            Piesa piesa = Tabla[pos];
            return piesa.GetMoves(pos, Tabla);
        }

        public void MakeMove(Move move)
        {
            move.Execute(Tabla);
            CurrentPlayer = CurrentPlayer.Adversar();
            VerificareStopJoc();
            
        }

        private void VerificareStopJoc()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    Pozitie pos = new Pozitie(i, j);

                    if (!Tabla.Liber(pos) && (pos.Rand == 3 || pos.Rand == 0))
                    {
                        Piesa piesa = Tabla[pos];
                        if (piesa.AFostMutat == true)
                        {
                            if (!PoateFiMutat && !PoateCaptura)
                            {
                                Rezultat = Rezultat.Win(CurrentPlayer.Adversar());
                            }
                        }
                    }
                }
            }
        }
        public bool StopJoc()
        {
            return Rezultat != null;
        }
    }
}
