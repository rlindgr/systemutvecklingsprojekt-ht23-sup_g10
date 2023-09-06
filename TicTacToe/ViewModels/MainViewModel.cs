using System.Collections.ObjectModel;
using System.Windows.Input;

namespace FiveInARow
{
    public class MainViewModel
    {
        public ObservableCollection<Cell> Cells { get; set; }
        public ICommand CellClickCommand { get; set; }

        private bool IsPlayerXTurn { get; set; } = true;  

        public MainViewModel()
        {
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
                cell.Value = IsPlayerXTurn ? "X" : "O";  
                IsPlayerXTurn = !IsPlayerXTurn;  

                // Implementera logik för att kolla om någon har vunnit
            }
        }
    }
}
