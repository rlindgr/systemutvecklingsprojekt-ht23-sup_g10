using FiveInARow;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows;
using TicTacToe.Enums;
using TicTacToe.Settings;
using TicTacToe.Views;
using YourNamespace;
using TicTacToe.Models;
using TicTacToe.Commands;

namespace TicTacToe.ViewModels
{
    class GameViewModel: BaseViewModel
    {
        private static bool _isComputerOpponent = GameSettings.IsComputerOpponent;
        private static int _winLength = GameSettings.WinLength;
        private static Algorithm _algorithmType = GameSettings.Algorithm;


        public bool IsPlayerXTurn;
        public string CurrentPlayer { get; set; }

        private int _playerXWins;
        private int _playerOWins;

        public static readonly string PlayerX = "X";
        public static readonly string PlayerO = "O";


        public ICommand ResetWinsCommand { get; }
        public ICommand CellClickCommand { get; set; }
        public ICommand MainMenuCommand { get; private set; }


        // En ObservableCollection av Cell-objekt som representerar varje cell i spelbrädet.
        public ObservableCollection<Cell> Cells { get; set; }

        private double _volume = 0.5; // Standardvolymen är satt till 0.5

        public double Volume
        {
            get { return _volume; }
            set
            {
                if (value >= 0.0 && value <= 1.0)
                {
                    _volume = value;
                }
            }
        }



        public GameViewModel()
        {
            Cells = new ObservableCollection<Cell>();
            for (int i = 0; i < 25; i++)
            {
                Cells.Add(new Cell { Value = Cell.emptyCell });
            }
            InitializeGame();

            // Binder CellClickCommand till CellClick-metoden så att den anropas när en cell klickas på.
            CellClickCommand = new RelayCommand(CellClick);
            // Anropar metoden för att initialisera spelet (visar upp dialogrutan där användaren väljer inställningar).
            /*InitializeGame();*/

            // Skapar en ny ObservableCollection och fyller den med 25 Cell-objekt, initialt satta till "-".
            ResetWinsCommand = new RelayCommand(obj => ResetWins());         
            MainMenuCommand = new RelayCommand(x => MainMenu());

        }

        public string ChangeCurrentPlayer()
        {

            if (CurrentPlayer == PlayerX)
            {
                return CurrentPlayer = PlayerO;
            }
            else
            {
                return CurrentPlayer = PlayerX;
            }
        }

        public void InitializeGame()
        {
 
                _isComputerOpponent = GameSettings.IsComputerOpponent;
                _winLength = GameSettings.WinLength;
                _algorithmType = GameSettings.Algorithm;
               
                Random random = new Random();
                int whoStarts = random.Next(0, 2);
                string startingPlayerMessage;

                if (whoStarts == 0)
                {

                    CurrentPlayer = PlayerX;
                    startingPlayerMessage = "Spelare 1 börjar spelet";
                }
                else
                {
                    CurrentPlayer = PlayerO;
                    startingPlayerMessage = "Spelare 2 börjar spelet";

                    if (_isComputerOpponent)
                    {
                        CurrentPlayer = PlayerO;
                        startingPlayerMessage = "Datorn börjar spelet";
                        DoComputerMove(PlayerO);
                        ChangeCurrentPlayer();
                    }
                }

                FirstMoveAnnouncer announcer = new FirstMoveAnnouncer(startingPlayerMessage);
                announcer.Show();
            
        }

        public void ShowStartingPlayer(string startingPlayer)
        {          
            FirstMoveAnnouncer firstMoveAnnouncement = new FirstMoveAnnouncer(startingPlayer);
            firstMoveAnnouncement.ShowDialog();
        }

        public void ResetGameBoard()
        {
            // Återställer alla celler till deras ursprungliga värden
            foreach (var cell in Cells)
            {
                cell.Value = Cell.emptyCell;
                cell.IsMarked = false;
            }

            // Slumpmässigt avgör vem som börjar
            Random random = new Random();
            int whoStarts = random.Next(0, 2);


            // Meddelande om vem som börjar
            string startingPlayerMessage;

            if (whoStarts == 0)
            {
                // Spelare 1 börjar               
                CurrentPlayer = PlayerX;
                startingPlayerMessage = "Spelare 1 börjar spelet";
            }
            else
            {
                // Spelare 2 (eller datorn) börjar
                CurrentPlayer = PlayerO;
                startingPlayerMessage = "Spelare 2 börjar spelet";

                // Om det är en dator motståndare, gör det första draget här
                if (_isComputerOpponent)
                {
                    CurrentPlayer = PlayerO;
                    startingPlayerMessage = "Datorn börjar spelet";
                    DoComputerMove(PlayerO);
                    ChangeCurrentPlayer();
                }
            }

            // Ytterligare logik för att återställa spelet kan läggas till här
            ShowStartingPlayer(startingPlayerMessage); // Antag att ShowStartingPlayer är en metod som visar vem som börjar
        }

