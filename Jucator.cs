namespace ChessLogic
{
    public enum Jucator
    {
        Niciunul,
        Alb,
        Negru
    }

    public static class PlayerExtensions
    {
        public static Jucator Adversar(this Jucator player)
        {
            return player switch
            {
                Jucator.Alb => Jucator.Negru,
                Jucator.Negru => Jucator.Alb,
                _ => Jucator.Niciunul,
            };
        }
    }
}
