using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.ComponentModel.Design;

namespace DSA1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly LoginValidator _loginValidator;

        public MainWindow()
        {
            InitializeComponent();
            AddIntoMemory();
            _loginValidator = new LoginValidator();
        }
        public static User users;

        MySqlConnection connection = new MySqlConnection("Server=localhost;" +
"Database=project_database;UserName= root;" +
"Password=Cedric1234%%");

        public static Dictionary<string, string> applicantMapList = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        public static Dictionary<string, int> applicantMapList1 = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
        public static Dictionary<int, User> applicantUserMapList2 = new Dictionary<int, User>();
        public static Dictionary<string, User> applicantUserByString = new Dictionary<string, User>(StringComparer.OrdinalIgnoreCase);

        public static Dictionary<string, string> companyMapList = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        public static Dictionary<string, int> companyMapList1 = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
        public static Dictionary<int, Company> companyUserMapList2 = new Dictionary<int, Company>();
        public static Dictionary<string, Company> companyUserByString = new Dictionary<string, Company>(StringComparer.OrdinalIgnoreCase);

        public static int userId;
        public static int companyID;

        public void ClearMemory()
        {
            applicantMapList.Clear();
            applicantMapList1.Clear();
            applicantUserMapList2.Clear();
            companyMapList.Clear();
            companyMapList1.Clear();
            companyUserMapList2.Clear();
        }

        private void AddIntoMemory()
        {
            if (applicantMapList.Count > 0 && companyMapList.Count > 0) return;

            try
            {
                connection.Open();
                // Load Applicant Accounts
                string applicantQuery = "SELECT * FROM Applicant_accounts";

                using (MySqlCommand command = new MySqlCommand(applicantQuery, connection))
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string firstName = reader.IsDBNull(reader.GetOrdinal("first_name")) ? "" : reader.GetString("first_name").Trim();
                        string lastName = reader.IsDBNull(reader.GetOrdinal("last_name")) ? "" : reader.GetString("last_name");
                        int userID = reader.IsDBNull(reader.GetOrdinal("id")) ? 0 : reader.GetInt32(reader.GetOrdinal("id"));
                        string jobTitle = reader.IsDBNull(reader.GetOrdinal("Job_title")) ? "" : reader.GetString("Job_title");
                        string phoneNumber = reader.IsDBNull(reader.GetOrdinal("Phone_Number")) ? "" : reader.GetString("Phone_Number");
                        string gender = reader.IsDBNull(reader.GetOrdinal("gender")) ? "" : reader.GetString("gender");
                        string address = reader.IsDBNull(reader.GetOrdinal("address")) ? "" : reader.GetString("address");
                        string email = reader.IsDBNull(reader.GetOrdinal("email")) ? "" : reader.GetString("email");
                        string password = reader.IsDBNull(reader.GetOrdinal("password")) ? "" : reader.GetString("password");
                        if (!applicantMapList.ContainsKey(email))
                        {
                            string name = firstName + " " + lastName;
                            applicantMapList[email] = password;
                            applicantMapList1[email] = userID;
                            applicantUserMapList2[userID] = new User(userID, firstName, lastName, email, jobTitle, phoneNumber, address, gender, password);
                            applicantUserByString[name] = new User(userID, firstName, lastName, email, jobTitle, phoneNumber, address, gender, password);
                        }
                        else
                        {
                            Console.WriteLine($"Warning: Duplicate key '{email}' found. Skipping entry.");
                        }
                    }
                }

                // Load Company Accounts
                string companyQuery = "SELECT * FROM Company_accounts";

                using (MySqlCommand command = new MySqlCommand(companyQuery, connection))
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string companyName = reader.IsDBNull(reader.GetOrdinal("company_name")) ? "" : reader.GetString("company_name").Trim();
                        int companyID = reader.IsDBNull(reader.GetOrdinal("company_id")) ? 0 : reader.GetInt32(reader.GetOrdinal("company_id"));
                        string email = reader.IsDBNull(reader.GetOrdinal("email")) ? "" : reader.GetString("email");
                        string password = reader.IsDBNull(reader.GetOrdinal("password")) ? "" : reader.GetString("password");
                        string address = reader.IsDBNull(reader.GetOrdinal("company_address")) ? "" : reader.GetString("company_address");
                        if (!companyMapList.ContainsKey(email))
                        {
                            companyMapList[email] = password;
                            companyMapList1[email] = companyID;
                            companyUserMapList2[companyID] = new Company(companyName, address, email, password, companyID);
                            companyUserByString[companyName] = new Company(companyName, address, email, password, companyID);
                        }
                        else
                        {
                            Console.WriteLine($"Warning: Duplicate key '{email}' found. Skipping entry.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        public bool LoggedIn(string username, string password, out int userId, out bool isCompany)
        {
            // Check if the username and password match an applicant account
            if (applicantMapList.TryGetValue(username, out string storedApplicantPassword))
            {
                if (storedApplicantPassword == password)
                {
                    userId = applicantMapList1[username]; // Retrieve the userId based on the email
                    isCompany = false;
                    return true;
                }
            }

            // Check if the username and password match a company account
            if (companyMapList.TryGetValue(username, out string storedCompanyPassword))
            {
                if (storedCompanyPassword == password)
                {
                    userId = companyMapList1[username]; // Retrieve the companyId based on the email

                    isCompany = true;
                    return true;
                }
            }

            userId = 0; // Set to 0 if the login fails
            isCompany = false;
            return false;
        }

        private void Log_In_Button_Click(object sender, RoutedEventArgs e)
        {
            // Get sanitized inputs
            string sanitizedUsername = UsernameTextBox.Text.Trim();
            string sanitizedPassword = PasswordTxtBox.Password;

            // Check if the user can log in
            if (LoggedIn(sanitizedUsername, sanitizedPassword, out userId, out bool isCompany))
            {
                MessageBox.Show("User ID: " + userId);
                if (isCompany)
                {
                    MessageBox.Show("Hi World");
                    Company_DashBoard companyWindow = new Company_DashBoard();
                    this.Hide();
                    companyWindow.Show();
                }
                else
                {
                    MessageBox.Show("Hello World");
                    DB mainWindow = new DB();
                    this.Hide();
                    mainWindow.Show();
                }
            }
            else
            {
                MessageBox.Show("Wrong username or password.");
            }
        }

        private void Register_Button_Click(object sender, RoutedEventArgs e)
        {
            Register res = new Register();
            this.Hide();
            res.Show();
        }
    }
}