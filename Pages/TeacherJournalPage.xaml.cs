using ControlStudy.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace ControlStudy
{
    public partial class TeacherJournalPage : Page
    {
        readonly ControlStudyEntities dataContext = new ControlStudyEntities();
        Dates dates;

        public TeacherJournalPage()
        {
            InitializeComponent();

            groupCB.ItemsSource = dataContext.Groups.ToList();
            disciplineCB.ItemsSource = dataContext.Disciplines.ToList();

            dates = new Dates(); 
            dates.EndDate = DateTime.Now;
            dates.StartDate = dates.EndDate.AddDays(-7);
            dates.CountDate = 7;
            SetDates();
            studentsLV.Items.Clear();

            gradesLV.DataContext = dates;
            List<Progress> a = new List<Progress>();

            for (int i = 0; i < dates.CountDate; i++)
            {
                a += dataContext.Progresses.Where(p => p.DateGrade.ToString() == gridView.Columns[i].Header.ToString());
                
            }
            gradesLV.ItemsSource = a;
            datePickerEnd.DataContext = dates;
        }

        private void SetDates ()
        {
            for ( int i = 0; i <= dates.CountDate; i++)
            {
                GridViewColumn column = new GridViewColumn();
                column.Header = dates.StartDate.AddDays(i);
                column.HeaderStringFormat = "dd.MM.yy";
                column.Width = 70;               
                gridView.Columns.Add(column);
            }
        }

        private void CleanListViewGrades ()
        {
            for (int i = dates.CountDate; i >= 0 ; i--)
            {              
                gridView.Columns.RemoveAt(i);     
            }
        }
      
        private void PrintClick(object sender, RoutedEventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();
            if((bool)printDialog.ShowDialog().GetValueOrDefault())
            {   
                //grid.Visibility = Visibility.Collapsed;
                //int pageMargin = 5;

                //Size pageSize = new Size(printDialog.PrintableAreaWidth - pageMargin * 2, printDialog.PrintableAreaHeight - 20);
                //dataGridProgress.Measure(pageSize);
                //dataGridProgress.Arrange(new Rect(pageMargin, pageMargin, pageSize.Width, pageSize.Height));

                //printDialog.PageRangeSelection = PageRangeSelection.AllPages;
                //printDialog.UserPageRangeEnabled = true;

                //dataGridProgress.Columns.Remove(dataGridProgress.ColumnFromDisplayIndex(6));
                //printDialog.PrintVisual(dataGridProgress, "Печать оценок");

                //grid.Visibility = Visibility.Visible;

                //dataGridProgress.ItemsSource = ControlStudyEntities.GetContext().Progresses.ToList();
            }
        }

        private void GroupCBSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Group g = (Group)groupCB.SelectedItem;
            var a = dataContext.People.Where(p => p.Group.Group1 == g.Group1).OrderBy(p => p.Family).ToList();
            studentsLV.ItemsSource = a;
            

            //DataTable dataTable = new DataTable();

            //dataTable.Rows.Clear();
            //dataTable.Columns.Clear();

            //int i = 0;

            //while (i <= 5)
            //{
            //    i++;
            //    dataTable.Columns.Add(new DataColumn(i.ToString())
            //    {
            //        DataType = typeof(int)
            //    });

            //}

            //for (int j = 0; j < a.Count; j++)
            //{
            //    dataTable.Rows.Add(dataTable.NewRow());
            //    dataTable.Rows[j][i] = "";
            //}


            //GridView gridView = new GridView();


            //foreach (DataColumn item in dataTable.Columns)
            //{
            //    GridViewColumn gridViewColumn = new GridViewColumn
            //    {
            //        Header = item.ColumnName,
            //        DisplayMemberBinding = new Binding(item.ColumnName),

            //    };

            //    gridView.Columns.Add(gridViewColumn);
            //}

            //gradesLV.View = gridView;
            //gradesLV.Items.Refresh();

            //gradesLV.DataContext = dataTable;
        }

        private void DatePickerEndSelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (datePickerEnd.SelectedDate != null && datePickerStart.SelectedDate != null)
            {
                CleanListViewGrades();
                dates = new Dates();
                dates.EndDate = (DateTime)datePickerEnd.SelectedDate;
                dates.StartDate = (DateTime)datePickerStart.SelectedDate;
                dates.CountDate = (dates.EndDate.Date - dates.StartDate.Date).Days;
                SetDates();
            }

            else
            {
                MessageBox.Show("Выберите обе даты!");
            }
        }

        private void GradesLVMouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            MessageBox.Show("!!!!!");
        }
    }
}