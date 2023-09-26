using System.Windows;
using System.Windows.Controls;
using TicTacToe.Enums;
using TicTacToe.Settings;

namespace YourNamespace
{
    public partial class MainMenuView : Window
    {

        // Konstruktor
        public MainMenuView()
        {
            InitializeComponent();
            radioButtonComputer.IsChecked = true;
        }

        // Eventhandler för klick på "Vän"-knappen
        //private void FriendButton_Click(object sender, RoutedEventArgs e)
        //{
        //    // Ställ in att motståndaren inte är en dator
        //    //GameSettings.IsComputerOpponent = false;

        //    // Kalla på metoden för att stänga dialogrutan och sätta DialogResult
        //    OnOkClicked();
        //}

        //// Eventhandler för klick på "AI"-knappen
        //private void AIButton_Click(object sender, RoutedEventArgs e)
        //{        
        //    //GameSettings.IsComputerOpponent = true;

        //    OnOkClicked();
        //}


        // Metod för att ställa in DialogResult och stänga dialogrutan
        //private void OnOkClicked()
        //{
        //    // Kontrollerar om användaren har valt något alternativ för antal i rad
        //    if (WinLengthComboBox.SelectedItem == null)
        //    {
        //        // Visar en varningsmeddelande om ingen val har gjorts
        //        MessageBox.Show("Var god välj hur många i rad du vill tävla till", "Varning", MessageBoxButton.OK, MessageBoxImage.Warning);

        //        return; // Avslutar metoden här, dialogrutan kommer att förbli öppen
        //    }

        //    DialogResult = true;
        //    Close();
        //}

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            // Kontrollerar om användaren har valt något alternativ för antal i rad
            if (WinLengthComboBox.SelectedItem == null)
            {
                // Visar en varningsmeddelande om ingen val har gjorts
                MessageBox.Show("Var god välj hur många i rad du vill tävla till", "Varning", MessageBoxButton.OK, MessageBoxImage.Warning);

                return; // Avslutar metoden här, dialogrutan kommer att förbli öppen
            }

            DialogResult = true;
            Close();
        }




        private void radioButtonHuman_Checked(object sender, RoutedEventArgs e)
        {
            if (radioButtonHuman.IsChecked == true)
            {
                txtBlockDifficulty.Opacity = 0.2;
                cmbBoxDifficulty.IsEnabled = false;
                cmbBoxDifficulty.Opacity = 0.2;
            }
        }

        private void radioButtonComputer_Checked(object sender, RoutedEventArgs e)
        {
            if (radioButtonComputer.IsChecked ==true)
            {
                txtBlockDifficulty.Opacity = 1.0;
                cmbBoxDifficulty.IsEnabled = true;
                cmbBoxDifficulty.Opacity = 1.0;
            }

        }

    }
}
