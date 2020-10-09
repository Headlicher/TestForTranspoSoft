using System.Windows;


namespace TestForTranspoSoft
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
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
            try
            {
                new Calculate(newTable, pathBox.Text, startDate.Value, finalDate.Value);
            }
            catch
            {
                MessageBox.Show("Выберите файл для работы.");
            }
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
