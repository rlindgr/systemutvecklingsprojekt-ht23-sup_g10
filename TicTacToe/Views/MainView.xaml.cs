using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TicTacToe.Views
{
    /// <summary>
    /// Interaction logic for StartView.xaml
    /// </summary>
    public partial class StartView : UserControl
    {
        public StartView()
        {
            InitializeComponent();
            radioButtonComputer.IsChecked = true;
            WinLengthComboBox.SelectedItem = 3;
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
            if (radioButtonComputer.IsChecked == true)
            {
                txtBlockDifficulty.Opacity = 1.0;
                cmbBoxDifficulty.IsEnabled = true;
                cmbBoxDifficulty.Opacity = 1.0;
            }

        }
    }
}
