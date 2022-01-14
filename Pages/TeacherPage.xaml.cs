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
using System.Data.Entity;
using System.ComponentModel;
using Authorization;

namespace ControlStudy
{
    public partial class TeacherPage : Page
    {
        readonly SessionTimer Timer = new SessionTimer(); //Dключение таймера

        public TeacherPage(string loginNowUser)
        {
            InitializeComponent();
            AddDataInDataGridProgress();

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

        private void AddData_Click(object sender, RoutedEventArgs e) // Вызывает метод добавления данных при нажатии на кнопку "Добавить"
        {
            string name = textName.Text;
            string grade = textGrade.Text;
            string discipline = textDiscipline.Text;
            string idStudent = IdStudent.Text;

            if (name == "" || grade == "" || discipline == "" || idStudent == "")
            {
                MessageBox.Show("Заполните все поля!");
            }

            else
            {
                AddData(grade, discipline, idStudent);
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
            //    var Result = (from Progress in userContext.Progresses
            //             where Progress.CodePerson == Cs && Progress.CodeDiscipline == 1 && Progress.Grade == Gr
            //             select Progress.CodeProgress).First();

            //    userContext.Progresses.RemoveRange(userContext.Progresses.Where(x => x.CodeProgress == Result));
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

        private void DeleteData_Click(object sender, RoutedEventArgs e) // Вызывает метод удаления данных при нажатии на кнопку "Удалить"
        {
            string name = textName.Text;
            string grade = textGrade.Text;
            string discipline = textDiscipline.Text;
            string idStudent = IdStudent.Text;

            if (name == "" || grade == "" || discipline == "" || idStudent == "")
            {
                MessageBox.Show("Заполните все поля!");
            }

            else
            {
                DeleteData(grade, discipline, idStudent);
                MessageBox.Show("Оценка Удалена!");
            }

        }
      
    

        private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e) // Выбор значений для textName (ФИО) в зависимости от значения comboBoxGroup
        {
            ComboBoxItem currentItem = ((ComboBoxItem)comboBoxGroup.SelectedItem);

            string valueComboBoxGroup = currentItem.Content.ToString();

            ControlStudyEntities userContext = new ControlStudyEntities();

            //switch (valueComboBoxGroup)
            //{
            //    case "101":
            //    {
            //        textName.Items.Clear();
            //        var Result = from Person in userContext.People
            //                     where Person.CodeGroup == 1
            //                     select new
            //                     {
            //                         Person.Family,
            //                         Person.Name,
            //                         Person.CodePerson
            //                     };

            //        Result.ToList();

            //        foreach (var item in Result)
            //        {
            //            textName.Items.Add(item.Family + " " + item.Name);
            //            codeStudent.Text = Convert.ToString(item.CodePerson);
            //        }
            //        break;
            //    }

            //    case "201":
            //    {
            //        textName.Items.Clear();
            //        var Result = from Person in userContext.People
            //                     where Person.CodeGroup == 2
            //                     select new
            //                     {
            //                         Person.Family,
            //                         Person.Name,
            //                         Person.CodePerson
            //                     };

            //        Result.ToList();

            //        foreach (var item in Result)
            //        {
            //            textName.Items.Add(item.Family + " " + item.Name);
            //            codeStudent.Text = Convert.ToString(item.CodePerson);
            //        }
            //        break;
            //    }

            //    case "301":
            //    {
            //        textName.Items.Clear();
            //        var Result = from Person in userContext.People
            //                     where Person.CodeGroup == 3
            //                     select new
            //                     {
            //                         Person.Family,
            //                         Person.Name,
            //                         Person.CodePerson
            //                     };

            //        Result.ToList();

            //        foreach (var item in Result)
            //        {
            //            textName.Items.Add(item.Family + " " + item.Name);
            //            codeStudent.Text = Convert.ToString(item.CodePerson);
            //        }
            //        break;
            //    }

            //    case "401":
            //    {
            //        textName.Items.Clear();
            //        var Result = from Person in userContext.People
            //                     where Person.CodeGroup == 4
            //                     select new
            //                     {
            //                         Person.Family,
            //                         Person.Name,
            //                         Person.CodePerson
            //                     };

            //        Result.ToList();

            //        foreach (var item in Result)
            //        {
            //            textName.Items.Add(item.Family + " " + item.Name);
            //            codeStudent.Text = Convert.ToString(item.CodePerson);
            //        }
            //        break;
            //    }

            //    case "501":
            //    {
            //        textName.Items.Clear();
            //        var Result = from Person in userContext.People
            //                     where Person.CodeGroup == 5
            //                     select new
            //                     {
            //                         Person.Family,
            //                         Person.Name,
            //                         Person.CodePerson
            //                     };

            //        Result.ToList();

            //        foreach (var item in Result)
            //        {
            //            textName.Items.Add(item.Family + " " + item.Name);
            //            codeStudent.Text = Convert.ToString(item.CodePerson);
            //        }
            //        break;
            //    }

            //    default:
            //    {
            //        break;
            //    }
            //}
        }
    } 
}