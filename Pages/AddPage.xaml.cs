using Authorization;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace ControlStudy
{
    public partial class RegistrationPage : Page
    {
        private readonly Person _currentPerson = new Person();

        //public RegistrationPage()
        //{
        //    InitializeComponent();
        //    DataContext = _currentPerson;
        //    ComboBoxGroup.ItemsSource = ControlStudyEntities.GetContext().Groups.ToList();
        //    ComboBoxRole.ItemsSource = ControlStudyEntities.GetContext().Roles.ToList();
        //}


        //private void Input_Click(object sender, RoutedEventArgs e) // Переход на страницу авторизации
        //{
        //    Manager.MainFrame.Navigate(new AuthorizationPage()); 
        //} 

        //private void Clean () //Очищение полей регистрации
        //{
        //    LoginText.Text = "";
        //    FamilyText.Text = "";
        //    NameText.Text = "";
        //    PatronimicText.Text = "";
        //    ComboBoxGroup.Text = "";
        //    PasswordPersonText.Password = "";
        //    ComboBoxRole.Text = "";
        //}

       

        //private void PasswordPersonText_PasswordChanged(object sender, RoutedEventArgs e) // Открывает и скрывает watermark поля Password
        //{
        //    if (PasswordPersonText.Password.Length == 0)
        //    {
        //        passwordText.Visibility = Visibility.Visible;
        //    }
        //    else
        //    {
        //        passwordText.Visibility = Visibility.Hidden;
        //    }
        //}
    }
}