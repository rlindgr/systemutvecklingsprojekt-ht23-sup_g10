using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe.Models
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        // Händelse som används när en property ändras.
        public event PropertyChangedEventHandler PropertyChanged;

        // Metod som startar PropertyChanged-händelsen.
        // Metoden tar emot ett string argument som är namnet på den ändrade egenskapen.
        protected void OnPropertyChanged(string propertyName)
        {
            // ?-operatorn ser till att Invoke endast kallas om PropertyChanged inte är null.
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
