using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using Xceed.Wpf.Toolkit;

namespace TestForTranspoSoft
{
    class Calculate
    {
        ObservableCollection<Data> data = new ObservableCollection<Data>();

        public Calculate(DataGrid newTable, TextBox pathBox, DateTimePicker startDate, DateTimePicker finalDate)
        {
            DataGenerate lists = new DataGenerate(pathBox);

            if (startDate.Value != null && finalDate.Value != null)
            {
                for (int i = 0; i < lists.startDatesList.Count; i++)
                {
                    if (startDate.Value <= lists.startDatesList[i] || finalDate.Value >= lists.startDatesList[i])
                    {
                        DateTime start;
                        DateTime final;
                        if (startDate.Value > lists.startDatesList[i])
                            start = lists.startDatesList[i];
                        else
                            start = (DateTime)startDate.Value;
                        
                        if (finalDate.Value > lists.finalDatesList[i])
                        {
                            final = lists.finalDatesList[i];
                        }
                        else final = (DateTime)finalDate.Value;

                        int days = (final - start).Days + 1;

                        DateTime periodStartCount = start;
                        DateTime periodEndCount;

                        for (int k = 0; k < lists.tarifNumbersList.Count; k++)
                        {
                            if (days > 0)
                            {
                                int daysOnTarif = daysOnTarifCheck(lists.periodStartByTarifList[k], lists.periodEndByTarifList[k]);

                                if (daysOnTarif > days)
                                    daysOnTarif = (final - periodStartCount).Days + 1;

                                periodEndCount = periodStartCount.AddDays(daysOnTarif).Date;
                                periodEndCount -= TimeSpan.FromSeconds(1);
                                if (final < periodEndCount)
                                    periodEndCount = final;

                                data.Add(new Data(lists.goodsList[i], periodStartCount, periodEndCount, daysOnTarif, lists.startDatesList[i], lists.finalDatesList[i], 
                                                  lists.betList[k], "Период №" + lists.tarifNumbersList[k]));

                                days -= daysOnTarif;
                                periodStartCount = periodEndCount + TimeSpan.FromSeconds(1);
                            }
                        }
                    }
                    newTable.ItemsSource = data;
                }
            }
            else
                System.Windows.MessageBox.Show("Выберите даты начала и окончания расчёта.");
        }

        public int daysOnTarifCheck(int start, int end)
        {
            if (start == 0)
                return end - start;

            else
                return end - start + 1;
        }
    }
}
