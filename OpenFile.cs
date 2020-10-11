using Microsoft.Win32;

namespace TestForTranspoSoft
{
    /// <summary>
    /// Класс, отвечающий за открытие файлов.
    /// </summary>
    class OpenFile
    {
        public string FilePath { get; set; }

        public OpenFile(System.Windows.Controls.TextBox pathBox)
        {
            var dialog = new OpenFileDialog();
            dialog.DefaultExt = ".xlsx";
            dialog.Filter = "Таблицы Excel (.xlsx)|*.xlsx";

            if (dialog.ShowDialog() == true)
            {
                FilePath = dialog.FileName;
                pathBox.Text = FilePath;
            }
        }
    }
}
