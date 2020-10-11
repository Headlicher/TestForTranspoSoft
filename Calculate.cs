using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Linq;
using System.Collections.Generic;

namespace TestForTranspoSoft
{
    /// <summary>
    /// Класс, вызываемый при нажатии на кнопку "Расчёт"
    /// </summary>
    class Calculate
    {
        List<Data> Data = new List<Data>();

        public Calculate(DataGrid newTable, string pathToFile, DateTime? startDate, DateTime? finalDate)
        {
            if (DatesCheck(startDate, finalDate) && EmptyDatePicker(startDate, finalDate))
            {
                DataGenerate Lists = new DataGenerate(pathToFile); // Вызываем класс генерации списков
                Data = FullDataList(Lists).ToList(); // Присваиваем списку Data полную таблицу.
                List<Data> newData = new List<Data>(); //Создаём новый список, который будет содержать в себе изменения из списка Data и выведен в DataGrid
                IEnumerable<Data> query = Data.Where(data => startDate <= data.StartDate && finalDate >= data.StartDate || startDate <= data.FinalDate && finalDate >= data.FinalDate); //Выводим из полной таблицы только часть, соответствующую условию
                foreach (Data data in query) //Выполняем преобразования и добавляем в список newData
                {
                    if (startDate >= data.StartDate)
                        data.StartDate = startDate;
                    if (finalDate <= data.FinalDate)
                        data.FinalDate = finalDate;
                    data.Days = (data.FinalDate - data.StartDate).Value.Days + 1;
                    newData.Add(data);
                }
                newTable.ItemsSource = newData;

            }
            else if (EmptyDatePicker(startDate, finalDate))
                MessageBox.Show("Указанная дата(или время) окончания расчёта больше, чем дата(или время) начала расчёта, пожалуйста, проверьте правильность введённых данных");
            else
                MessageBox.Show("Не указаны даты начала и окончания расчёта");
        }

        public int DaysOnTarifCheck(int start, int end) // Генерация расчёта дней по тарифу.
        {
            if (start == 0)
                return end - start;

            else
                return end - start + 1;
        }

        public bool DatesCheck(DateTime? startDate, DateTime? finalDate) // Проверка на то, не больше ли дата начала расчёта, чем дата окончания.
        {
            if (startDate > finalDate)
            {
                return false;
            }
            return true;
        }

        public DateTime? IsEmptyFinalDate(DateTime final) // Проверка на то будет ли ячейка даты ухода со склада пустой.
        {
            if (final >= DateTime.Now - TimeSpan.FromSeconds(30))
            {
                return null;
            }
            return final;
        }

        public bool EmptyDatePicker(DateTime? startDate, DateTime? finalDate) // Проверка на то, ввёл пользователь даты или нет
        {
            if(startDate > DateTime.MinValue && finalDate > DateTime.MinValue)
            {
                return true;
            }
            return false;
        }

        public ObservableCollection<Data> FullDataList(DataGenerate Lists) //Генерация полной таблицы.
        {
            ObservableCollection<Data> fullData = new ObservableCollection<Data>(); // Создаём коллекцию, в которой будет лежать таблица
            for(int i = 0; i < Lists.startDatesList.Count; i++)
            {
                int days = (Lists.finalDatesList[i] - Lists.startDatesList[i]).Days; // Счётчик общего количества дней, которые товар лежал на складе

                DateTime periodStartCount = Lists.startDatesList[i]; //Счётчик даты начала расчёта
                DateTime periodEndCount; //Счётчик даты конца расчёта

                for (int k = 0; k < Lists.tarifNumbersList.Count; k++)
                {
                    if (days > 0)
                    {
                        int daysOnTarif; //Счётчик дней по тарифу.
                        daysOnTarif = DaysOnTarifCheck(Lists.periodStartByTarifList[k], Lists.periodEndByTarifList[k]);

                        if (daysOnTarif > days)
                            daysOnTarif = (Lists.finalDatesList[i] - periodStartCount).Days + 1;

                        periodEndCount = periodStartCount.AddDays(daysOnTarif).Date;
                        periodEndCount -= TimeSpan.FromSeconds(1);
                        if (Lists.finalDatesList[i] < periodEndCount)
                            periodEndCount = Lists.finalDatesList[i];

                        DateTime? finalDates = IsEmptyFinalDate(Lists.finalDatesList[i]);

                        fullData.Add(new Data(Lists.goodsList[i], periodStartCount, periodEndCount, daysOnTarif, Lists.startDatesList[i], finalDates,
                                          Lists.betList[k], "Период №" + Lists.tarifNumbersList[k]));

                        days -= daysOnTarif;
                        periodStartCount = periodEndCount + TimeSpan.FromSeconds(1);
                    }
                }
            }
            return fullData;
        }
    }
}
