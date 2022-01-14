using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.ComponentModel;
using Authorization;

namespace ControlStudy
{
    public partial class TeacherPage : Page
    {
        readonly SessionTimer Timer = new SessionTimer(); //Включение таймера

        public TeacherPage(string loginNowUser)
        {
            InitializeComponent();
            AddDataInDataGridProgress();
            textDiscipline.ItemsSource = ControlStudyEntities.GetContext().Disciplines.ToList();
            comboBoxGroup.ItemsSource = ControlStudyEntities.GetContext().Groups.ToList();

            Application.Current.MainWindow.Closing += new CancelEventHandler(MainWindowClosing);

            void MainWindowClosing(object sender, CancelEventArgs e) // При закрытии формы сохраняет значение таймера
            {
                Timer.SaveTimeSession(loginNowUser);
            }
        }

        private static int InstallIdDiscipline(string discipline)
        {
            int idDiscipline = 0;

            if (discipline == "Математика")
                idDiscipline = 1;

            else if (discipline == "Английский язык")
                idDiscipline = 2;

            else if (discipline == "Французский язык")
                idDiscipline = 3;

            else if (discipline == "Экономика")
                idDiscipline = 4;

            else if (discipline == "Философия")
                idDiscipline = 5;

            return idDiscipline;
        }

        private void AddData(string grade, string discipline, string codeSt) // Добавление данных
        {
            ControlStudyEntities userContext = new ControlStudyEntities();

            Progress progress = new Progress
            {
                IdDiscipline = InstallIdDiscipline(discipline),
                IdPerson = Convert.ToInt32(codeSt),
                Grade = Convert.ToInt32(grade),
                DateGrade = DateTime.Today
            };

            userContext.Progresses.Add(progress);

            userContext.SaveChanges();
            userContext.Dispose();

            AddDataInDataGridProgress();
        }

        private void AddDataInDataGridProgress() //Добавление данных в DataGrid
        {
            ControlStudyEntities userContext = new ControlStudyEntities();

            var result = from Progress in userContext.Progresses
                         join Person in userContext.People on Progress.IdPerson equals Person.IdPerson
                         join Discipline in userContext.Disciplines on Progress.IdDiscipline equals Discipline.IdDiscipline
                         select new
                         {
                             Дисциплина = Discipline.Discipline1,
                             Фамилия = Person.Family,
                             Имя = Person.Name,
                             Оценка = Progress.Grade,
                             Дата = Progress.DateGrade.ToString()
                         };

            dataGridProgress.ItemsSource = result.ToList();
        }

        private void AddDataClick(object sender, RoutedEventArgs e) // Вызывает метод добавления данных при нажатии на кнопку "Добавить"
        {
            string name = textName.Text;
            string grade = textGrade.Text;
            string discipline = textDiscipline.Text;
            string studentId = idStudent.Text;

            if (name == "" || grade == "" || discipline == "" || studentId == "")
            {
                MessageBox.Show("Заполните все поля!");
            }

            else
            {
                AddData(grade, discipline, studentId);
                MessageBox.Show("Оценка добавлена!");
            }

        }

        private void DeleteData (string grade, string discipline, string codeSt) // Удаление данных 
        {

            ControlStudyEntities userContext = new ControlStudyEntities();

            int Cs = int.Parse(codeSt);
            int Gr = int.Parse(grade);

            //if (discipline == "Математика")
            //{ 
            //var Result = (from Progress in userContext.Progresses
            //              where Progress.CodePerson == Cs && Progress.CodeDiscipline == 1 && Progress.Grade == Gr
            //              select Progress.CodeProgress).First();

            //userContext.Progresses.RemoveRange(userContext.Progresses.Where(x => x.CodeProgress == Result));
            //}

            //else if (discipline == "Английский язык")
            //{
            //    var Result = (from Progress in userContext.Progresses
            //                  where Progress.CodePerson == Cs && Progress.CodeDiscipline == 2 && Progress.Grade == Gr
            //                  select Progress.CodeProgress).First();

            //    userContext.Progresses.RemoveRange(userContext.Progresses.Where(x => x.CodeProgress == Result));
            //}

            //else if (discipline == "Французский язык")
            //{
            //    var Result = (from Progress in userContext.Progresses
            //                  where Progress.CodePerson == Cs && Progress.CodeDiscipline == 3 && Progress.Grade == Gr
            //                  select Progress.CodeProgress).First();

            //    userContext.Progresses.RemoveRange(userContext.Progresses.Where(x => x.CodeProgress == Result));
            //}

            //else if (discipline == "Экономика")
            //{
            //    var Result = (from Progress in userContext.Progresses
            //                  where Progress.CodePerson == Cs && Progress.CodeDiscipline == 4 && Progress.Grade == Gr
            //                  select Progress.CodeProgress).First();

            //    userContext.Progresses.RemoveRange(userContext.Progresses.Where(x => x.CodeProgress == Result));
            //}

            //else if (discipline == "Философия")
            //{
            //    var Result = (from Progress in userContext.Progresses
            //                  where Progress.CodePerson == Cs && Progress.CodeDiscipline == 5 && Progress.Grade == Gr
            //                  select Progress.CodeProgress).First();

            //    userContext.Progresses.RemoveRange(userContext.Progresses.Where(x => x.CodeProgress == Result));
            //}

            //userContext.SaveChanges();
            //userContext.Dispose();
            //AddDataInDataGridProgress();
        }

        private void DeleteDataClick(object sender, RoutedEventArgs e) // Вызывает метод удаления данных при нажатии на кнопку "Удалить"
        {
            string name = textName.Text;
            string grade = textGrade.Text;
            string discipline = textDiscipline.Text;
            string studentId = idStudent.Text;

            if (name == "" || grade == "" || discipline == "" || studentId == "")
            {
                MessageBox.Show("Заполните все поля!");
            }

            else
            {
                DeleteData(grade, discipline, studentId);
                MessageBox.Show("Оценка Удалена!");
            }

        }

        private static int FillIdGroup(string group)
        {
            int id = 0;

            if (group == "115")
                id = 1;

            else if (group == "215")
                id = 2;

            else if (group == "315")
                id = 3;

            else if (group == "415")
                id = 4;

            else if (group == "515")
                id = 5;

            return id;
        }

        private void SelectorOnSelectionChanged(object sender, SelectionChangedEventArgs e) // Выбор значений для textName (ФИО) в зависимости от значения comboBoxGroup
        {
            ComboBoxItem currentItem = ((ComboBoxItem)comboBoxGroup.SelectedItem);
            string valueComboBoxGroup = currentItem.Content.ToString();
            ControlStudyEntities userContext = new ControlStudyEntities();

            textName.Items.Clear();
            var result = from Person in userContext.People
                            where Person.IdGroup == FillIdGroup(valueComboBoxGroup)
                            select new
                            {
                                Person.Family,
                                Person.Name,
                                Person.IdPerson
                            };

            result.ToList();

            foreach (var item in result)
            {
                textName.Items.Add(item.Family + " " + item.Name);
                idStudent.Text = Convert.ToString(item.IdPerson);
            }            
        }
    } 
}