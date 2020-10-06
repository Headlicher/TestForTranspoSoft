using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace TestForTranspoSoft
{
    class DataGenerate
    {
        public List<string> goodsList;
        public List<DateTime> startDatesList;
        public List<DateTime> finalDatesList;
        public List<int> tarifNumbersList;
        public List<int> periodStartByTarifList;
        public List<int> periodEndByTarifList;
        public List<int> betList;

        public DataGenerate(TextBox pathBox)
        {
            ExcelWork ex = new ExcelWork();
            goodsList = ex.ListGeneric<string>(pathBox, 1, "A", 30);
            startDatesList = ex.ListGeneric<DateTime>(pathBox, 1, "B", 30);
            finalDatesList = ex.ListGeneric<DateTime>(pathBox, 1, "C", 30);
            tarifNumbersList = ex.ListGeneric<int>(pathBox, 2, "A", 5);
            periodStartByTarifList = ex.ListGeneric<int>(pathBox, 2, "B", 5);
            periodEndByTarifList = ex.ListGeneric<int>(pathBox, 2, "C", 4);
            periodEndByTarifList.Add(3650);
            betList = ex.ListGeneric<int>(pathBox, 2, "D", 5);
        }
    }
}
