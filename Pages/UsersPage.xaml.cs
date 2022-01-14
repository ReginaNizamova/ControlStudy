using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Authorization.Pages
{
    public partial class UsersPage : Page
    {
        readonly ControlStudyEntities userContext = new ControlStudyEntities();
        List<object> list = new List<object>();

        public UsersPage()
        {
            InitializeComponent();

            //var result = from Person in userContext.People
            //             join User in userContext.Users on Person.IdPerson equals User.IdPerson
            //             join Role in userContext.Roles on User.IdRole equals Role.IdRole
            //             join Group in userContext.Groups on Person.IdGroup equals Group.IdGroup
            //             select new
            //             {
            //                 Роль = Role.Role1,
            //                 Логин = User.LoginUser,
            //                 Пароль = User.Password,
            //                 Группа = Group.Group1,
            //                 Фамилия = Person.Family,
            //                 Имя = Person.Name,
            //                 Отчество = Person.Patronimic
            //             };

            //foreach (var item in result)
            //{
            //    list.Add(item);
            //}

            //dataGridAdmin.ItemsSource = list;
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            AddWindow window = new AddWindow();
            window.Show();
        }

        private void change_Click(object sender, RoutedEventArgs e)
        {

        }

        private void delete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Journal_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                ControlStudyEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());

                var result = from Person in userContext.People
                             join User in userContext.Users on Person.IdPerson equals User.IdPerson
                             join Role in userContext.Roles on User.IdRole equals Role.IdRole
                             join Group in userContext.Groups on Person.IdGroup equals Group.IdGroup
                             select new
                             {
                                 Роль = Role.Role1,
                                 Логин = User.LoginUser,
                                 Пароль = User.Password,
                                 Группа = Group.Group1,
                                 Фамилия = Person.Family,
                                 Имя = Person.Name,
                                 Отчество = Person.Patronimic
                             };

                foreach (var item in result)
                {
                    list.Add(item);
                }

                dataGridAdmin.ItemsSource = list;
            }         
        }
    }
}
