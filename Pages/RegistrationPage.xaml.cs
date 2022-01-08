using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using System.Windows.Controls.Primitives;

namespace Authorization
{
    public partial class RegistrationPage : Page
    {
        public RegistrationPage()
        {
            InitializeComponent();
        }

        // Добаление в класс значений, введенных пользователем
        public static void Addition(string login, string family, string name, string patronimic, string birthday, string gender, string email, string group, string password, string role)
        {
            AuthorizationnEntities userContext = new AuthorizationnEntities();
   
            if (role == "Студент")
            {
                User user = new User            
                {
                    LoginUser = login,
                    Password = password,
                    CodeRole = 1
                };

                userContext.Users.Add(user);
            }

            else if (role == "Преподаватель")
            {
                User user = new User
                {
                    LoginUser = login,
                    Password = password,
                    CodeRole = 2
                };

                userContext.Users.Add(user);
            }

            else if (role == "Администратор")
            {
                User user = new User
                {
                    LoginUser = login,
                    Password = password,
                    CodeRole = 3
                };

                userContext.Users.Add(user);
            }


            if (group == "101" && role == "Студент")
            {
                Person person = new Person
                {
                    Family = family,
                    Name = name,
                    Patronimic = patronimic,
                    Birthday = DateTime.Parse(birthday),
                    Gender = gender,
                    Email = email,
                    CodeGroup = 1
                };
                userContext.People.Add(person);        
            }

            else if (group == "201" && role == "Студент")
            {
                Person person = new Person
                {
                    Family = family,
                    Name = name,
                    Patronimic = patronimic,
                    Birthday = DateTime.Parse(birthday),
                    Gender = gender,
                    Email = email,
                    CodeGroup = 2
                };
                userContext.People.Add(person);
            }

            else if (group == "301" && role == "Студент")
            {
                Person person = new Person
                {
                    Family = family,
                    Name = name,
                    Patronimic = patronimic,
                    Birthday = DateTime.Parse(birthday),
                    Gender = gender,
                    Email = email,
                    CodeGroup = 3
                };
                userContext.People.Add(person);
            }

            else if (group == "401" && role == "Студент")
            {
                Person person = new Person
                {
                    Family = family,
                    Name = name,
                    Patronimic = patronimic,
                    Birthday = DateTime.Parse(birthday),
                    Gender = gender,
                    Email = email,
                    CodeGroup = 4
                };
                userContext.People.Add(person);
            }

            else if (group == "501" && role == "Студент")
            {
                Person person = new Person
                {
                    Family = family,
                    Name = name,
                    Patronimic = patronimic,
                    Birthday = DateTime.Parse(birthday),
                    Gender = gender,
                    Email = email,
                    CodeGroup = 5
                };
                userContext.People.Add(person);  // Запись значений в таблицы базы данных
            }
            else 
            {
                Person person = new Person
                {
                    Family = family,
                    Name = name,
                    Patronimic = patronimic,
                    Birthday = DateTime.Parse(birthday),
                    Gender = gender,
                    Email = email,
                    CodeGroup = 6
                };
                userContext.People.Add(person);  // Запись значений в таблицы базы данных
            }
 
            userContext.SaveChanges();        // Сохранение изменений
            userContext.Dispose();
        }

        private void Registration_Click(object sender, RoutedEventArgs e)
        {
            string loginUser = LoginText.Text;               // Присвоение переменным значений из textbox
            string familyPerson = FamilyText.Text;
            string namePerson = NameText.Text;
            string patronimicPerson = PatronimicText.Text;
            string birthdayPerson = BirthdayText.Text;
            string genderPerson = ComboBoxGender.Text;
            string emailPerson = EmailText.Text;
            string group = ComboBoxGroup.Text;
            string passwordPerson = PasswordPersonText.Password;
            string roleRole = ComboBoxRole.Text;


            if (LoginText.Text == "" || FamilyText.Text == "" || NameText.Text == "" || PatronimicText.Text == "" || BirthdayText.Text == "" || ComboBoxGender.Text == "" || EmailText.Text == "" ||  PasswordPersonText.Password == "" || ComboBoxRole.Text == "")
            {
                MessageBox.Show("Заполните все поля!");
            }
            else
            {
                if (CheckPass() == false)
                    MessageBox.Show("Пароль введен не верно! Проверьте правильность пароля!");
                else
                {
                    Addition(loginUser, familyPerson, namePerson, patronimicPerson, birthdayPerson, genderPerson, emailPerson, group, passwordPerson, roleRole);

                    MessageBox.Show("Регистрация прошла успешно!"); 
                }
            } 

            Clean();
        }

        private void Input_Click(object sender, RoutedEventArgs e) // Переход на страницу авторизации
        {
            Manager.MainFrame.Navigate(new AuthorizationPage()); 
        } 

        private void Clean () //Очищение полей регистрации
        {
            LoginText.Text = "";
            FamilyText.Text = "";
            NameText.Text = "";
            PatronimicText.Text = "";
            BirthdayText.Text = "";
            ComboBoxGender.Text = "";
            EmailText.Text = "";
            ComboBoxGroup.Text = "";
            PasswordPersonText.Password = "";
            ComboBoxRole.Text = "";
        }

        private bool CheckPass() // Проверка пароля
        {
            var input = PasswordPersonText.Password;

            var minMaxChar = new Regex(@".{8}");
            var number = new Regex(@"[0-9]+");
            var upperChar = new Regex(@"[A-Z]");
            var lowerChar = new Regex(@"[a-z]");
            var symbols = new Regex(@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");

            if (!lowerChar.IsMatch(input))
                return false;
            

            else if (!upperChar.IsMatch(input))
                return false;
            

            else if (!minMaxChar.IsMatch(input))
                return false;
            

            else if (!number.IsMatch(input))
                return false;
            

            else if (!symbols.IsMatch(input))
                return false;
            
            else 
                return true;
            
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