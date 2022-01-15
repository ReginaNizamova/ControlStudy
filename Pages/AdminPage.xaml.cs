using ControlStudy;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace Authorization.Pages
{
    public partial class AdminPage : Page
    {
        readonly SessionTimer Timer = new SessionTimer(); //Включение таймера

        public AdminPage(string loginNowUser)
        {
            InitializeComponent();

            adminFrame.Navigate(new AdminUsersPage());

            Application.Current.MainWindow.Closing += new CancelEventHandler(MainWindowClosing);
            void MainWindowClosing(object sender, CancelEventArgs e) // При закрытии формы сохраняет значение таймера
            {
                Timer.SaveTimeSession(loginNowUser);
            }
        }

        private void UsersClick(object sender, RoutedEventArgs e)
        {
            adminFrame.Navigate(new AdminUsersPage());
        }

        private void JournalClick(object sender, RoutedEventArgs e)
        {
            adminFrame.Navigate(new AdminJournalPage());
        }
    }
}
