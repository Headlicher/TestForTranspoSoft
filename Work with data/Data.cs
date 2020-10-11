using System;

namespace TestForTranspoSoft
{
    class Data
    {

        public Data(string goods, DateTime? startDate, DateTime? finalDate, int days, DateTime dateOfStart, DateTime? dateOfFinish, int bet, string description)
        {
            Goods = goods;
            StartDate = startDate;
            FinalDate = finalDate;
            Days = days;
            DateOfStart = dateOfStart;
            DateOfFinish = dateOfFinish;
            Bet = bet;
            Description = description;
        }

        public string Goods { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? FinalDate { get; set; }
        public int Days { get; set; }
        public int Bet { get; set; }
        public string Description { get; set; }
        public DateTime DateOfStart { get; set; }
        public DateTime? DateOfFinish { get; set; }
    }
}
