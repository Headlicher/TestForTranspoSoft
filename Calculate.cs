using System;
using System.Collections.Generic;
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
            try
            {
                ExcelWork ex = new ExcelWork();
                List<string> goodsList = ex.ListGeneric<string>(pathBox, 1, "A", 30);
                List<DateTime> startDatesList = ex.ListGeneric<DateTime>(pathBox, 1, "B", 30);
                List<DateTime> finalDatesList = ex.ListGeneric<DateTime>(pathBox, 1, "C", 30);
                List<int> tarifNumbersList = ex.ListGeneric<int>(pathBox, 2, "A", 5);
                List<int> periodStartByTarifList = ex.ListGeneric<int>(pathBox, 2, "B", 5);
                List<int> periodEndByTarifList = ex.ListGeneric<int>(pathBox, 2, "C", 4);
                periodEndByTarifList.Add(3650);
                List<int> betList = ex.ListGeneric<int>(pathBox, 2, "D", 5);

                if (startDate.Value != null && finalDate.Value != null)
                {
                    for (int i = 0; i < startDatesList.Count; i++)
                    {
                        if (startDate.Value <= startDatesList[i] || finalDate.Value >= startDatesList[i])
                        {
                            DateTime start;
                            DateTime final;

                            if (startDatesList[i] > startDate.Value)
                            {
                                start = startDatesList[i];
                            }
                            else start = (DateTime)startDate.Value;

                            if (finalDatesList[i] < finalDate.Value)
                            {
                                final = finalDatesList[i];
                            }
                            else final = (DateTime)finalDate.Value;

                            int days = (final - start).Days + 1;

                            DateTime periodStartCount = start;
                            DateTime periodEndCount;

                            for (int k = 0; k < tarifNumbersList.Count; k++)
                            {
                                if (days > 0)
                                {
                                    int daysOnTarif = 0;
                                    if (periodStartByTarifList[k] == 0)
                                        daysOnTarif = periodEndByTarifList[k] - periodStartByTarifList[k];
                                    else daysOnTarif = periodEndByTarifList[k] - periodStartByTarifList[k] + 1;


                                    if (daysOnTarif > days)
                                    {
                                        daysOnTarif = (final - periodStartCount).Days + 1;
                                    }

                                    periodEndCount = periodStartCount.AddDays(daysOnTarif).Date;
                                    periodEndCount -= TimeSpan.FromSeconds(1);
                                    if (final < periodEndCount)
                                        periodEndCount = final;
                                    data.Add(new Data(goodsList[i], periodStartCount, periodEndCount, daysOnTarif, startDatesList[i], finalDatesList[i], betList[k], "Период №" + tarifNumbersList[k]));
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
            catch
            {
                System.Windows.MessageBox.Show("Файл не выбран, пожалуйста, выберите файл.");
            }
        }
    }
}
