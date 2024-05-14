using System.Windows.Media;
using System.Windows.Media.Imaging;
using ChessLogic;

namespace ChessUI
{
    public static class Imagini
    {
        private static readonly Dictionary<PieceType, ImageSource> whiteSources = new()
        {
            {PieceType.Pion, LoadImage("Assets/PawnW.png") }
        };
        private static readonly Dictionary<PieceType, ImageSource> blackSources = new()
        {
            {PieceType.Pion, LoadImage("Assets/PawnB.png") }
        };
        private static ImageSource LoadImage(string filePath)
        {
            return new BitmapImage(new Uri(filePath, UriKind.Relative));
        }

        public static ImageSource GetImage(Jucator color, PieceType type)
        {
            return color switch
            {
                Jucator.Alb => whiteSources[type],
                Jucator.Negru => blackSources[type],
                _ => null
            };
        }

        public static ImageSource GetImage(Piesa piece)
        {
            if (piece == null)
            {
                return null;
            }

            return GetImage(piece.Culoare, piece.Type);
        }
    }
}