        private void ResetWins()
        {
            PlayerXWins = 0;
            PlayerOWins = 0;
            OnPropertyChanged(nameof(PlayerXWins));
            OnPropertyChanged(nameof(PlayerOWins));
        }
        public int PlayerXWins
        {
            get { return _playerXWins; }
            set
            {
                if (_playerXWins != value)
                {
                    _playerXWins = value;
                    OnPropertyChanged(nameof(PlayerXWins));
                }
            }
        }

        public int PlayerOWins
        {
            get { return _playerOWins; }
            set
            {
                if (_playerOWins != value)
                {
                    _playerOWins = value;
                    OnPropertyChanged(nameof(PlayerOWins));
                }
            }
        }



        public void CellClick(object cellObj)
        {
            // Skapa en ny MediaPlayer-instans och öppna ljudfilen
            MediaPlayer mediaPlayer = new MediaPlayer();
            mediaPlayer.Open(new Uri("201806__fartheststar__poker_chips2.wav", UriKind.Relative));

            // Sätt volymen på MediaPlayer-instansen
            mediaPlayer.Volume = Volume;

            // Spela upp ljudet
            mediaPlayer.Play();

            if (cellObj is Cell cell && cell.Value == Cell.emptyCell)
            {
                string currentPlayerMark = CurrentPlayer;

                //// Uppdaterar den nuvarande spelaren här
                //CurrentPlayer = currentPlayerMark;

                cell.Value = currentPlayerMark;
                cell.IsMarked = true;

                if (CheckForWin(currentPlayerMark))
                {
                    if (currentPlayerMark == PlayerX)
                    {
                        PlayerXWins++;
                        OnPropertyChanged(nameof(PlayerXWins));
                    }
                    else if (currentPlayerMark == PlayerO)
                    {
                        PlayerOWins++;
                        OnPropertyChanged(nameof(PlayerOWins));
                    }

                    MediaPlayer mediaWin = new MediaPlayer();
                    mediaWin.Open(new Uri("170583__audiosmedia__party-horn.wav", UriKind.Relative));
                    mediaWin.Volume = Volume;
                    mediaWin.Play();

                    EndGame($"Spelaren med {currentPlayerMark} markören fick {_winLength} i rad och vinner därmed matchen!");
                    return;
                }

                if (IsBoardFull())
                {
                    MediaPlayer mediaDraw = new MediaPlayer();
                    mediaDraw.Open(new Uri("495541__matrixxx__retro-death.wav", UriKind.Relative));
                    mediaDraw.Volume = Volume;
                    mediaDraw.Play();
                    EndGame("Det blev oavgjort. Vill du spela igen?");
                    return;
                }

                ChangeCurrentPlayer();

                if (_isComputerOpponent && !IsPlayerXTurn)
                {
                    DoComputerMove(PlayerO);

                    if (CheckForWin(PlayerO))
                    {
                        PlayerOWins++;
                        OnPropertyChanged(nameof(PlayerOWins));

                        MediaPlayer mediaLoss = new MediaPlayer();
                        mediaLoss.Open(new Uri("572936__bloodpixelhero__error.wav", UriKind.Relative));
                        mediaLoss.Volume = Volume;
                        mediaLoss.Play();
                        EndGame("Datorn vann! Vill du spela igen?");
                        return;
                    }

                    if (IsBoardFull())
                    {
                        MediaPlayer mediaDraw = new MediaPlayer();
                        mediaDraw.Open(new Uri("495541__matrixxx__retro-death.wav", UriKind.Relative));
                        mediaDraw.Volume = Volume;
                        mediaDraw.Play();
                        EndGame("Det blev oavgjort. Vill du spela igen?");
                        return;
                    }

                    ChangeCurrentPlayer();
                }

            }
        }


        private void EndGame(string displayMessage)
        {
            WinnerAnnouncement winnerAnnouncement = new WinnerAnnouncement(displayMessage);
            winnerAnnouncement.ShowDialog();

            if (winnerAnnouncement.Result == MessageBoxResult.Yes)
            {
                ResetBoard();
                ResetGameBoard(); // Starta om spelet med samma inställningar som tidigare (svårighetsgrad, antal i rad) 
            }
            else if (winnerAnnouncement.Result == MessageBoxResult.No)
            {
                Application.Current.Shutdown();
            }
            else if (winnerAnnouncement.Result == null)
            {
                // Om dialogrutan har stängts med krysset högst upp till höger
                // gör ingenting (återgå till fullagd spelplan)
            }
        }



