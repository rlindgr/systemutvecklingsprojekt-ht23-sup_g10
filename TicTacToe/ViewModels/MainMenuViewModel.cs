using FiveInARow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TicTacToe.Enums;
using TicTacToe.Settings;

namespace TicTacToe.ViewModels
{
    public partial class MainMenuViewModel: BaseViewModel
    {
        public static bool IsComputerOpponent { get; set; }
        public static int WinLength { get; private set; }
        public static Algorithm Algorithm { get; set; }

        public MainMenuViewModel()
        {
            AlgorithmOptions = Enum.GetValues(typeof(Algorithm)).Cast<Algorithm>().ToList();
            SelectedAlgorithm = GameSettings.Algorithm;
            WinLengthOptions = new List<int> { 3, 4, 5 };
            
            //IsComputerOpponent = GameSettings.IsComputerOpponent;

        } 
        public List<int> WinLengthOptions { get; }
        private int _selectedWinLength;
        public int SelectedWinLength
        {

            get => _selectedWinLength;
            set
            {
                if (_selectedWinLength != value)
                {

                    _selectedWinLength = value;
                    WinLength = _selectedWinLength;
                    OnPropertyChanged(nameof(SelectedWinLength));
                    UpdateWinLenght();

                }
            }
        }
       

        public List<Algorithm> AlgorithmOptions { get; }
        private Algorithm _selectedAlgorithm;
        public Algorithm SelectedAlgorithm
        {
            get => _selectedAlgorithm;
            set
            {
                if (_selectedAlgorithm != value)
                {
                    _selectedAlgorithm = value;
                    Algorithm = _selectedAlgorithm;
                    OnPropertyChanged(nameof(SelectedAlgorithm));
                    UpdateAlgorithm();
                }
            }
        }

        
        private bool _selectedOpponent;
        public bool SelectedOpponent
        {

            get => _selectedOpponent; 
            set
            {
                if (_selectedOpponent != value)
                {
                    _selectedOpponent = value;
                    IsComputerOpponent = !_selectedOpponent;
                    OnPropertyChanged(nameof(SelectedOpponent));
                    UpdateOpponent();
                }
            }
        }

        public static void UpdateWinLenght()
        {
            GameSettings.WinLength = WinLength;
        }

        public static void UpdateAlgorithm()
        {
            GameSettings.Algorithm = Algorithm;
        }
        public static void UpdateOpponent()
        {
            GameSettings.IsComputerOpponent = IsComputerOpponent;
        }
    }
}
