using System.Windows;
namespace TicTacToe;

public partial class InvalidInput : Window
{
    public InvalidInput(string inputText)
    {
        InitializeComponent();
        InvalidInputText.Text = inputText;
        this.Left = 600;  // x-koordinat
        this.Top = 300;   // y-koordinat
    }


    private void ExitButton_Click(object sender, RoutedEventArgs e)
    {
        // Logik för att stänga programmet
        Application.Current.Shutdown();
    }
}
