namespace FiveInARow
{
    public class Cell : BaseViewModel
    {
        private string _value;
        public string Value
        {
            get => _value;
            set
            {
                _value = value;
                OnPropertyChanged(nameof(Value));
            }
        }
    }
}
