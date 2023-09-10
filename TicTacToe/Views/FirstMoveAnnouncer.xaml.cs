using System;
using System.Windows;
using System.Windows.Media.Animation;

namespace YourNamespace
{
    public partial class FirstMoveAnnouncer : Window
    {
        public FirstMoveAnnouncer()
        {
            InitializeComponent();
            InitializeWindowPosition();
        }

        public FirstMoveAnnouncer(string message)
        {
            InitializeComponent();
            InitializeWindowPosition();
            // Sätt texten i Label till det meddelande som skickats in
            // (Förutsatt att Label har en Name attribut, ex: x:Name="MessageLabel")
            MessageLabel.Content = message;
        }

        private void InitializeWindowPosition()
        {
            this.Left = 800;  // x-koordinat
            this.Top = 300;   // y-koordinat
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DoubleAnimation fadeOutAnimation = new DoubleAnimation(1, 0, new Duration(TimeSpan.FromSeconds(3)));
            fadeOutAnimation.Completed += FadeOutAnimation_Completed;
            RootGrid.BeginAnimation(OpacityProperty, fadeOutAnimation);
        }

        private void FadeOutAnimation_Completed(object sender, EventArgs e)
        {
            Close();
        }
    }
}
