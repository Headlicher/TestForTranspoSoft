using System.Windows;
using Excel = Microsoft.Office.Interop.Excel;


namespace TestForTranspoSoft
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Excel.Application exApp = new Excel.Application();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void fileButton_Click(object sender, RoutedEventArgs e)
        {
           new OpenFile(pathBox);
        }

        private void calculationButton_Click(object sender, RoutedEventArgs e)
        {
            new Calculate(newTable, pathBox, startDate, finalDate);
            exApp.Quit();
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            exApp.Quit();
            this.Close();
        }
    }
}
