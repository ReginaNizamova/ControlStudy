using System;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Authorization
{
    public partial class AddEditGradeWindow : Window
    {
        private Progress _currentProgress = new Progress();
        private Person _currentPerson = new Person();
        private DataGrid _dataGrid;

        public AddEditGradeWindow(Progress selectedProgress, DataGrid dataGridAdmin)
        {
            InitializeComponent();

            if (selectedProgress != null)
                _currentProgress = selectedProgress;
            else
                _currentProgress.Person = _currentPerson;

            DataContext = _currentProgress;
            comboBoxGroup.ItemsSource = ControlStudyEntities.GetContext().Groups.ToList();
            comboBoxDiscipline.ItemsSource = ControlStudyEntities.GetContext().Disciplines.ToList();
            _dataGrid = dataGridAdmin;
        }

        private void AddEditGradeClick(object sender, RoutedEventArgs e) //Добавление/изменение пользователя
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(_currentProgress.Person.Family) || string.IsNullOrWhiteSpace(_currentProgress.Person.Name))
                errors.AppendLine("Фамилия, имя");
            if (_currentProgress.Discipline == null)
                errors.AppendLine("Дисциплина");
            if (_currentProgress.Person.Group == null)
                errors.AppendLine("Группа");
            if (string.IsNullOrWhiteSpace(Convert.ToString(_currentProgress.Grade)))
                errors.AppendLine("Оценка");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (_currentProgress.IdProgress== 0)
                ControlStudyEntities.GetContext().Progresses.Add(_currentProgress);

            try
            {
                ControlStudyEntities.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        } 

        private void WindowClosed(object sender, EventArgs e)//Обновляет DataGrid 
        {
            ControlStudyEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
            _dataGrid.ItemsSource = ControlStudyEntities.GetContext().Progresses.ToList();
        }
    }
}
