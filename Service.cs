namespace ConsoleAppQ
{
    public class Service
    {
        private string _title;
        private int _duration;

        // наименование услуги
        public string Title
        {
            get => _title;
            set => _title = value;
        }

        // плановое время услуги в минутах
        public int Duration
        {
            get => _duration;
            set => _duration = value;
        }
    }
}