using System.Windows;
namespace TicTacToe;
public partial class WinnerAnnouncement : Window
{
    public WinnerAnnouncement(string winnerText)
    {
        InitializeComponent();
        WinnerText.Text = winnerText;
        this.Left = 100;  // x-koordinat
        this.Top = 10;   // y-koordinat
    }
    public MessageBoxResult Result { get; set; }

    private void PlayAgainButton_Click(object sender, RoutedEventArgs e)
    {
        // Sätt Result till Yes och stäng dialogen
        Result = MessageBoxResult.Yes;
        this.Close();
    }

    private void ExitButton_Click(object sender, RoutedEventArgs e)
    {
        // Sätt Result till No och stäng dialogen
        Result = MessageBoxResult.No;
        this.Close();
    }
}
