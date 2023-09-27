using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TicTacToe.Commands
{
    public class RelayCommand : ICommand
    {
        // En privat delegat (pekare) till en metod som tar en parameter av typen object och returnerar ingenting.
        private readonly Action<object> _execute;

        // Konstruktor som tar en Action som parameter och tilldelar det till _execute.
        public RelayCommand(Action<object> execute)
        {
            _execute = execute;
        }

        // Event som triggers om villkoren för om kommandot kan exekveras ändras.
        // I den här enkla implementeringen används inte detta event.
        public event EventHandler CanExecuteChanged;

        // Metod för att avgöra om kommandot kan exekveras.
        // Här är det alltid satt till true, vilket innebär att kommandot alltid kan köras. 
        public bool CanExecute(object parameter)
        {
            return true;
        }

        // Execute-metoden där det faktiska arbetet görs.
        // Här kallas _execute-delegaten, vilket i sin tur kommer att kalla den faktiska metoden som vi vill köra.
        public void Execute(object parameter)
        {
            _execute(parameter);
        }
    }
}
