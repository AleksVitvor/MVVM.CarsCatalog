using System.Windows;

namespace MVVM.CarsCatalog
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Singleton.getInstance();
            DataContext = new CarViewModel();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            ((CarViewModel)DataContext).AddCar();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            ((CarViewModel)DataContext).DeleteCar();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Singleton.Close();
        }
    }

}