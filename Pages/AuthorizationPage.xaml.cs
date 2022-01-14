using Authorization;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;


namespace ControlStudy
{
    public partial class AuthorizationPage : Page
    {

        public AuthorizationPage()
        {
            InitializeComponent(); 
        }

        private void InputAuthorization_Click(object sender, RoutedEventArgs e)
        {
            ControlStudyEntities userContext = new ControlStudyEntities();

            var check = userContext.Users.Where(top => top.Password == PasswordPersonText.Password && top.LoginUser == LoginText.Text).FirstOrDefault(); // Проверка пароля и логина

            if (check != null) // Переход на соответствующую страницу
            {
                List<int> userRole = (from User in userContext.Users where User.LoginUser == LoginText.Text select User.IdRole).ToList();

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
}
