using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace DSA1
{
    /// <summary>
    /// Interaction logic for SearchJobWindow.xaml
    /// </summary>
    ///
    public partial class SearchJobWindow : Window
    {
        
        public SearchJobWindow()
        {
            InitializeComponent();
            PopulateListBox();
        }
        private List<Jobs> allJobs = new List<Jobs>();
        private void PopulateListBox()
        {
            allJobs = Job_DataBase();
            JobList.ItemsSource = allJobs; 
        }
        public static List<Jobs> Job_DataBase()
        {
            List<Jobs> jobFeed = new List<Jobs>();
            using (MySqlConnection connection = new MySqlConnection("Server=localhost;" +
"Database=project_database;UserName= root;" +
"Password=Cedric1234%%"))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM jobs_db";
                    MySqlCommand cmd = new MySqlCommand(query, connection);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Jobs item = new Jobs
                            {
                                Job_Position = reader["Job_Position"].ToString(),
                                Job_Description = reader["Job_Desctription"].ToString(),
                                IsFilled = reader["is_filled"].ToString(),
                                Job_id = Convert.ToInt32(reader["id"]),
                                Company_userNumber = Convert.ToInt32(reader["Company_userNumber"])
                            };
                            jobFeed.Add(item);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }

            return jobFeed;
        }

        Jobs TheSelectedJobs;
  
        private void SearchBox_txtchange(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

            string searchText = SearchBox.Text.ToLower();
            var filteredList = allJobs.Where(c =>
                c.Job_Position.ToLower().Contains(searchText) ||
                c.Job_Description.ToLower().Contains(searchText)
            ).ToList();

            JobList.ItemsSource = filteredList;
        }

        private void Search_btn(object sender, RoutedEventArgs e)
        {
            if (JobList.SelectedItem is Jobs selectedCompany)
            {
                MessageBox.Show("Application send");
            }
        }

        private void Search_selectionChange(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (JobList.SelectedItem is Jobs selectedJobs)
            {
                TheSelectedJobs = selectedJobs;
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
