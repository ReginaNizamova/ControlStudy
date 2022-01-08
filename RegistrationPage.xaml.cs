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
    /// Логика взаимодействия для RegPage.xaml
    /// </summary>
    public partial class RegistrationPage : Page
    {
        public RegistrationPage()
        {
            InitializeComponent();
        }

        public static void Addition(string LoginUser, string FamilyPerson, string NamePerson, string PatronimicPerson, string BirthdayPerson, string GenderPerson, string EmailPerson, string PhoneNumberPerson, string PasswordUser, string RoleRole)
        {
            AuthorizationnEntities userContext = new AuthorizationnEntities();

            Person person = new Person  // Добаление в класс значений, введенных пользователем
            {
                Family = FamilyPerson,
                Name = NamePerson,
                Patronimic = PatronimicPerson,
                Birthday = DateTime.Parse(BirthdayPerson),
                Gender = GenderPerson,
                Email = EmailPerson,
                PhoneNumber = PhoneNumberPerson,
            };


            Role role = new Role       // Добаление в класс значений, введенных пользователем
            {
                Role1 = RoleRole
            };


            User user = new User      // Добаление в класс значений, введенных пользователем
            {
                LoginUser = LoginUser,
                Password = PasswordUser
                
                
            };

                //switch (RoleRole)
                //{

                //case "Студент":
                //{
                //   CodeRole = 1;
                //   break;

                //}

                //case "Преподаватель":
                //{
                //    CodeRole = 2;
                //    break;
                //}

                //case "Администратор":
                //{
                //    CodeRole = 3;
                //    break;
                //}
                //}
           

            userContext.People.Add(person);    // Запись значений в таблицы базы данных
            //userContext.Roles.Add(role);
            userContext.Users.Add(user);
            userContext.SaveChanges();        // Сохранение изменений
            userContext.Dispose();
        }


        private void Registration_Click(object sender, RoutedEventArgs e)
        {
          

            string LoginUser = LoginText.Text;               // Присвоение переменным значений из textbox
            string FamilyPerson = FamilyText.Text;
            string NamePerson = NameText.Text;
            string PatronimicPerson = PatronimicText.Text;
            string BirthdayPerson = BirthdayText.Text;
            string GenderPerson = ComboBoxGender.Text;
            string EmailPerson = EmailText.Text;
            string PhoneNumberPerson = PhoneNumberText.Text;
            string PasswordPerson = PasswordPersonText.Text;
            string RoleRole = ComboBoxRole.Text;

            if (LoginText.Text == "" || FamilyText.Text == "" || NameText.Text == "" || PatronimicText.Text == "" || BirthdayText.Text == "" || ComboBoxGender.Text == "" || EmailText.Text == "" || PhoneNumberText.Text == "" || PasswordPersonText.Text == "" || ComboBoxRole.Text == "")
            {
                MessageBox.Show("Заполните все поля!"); 
            }
            else
            {
                Addition(LoginUser, FamilyPerson, NamePerson, PatronimicPerson, BirthdayPerson, GenderPerson, EmailPerson, PhoneNumberPerson, PasswordPerson, RoleRole);

                MessageBox.Show("Регистрация прошла успешно!");
            }
        }

        private void Input_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AuthorizationPage());          // Переход на другую страницу
        }    
    }
}
