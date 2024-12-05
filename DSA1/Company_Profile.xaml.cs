using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace DSA1
{
    public partial class Company_Profile : Window
    {
        public Company_Profile(Company company)
        {
            InitializeComponent();
            LoadJobfeedAsync();
            Company_Name.Text = company.CompanyName;
            Company_Email.Text = company.CompanyEmail;
            Company_Address.Text = company.CompanyAddress;
        }

        public Company_Profile()
        {
            InitializeComponent();
            LoadJobfeedAsync();
            if (MainWindow.userId > 0 && MainWindow.companyUserMapList2 != null)
            {
                ShowInfo();
            }
            else
            {
                MessageBox.Show("User ID or company information is not properly initialized.");
            }
        }

        private void ShowInfo()
        {
            int key = MainWindow.userId;
            if (MainWindow.companyUserMapList2.TryGetValue(key, out Company loggedUser))
            {

                Company_Name.Text = loggedUser.CompanyName;
                Company_Email.Text = loggedUser.CompanyEmail;
                Company_Address.Text = loggedUser.CompanyAddress;
            }
        }

        private ListBoxItem CreateJobPost(string userName, string content)
        {
            var listBoxItem = new ListBoxItem();
            var border = new Border
            {
                Background = new SolidColorBrush(Color.FromRgb(213, 160, 33)),
                CornerRadius = new CornerRadius(10),
                Padding = new Thickness(10),
                Height = 100,
                Margin = new Thickness(5)
            };

            var stackPanel = new StackPanel
            {
                Orientation = Orientation.Vertical
            };

            var userNameTextBlock = new TextBlock
            {
                Text = userName,
                FontWeight = FontWeights.Bold,
                Margin = new Thickness(5, 0, 5, 5)
            };

            var contentTextBlock = new TextBlock
            {
                Text = content,
                TextWrapping = TextWrapping.Wrap,
                Margin = new Thickness(5, 0, 5, 0)
            };
            stackPanel.Children.Add(userNameTextBlock);
            stackPanel.Children.Add(contentTextBlock);
            border.Child = stackPanel;
            listBoxItem.Content = border;

            return listBoxItem;
        }

        private async void LoadJobfeedAsync()
        {
            try
            {
                var jobItems = await Task.Run(() => Company_DashBoard.Job_DataBase());
                foreach (var i in jobItems)
                {
                    if (i.Company_userNumber == MainWindow.userId)
                    {
                        Newsfeed_ListBox.Items.Add(CreateJobPost(i.Job_Position, i.Job_Description));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading job feed: {ex.Message}");
            }
        }

        private void BackButton(object sender, RoutedEventArgs e)
        {
            Company_Search cs = new Company_Search();
            this.Hide();
            cs.Show();
        }
    }
}
