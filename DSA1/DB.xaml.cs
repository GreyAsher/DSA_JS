using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Data;
using System.Windows.Input;
using modernUIDesign.MVVM.ViewModel;

namespace DSA1
{
    public partial class DB : Window
    {
        public List<User> list = new List<User>();
        public List<string> names = new List<string>();
        public List<NewsFeed> nFeeds = new List<NewsFeed>();
        private DataTable searchData;
        public static int otherUserKey;
        private int WindowTracker;
        public DB()
        {
            DataContext = new MainViewModel();
            InitializeComponent();
            nFeeds = GetNewsFeedFromDatabase();
            names = getNames();
            LoadComboBoxData();
            LoadNewsfeed();
            
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void Notification_Button(object sender, RoutedEventArgs e)
        {
            InBox ib = new InBox();
            this.Hide();
            ib.Show();
        }

        private void Profile_Button(object sender, RoutedEventArgs e)
        {
            
            UserProfile up = new UserProfile();
            UserProfile.windowNumber = 1;
            this.Hide();
            up.Show();
        }

        private void RadioButton_Checked_1(object sender, RoutedEventArgs e)
        {

        }

        private void Log_Out_Button(object sender, RoutedEventArgs e)
        {
            MainWindow loginWindow = new MainWindow();
            loginWindow.ClearMemory();
            this.Hide();
            loginWindow.Show();
        }

        private void MyTextChange(object sender, TextChangedEventArgs e)
        {
    
        }
        private List<string> getNames()
        {
            List<string> names = new List<string>();
            foreach (var i in list) 
            {
                string fullName = i.First_Name + " " + i.Last_Name;
                names.Add(fullName);    
            }
            return names;
        }
        private List<NewsFeed> GetNewsFeedFromDatabase()
        {
            List<NewsFeed> newsFeed = new List<NewsFeed>();
            using (MySqlConnection connection = new MySqlConnection("Server=localhost;" +
    "Database=project_database;UserName= root;" +
    "Password=Cedric1234%%"))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM posts";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            NewsFeed item = new NewsFeed
                            {
                                Author = reader["company_name"].ToString(),
                                Content = reader["Content"].ToString(),
                                Photos = "Enter Photo here",//reader["Photo"].ToString()
                                Id = Convert.ToInt32(reader["post_id"]),
                                UserID = Convert.ToInt32(reader["user_id"])
                            };
                            newsFeed.Add(item);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
            return newsFeed;
        }

        private void Search_ComboBox_TextChanged(object sender, TextChangedEventArgs e)
        {
         
        }
        private void Search_ComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            if (sender is ComboBox comboBox)
            {
                if (comboBox.Template.FindName("PART_EditableTextBox", comboBox) is TextBox textBox)
                {
                    textBox.TextChanged += Search_ComboBox_TextChanged;
                }
            }
        }

        private ListBoxItem CreateNewsfeedItem(string userName, string content)
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

        private void LoadNewsfeed()
        {
            var newsFeedItems = GetNewsFeedFromDatabase();
            foreach (var newsFeed in newsFeedItems)
            {
                //Newsfeed_ListBox.Items.Add(CreateNewsfeedItem(newsFeed.Author, newsFeed.Content));
            }
        }
    
    private void LoadComboBoxData()
        {
            MySqlConnection connection = new MySqlConnection("Server=localhost;" +
    "Database=project_database;UserName= root;" +
    "Password=Cedric1234%%");
            string query = "SELECT first_name,last_name FROM applicant_accounts";

            try
            {
                using (connection)
                {
                    connection.Open();
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    searchData = new DataTable();
                    adapter.Fill(searchData);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    
    private void Add_Post(object sender, RoutedEventArgs e)
        {
            Post_Resume pr = new Post_Resume();
            pr.Show(); 
        }

        private void Pending_Button(object sender, RoutedEventArgs e)
        {
            ApplicantUI ap = new ApplicantUI();
            this.Hide();
            ap.Show();
        }

        private void Searched_Person(object sender, MouseEventArgs e)
        {

            if (sender is ListBox listBox && listBox.SelectedItem is string selectedName)
            {
                Dictionary<string, User> mn = MainWindow.applicantUserByString;
                User accountName = mn[selectedName.ToString()];
                string fullName = accountName.First_Name + " " + accountName.Last_Name;

                UserProfile us = new UserProfile();
                this.Hide();
                us.changeInfo(fullName, accountName.JobTitle, accountName.Address);
                us.Show();
                otherUserKey = accountName.Id;

            }
            else if (e.OriginalSource is TextBlock textBlock)
            {
                string clickedUserName = textBlock.Text;
                MessageBox.Show("Hello World", "User Selected", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("User not recognized.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void searchJobs_btn(object sender, RoutedEventArgs e)
        {
            SearchJobWindow sjw = new SearchJobWindow();
          
            this.Hide();
            sjw.Show();
        }

        private void company_search(object sender, RoutedEventArgs e)
        {
            Company_Search cs = new Company_Search();
            this.Hide();
            cs.Show();
        }

        private void applicantsearch_btn(object sender, RoutedEventArgs e)
        {
            Applicant_search applicant_Search = new Applicant_search();
            UserProfile.windowNumber = 2;
            this.Hide();
            applicant_Search.Show();     
        }
    }
}

