using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ChessLogic;

namespace ChessUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Image[,] pieceImages = new Image[4, 4];
        private readonly Rectangle[,] highlights = new Rectangle[4, 4];
        private readonly Dictionary<Pozitie, Move> moveCache = new Dictionary<Pozitie, Move>();

        private GameState gameState;
        private Pozitie selectedPos = null;
        private Pion pion;


        public MainWindow()
        {
            InitializeComponent();
            InitializeBoard();

            gameState = new GameState(Jucator.Alb, Tabla.Initiala());
            DrawBoard(gameState.Tabla);
        }

        private void InitializeBoard()
        {
            for (int i = 0; i<4;i++)
            {
                for (int j = 0; j < 4;j++)
                {
                    Image image = new Image();
                    pieceImages[i,j] = image;
                    PieceGrid.Children.Add(image);

                    Rectangle highlight = new Rectangle();
                    highlights[i, j] = highlight;
                    HighLightGrid.Children.Add(highlight);
                }
            }
        }

        private void DrawBoard (Tabla tabla)
        {
            for(int i =0; i<4;i++)
            {
                for(int j =0;j<4;j++)
                {
                    Piesa piesa = tabla[i, j];
                    pieceImages[i, j].Source = Imagini.GetImage(piesa);
                }
            }
        }

        private void Boardgrid_MouseDown(object sender, MouseButtonEventArgs e)
        {

            if (IsMenuOnScreen())
            {
                return;
            }

            Point point = e.GetPosition(Boardgrid);
            Pozitie pos = ToSquarePosition(point);

            if(selectedPos == null)
            {
                OnFrontPositionSelected(pos);
            }
            else
            {
                OnToPositionSelected(pos);
            }
        }


        private Pozitie ToSquarePosition(Point point)
        {
            double squareSize = Boardgrid.ActualWidth / 4;
            int Row = (int)(point.Y/squareSize);
            int Column = (int)(point.X/squareSize);
            return new Pozitie(Row, Column);
        }

        private void CacheMoves(IEnumerable<Move> moves)
        {
            moveCache.Clear();
            foreach (Move move in moves)
            {
                moveCache[move.ToPos] = move;
            }
        }

        private void OnFrontPositionSelected(Pozitie pos)
        {
            IEnumerable<Move> moves = gameState.MutariLegale(pos);

            if (moves.Any())
            {
                selectedPos = pos;
                CacheMoves(moves);
                ShowHighlights();
            }
        }

        private void OnToPositionSelected(Pozitie pos)
        {
            selectedPos = null;
            HideHighlights();
            if(moveCache.TryGetValue(pos, out Move move))
            {
                HandleMove(move);
            }

        }

        private void HandleMove(Move move)
        {
            gameState.MakeMove(move);
            DrawBoard(gameState.Tabla);

            if (gameState.StopJoc())
            {
                ShowGameOver();
            }
        }

        private void ShowHighlights()
        {
            Color color = Color.FromArgb(150, 125, 255, 125);

            foreach(Pozitie to in moveCache.Keys)
            {
                highlights[to.Rand, to.Coloana].Fill = new SolidColorBrush(color);
            }
        }

        private void HideHighlights()
        {
            foreach(Pozitie to in moveCache.Keys)
            {
                highlights[to.Rand, to.Coloana].Fill = Brushes.Transparent;
            }
        }

        private bool IsMenuOnScreen()
        {
            return MenuContainer.Content != null;
        }

        private void ShowGameOver()
        {
            GameOverMenu gameOverMenu = new GameOverMenu(gameState, pion);
            MenuContainer.Content = gameOverMenu;

            gameOverMenu.OptionSelected += option =>
            {
                if (option == Option.Restart)
                {
                    MenuContainer.Content = null;
                    RestartGame();
                }
                else
                {
                    Application.Current.Shutdown();
                }
            };
        }

        private void RestartGame()
        {
            HideHighlights();
            moveCache.Clear();
            gameState = new GameState(Jucator.Alb, Tabla.Initiala());
            DrawBoard(gameState.Tabla);
        }
    }
}