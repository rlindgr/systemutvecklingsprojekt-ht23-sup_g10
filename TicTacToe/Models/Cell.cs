using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe.Models
{
        public class Cell : BaseViewModel
        {
            // Privat fält som håller cellens värde ("X", "O", eller "-").
            private string _value;
            public bool _isMarked;
            private string _cellColor; // Ny egenskap för att hålla färgenpå cellen.
            public static string emptyCell = "-";
            // Property som andra klasser använder för att få eller sätta cellens värde.
            public string Value
            {
                // Returnerar värdet av det privata fältet.
                get => _value;

                // Sätter värdet av det privata fältet och meddelar att egenskapen har ändrats.
                set
                {
                    _value = value;

                    // Anropar OnPropertyChanged-metoden i BaseViewModel för att meddela att denna egenskap har ändrats.
                    // Detta är nödvändigt för att UI ska uppdateras.
                    OnPropertyChanged(nameof(Value));

                }
            }

            public bool IsMarked
            {
                get
                {
                    return _isMarked;
                }
                set
                {
                    _isMarked = value;
                    OnPropertyChanged(nameof(IsMarked));
                }
            }
            // Ny property för att hålla cellens färg
            public string CellColor
            {
                get => _cellColor;
                set
                {
                    _cellColor = value;
                    OnPropertyChanged(nameof(CellColor));
                }
            }

        }
    }

