using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using Timer = System.Timers.Timer;


namespace Authorization
{
    public partial class AuthorizationPage : Page
    {
        public AuthorizationPage()
        {
            InitializeComponent(); 
        }

        private void InputAuthorization_Click(object sender, RoutedEventArgs e)
        {
            AuthorizationnEntities userContext = new AuthorizationnEntities();



            var check = userContext.Users.Where(top => top.Password == PasswordPersonText.Password && top.LoginUser == LoginText.Text).FirstOrDefault(); // Проверка пароля и логина

            if (check != null) // Переход на соответствующую страницу
            {
                List<int> userRole = (from User in userContext.Users where User.LoginUser == LoginText.Text select User.CodeRole).ToList();

                if (userRole[0] == 1)
                {
                    Manager.MainFrame.Navigate(new StudentPage(LoginText.Text));
                }
                else if (userRole[0] == 2)
                {
                    Manager.MainFrame.Navigate(new TeacherPage(LoginText.Text));
                }
                else if (userRole[0] == 3)
                {
                    Manager.MainFrame.Navigate(new AdminPage(LoginText.Text));
                }
            }
            else
            {
                MessageBox.Show("Не правильный логин или пароль!");
            }
            userContext.Dispose();

            Clean();
        }

        private void Clean() //Очищение полей авторизации
        {
            LoginText.Text = "";
            PasswordPersonText.Password = "";
        }

        private void PasswordPersonText_PasswordChanged(object sender, RoutedEventArgs e) // Открывает и скрывает watermark поля Password
        {
            if (PasswordPersonText.Password.Length == 0)
            {
                passwordText.Visibility = Visibility.Visible;
            }
            else
            {
                passwordText.Visibility = Visibility.Hidden;
            }
        }
    }

    public class SessionTimer //Таймер сессии
    {
        public DateTime startTime; 
        public DateTime endTime;
        readonly Timer timer = new Timer();

        public SessionTimer() //Включение таймера
        {
            timer.Interval = 1000;
            timer.Start();
            startTime = DateTime.Now;
        }

        public void SaveTimeSession (string login) // Сохранение таймера
        {
            endTime = DateTime.Now;
            TimeSpan ts = endTime - startTime;
            DateTime Date = DateTime.Now;

            AuthorizationnEntities userContext = new AuthorizationnEntities();
            
            int codePerson = userContext.People.Where(c => c.User.LoginUser== login).Select(c => c.CodePerson).FirstOrDefault();

            Session sessionUser = new Session
            {
                DateSession = Date,
                CodePerson = codePerson,
                Time = Convert.ToString(ts.Hours + ":" + ts.Minutes + ":" + ts.Seconds)
            };

            userContext.Sessions.Add(sessionUser);
            userContext.SaveChanges();
            timer.Stop();
        }

        public static string DataLastSession (string login) //Получение данных последней сессии
        {
            AuthorizationnEntities userContext = new AuthorizationnEntities();

            int codePerson = userContext.People.Where(c => c.User.LoginUser == login).Select(c => c.CodePerson).FirstOrDefault();
            string time = userContext.Sessions.Where(c => c.CodePerson == codePerson).OrderByDescending(c => c.CodeSession).Select(c => c.Time).FirstOrDefault();
            string date = Convert.ToString(userContext.Sessions.Where(c => c.CodePerson == codePerson).OrderByDescending(c => c.CodeSession).Select(c => c.DateSession).FirstOrDefault());

            if (date == "01.01.0001 0:00:00")
                return "Нет данных";

            return date.ToString() + '\n' + time;

        }
    }
}
