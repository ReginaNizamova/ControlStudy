using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace ControlStudy.Pages
{
    public partial class TeacherPage : Page
    {
        readonly SessionTimer Timer = new SessionTimer(); //Включение таймера

        public TeacherPage(string loginNowUser)
        {
            InitializeComponent();
            Application.Current.MainWindow.Closing += new CancelEventHandler(MainWindowClosing);
            void MainWindowClosing(object sender, CancelEventArgs e) // При закрытии формы сохраняет значение таймера
            {
                Timer.SaveTimeSession(loginNowUser);
            }
        }

        private void ReportsClick(object sender, RoutedEventArgs e)
        {
            //teacherFrame.Navigate(new TeacherReportsPage());
        }

        private void JournalClick(object sender, RoutedEventArgs e)
        {
            teacherFrame.Navigate(new TeacherJournalPage());
        }
    }
}
