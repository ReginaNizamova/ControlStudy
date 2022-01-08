using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.Entity;


namespace Authorization
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private static readonly Random random = new Random();

        public MainWindow()
        {
            InitializeComponent();
            Manager.MainFrame = this.frame;
            frame.Navigate(new RegistrationPage());

            Array images = (Array)Resources["Images"];                                    //Массив картинок для фона
            BackImage.Source = (ImageSource)images.GetValue(random.Next(images.Length));  //Случайный выбор картинки
        }

        private void Frame_ContentRendered(object sender, EventArgs e)
        {
            if (Manager.MainFrame.NavigationService.CanGoBack == true)
            {
                buttonBack.Visibility = Visibility.Visible;
            }
            else
            {
                buttonBack.Visibility = Visibility.Collapsed;
            }
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)         //Возврат на предыдущую страницу
        {
            Manager.MainFrame.GoBack();
        }

    }
}
