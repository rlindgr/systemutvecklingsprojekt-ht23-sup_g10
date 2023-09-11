using System.Windows;
using System.Windows.Controls;

namespace YourNamespace
{
    public partial class CustomMessageBox : Window
    {
        // Property som håller information om vilken typ av motståndare och antal i rad som användaren har valt
        public bool IsComputerOpponent { get; private set; }
        public int WinLength { get; private set; }

        // Konstruktor
        public CustomMessageBox()
        {
            InitializeComponent();
        }

        // Eventhandler för klick på "Vän"-knappen
        private void FriendButton_Click(object sender, RoutedEventArgs e)
        {
            // Ställ in att motståndaren inte är en dator
            IsComputerOpponent = false;

            // Kalla på metoden för att stänga dialogrutan och sätta DialogResult
            OnOkClicked();
        }

        // Eventhandler för klick på "AI"-knappen
        private void AIButton_Click(object sender, RoutedEventArgs e)
        {
            // Ställ in att motståndaren är en dator
            IsComputerOpponent = true;

            // Kalla på metoden för att stänga dialogrutan och sätta DialogResult
            OnOkClicked();
        }

        // Metod för att ställa in DialogResult och stänga dialogrutan
        private void OnOkClicked()
        {
            // Kontrollerar om användaren har valt något alternativ för antal i rad
            if (WinLengthComboBox.SelectedItem == null)
            {
                // Visar en varningsmeddelande om ingen val har gjorts
                MessageBox.Show("Var god välj hur många i rad du vill tävla till", "Varning", MessageBoxButton.OK, MessageBoxImage.Warning);

                return; // Avslutar metoden här, dialogrutan kommer att förbli öppen
            }

            // Ställ in WinLength från det valda alternativet i ComboBox
            WinLength = int.Parse(((ComboBoxItem)WinLengthComboBox.SelectedItem).Content.ToString());

            // Ställ in DialogResult för att indikera att dialogrutan kan stängas
            DialogResult = true;
            Close();
        }

    }
}