        // Metod för att kontrollera om brädet är fullt
        private bool IsBoardFull()
        {
            // Loopar igenom varje cell i samlingen "Cells"
            foreach (var cell in Cells)
            {
                // Om någon cell har värdet Cell.emptyCell, betyder det att den är tom
                // och brädet är inte fullt. Funktionen returnerar "false".
                if (cell.Value == Cell.emptyCell)
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
                // Återställer värdet av varje cell till Cell.emptyCell vilket innebär att cellen är tom
                cell.Value = Cell.emptyCell;
                cell.IsMarked = false;
            }
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
                        CheckDirection(row, col, 0, 1, mark, _winLength) || // Horisontell riktning
                        CheckDirection(row, col, 1, 0, mark, _winLength) || // Vertikal riktning
                        CheckDirection(row, col, 1, 1, mark, _winLength) || // Diagonal riktning åt höger
                        CheckDirection(row, col, 1, -1, mark, _winLength)   // Diagonal riktning åt vänster
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
            for (int i = 0; i < _winLength; i++)
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


        private void DoComputerMove(string mark)
        {
            Random random = new Random();


            if (_algorithmType == Algorithm.Lätt)
            {
                foreach (var cell in Cells)
                {
                    // om cellen är ledig, sätt värdet till mark och bryt loopen
                    if (cell.Value == Cell.emptyCell)
                    {
                        // Om cellen är ledig så sätts värdet till mark
                        cell.Value = mark;
                        cell.IsMarked = true;  // Sätt IsMarked till true för att ändra färg
                        break;
                    }
                }
            }
            else if (_algorithmType == Algorithm.Mellan)
            {
                List<Cell> availableCells = new List<Cell>();

                // Loopar igenom alla celler för att hitta de som är lediga (Cell.emptyCell)
                foreach (var cell in Cells)
                {
                    if (cell.Value == Cell.emptyCell)
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
                    randomCell.Value = mark;
                    randomCell.IsMarked = true;  // Sätt IsMarked till true för att ändra färg
                }
            }
            else if (_algorithmType == Algorithm.Svår)
            {
                Cell winningCell = FindCellToWin(mark);
                if (winningCell != null)
                {
                    winningCell.Value = mark;
                    winningCell.IsMarked = true;  // Sätt IsMarked till true för att ändra färg
                    return;
                }

                // Försöker blockera spelaren från att vinna
                Cell cellToBlock = FindCellToBlock(mark == PlayerX ? PlayerO : PlayerX);
                if (cellToBlock != null)
                {
                    cellToBlock.Value = mark;
                    cellToBlock.IsMarked = true;  // Sätt IsMarked till true för att ändra färg
                    return;
                }

                // Om ingen cell att blockera hittas, fortsätt med den ursprungliga strategin

                // Skapar en lista för att lagra alla lediga celler
                List<Cell> availableCells = new List<Cell>();

                // Loopar igenom alla celler för att hitta de som är lediga (Cell.emptyCell)
                foreach (var cell in Cells)
                {
                    if (cell.Value == Cell.emptyCell)
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
                    randomCell.Value = mark;
                    randomCell.IsMarked = true;  // Sätt IsMarked till true för att ändra färg
                }
            }
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
                    Cell cell = CheckDirectionForBlocking(row, col, 0, 1, opponentMark, _winLength);
                    if (cell != null) return cell; // Om en cell hittas, returneras den direkt

                    // Kollar i vertikal riktning om det finns en cell att blockera
                    cell = CheckDirectionForBlocking(row, col, 1, 0, opponentMark, _winLength);
                    if (cell != null) return cell; // Om en cell hittas, returneras den direkt

                    // Kollar i diagonal riktning åt höger om det finns en cell att blockera
                    cell = CheckDirectionForBlocking(row, col, 1, 1, opponentMark, _winLength);
                    if (cell != null) return cell; // Om en cell hittas, returneras den direkt

                    // Kollar i diagonal riktning åt vänster om det finns en cell att blockera
                    cell = CheckDirectionForBlocking(row, col, 1, -1, opponentMark, _winLength);
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
            for (int i = 0; i < _winLength; i++)
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
                else if (cell.Value == Cell.emptyCell)
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
            if (count == _winLength - 1 && emptyCell != null)
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

            for (int i = 0; i < _winLength; i++)
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
                else if (cell.Value == Cell.emptyCell)
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

            if (count == _winLength - 1 && emptyCell != null)
            {
                return emptyCell; // Returnera den tomma cellen för att vinna
            }

            return null;
        }
        private void MainMenu()
        {
          
             // Här kan du bestämma vilken text som ska visas
            string displayText = "Vill du gå till huvudmenyn?";

            // Skapa ett nytt MainMenuClick-fönster och visa det
            MainMenuClick mainMenuClick = new MainMenuClick(displayText);
            mainMenuClick.ShowDialog();

            // Hantera användarens val
            if (mainMenuClick.Result == MessageBoxResult.Yes)
            {

                MainViewModel.Instance.CurrentViewModel = new MainMenuViewModel();
            }                
        }
    }

}
