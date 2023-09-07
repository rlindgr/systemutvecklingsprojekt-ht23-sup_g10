// Namespace som innehåller alla klasser för projektet "FiveInARow".
using System.ComponentModel;

namespace FiveInARow
{
    // BaseViewModel implementerar INotifyPropertyChanged för att kunna 
    // meddela när en egenskap (property) ändras. Detta är viktigt för 
    // data bindning i WPF, för att uppdatera UI när modellens data ändras.
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
