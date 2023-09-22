using System;
using System.Windows;
using System.Windows.Media.Animation;

namespace YourNamespace // namespace för projekt
{
    public partial class FirstMoveAnnouncer : Window // Klassdefinition som ärver från WPF:s Window-klass
    {
        // Standardkonstruktor
        public FirstMoveAnnouncer()
        {
            InitializeComponent(); // Initialiserar GUI-komponenter
            InitializeWindowPosition(); // Positionerar fönstret
        }

        // Överbelastad konstruktor som tar emot ett meddelande
        public FirstMoveAnnouncer(string message)
        {
            InitializeComponent(); // Initialiserar GUI-komponenter
            InitializeWindowPosition(); // Positionerar fönstret

            this.Topmost = true; // Gör fönstret till det översta fönstret
            MessageLabel.Content = message; // Sätter texten för MessageLabel till det inkommande meddelandet
        }

        // Metod för att positionera fönstret
        private void InitializeWindowPosition()
        {
            this.Left = 800;  // Sätter x-koordinaten för fönstret
            this.Top = 300;   // Sätter y-koordinaten för fönstret
        }

        // Metod som triggas när fönstret har laddats
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Skapar en ny DoubleAnimation för att gradvis ändra fönstrets opacitet från 1 till 0 på 2 sekunder
            DoubleAnimation fadeOutAnimation = new DoubleAnimation(1, 0, new Duration(TimeSpan.FromSeconds(2)));

            // Lägger till en händelsehanterare som triggas när animationen är klar
            fadeOutAnimation.Completed += FadeOutAnimation_Completed;

            // Startar animationen på RootGrid-elementet
            RootGrid.BeginAnimation(OpacityProperty, fadeOutAnimation);
        }

        // Metod som kallas när animationen är färdig
        private void FadeOutAnimation_Completed(object sender, EventArgs e)
        {
            Close(); // Stänger fönstret
        }
    }
}
