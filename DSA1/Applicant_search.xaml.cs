using MySql.Data.MySqlClient;
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
using System.Windows.Shapes;

namespace DSA1
{
    /// <summary>
    /// Interaction logic for Applicant_search.xaml
    /// </summary>
    public partial class Applicant_search : Window
    {
        private List<User> allUsers = new List<User>();
        public static int WindowTracker;
        public Applicant_search()
        {
            InitializeComponent();
            PopulateListBox();
        }

        private void PopulateListBox()
        {
            allUsers = User_DataBase(); 
            JobList.ItemsSource = allUsers; 
        }

        public static List<User> User_DataBase()
        {
            List<User> applicantFeed = new List<User>();
            using (MySqlConnection connection = new MySqlConnection("Server=127.0.0.1;" +
                "Database=project_database;User ID=root;Password=SQLDatabase404"))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM applicant_accounts";
                    MySqlCommand cmd = new MySqlCommand(query, connection);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            User item = new User
                            {
                                First_Name = reader["first_name"].ToString(),
                                Last_Name = reader["last_name"].ToString(),
                                Email = reader["email"].ToString(),
                                JobTitle = reader["Job_Title"].ToString(),
                                Password = reader["password"].ToString(),
                                PhoneNumber = reader["Phone_Number"].ToString(),
                                Gender = reader["gender"].ToString(),
                                Address = reader["address"].ToString(),
                                Id = Convert.ToInt32(reader["id"])
                            };
                            applicantFeed.Add(item);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }

            return applicantFeed;
        }
        User user;
        private void Company_Profile(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (JobList.SelectedItem is User selectedUser)
            {
                user = selectedUser;
                UserProfile userSelected = new UserProfile(selectedUser);
                userSelected.Show();
            }
        }

        private void SearchBox_txtchange(object sender, RoutedEventArgs e)
        {
            string searchText = SearchBox.Text.ToLower();
            var filteredList = allUsers.Where(c =>
                c.First_Name.ToLower().Contains(searchText) ||
                c.Last_Name.ToLower().Contains(searchText)
            ).ToList();

            JobList.ItemsSource = filteredList; 
        }
        private void viewProfile_btn(object sender, RoutedEventArgs e)
        {
            if (JobList.SelectedItem is User selectedUser)
            {
                UserProfile.windowNumber = 2;
                UserProfile userSelected = new UserProfile(selectedUser);
                userSelected.Show();
            }
        }

        private void Back_Button(object sender, RoutedEventArgs e)
        {
            DB db = new DB();
            this.Hide();
            db.Show();
        }
    }
}

