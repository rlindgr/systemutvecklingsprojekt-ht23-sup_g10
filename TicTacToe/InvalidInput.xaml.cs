// Importera nödvändiga bibliotek
using System.Windows;

// Namespace, samma som i XAML
namespace TicTacToe
{
    // Definiera klassen, som är en delvis klass. Andra delen är definierad i XAML.
    public partial class InvalidInput : Window
    {
        // Konstruktor som tar emot en sträng för att sätta texten i InvalidInputText
        public InvalidInput(string inputText)
        {
            // Initialisera de grafiska komponenterna, en metod genererad av XAML
            InitializeComponent();

            // Sätter texten för textblocket med namnet InvalidInputText till inputText
            InvalidInputText.Text = inputText;

            // Placering av fönstret i skärmen
            this.Left = 600;  // x-koordinat
            this.Top = 300;   // y-koordinat
        }

        // Hanterar klick-event för knappen med namnet ExitButton
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            // Kod för att stänga hela applikationen
            Application.Current.Shutdown();
        }
    }
}
