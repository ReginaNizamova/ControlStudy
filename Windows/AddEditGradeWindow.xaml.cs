using System;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace ControlStudy
{
    public partial class AddEditGradeWindow : Window
    {
        private Progress _currentProgress = new Progress();
        private DataGrid _dataGrid;

        public AddEditGradeWindow(Progress selectedProgress, DataGrid dataGridAdmin)
        {
            InitializeComponent();

            if (selectedProgress != null)
                _currentProgress = selectedProgress;

            DataContext = _currentProgress;
            comboBoxGroup.ItemsSource = ControlStudyEntities.GetContext().Groups.ToList();
            comboBoxDiscipline.ItemsSource = ControlStudyEntities.GetContext().Disciplines.ToList();
           _dataGrid = dataGridAdmin;
        }

        private void AddEditGradeClick(object sender, RoutedEventArgs e) //Добавление/изменение пользователя
        {
           
            StringBuilder errors = new StringBuilder();

            if (comboBoxPerson.Text == "")
                errors.AppendLine("Фамилия");
            if (_currentProgress.Discipline == null)
                errors.AppendLine("Дисциплина");
            if (comboBoxGroup.Text == "")
                errors.AppendLine("Группа");
            if (string.IsNullOrWhiteSpace(Convert.ToString(_currentProgress.Grade)))
                errors.AppendLine("Оценка");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            _currentProgress.DateGrade = DateTime.Today;
            _currentProgress.IdPerson = Convert.ToInt32(idStudent.Text);
            

            if (_currentProgress.IdProgress == 0)
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

        private int FillIdGroup (string valueComboBoxGroup) 
        {
            int id = 0;

            if (valueComboBoxGroup == "1")
                id = 1;

            if (valueComboBoxGroup == "2")
                id = 2;

            if (valueComboBoxGroup == "3")
                id = 3;

            if (valueComboBoxGroup == "4")
                id = 4;

            if (valueComboBoxGroup == "5")
                id = 5;

            if (valueComboBoxGroup == "0")
                id = 6;

            return id;
        }

        private void СomboBoxGroupOnSelectionChanged(object sender, SelectionChangedEventArgs e) // Выбор значений для textName (ФИО) в зависимости от значения comboBoxGroup
        {
            string valueComboBoxGroup = comboBoxGroup.SelectedIndex.ToString();

            comboBoxPerson.Items.Clear();
            int idGroup = FillIdGroup(valueComboBoxGroup);

            var result = from Person in ControlStudyEntities.GetContext().People
                            where Person.IdGroup == idGroup
                            select new
                            {
                                Person.Family,
                                Person.Name,
                                Person.IdGroup,
                                Person.IdPerson
                            };

            result.ToList();

            foreach (var item in result)
            {
                comboBoxPerson.Items.Add(item.Family);
                idStudent.Text = Convert.ToString(item.IdPerson);
            }    
        }

        private void GradePreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e) //Разрешает ввод в textbox оценки только цифры от 2 до 5
        {
            e.Handled = "2345".IndexOf(e.Text) < 0;
        }
    }
}
