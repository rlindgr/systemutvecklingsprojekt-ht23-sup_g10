using System.Windows;
using System.ComponentModel;  // För CancelEventArgs
using TicTacToe.Views;  // Lägg till detta för att använda ExitGameAnnouncement

namespace FiveInARow
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.Left = 600;  // x-koordinat
            this.Top = 100;   // y-koordinat
            InitializeComponent();

            DataContext = new GameViewModel();

            // Registrera Closing-eventet
            this.Closing += MainWindow_Closing;
        }

        // Hanterare för Closing-eventet
        private void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            // Skapar och visar den anpassade dialogrutan
            ExitGameAnnouncement dialog = new ExitGameAnnouncement("Vill du verkligen avsluta?");
            dialog.ShowDialog();  // Detta gör att dialogrutan blir modal

            // Om dialogrutan stängdes med 'Nej'-knappen kommer DialogResult vara null eller false
            if (dialog.DialogResult != true)
            {
                // Avbryt stängning av programmet
                e.Cancel = true;
            }
        }
    }
}
