using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.ComponentModel;
using Authorization.Pages;

namespace ControlStudy
{
    public partial class AdminPage : Page
    {
        readonly SessionTimer Timer = new SessionTimer(); //Включение таймера
       

        public AdminPage(string loginNowUser)
        {
            InitializeComponent();

            Application.Current.MainWindow.Closing += new CancelEventHandler(MainWindowClosing);

            void MainWindowClosing(object sender, CancelEventArgs e) // При закрытии формы сохраняет значение таймера
            {
                Timer.SaveTimeSession(loginNowUser);
            }
        }

        private void Users_Click(object sender, RoutedEventArgs e) //Переход на страницу UserPage
        {
            Manager.MainFrame.Navigate(new UsersPage());
        }

        private void Journal_Click(object sender, RoutedEventArgs e) //Выводит данные в DataGrid
        {
            //var result = from Person in userContext.People
            //             join User in userContext.Users on Person.IdPerson equals User.IdPerson
            //             join Role in userContext.Roles on User.IdRole equals Role.IdRole
            //             join Session in userContext.Sessions on Person.IdPerson equals Session.IdPerson
            //             select new
            //             {
            //                 Дата = Session.DateSession,
            //                 Время = Session.Time,
            //                 Роль = Role.Role1,
            //                 Логин = User.LoginUser,
            //                 Фамилия = Person.Family,
            //                 Имя = Person.Name,
            //             };

            //dataGridAdmin.ItemsSource = result.ToList();
        }

    }
}
