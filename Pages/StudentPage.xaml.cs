using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Data;
using System.Linq;
using Authorization;

namespace ControlStudy
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

        readonly ControlStudyEntities userContext = new ControlStudyEntities();

        private void WindowLoaded(object sender, RoutedEventArgs e) // Заполнение DataGrid
        {
            string loginNowUser = buttonProgress.Tag.ToString();

            var query = from Progress in userContext.Progresses
                        join Discipline in userContext.Disciplines on Progress.IdDiscipline equals Discipline.IdDiscipline
                        join Person in userContext.People on Progress.IdPerson equals Person.IdPerson
                        join User in userContext.Users on Person.IdPerson equals User.IdPerson
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
