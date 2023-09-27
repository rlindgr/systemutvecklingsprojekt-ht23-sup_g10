using FiveInARow;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TicTacToe.Commands;
using TicTacToe.Enums;
using TicTacToe.Models;
using TicTacToe.Settings;

namespace TicTacToe.ViewModels
{
    public partial class MainMenuViewModel: BaseViewModel
    {
        
        public static bool IsComputerOpponent { get; set; }
        public static int WinLength { get; private set; }
        public static Algorithm Algorithm { get; set; }
        public ICommand StartGameCommand { get; private set; }
        
        public MainMenuViewModel()
        {
            AlgorithmOptions = Enum.GetValues(typeof(Algorithm)).Cast<Algorithm>().ToList();
            SelectedAlgorithm = GameSettings.Algorithm;
            WinLengthOptions = new List<int> { 3, 4, 5 };
            StartGameCommand = new RelayCommand(x => StartGame());
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

        

        private bool _computerSelected;
        private bool _humanSelected;
        public bool ComputerSelected
        {
            get => _computerSelected; 
            set
            {
                if (_computerSelected != value)
                {
                    _computerSelected = value;
                    IsComputerOpponent = _computerSelected;
                    OnPropertyChanged(nameof(ComputerSelected));
                    UpdateOpponent();
                }
            }
        }
        public bool HumanSelected
        {
            get => _humanSelected; 
            set
            {
                if (_humanSelected != value)
                {
                    _humanSelected = value;
                    IsComputerOpponent = !_humanSelected;
                    OnPropertyChanged(nameof(HumanSelected));
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

        public delegate void StartGameEventHandler(object sender, EventArgs e);
        public event StartGameEventHandler OnStartGame;
        public event PropertyChangedEventHandler PropertyChanged;


        private void StartGame()
        {
            MainViewModel.Instance.CurrentViewModel = new GameViewModel();
        }
       
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
