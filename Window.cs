namespace ConsoleAppQ
{
    public class Window
    {
        private string _title;
        private int _workingHours;


        public string Title
        {
            get => _title;
            set => _title = value;
        }

        public int WorkingHours
        {
            get => _workingHours;
            set => _workingHours = value;
        }
    }
}