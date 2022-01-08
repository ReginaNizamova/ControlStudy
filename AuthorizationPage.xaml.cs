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

namespace Authorization
{
    /// <summary>
    /// Логика взаимодействия для AuthorizationPage.xaml
    /// </summary>


    public partial class AuthorizationPage : Page
    {
        public AuthorizationPage()
        {
            InitializeComponent(); 
        }

        private void InputAuthorization_Click(object sender, RoutedEventArgs e)
        {
            AuthorizationnEntities userContext = new AuthorizationnEntities();

            var Pass = userContext.Users.Where(top => top.Password == PasswordText.Text && top.LoginUser == LoginText.Text).FirstOrDefault();
            //var Login = userContext.Users.Where(top => top.LoginUser == LoginText.Text).FirstOrDefault();

            if (Pass != null)
            {
                //Manager.MainFrame.Navigate(new Page);   Добавить страницу личного кабинета
                MessageBox.Show("Вход выполнен!");
            }
            else
            {
                MessageBox.Show("Пароль не верный!");
            }

            userContext.Dispose();
        }
    }
}
