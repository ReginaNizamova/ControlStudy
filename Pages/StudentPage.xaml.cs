using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public partial class StudentPage : Page
    {
        readonly SessionTimer Timer = new SessionTimer(); //Dключение таймера

        public StudentPage(string loginNowUser)
        {
            InitializeComponent();

            Application.Current.MainWindow.Closing += new CancelEventHandler(MainWindowClosing);

            void MainWindowClosing(object sender, CancelEventArgs e) // При закрытии формы сохраняет значение таймера
            {
                Timer.SaveTimeSession(loginNowUser);
            }

            buttonProgress.Tag = loginNowUser;
        }

        readonly AuthorizationnEntities userContext = new AuthorizationnEntities();

        private void WindowLoaded(object sender, RoutedEventArgs e) // Заполнение DataGrid
        {
            string loginNowUser = buttonProgress.Tag.ToString();

            var query = from Progress in userContext.Progresses
                        join Discipline in userContext.Disciplines on Progress.CodeDiscipline equals Discipline.CodeDiscipline
                        join Person in userContext.People on Progress.CodePerson equals Person.CodePerson
                        join User in userContext.Users on Person.CodePerson equals User.CodePerson
                        where User.LoginUser == loginNowUser
                        select new
                        {
                            Дисциплина = Discipline.Discipline1,
                            Оценка = Progress.Grade,
                            Дата = Progress.DateGrade.ToString()
                        };

            dataGridProgress.ItemsSource = query.ToList(); 
        }
    }
}
