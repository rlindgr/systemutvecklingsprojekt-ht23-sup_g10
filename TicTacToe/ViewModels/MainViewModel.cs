using FiveInARow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TicTacToe.Commands;
using TicTacToe.Models;
using TicTacToe.Settings;

namespace TicTacToe.ViewModels
{
    internal sealed class MainViewModel:BaseViewModel
    {
       private static MainViewModel? _instance;
        public BaseViewModel CurrentViewModel { get; set; } = new MainMenuViewModel();
        public static MainViewModel Instance { get => _instance ?? (_instance = new MainViewModel()); }
        public ICommand ChangeGameCommand { get; }
        public MainViewModel()
        {
         ChangeGameCommand = new RelayCommand(page => ChangePage());
        }
        private void ChangePage()
        {
            CurrentViewModel = new GameViewModel();
        }
    }
}
