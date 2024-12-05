using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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

namespace DSA_Project
{
    /// <summary>
    /// Interaction logic for Log_In.xaml
    /// </summary>
    public partial class Log_In : Window
    {
        public Log_In()
        {
            InitializeComponent();
        }
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
                DashBoard mainWindow = new DashBoard();
                this.Hide();
                mainWindow.Show();
            }
            else
            {
                MessageBox.Show("Wrong username or password.");
            }
        }
    }
}
