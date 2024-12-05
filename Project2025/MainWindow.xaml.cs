using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Windows;


namespace Project2025
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            AddIntoMemory();
        }

       // public Applicants user;
        MySqlConnection connection = new MySqlConnection("Server=localhost;" +
   "Database=sakila;UserName= root;" +
   "Password=Cedric1234%%");

        Dictionary<string, string> mapList = new Dictionary<string, string>();
        public static string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(bytes);
            }
        }

        private void AddIntoMemory()
        {
            if (mapList.Count > 0) return;

            try
            {
                connection.Open();
                string query = "SELECT first_name, last_name FROM actor ";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string userName = reader.IsDBNull(reader.GetOrdinal("first_name")) ? "" : reader.GetString("first_name").Trim();
                        string passWord = reader.IsDBNull(reader.GetOrdinal("last_name")) ? "" : reader.GetString("last_name");
                        mapList.Add(userName, passWord);

                    }
                }
            }

            catch (Exception ex) { MessageBox.Show("Error: " + ex.Message); }
            finally { connection.Close(); }
        }
        private bool LoggedIn(string username, string password)
        {

            string sanitizedUsername = username;
            string sanitizedPassword = mapList[username];
            //user.set_Applicant(1, sanitizedUsername, sanitizedPassword);

            // Hash the entered password
            string hashedPassword = HashPassword(sanitizedPassword);

            // Check if username exists and passwords match
            if (mapList.TryGetValue(sanitizedUsername, out string storedHash))
            {
                return storedHash == sanitizedPassword;
            }

            return false;
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            // Get sanitized inputs
            string sanitizedUsername = UsernameTextBox.Text.Trim();
            string sanitizedPassword = PasswordTxtBox.Password;

            // Check if the user can log in
            if (LoggedIn(sanitizedUsername, sanitizedPassword))
            {
                //MessageBox.Show("Logged In");
                Main_DashBoard.MainWindow mainWindow = new Main_DashBoard.MainWindow();
                this.Hide();
                mainWindow.Show();
            }
            else
            {
                MessageBox.Show("Wrong username or password.");
            }
        }

        private void UsernameTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }
    }
}
