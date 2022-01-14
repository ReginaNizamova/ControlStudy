
using ControlStudy;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;


namespace Authorization.Pages
{
    public partial class AdminUsersPage : Page
    {
        readonly SessionTimer Timer = new SessionTimer(); //Включение таймера

        public AdminUsersPage(string loginNowUser)
        {
            InitializeComponent();

            dataGridAdmin.Items.Clear();
            dataGridAdmin.ItemsSource = ControlStudyEntities.GetContext().Users.ToList();

            Application.Current.MainWindow.Closing += new CancelEventHandler(MainWindowClosing);
            void MainWindowClosing(object sender, CancelEventArgs e) // При закрытии формы сохраняет значение таймера
            {
                Timer.SaveTimeSession(loginNowUser);
            }
        }

        private void AddClick(object sender, RoutedEventArgs e)
        {
            AddWindow window = new AddWindow(dataGridAdmin);
            window.Show();
        }

        //private void ChangeClick(object sender, RoutedEventArgs e)
        //{

        //}

        //private void DeleteClick(object sender, RoutedEventArgs e)
        //{

        //}

        //private void JournalClick(object sender, RoutedEventArgs e)
        //{

        //}

        //private void DeleteData(int personId) // Удаление данных 
        //{


        //    var result = (from Person in userContext.People
        //                  join User in userContext.Users on Person.IdPerson equals User.IdPerson
        //                  where Person.IdPerson == personId && User.IdPerson == personId
        //                  select Person.IdPerson).First();

        //    userContext.People.RemoveRange(userContext.People.Where(x => x.IdPerson == result));


        //    userContext.SaveChanges();
        //    userContext.Dispose();
        //}

        private void EditClick(object sender, RoutedEventArgs e)
        {

        }
    }
}
