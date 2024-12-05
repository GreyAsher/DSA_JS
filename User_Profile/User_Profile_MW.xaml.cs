using Project2025.MyClass;
using System;
using System.Windows;
using System.Windows.Controls;


namespace User_Profile
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            showInfo();
        }
        private void showInfo()
        {
            FullNameTextBox.Text = Project2025.MainWindow.user.Name + " " + 
                Project2025.MainWindow.user.Lastname;

        }

        private void FullNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
        }

        private void FullNameTextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }
    }
}
