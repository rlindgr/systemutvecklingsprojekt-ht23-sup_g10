using FiveInARow;
using System;
using System.Windows;
using YourNamespace;
// Lägg till ytterligare using-deklarationer här, om det behövs

namespace TicTacToe.ViewModels
{
    public class GameSettingsViewModel:BaseViewModel
    {
        public string AlgorithmType;
        public bool IsComputerOpponent;
        public int WinLength;
        public bool IsPlayerXTurn;
        public string CurrentPlayer; 

        public static readonly string Hard = "Hard";
        public static readonly string Medium = "Medium";
        public static readonly string Easy = "Easy";

        public static readonly string PlayerX = "X";
        public static readonly string PlayerO = "O";
        public GameSettingsViewModel()
        {
            
        }
     
        public void InitializeGame()
        {
            var MainMenuView = new MainMenuView();
            if (MainMenuView.ShowDialog() == true)
            {
                if (MainMenuView.EasyRadio.IsChecked == true)
                {
                    AlgorithmType = Easy;
                }
                else if (MainMenuView.MediumRadio.IsChecked == true)
                {
                    AlgorithmType = Medium;
                }
                else if (MainMenuView.HardRadio.IsChecked == true)
                {
                    AlgorithmType = Hard;
                }

                IsComputerOpponent = MainMenuView.IsComputerOpponent;
                WinLength = MainMenuView.WinLength;

                // Resten av koden för att initialisera spelet
                Random random = new Random();
                int whoStarts = random.Next(0, 2);
                string startingPlayerMessage;

                if (whoStarts == 0)
                {
                    IsPlayerXTurn = true;
                    CurrentPlayer = PlayerX;
                    startingPlayerMessage = "Spelare 1 börjar spelet";
                }
                else
                {
                    IsPlayerXTurn = false;
                    CurrentPlayer = PlayerO;
                    startingPlayerMessage = "Spelare 2 börjar spelet";

                    if (IsComputerOpponent)
                    {
                        CurrentPlayer = PlayerO;
                        startingPlayerMessage = "Datorn börjar spelet";

                    }
                }

                FirstMoveAnnouncer announcer = new FirstMoveAnnouncer(startingPlayerMessage);
                announcer.Show();
            }
            else
            {
                string invalidInput = "Inget val gjordes. Spelet avslutas!";
                InvalidInput invalidInputBox = new InvalidInput(invalidInput);
                invalidInputBox.ShowDialog();
            }
        }
    }
}
