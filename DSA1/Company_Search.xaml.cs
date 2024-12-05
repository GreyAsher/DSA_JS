using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace DSA1
{
    /// <summary>
    /// Interaction logic for Company_Search.xaml
    /// </summary>
    public partial class Company_Search : Window
    {
        private List<Company> allCompanies = new List<Company>();
        public Company_Search()
        {
            InitializeComponent();
            PopulateListBox(); 
        }

        private void PopulateListBox()
        {
            try
            {
                allCompanies = User_DataBase();
                CompanyListBox.ItemsSource = allCompanies; 
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error populating the list: " + ex.Message);
            }
        }

        public static List<Company> User_DataBase()
        {
            List<Company> companyList = new List<Company>();
            string connectionString = "Server=localhost;" +
"Database=project_database;UserName= root;" +
"Password=Cedric1234%%";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM company_accounts"; 
                    MySqlCommand cmd = new MySqlCommand(query, connection);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Company item = new Company
                            {
                                CompanyName = reader["Company_name"].ToString(),
                                CompanyEmail = reader["email"].ToString(),
                                CompanyPassword = reader["password"].ToString(),
                                CompanyAddress = reader["Company_address"].ToString(), 
                                CompanyId = Convert.ToInt32(reader["company_id"])
                            };
                            companyList.Add(item);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Database error: " + ex.Message);
                }
            }

            return companyList;
        }

        private void SearchBox_txtchange(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

            string searchText = SearchBox.Text.ToLower();
            var filteredList = allCompanies.Where(c =>
                c.CompanyName.ToLower().Contains(searchText) ||
                c.CompanyEmail.ToLower().Contains(searchText) ||
                c.CompanyAddress.ToLower().Contains(searchText)
            ).ToList();

            CompanyListBox.ItemsSource = filteredList;
        }

        Company TheSelectedCompany;
        private void Company_Profile(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (CompanyListBox.SelectedItem is Company selectedCompany)
            {
                TheSelectedCompany = selectedCompany;
            }
        }

        private void viewProfile_btn(object sender, RoutedEventArgs e)
        {
            if (CompanyListBox.SelectedItem is Company TheSelectedCompany)
            {
                Company_Profile cp = new Company_Profile(TheSelectedCompany);
                this.Hide();
                cp.Show();
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
