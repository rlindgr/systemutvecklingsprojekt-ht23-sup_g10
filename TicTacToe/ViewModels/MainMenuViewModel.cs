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
    public class MainMenuViewModel: BaseViewModel
    {
        //public bool IsComputerOpponent { get; private set; }
        public static int WinLength { get; private set; }
        //public static Algorithm Algorithm { get; set; }

        public MainMenuViewModel()
        { 
            
        }

        //public static void UpdateWinLenght()
        //{
        //    GameSettings.WinLength = WinLength;
        //}
        //public static void UpdateAlgorithm()
        //{
        //    GameSettings.Algorithm = Algorithm;
        //}

        

        public int SelectedOption
        {
            get { return WinLength; }
            set
            {
                if (WinLength != value)
                {
                    WinLength = value;
                    OnPropertyChanged(nameof(SelectedOption));
                }
            }
        }
    }
}
