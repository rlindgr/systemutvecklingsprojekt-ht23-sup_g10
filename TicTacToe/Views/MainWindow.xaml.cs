using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TicTacToe.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Closing += MainWindow_Closing;
        }


        private void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            // Skapar och visar den anpassade dialogrutan
            ExitGameAnnouncement dialog = new ExitGameAnnouncement("Vill du verkligen avsluta?");
            dialog.ShowDialog();  // Detta gör att dialogrutan blir modal

            // Om dialogrutan stängdes med 'Nej'-knappen kommer DialogResult vara null eller false
            if (dialog.DialogResult != true)
            {
                // Avbryt stängning av programmet
                e.Cancel = true;
            }
        }




    }


}
