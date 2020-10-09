using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace TestForTranspoSoft
{
    class Calculate
    {
        ObservableCollection<Data> data = new ObservableCollection<Data>();

        public Calculate(DataGrid newTable, string pathToFile, DateTime? startDate, DateTime? finalDate)
        {
            if (datesCheck(startDate, finalDate) && emptyDatePicker(startDate, finalDate))
            {
                DataGenerate lists = new DataGenerate(pathToFile);
                for (int i = 0; i < lists.startDatesList.Count; i++)
                {
                    if (startDate <= lists.startDatesList[i] || finalDate >= lists.startDatesList[i])
                    {
                        DateTime start;
                        DateTime final;

                        if (lists.startDatesList[i] > startDate)
                            start = lists.startDatesList[i];
                        else
                            start = (DateTime)startDate;

                        if (finalDate > lists.finalDatesList[i])
                        {
                            final = lists.finalDatesList[i];
                        }
                        else final = (DateTime)finalDate;

                        int days = (final - start).Days + 1;

                        DateTime periodStartCount = start;
                        DateTime periodEndCount;

                        for (int k = 0; k < lists.tarifNumbersList.Count; k++)
                        {
                            if (days > 0)
                            {
                                int daysOnTarif;
                                daysOnTarif = daysOnTarifCheck(lists.periodStartByTarifList[k], lists.periodEndByTarifList[k]);

                                if (daysOnTarif > days)
                                    daysOnTarif = (final - periodStartCount).Days + 1;

                                periodEndCount = periodStartCount.AddDays(daysOnTarif).Date;
                                periodEndCount -= TimeSpan.FromSeconds(1);
                                if (final < periodEndCount)
                                    periodEndCount = final;

                                DateTime? finalDates = isEmptyFinalDate(lists.finalDatesList[i]);

                                data.Add(new Data(lists.goodsList[i], periodStartCount, periodEndCount, daysOnTarif, lists.startDatesList[i], finalDates,
                                                  lists.betList[k], "Период №" + lists.tarifNumbersList[k]));

                                days -= daysOnTarif;
                                periodStartCount = periodEndCount + TimeSpan.FromSeconds(1);
                            }
                        }
                    }
                    newTable.ItemsSource = data;
                }
            }
            else if (emptyDatePicker(startDate, finalDate))
                MessageBox.Show("Указанная дата(или время) окончания расчёта больше, чем дата(или время) начала расчёта, пожалуйста, проверьте правильность введённых данных");
            else
                MessageBox.Show("Не указаны даты начала и окончания расчёта");
        }

        public int daysOnTarifCheck(int start, int end)
        {
            if (start == 0)
                return end - start;

            else
                return end - start + 1;
        }

        public bool datesCheck(DateTime? startDate, DateTime? finalDate)
        {
            if (startDate > finalDate)
            {
                return false;
            }
            return true;
        }

        public DateTime? isEmptyFinalDate(DateTime final)
        {
            if (final >= DateTime.Now - TimeSpan.FromSeconds(30))
            {
                return null;
            }
            return final;
        }

        public bool emptyDatePicker(DateTime? startDate, DateTime? finalDate)
        {
            if(startDate > DateTime.MinValue && finalDate > DateTime.MinValue)
            {
                return true;
            }
            return false;
        }
    }
}
