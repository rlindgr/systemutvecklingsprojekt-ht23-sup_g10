using System.Windows;

namespace TicTacToe.Views
{
    // En delvis klass som representerar fönstret MainMenuClick
    public partial class MainMenuClick : Window
    {
        // Konstruktor som tar emot en textsträng som argument
        public MainMenuClick(string text)
        {
            InitializeComponent();  // Inledande setup för WPF-kontroller
            MainMenuText.Text = text;  // Sätter texten för en TextBlock i XAML till den inkommande texten
            this.Left = 600;  // Placerar fönstret på x-koordinat 600
            this.Top = 300;   // Placerar fönstret på y-koordinat 300
        }

        // En egenskap för att lagra användarens val (Ja/Nej)
        public MessageBoxResult Result { get; set; }

        // Eventhandler för knappen "Spela igen"
        private void PlayAgainButton_Click(object sender, RoutedEventArgs e)
        {
            // Sätter egenskapen 'Result' till MessageBoxResult.Yes och stänger dialogen
            Result = MessageBoxResult.Yes;
            this.Close();  // Stänger fönstret
        }

        // Eventhandler för knappen "Avsluta"
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            // Sätter egenskapen 'Result' till MessageBoxResult.No och stänger dialogen
            Result = MessageBoxResult.No;
            this.Close();  // Stänger fönstret
        }
    }
}
