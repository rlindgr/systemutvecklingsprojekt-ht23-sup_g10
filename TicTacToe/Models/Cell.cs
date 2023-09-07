// Samma namespace som föregående fil, vilket innebär att de kan interagera fritt med varandra.
namespace FiveInARow
{
    // Klassen Cell ärver från BaseViewModel, vilket ger den möjlighet att anmäla ändringar i property.
    public class Cell : BaseViewModel
    {
        // Privat fält som håller cellens värde ("X", "O", eller "-").
        private string _value;

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
    }
}
