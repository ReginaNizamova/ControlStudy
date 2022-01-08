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
using System.ComponentModel;

namespace Authorization
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

        private void AdminButton_Click(object sender, RoutedEventArgs e) //Выводит данные в DataGrid
        {
            AuthorizationnEntities userContext = new AuthorizationnEntities();

            var result = from Person in userContext.People 
                         join User in userContext.Users on Person.CodePerson equals User.CodePerson
                         join Role in userContext.Roles on User.CodeRole equals Role.CodeRole
                         select new
                         { 
                             Логин = User.LoginUser,
                             Должность = Role.Role1,
                             Фамилия = Person.Family,
                             Имя = Person.Name,
                             Отчество = Person.Patronimic
                         };

            dataGridAdmin.ItemsSource = result.ToList();
        }

        private void DataUser (object sender, RoutedEventArgs e) // Выводит информацию о последней сессии пользователя
        {
            labelDataUser.Background = Brushes.Azure;
            labelDataUser.Content = SessionTimer.DataLastSession(loginTextBox.Text);
        }
    }
}
