using ChessLogic;
using System.Windows;
using System.Windows.Controls;

namespace ChessUI
{
    /// <summary>
    /// Interaction logic for GameOverMenu.xaml
    /// </summary>
    public partial class GameOverMenu : UserControl
    {
        public event Action<Option> OptionSelected;

        public GameOverMenu(GameState gameState, Pion pion)
        {
            InitializeComponent();
            if(gameState.Rezultat!=null)
            {
                Rezultat rezultat = gameState.Rezultat;
                WinnerText.Text = GetWinnerText(rezultat.Castigator);
                ReasonText.Text = GetReasonText(rezultat.Motiv, gameState.CurrentPlayer);
            }
        }

        private static string GetWinnerText(Jucator castigator)
        {
            return castigator switch
            {
                Jucator.Alb => "Alb a castigat!",
                Jucator.Negru => "Negru a castigat!",
                _ => "REMIZA"
            };
        }

        private static string PlayerString(Jucator player)
        {
            return player switch
            {
                Jucator.Alb => "ALB",
                Jucator.Negru => "NEGRU",
                _=> ""
            };
        }

        private static string GetReasonText(EndReason reason, Jucator currentPlayer)
        {
            return reason switch
            {
                EndReason.Capat => $" {PlayerString(currentPlayer.Adversar())} A AJUNS LA CAPAT",
                _ => ""
            };
        }

        private void Restart_Click(object sender, RoutedEventArgs e)
        {
            OptionSelected?.Invoke(Option.Restart);
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            OptionSelected?.Invoke(Option.Iesire);
        }
    }
}
