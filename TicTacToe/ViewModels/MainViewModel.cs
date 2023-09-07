using System;
using System.Collections.Generic; // Importera för List<T>
using System.Linq;
using System.Collections.ObjectModel; // För ObservableCollection  
using System.Windows; // För MessageBox och andra WPF-komponenter
using System.Windows.Input;//För Icommand   
using YourNamespace;

namespace FiveInARow // Namn på projektet
{

    public class MainViewModel
    {
        public string AlgorithmType { get; set; }
        //skapar en instans av klassen Random
        private Random random = new Random();
        // En ObservableCollection av Cell-objekt som representerar varje cell i spelbrädet.
        public ObservableCollection<Cell> Cells { get; set; }

        // Ett ICommand-objekt som binder till ett klick-event i UI, så att vi kan hantera när en cell klickas på.
        public ICommand CellClickCommand { get; set; }

        // En bool som håller reda på om det är spelare X:s tur eller inte.
        private bool IsPlayerXTurn { get; set; } = true;

        // En bool som kontrollerar om spelaren spelar mot en dator (AI) eller en mänsklig spelare.
        public bool IsComputerOpponent { get; set; }

        // Ett heltal som håller reda på hur många markörer som krävs i rad för att vinna spelet.
        public int WinLength { get; set; }

        // Konstruktor som kallas när en ny instans av MainViewModel skapas.
        public MainViewModel()
        {
            // Anropar metoden för att initialisera spelet (visar upp dialogrutan där användaren väljer inställningar).
            InitializeGame();

            // Skapar en ny ObservableCollection och fyller den med 25 Cell-objekt, initialt satta till "-".
            Cells = new ObservableCollection<Cell>();
            for (int i = 0; i < 25; i++)
            {
                Cells.Add(new Cell { Value = "-" });
            }

            // Binder CellClickCommand till CellClick-metoden så att den anropas när en cell klickas på.
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
                    // MessageBox Som presenterar vinnaren och frågar om spelaren vill spela igen
                    MessageBoxResult result = MessageBox.Show($"Vi har en vinnare! Spelaren med {currentPlayerMark} markören fick {WinLength} i rad och vinner därmed matchen! Vill du spela igen?", "Vinnare!", MessageBoxButton.YesNo);

                    //If sats som hanterar valet om spelaren vill spela igen eller avsluta spelet
                    if (result == MessageBoxResult.Yes)
                    {
                        
                        ResetBoard(); // Återställer brädet om spelaren vill spela igen
                       //Resetboard före InitializeGame, fu
                        InitializeGame();// Kör spelets första dialogruta igen
                    }
                    else
                    {
                        Application.Current.Shutdown();
                    }
                    return;
                }             

