using System;
using System.Windows;
using System.Windows.Media;


namespace ControlStudy
{
    public partial class MainWindow : Window
    {

        private static readonly Random random = new Random();

        public MainWindow()
        {
            InitializeComponent();
            Manager.MainFrame = frame;
            frame.Navigate(new AuthorizationPage());

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
