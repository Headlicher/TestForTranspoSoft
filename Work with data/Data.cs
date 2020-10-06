using System;

namespace TestForTranspoSoft
{
    class Data
    {

        public Data(string goods, DateTime startDate, DateTime finalDate, int days, DateTime dateOfStart, DateTime dateOfFinish,int bet, string description)
        {
            this.Goods = goods;
            this.StartDate = startDate;
            this.FinalDate = finalDate;
            this.Days = days;
            this.DateOfStart = dateOfStart;
            this.DateOfFinish = dateOfFinish;
            this.Bet = bet;
            this.Description = description;
        }

        public string Goods { get; }
        public DateTime StartDate { get; }
        public DateTime FinalDate { get;}
        public int Days { get; }
        public int Bet { get; }
        public string Description { get; }
        public DateTime DateOfStart { get; }
        public DateTime DateOfFinish { get; }
    }
}
