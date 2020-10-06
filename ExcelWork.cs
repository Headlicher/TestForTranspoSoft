using System;
using System.Collections.Generic;
using System.Windows.Controls;
using Excel = Microsoft.Office.Interop.Excel;

namespace TestForTranspoSoft
{
    class ExcelWork
    {
        Excel.Application exApp = new Excel.Application();
        public ExcelWork()
        {
            exApp.Quit();
        }

        public List<T> ListGeneric<T>(TextBox pathBox, int sheetNumber, string charOfColumn, int countOfRows)
        {
            Excel.Workbook workbook = exApp.Workbooks.Open(pathBox.Text);
            Excel.Worksheet sheet = (Excel.Worksheet)workbook.Sheets[sheetNumber];

            List <T> list = new List<T>();
            for (int i = 2; i <= countOfRows; i++)
            {
                Excel.Range range = sheet.get_Range(charOfColumn + i.ToString(), charOfColumn + i.ToString());
                if (range.Text != "")
                {
                    list.Add((T)Convert.ChangeType(range.Text, typeof(T)));
                }
                else
                    list.Add((T)Convert.ChangeType(DateTime.Now, typeof(T)));
            }
            return list;
        }
    }
}
