using Authorization;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Authorization.Pages;

namespace ControlStudy
{
    public partial class AuthorizationPage : Page
    {
        public AuthorizationPage()
        {
            InitializeComponent(); 
        }

        private void InputAuthorizationClick(object sender, RoutedEventArgs e)
        {
            ControlStudyEntities userContext = new ControlStudyEntities();

            var check = userContext.Users.Where(top => top.Password == passwordPersonText.Password && top.LoginUser == loginText.Text).FirstOrDefault(); // Проверка пароля и логина

            if (check != null) // Переход на соответствующую страницу
            {
                List<int> userRole = (from User in userContext.Users where User.LoginUser == loginText.Text select User.IdRole).ToList();

                if (userRole[0] == 1)
                {
                    Manager.MainFrame.Navigate(new StudentPage(loginText.Text));
                }
                else if (userRole[0] == 2)
                {
                    Manager.MainFrame.Navigate(new TeacherPage(loginText.Text));
                }
                else if (userRole[0] == 3)
                {
                    Manager.MainFrame.Navigate(new AdminPage(loginText.Text));
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
            loginText.Text = "";
            passwordPersonText.Password = "";
        }

        private void PasswordPersonTextPasswordChanged(object sender, RoutedEventArgs e) // Открывает и скрывает watermark поля Password
        {
            if (passwordPersonText.Password.Length == 0)
            {
                passwordText.Visibility = Visibility.Visible;
            }
            else
            {
                passwordText.Visibility = Visibility.Hidden;
            }
        }
    }
}
