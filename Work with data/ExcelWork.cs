using System;
using System.Collections.Generic;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
using System.Windows;

namespace TestForTranspoSoft
{
    /// <summary>
    /// Класс, отвечающий за работу с Excel файлами.
    /// </summary>
    class ExcelWork
    {
        readonly Excel.Application exApp = new Excel.Application();

        public List<T> ListGeneric<T>(string pathToFile, int sheetNumber, string charOfColumn)
        {
            Excel.Workbook workbook = exApp.Workbooks.Open(pathToFile);
            Excel.Worksheet sheet = (Excel.Worksheet)workbook.Sheets[sheetNumber];
            int countOfRows = sheet.Cells.Find("*", Missing.Value, Missing.Value, Missing.Value, Excel.XlSearchOrder.xlByRows, Excel.XlSearchDirection.xlPrevious, false, 
                                                Missing.Value, Missing.Value).Row; //Подсчёт количества заполненных строк в таблице

            List <T> list = new List<T>(); // Генерация листа, заданного типа
            for (int i = 2; i <= countOfRows; i++)
            {
                Excel.Range range = sheet.get_Range(charOfColumn + i.ToString(), charOfColumn + i.ToString());
                try
                {
                    if (range.Text != "")
                        list.Add((T)Convert.ChangeType(range.Text, typeof(T)));
                    else
                    {
                        T typeCheck = default;
                        if (typeCheck is DateTime)
                            list.Add((T)Convert.ChangeType(DateTime.Now, typeof(T)));
                        else if (typeCheck is int)
                            list.Add((T)Convert.ChangeType(int.MaxValue, typeof(T)));
                        else
                            list.Add((T)Convert.ChangeType("", typeof(T)));
                    }
                }
                catch(FormatException)
                {
                    MessageBox.Show("Выбранный документ не поддерживается.");
                }
            }
            workbook.Close();
            exApp.Quit();
            return list;
        }
    }
}
