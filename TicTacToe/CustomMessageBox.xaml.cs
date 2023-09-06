using System.Windows;

namespace YourNamespace
{
    public partial class CustomMessageBox : Window
    {
        public bool IsComputerOpponent { get; private set; }

        public CustomMessageBox()
        {
            InitializeComponent();
        }

        private void FriendButton_Click(object sender, RoutedEventArgs e)
        {
            IsComputerOpponent = false;
            this.DialogResult = true;
            this.Close();
        }

        private void AIButton_Click(object sender, RoutedEventArgs e)
        {
            IsComputerOpponent = true;
            this.DialogResult = true;
            this.Close();
        }
    }
}
