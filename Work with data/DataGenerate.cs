using System;
using System.Collections.Generic;

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

        public DataGenerate(string pathToExcelFile)
        {
            ExcelWork ex = new ExcelWork();
            goodsList = ex.ListGeneric<string>(pathToExcelFile, 1, "A");
            startDatesList = ex.ListGeneric<DateTime>(pathToExcelFile, 1, "B");
            finalDatesList = ex.ListGeneric<DateTime>(pathToExcelFile, 1, "C");
            tarifNumbersList = ex.ListGeneric<int>(pathToExcelFile, 2, "A");
            periodStartByTarifList = ex.ListGeneric<int>(pathToExcelFile, 2, "B");
            periodEndByTarifList = ex.ListGeneric<int>(pathToExcelFile, 2, "C");
            betList = ex.ListGeneric<int>(pathToExcelFile, 2, "D");
        }
    }
}
