using DSA1;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MySqlConnection connection = new MySqlConnection("Server=localhost;" +
"Database= dsa_project ;UserName= root;" +
"Password=Cedric43210$");
        TextCompositionAutoComplete data = new TextCompositionAutoComplete();
        public MainWindow()
        {
            InitializeComponent();
            LoadStates();
        }
        private void LoadStates()
        {
            List<string> states = GetStatesFromDatabase();
            SearchBox.ItemsSource = states;
        }
        public List<string> user = new List<string>();
        private List<string> GetStatesFromDatabase()
        {
            {
                List<string> states = new List<string>();

                try
                {
                    using (connection)
                    {
                        connection.Open();
                        string query = "SELECT first_name,Last_name FROM table_3";
                        MySqlCommand cmd = new MySqlCommand(query, connection);
                        MySqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            states.Add(reader["first_name"].ToString());
                            user.Add(reader["first_name"].ToString());

                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}");
                }

                return states;
            }
        }

        private void StateComboBox_GotFocus(object sender, RoutedEventArgs e)
        {
            SearchBox.IsDropDownOpen = true;
        }

        private void SearchBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void StateComboBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            String input = SearchBox.Text;

            // Filter the states list to match exactly what the user has typed
            var filteredStates = user.FindAll(state => state.StartsWith(input, StringComparison.OrdinalIgnoreCase));

            // Update the ComboBox ItemsSource with the filtered list
            SearchBox.ItemsSource = filteredStates;

            // Open the dropdown if there's matching items
            SearchBox.IsDropDownOpen = filteredStates.Count > 0;

        }

        private void SearchBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }

        private void SearchBox_SelectionChanged_2(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}

    