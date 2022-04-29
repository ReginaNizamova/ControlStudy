using System;

namespace ControlStudy.Classes
{
    public class Dates
    {
        private int _countDate;
        public int CountDate
        {
            get
            {
                return _countDate;
            }
            set
            {
                _countDate = value;
            }
        }


        private DateTime _endDate;
        public DateTime EndDate
        {
            get
            {
                return _endDate;
            }
            set
            {
                _endDate = value;
            }
        }


        private DateTime _startDate;
        public DateTime StartDate
        {
            get
            {
                return _startDate;
            }
            set
            {
                _startDate = value;
            }
        }

    }
}
