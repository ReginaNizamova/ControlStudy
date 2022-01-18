using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Data;
using System.Linq;
using Authorization;
using System.Collections.Generic;

namespace ControlStudy
{
    public partial class StudentPage : Page
    {
        readonly SessionTimer Timer = new SessionTimer(); //Dключение таймера

        public StudentPage(string loginNowUser)
        {
            InitializeComponent();

            List<Progress> progresses = new List<Progress>();

            var idPerson = ControlStudyEntities.GetContext().Users.Where(p => p.LoginUser == loginNowUser).Select(p => p.IdPerson).FirstOrDefault();

            progresses = ControlStudyEntities.GetContext().Progresses.Where(p => p.IdPerson == idPerson).ToList();

            dataGridProgress.Items.Clear();
            dataGridProgress.ItemsSource = progresses;

            Application.Current.MainWindow.Closing += new CancelEventHandler(MainWindowClosing);
            void MainWindowClosing(object sender, CancelEventArgs e) // При закрытии формы сохраняет значение таймера
            {
                Timer.SaveTimeSession(loginNowUser);
            }
        }
    }
}
