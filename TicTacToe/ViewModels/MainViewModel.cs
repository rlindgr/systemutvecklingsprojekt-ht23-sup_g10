using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using YourNamespace;

namespace FiveInARow
{
    public class MainViewModel
    {
        public ObservableCollection<Cell> Cells { get; set; }
        public ICommand CellClickCommand { get; set; }

        private bool IsPlayerXTurn { get; set; } = true;
        public bool IsComputerOpponent { get; set; }


        public MainViewModel()
        {
            var customMessageBox = new CustomMessageBox();
            if (customMessageBox.ShowDialog() == true)
            {
                IsComputerOpponent = customMessageBox.IsComputerOpponent;
            }
            Cells = new ObservableCollection<Cell>();
            for (int i = 0; i < 25; i++)
            {
                Cells.Add(new Cell { Value = "-" });
            }

            CellClickCommand = new RelayCommand(CellClick);
        }

        public void CellClick(object cellObj)
        {
            if (cellObj is Cell cell && cell.Value == "-")
            {
                string currentPlayerMark = IsPlayerXTurn ? "X" : "O";
                cell.Value = IsPlayerXTurn ? "X" : "O";
                IsPlayerXTurn = !IsPlayerXTurn;


                // Implementerar kod för att kolla om någon har vunnit
                if (CheckForWin(currentPlayerMark))
                {
                    MessageBoxResult result = MessageBox.Show($"Vi har en vinnare! Spelaren med {currentPlayerMark} markören fick 5 i rad och vinner därmed matchen! Vill du spela igen?", "Vinnare!", MessageBoxButton.YesNo);

                    if (result == MessageBoxResult.Yes)
                    {
                        ResetBoard();
                    }
                    else
                    {
                        Application.Current.Shutdown();
                    }
                    return;
                }
                // ...

                // Lägger till följande kod för att kolla om brädet är fullt och även alternativ
                // för att starta om spelet eller avsluta det
                if (IsBoardFull())
                {
                    MessageBoxResult result = MessageBox.Show("Det blev oavgjort. Vill du spela igen?", "Oavgjort", MessageBoxButton.YesNo);

                    if (result == MessageBoxResult.Yes)
                    {
                        ResetBoard();
                    }
                    else
                    {
                        Application.Current.Shutdown();
                    }
                }
                if (IsComputerOpponent && !IsPlayerXTurn)
                {
                    DoComputerMove("O");
                    CheckForWin("0"); /// Kan va fel?

                    IsPlayerXTurn = !IsPlayerXTurn;
                }

            }
        }
        // Metod för att kontrollera om brädet är fullt
        private bool IsBoardFull()
        {
            foreach (var cell in Cells)
            {
                if (cell.Value == "-")
                {
                    return false;
                }
            }
            return true;
        }
        // Metod för att återställa brädet
        private void ResetBoard()
        {
            foreach (var cell in Cells)
            {
                cell.Value = "-";
            }
            IsPlayerXTurn = true;
        }
        //Metoder för att kontrollera om någon har vunnit i någon riktning
        private bool CheckForWin(string mark)
        {
            for (int row = 0; row < 5; row++)
            {
                for (int col = 0; col < 5; col++)
                {
                    if (
                        CheckDirection(row, col, 0, 1, mark) || // Horisontell
                        CheckDirection(row, col, 1, 0, mark) || // Vertikal
                        CheckDirection(row, col, 1, 1, mark) || // Diagonal höger
                        CheckDirection(row, col, 1, -1, mark)   // Diagonal vänster
                       )
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        private bool CheckDirection(int startRow, int startCol, int dRow, int dCol, string mark)
        {
            for (int i = 0; i < 5; i++)
            {
                int row = startRow + dRow * i;
                int col = startCol + dCol * i;

                if (row < 0 || row >= 5 || col < 0 || col >= 5)
                {
                    return false;
                }

                if (Cells[row * 5 + col].Value != mark)
                {
                    return false;
                }
            }
            return true;
        }
        private void DoComputerMove(string mark)
        {
            // PC kommer välja den första lediga cellen
            foreach (var cell in Cells)
            {
                if (cell.Value == "-")
                {
                    cell.Value = mark;
                    break;
                }
            }
        }
        



    }
}
