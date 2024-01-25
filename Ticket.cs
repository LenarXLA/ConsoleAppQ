using System;

namespace ConsoleAppQ
{
    public class Ticket
    {
        private int _number;
        private Service _service;
        private DateTime _startTime;
        private DateTime _endTime;

        // номер талона
        public int Number
        {
            get => _number;
            set => _number = value;
        }

        // время начала обработки талона
        public DateTime StartTime
        {
            get => _startTime;
            set => _startTime = value;
        }

        // время окончания обработки талона
        public DateTime EndTime
        {
            get => _endTime;
            set => _endTime = value;
        }

        public Service Service
        {
            get => _service;
            set => _service = value;
        }
    }
}