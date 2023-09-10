using System.Windows;

namespace TicTacToe.Views
{
    public partial class ExitGameAnnouncement : Window
    {
        public ExitGameAnnouncement(string text)
        {
            InitializeComponent();
            this.Left = 600;  // x-koordinat
            this.Top = 300;   // y-koordinat
        }

        private void YesButton_Click(object sender, RoutedEventArgs e)
        {
            // Logik för att stänga programmet
            Application.Current.Shutdown();
        }

        private void NoButton_Click(object sender, RoutedEventArgs e)
        {
            // Logik för att inte stänga programmet och återgå till spelet
            this.Close();
        }
    }
}
