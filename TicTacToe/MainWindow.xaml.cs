using System.Windows;

namespace FiveInARow
{
    // MainWindow är en delmängd av WPF-klass Window.
    public partial class MainWindow : Window
    {
        // Konstruktor för MainWindow-klassen
        public MainWindow()
        {
            // Initialiserar komponenter, inklusive de som definieras i XAML.
            // Detta är en automagisk funktion som WPF genererar.
            InitializeComponent();

            // Sätter DataContext till en ny instans av MainViewModel.
            // DataContext är det objekt som exponerar de egenskaper som binds till XAML.
            DataContext = new MainViewModel();
        }
    }
}
