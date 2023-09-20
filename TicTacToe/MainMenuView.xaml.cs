using System.Windows;
using System.Windows.Controls;

namespace YourNamespace
{
    public partial class MainMenuView : Window
    {
        // Property som håller information om vilken typ av motståndare och antal i rad som användaren har valt
        public bool IsComputerOpponent { get; private set; }
        public int WinLength { get; private set; }

        // Konstruktor
        public MainMenuView()
        {
            InitializeComponent();

        }


        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            if (rb.Content.ToString() == "Vän")
            {
                IsComputerOpponent = false;
            }
            else if (rb.Content.ToString() == "AI")
            {
                IsComputerOpponent = true;
            }

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
