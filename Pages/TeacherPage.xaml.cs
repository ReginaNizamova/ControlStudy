using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.ComponentModel;

namespace ControlStudy
{
    public partial class TeacherPage : Page
    {
        readonly SessionTimer Timer = new SessionTimer(); //Включение таймера

        public TeacherPage(string loginNowUser)
        {
            InitializeComponent();

            dataGridProgress.Items.Clear();
            dataGridProgress.ItemsSource = ControlStudyEntities.GetContext().Progresses.ToList();

            Application.Current.MainWindow.Closing += new CancelEventHandler(MainWindowClosing);
            void MainWindowClosing(object sender, CancelEventArgs e) // При закрытии формы сохраняет значение таймера
            {
                Timer.SaveTimeSession(loginNowUser);
            }
        }

        private void AddClick(object sender, RoutedEventArgs e)
        {
            AddEditGradeWindow window = new AddEditGradeWindow(null, dataGridProgress);
            window.ShowDialog();
        }

        private void EditClick(object sender, RoutedEventArgs e)
        {
            AddEditGradeWindow window = new AddEditGradeWindow((sender as Button).DataContext as Progress, dataGridProgress);
            window.ShowDialog();
        }

        private void DeleteClick(object sender, RoutedEventArgs e)
        {
            var progressDelete = dataGridProgress.SelectedItems.Cast<Progress>().ToList();

            if (MessageBox.Show($"Удалить следующие {progressDelete.Count()} элементы?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    ControlStudyEntities.GetContext().Progresses.RemoveRange(progressDelete);
                    ControlStudyEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены!");

                    dataGridProgress.ItemsSource = ControlStudyEntities.GetContext().Progresses.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void PrintClick(object sender, RoutedEventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();
            if((bool)printDialog.ShowDialog().GetValueOrDefault())
            {   
                grid.Visibility = Visibility.Collapsed;
                int pageMargin = 5;

                Size pageSize = new Size(printDialog.PrintableAreaWidth - pageMargin * 2, printDialog.PrintableAreaHeight - 20);
                dataGridProgress.Measure(pageSize);
                dataGridProgress.Arrange(new Rect(pageMargin, pageMargin, pageSize.Width, pageSize.Height));

                printDialog.PageRangeSelection = PageRangeSelection.AllPages;
                printDialog.UserPageRangeEnabled = true;

                dataGridProgress.Columns.Remove(dataGridProgress.ColumnFromDisplayIndex(6));
                printDialog.PrintVisual(dataGridProgress, "Печать оценок");

                grid.Visibility = Visibility.Visible;

                dataGridProgress.ItemsSource = ControlStudyEntities.GetContext().Progresses.ToList();
            }
        }
    } 
}