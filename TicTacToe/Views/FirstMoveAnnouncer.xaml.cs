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
        }

        public FirstMoveAnnouncer(string message)
        {
            InitializeComponent();
            // Sätt texten i Label till det meddelande som skickats in
            // (Förutsatt att Label har en Name attribut, ex: x:Name="MessageLabel")
            MessageLabel.Content = message;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DoubleAnimation fadeOutAnimation = new DoubleAnimation(1, 0, new Duration(TimeSpan.FromSeconds(2)));
            fadeOutAnimation.Completed += FadeOutAnimation_Completed;
            RootGrid.BeginAnimation(OpacityProperty, fadeOutAnimation);
        }

        private void FadeOutAnimation_Completed(object sender, EventArgs e)
        {
            Close();
        }
    }
}
