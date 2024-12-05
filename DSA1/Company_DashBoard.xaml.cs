using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace DSA1
{
    /// <summary>
    /// Interaction logic for Company_DashBoard.xaml
    /// </summary>
    public partial class Company_DashBoard : Window
    {
        public Company_DashBoard()
        {
            InitializeComponent();
         
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

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Click_Profile(object sender, RoutedEventArgs e)
        {
            Company_Profile cp = new Company_Profile();
            this.Hide();
            cp.Show();
        }
    }
}
