using ControlStudy;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace Authorization
{
    public partial class AddWindow : Window
    {
        public AddWindow()
        {
            InitializeComponent();
            ComboBoxGroup.ItemsSource = ControlStudyEntities.GetContext().Groups.ToList();
            ComboBoxRole.ItemsSource = ControlStudyEntities.GetContext().Roles.ToList();
        }


        private void addUserClick(object sender, RoutedEventArgs e)
        {
            string loginUser = LoginText.Text;               // Присвоение переменным значений из textbox
            string familyPerson = FamilyText.Text;
            string namePerson = NameText.Text;
            string patronimicPerson = PatronimicText.Text;
            string group = ComboBoxGroup.Text;
            string passwordPerson = PasswordPersonText.Password;
            string roleRole = ComboBoxRole.Text; 

            if (LoginText.Text == "" || FamilyText.Text == "" || NameText.Text == "" || PatronimicText.Text == "" || PasswordPersonText.Password == "" || ComboBoxRole.Text == "")
            {
                MessageBox.Show("Заполните все поля!");
            }
            else
            {
                if (CheckPass() == false)
                    MessageBox.Show("Пароль введен не верно! Проверьте правильность пароля!");
                else
                {
                    Addition(loginUser, familyPerson, namePerson, patronimicPerson, group, passwordPerson, roleRole);

                    MessageBox.Show("Регистрация прошла успешно!");


                }
            }
        }

        private static int FillIdRole(string role)
        {
            int id = 0;

            if (role == "Студент")
                id = 1;     

            else if (role == "Преподаватель")
                id = 2;

            else if (role == "Администратор")
                id = 3;

            return id;  
        }

        private static int FillIdGroup(string group)
        {
            int idGroup = 0;

            if (group == "115")
                idGroup = 1;

            else if (group == "215")
                idGroup = 2;

            else if (group == "315")
                idGroup = 3;

            else if (group == "415")
                idGroup = 4;

            else if (group == "515")
                idGroup = 5;

            return idGroup;
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

        // Добаление в класс значений, введенных пользователем
        public static void Addition(string login, string family, string name, string patronimic, string group, string password, string role)
        {
            ControlStudyEntities userContext = new ControlStudyEntities();

            User user = new User
            {
                LoginUser = login,
                Password = password,
                IdRole = FillIdRole(role)
            };

            userContext.Users.Add(user);


            Person person = new Person
            {
                Family = family,
                Name = name,
                Patronimic = patronimic,
                IdGroup = FillIdGroup(group)
            };
            userContext.People.Add(person);

            userContext.SaveChanges();        // Сохранение изменений
            userContext.Dispose();
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
