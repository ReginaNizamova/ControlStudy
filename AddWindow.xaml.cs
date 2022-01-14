using ControlStudy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace Authorization
{
    public partial class AddWindow : Window
    {
        private User _currentUser = new User();
        public AddWindow(DataGrid dataGridAdmin)
        {
            InitializeComponent();
            DataContext = _currentUser;
            comboBoxGroup.ItemsSource = ControlStudyEntities.GetContext().Groups.ToList();
            comboBoxRole.ItemsSource = ControlStudyEntities.GetContext().Roles.ToList();
            //dataGrid = dataGridAdmin;
        }

        private void AddUserClick(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(_currentUser.Person.Family) || string.IsNullOrWhiteSpace(_currentUser.Person.Name) || string.IsNullOrWhiteSpace(_currentUser.Person.Patronimic))
                errors.AppendLine("ФИО");
            if (_currentUser.Role.Role1 == null)
                errors.AppendLine("Роль");
            if (_currentUser.Person.Group.Group1 == null)
                errors.AppendLine("Группа (если роль не студент выбрать пустую строку)");
            if (string.IsNullOrWhiteSpace(_currentUser.LoginUser))
                errors.AppendLine("Логин");
            if (string.IsNullOrWhiteSpace(_currentUser.Password))
                errors.AppendLine("Пароль");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (_currentUser.IdUser == 0)
                ControlStudyEntities.GetContext().Users.Add(_currentUser);

            try
            {
                ControlStudyEntities.GetContext().SaveChanges();
                MessageBox.Show("Пользователь добавлен!");
                Manager.MainFrame.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

            //string loginUser = loginText.Text;               // Присвоение переменным значений из textbox
            //string familyPerson = familyText.Text;
            //string namePerson = nameText.Text;
            //string patronimicPerson = patronimicText.Text;
            //string group = comboBoxGroup.Text;
            //string passwordPerson = passwordPersonText.Password;
            //string roleRole = comboBoxRole.Text; 

            //if (loginText.Text == "" || familyText.Text == "" || nameText.Text == "" || patronimicText.Text == "" || passwordPersonText.Password == "" || comboBoxRole.Text == "")
            //{
            //    MessageBox.Show("Заполните все поля!");
            //}
            //else
            //{
            //    if (CheckPass() == false)
            //        MessageBox.Show("Пароль введен не верно! Проверьте правильность пароля!");
            //    else
            //    {
            //        Addition(loginUser, familyPerson, namePerson, patronimicPerson, group, passwordPerson, roleRole);

            //        MessageBox.Show("Пользователь добавлен!");
            //    }
            //}
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

            else if (group == " ")
                idGroup = 6;

            return idGroup;
        }

        private bool CheckPass() // Проверка пароля
        {
            var input = passwordPersonText.Text;

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
        //public static void Addition(string login, string family, string name, string patronimic, string group, string password, string role)
        //{
        //    ControlStudyEntities userContext = new ControlStudyEntities();

        //    User user = new User
        //    {
        //        LoginUser = login,
        //        Password = password,
        //        IdRole = FillIdRole(role)
        //    };

        //    userContext.Users.Add(user);


        //    Person person = new Person
        //    {
        //        Family = family,
        //        Name = name,
        //        Patronimic = patronimic,
        //        IdGroup = FillIdGroup(group)
        //    };
        //    userContext.People.Add(person);

        //    userContext.SaveChanges();        // Сохранение изменений
        //    userContext.Dispose();
        //}

        private void WindowIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e) // Обновляет данные в DataGrid
        {
            ControlStudyEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
            ControlStudyEntities.GetContext().Users.ToList();
        }
    }
}    