                // Lägger till följande kod för att kolla om brädet är fullt och även alternativ
                // för att starta om spelet eller avsluta det
                if (IsBoardFull())
                {
                    //MessageBox om ingen vinner och brädet är fullt samt alternativ för att spela igen eller ej
                    MessageBoxResult result = MessageBox.Show("Det blev oavgjort. Vill du spela igen?", "Oavgjort", MessageBoxButton.YesNo);

                    if (result == MessageBoxResult.Yes)
                    {
                        //Om spelaren vill spela igen så återställs brädet
                        ResetBoard();
                        InitializeGame();// Kör spelets första dialogruta igen
                    }
                    else
                    {
                        // Om spelar inte vill spela igen så avslutas spelet
                        Application.Current.Shutdown();
                    }
                }
                // ifsats som kollar om det är datorns tur och om det är datorns tur så anropas metoden DoComputerMove
                if (IsComputerOpponent && !IsPlayerXTurn)
                {
                    DoComputerMove("O");
                    // Kollar om datorn vinner och presenterar en dialogruta med alternativ för att spela igen eller avsluta spelet
                    if (CheckForWin("O"))
                    {
                        MessageBoxResult result = MessageBox.Show("Datorn vann! Vill du spela igen?", "Förlust", MessageBoxButton.YesNo);

                        if (result == MessageBoxResult.Yes)
                        {
                            ResetBoard();// Återställer brädet om spelaren vill spela igen

                            InitializeGame();// Kör spelets första dialogruta igen
                        }
                        else
                        {
                            // Avslutar spelet om spelaren inte vill spela igen
                            Application.Current.Shutdown();
                        }
                        return;
                    }
                    // Ändrar vems tur det är att spela.
                    IsPlayerXTurn = !IsPlayerXTurn;
                }


            }
        }
        // Metod för att kontrollera om brädet är fullt
        private bool IsBoardFull()
        {
            // Loopar igenom varje cell i samlingen "Cells"
            foreach (var cell in Cells)
            {
                // Om någon cell har värdet "-", betyder det att den är tom
                // och brädet är inte fullt. Funktionen returnerar "false".
                if (cell.Value == "-")
                {
                    return false;
                }
            }

            // Om loopen genomförs utan att returnera "false",
            // betyder det att alla celler är fyllda och brädet är fullt.
            // Funktionen returnerar "true".
            return true;
        }

        // Metod för att återställa brädet
        private void ResetBoard()
        {
            // Loopar igenom varje cell i samlingen "Cells"
            foreach (var cell in Cells)
            {
                // Återställer värdet av varje cell till "-" vilket innebär att cellen är tom
                cell.Value = "-";
            }

            // Återställer turordningen till spelaren "X"
            IsPlayerXTurn = true;
        }

        //Metoder för att kontrollera om någon har vunnit i någon riktning
        private bool CheckForWin(string mark)
        {
            // Loopar igenom varje rad på brädet (antalet rader är 5)
            for (int row = 0; row < 5; row++)
            {
                // Loopar igenom varje kolumn på brädet (antalet kolumner är 5)
                for (int col = 0; col < 5; col++)
                {
                    // Kollar om det finns en vinstsekvens med start från den aktuella cellen (row, col).
                    // Det görs genom att kolla i fyra olika riktningar: horisontell, vertikal och två diagonaler.
                    if (
                        CheckDirection(row, col, 0, 1, mark, WinLength) || // Horisontell riktning
                        CheckDirection(row, col, 1, 0, mark, WinLength) || // Vertikal riktning
                        CheckDirection(row, col, 1, 1, mark, WinLength) || // Diagonal riktning åt höger
                        CheckDirection(row, col, 1, -1, mark, WinLength)   // Diagonal riktning åt vänster
                       )
                    {
                        return true; // Returnerar true om en vinst har hittats
                    }
                }
            }
            return false; // Returnerar false om ingen vinst har hittats
        }

        private bool CheckDirection(int startRow, int startCol, int dRow, int dCol, string mark, int winLength)
        {
            // Loopar genom antalet celler specificerat i winLength (3,4 eller 5)
            for (int i = 0; i < winLength; i++)
            {
                // Beräknar den aktuella raden och kolumnen baserat på startpunkten och den givna riktningen (dRow, dCol)
                int row = startRow + dRow * i;
                int col = startCol + dCol * i;

                // Kontrollerar om den beräknade raden och kolumnen är utanför brädet
                // Om så är fallet, returneras false (dvs ingen vinstsekvens i denna riktning)
                if (row < 0 || row >= 5 || col < 0 || col >= 5)
                {
                    return false;
                }

                // Kontrollerar om cellens värde på den beräknade positionen (row, col) matchar det givna märket ("X" eller "O")
                // Om det inte gör det, returneras false
                if (Cells[row * 5 + col].Value != mark)
                {
                    return false;
                }
            }

            // Om loopen inte har returnerat false, betyder det att en vinnande sekvens har hittats i den givna riktningen
            return true;
        }

        // Mycket simpel algoritm där PC kommer välja den första lediga cellen
        //private void DoComputerMove(string mark)
        //{
        //    
        //    //Foreach loop som kollar alla celler i samlingen Cells
        //    foreach (var cell in Cells)
        //    {
        //        // om cellen är ledig, sätt värdet till mark och bryt loopen
        //        if (cell.Value == "-")
        //        {
        //            //Om cellen är ledig så sätts värdet till mark
        //            cell.Value = mark;
        //            break;
        //        }
        //    }
        //}

        // Algoritm som slumpar fram en ledig cell, lite mer avancerad än den ovan
        //private void DoComputerMove(string mark)
        //{
        //    // Skapar en lista för att lagra alla lediga celler
        //    List<Cell> availableCells = new List<Cell>();

        //    // Loopar igenom alla celler för att hitta de som är lediga ("-")
        //    foreach (var cell in Cells)
        //    {
        //        if (cell.Value == "-")
        //        {
        //            availableCells.Add(cell);
        //        }
        //    }

        //    // Kontrollerar om det finns några lediga celler
        //    if (availableCells.Any())
        //    {
        //        // Väljer en slumpmässig cell från listan av tillgängliga celler
        //        int index = random.Next(availableCells.Count);
        //        Cell randomCell = availableCells[index];

        //        // Sätter värdet på den slumpmässigt valda cellen till datorns mark ("O" eller "X")
        //        randomCell.Value = mark;
        //    }
        //}

        //Algoritm som är än mer utvecklad än de föregående.
        // Den kollar om spelaren är på väg att vinna och blockerar i så fall
        // Om inte, så väljer den en slumpmässig cell
        //private void DoComputerMove(string mark)
        //{
        //    Cell winningCell = FindCellToWin(mark);
        //    if (winningCell != null)
        //    {
        //        winningCell.Value = mark;
        //        return;
        //    }
        //    // Försöker blockera spelaren från att vinna
        //    Cell cellToBlock = FindCellToBlock(mark == "X" ? "O" : "X");
        //    if (cellToBlock != null)
        //    {
        //        cellToBlock.Value = mark;
        //        return;
        //    }

        //    // Om ingen cell att blockera hittas, fortsätt med den ursprungliga strategin

        //    // Skapar en lista för att lagra alla lediga celler
        //    List<Cell> availableCells = new List<Cell>();

        //    // Loopar igenom alla celler för att hitta de som är lediga ("-")
        //    foreach (var cell in Cells)
        //    {
        //        if (cell.Value == "-")
        //        {
        //            availableCells.Add(cell);
        //        }
        //    }

        //    // Kontrollerar om det finns några lediga celler
        //    if (availableCells.Any())
        //    {
        //        // Väljer en slumpmässig cell från listan av tillgängliga celler
        //        int index = random.Next(availableCells.Count);
        //        Cell randomCell = availableCells[index];

        //        // Sätter värdet på den slumpmässigt valda cellen till datorns mark ("O" eller "X")
        //        randomCell.Value = mark;
        //    }
        //}
        private void DoComputerMove(string mark)
        {
            if (AlgorithmType == "Easy")
            {
                foreach (var cell in Cells)
                {
                    // om cellen är ledig, sätt värdet till mark och bryt loopen
                    if (cell.Value == "-")
                    {
                        //Om cellen är ledig så sätts värdet till mark
                        cell.Value = mark;
                        break;
                    }
                }
            }
            else if (AlgorithmType == "Medium")
            {
                List<Cell> availableCells = new List<Cell>();

                // Loopar igenom alla celler för att hitta de som är lediga ("-")
                foreach (var cell in Cells)
                {
                    if (cell.Value == "-")
                    {
                        availableCells.Add(cell);
                    }
                }

                // Kontrollerar om det finns några lediga celler
                if (availableCells.Any())
                {
                    // Väljer en slumpmässig cell från listan av tillgängliga celler
                    int index = random.Next(availableCells.Count);
                    Cell randomCell = availableCells[index];

                    // Sätter värdet på den slumpmässigt valda cellen till datorns mark ("O" eller "X")
                    randomCell.Value = mark;
                }
            }
            else if (AlgorithmType == "Hard")
            {
                Cell winningCell = FindCellToWin(mark);
                if (winningCell != null)
                {
                    winningCell.Value = mark;
                    return;
                }
                // Försöker blockera spelaren från att vinna
                Cell cellToBlock = FindCellToBlock(mark == "X" ? "O" : "X");
                if (cellToBlock != null)
                {
                    cellToBlock.Value = mark;
                    return;
                }

                // Om ingen cell att blockera hittas, fortsätt med den ursprungliga strategin

                // Skapar en lista för att lagra alla lediga celler
                List<Cell> availableCells = new List<Cell>();

                // Loopar igenom alla celler för att hitta de som är lediga ("-")
                foreach (var cell in Cells)
                {
                    if (cell.Value == "-")
                    {
                        availableCells.Add(cell);
                    }
                }

                // Kontrollerar om det finns några lediga celler
                if (availableCells.Any())
                {
                    // Väljer en slumpmässig cell från listan av tillgängliga celler
                    int index = random.Next(availableCells.Count);
                    Cell randomCell = availableCells[index];

                    // Sätter värdet på den slumpmässigt valda cellen till datorns mark ("O" eller "X")
                    randomCell.Value = mark;
                }
            }
            // ... din tidigare kod ...
        }


        private Cell FindCellToBlock(string opponentMark)
        {
            // Loopar igenom varje rad på planen (antalet rader är 5)
            for (int row = 0; row < 5; row++)
            {
                // Loopar igenom varje kolumn på planen (antalet kolumner är 5)
                for (int col = 0; col < 5; col++)
                {
                    // Kollar i horisontell riktning om det finns en cell att blockera
                    Cell cell = CheckDirectionForBlocking(row, col, 0, 1, opponentMark, WinLength);
                    if (cell != null) return cell; // Om en cell hittas, returneras den direkt

                    // Kollar i vertikal riktning om det finns en cell att blockera
                    cell = CheckDirectionForBlocking(row, col, 1, 0, opponentMark, WinLength);
                    if (cell != null) return cell; // Om en cell hittas, returneras den direkt

                    // Kollar i diagonal riktning åt höger om det finns en cell att blockera
                    cell = CheckDirectionForBlocking(row, col, 1, 1, opponentMark, WinLength);
                    if (cell != null) return cell; // Om en cell hittas, returneras den direkt

                    // Kollar i diagonal riktning åt vänster om det finns en cell att blockera
                    cell = CheckDirectionForBlocking(row, col, 1, -1, opponentMark, WinLength);
                    if (cell != null) return cell; // Om en cell hittas, returneras den direkt
                }
            }

            // Om ingen cell hittas att blockera, returneras null
            return null;
        }


        private Cell CheckDirectionForBlocking(int startRow, int startCol, int dRow, int dCol, string opponentMark, int winLength)
        {
            // Initierar en räknare för att hålla koll på antalet motståndarmarkörer i en given riktning
            int count = 0;

            // Initierar en variabel för att hålla koll på den tomma cellen i en given riktning
            Cell emptyCell = null;

            // Loopar genom antalet celler specificerat i winLength (3, 4 eller 5)
            for (int i = 0; i < winLength; i++)
            {
                // Beräknar den aktuella raden och kolumnen baserat på startpunkten och den givna riktningen (dRow, dCol)
                int row = startRow + dRow * i;
                int col = startCol + dCol * i;

                // Kontrollerar om den beräknade raden och kolumnen är utanför brädet
                if (row < 0 || row >= 5 || col < 0 || col >= 5)
                {
                    return null; // Om så är fallet, returneras null
                }

                // Hämtar cellen vid den beräknade positionen (row, col)
                Cell cell = Cells[row * 5 + col];

                // Kollar om cellens värde matchar motståndarens märke
                if (cell.Value == opponentMark)
                {
                    count++; // Ökar räknaren om det gör det
                }
                // Kollar om cellen är tom
                else if (cell.Value == "-")
                {
                    // Lagrar den tomma cellen om ingen tidigare tom cell har lagrats
                    if (emptyCell == null)
                    {
                        emptyCell = cell;
                    }
                    else
                    {
                        return null; // Mer än en tom cell i sekvensen, kan inte blockera
                    }
                }
                else
                {
                    return null; // Cellen innehåller datorns egen markör, så sekvensen kan inte blockeras
                }
            }

            // Kollar om antalet motståndarmarkörer är ett mindre än winLength och om det finns en tom cell
            if (count == winLength - 1 && emptyCell != null)
            {
                return emptyCell; // Returnerar den tomma cellen för att blockera
            }

            // Om ingen lämplig cell för blockering hittas, returneras null
            return null;
        }

        private Cell FindCellToWin(string computerMark)
        {
            for (int row = 0; row < 5; row++)
            {
                for (int col = 0; col < 5; col++)
                {
                    // Kontrollerar olika riktningar för att se om det finns en vinstmöjlighet
                    Cell cell = CheckDirectionForWinning(row, col, 0, 1, computerMark, 5); // Horisontellt
                    if (cell != null) return cell;

                    cell = CheckDirectionForWinning(row, col, 1, 0, computerMark, 5); // Vertikalt
                    if (cell != null) return cell;

                    cell = CheckDirectionForWinning(row, col, 1, 1, computerMark, 5); // Diagonalt (nedåt)
                    if (cell != null) return cell;

                    cell = CheckDirectionForWinning(row, col, 1, -1, computerMark, 5); // Diagonalt (uppåt)
                    if (cell != null) return cell;
                }
            }
            return null;
        }
        private Cell CheckDirectionForWinning(int startRow, int startCol, int dRow, int dCol, string computerMark, int winLength)
        {
            int count = 0;
            Cell emptyCell = null;

            for (int i = 0; i < winLength; i++)
            {
                int row = startRow + dRow * i;
                int col = startCol + dCol * i;

                if (row < 0 || row >= 5 || col < 0 || col >= 5)
                {
                    return null;
                }

                Cell cell = Cells[row * 5 + col];

                if (cell.Value == computerMark)
                {
                    count++;
                }
                else if (cell.Value == "-")
                {
                    if (emptyCell == null)
                    {
                        emptyCell = cell;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }

            if (count == winLength - 1 && emptyCell != null)
            {
                return emptyCell; // Returnera den tomma cellen för att vinna
            }

            return null;
        }
        private void InitializeGame()
        {
            // Skapar en instans av klassen CustomMessageBox
            var customMessageBox = new CustomMessageBox();

            // If-sats som kollar om dialogrutan har visats
            if (customMessageBox.ShowDialog() == true)
            {
                // Om dialogrutan har visats och användaren har gjort ett val,
                // sätt IsComputerOpponent och WinLength till de värden som valdes i dialogrutan
                IsComputerOpponent = customMessageBox.IsComputerOpponent;
                WinLength = customMessageBox.WinLength;
            }
            else
            {
                // Om dialogrutan stängs utan att ett val görs (t.ex. genom att trycka på "krysset"),
                // avsluta applikationen
                MessageBox.Show("Inget val gjordes. Spelet avslutas.");
                Application.Current.Shutdown();
            }
            if (customMessageBox.EasyRadio.IsChecked == true)
            {
                AlgorithmType = "Easy";
            }
            else if (customMessageBox.MediumRadio.IsChecked == true)
            {
                AlgorithmType = "Medium";
            }
            else if (customMessageBox.HardRadio.IsChecked == true)
            {
                AlgorithmType = "Hard";
            }

        }
       
        
    }

}







    

