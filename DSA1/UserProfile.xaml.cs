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
using MySql.Data.MySqlClient;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Diagnostics;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;

namespace DSA1
{
    public partial class UserProfile : Window
    {
        MySqlConnection connection = new MySqlConnection("Server=localhost;" + "Database=project_database;UserName= root;" + "Password=Cedric1234%%");
        public static int windowNumber { get; set; }
        public static List<int> keys = new List<int>();
        public UserProfile()
        {
            InitializeComponent();
            showInfo();
            addFriendList();
        }

        public UserProfile(User userInfo)
        {
            InitializeComponent();
            addFriendList();

            Full_Name.Text = userInfo.First_Name + " " + userInfo.Last_Name;
            Job_Title_txtbox.Text = userInfo.JobTitle;
            Address_txtbox.Text = userInfo.Address;

        }

        private void showInfo()
        {
            int key = MainWindow.userId;
            User logedUser = MainWindow.applicantUserMapList2[key];

            if (MainWindow.applicantUserMapList2.TryGetValue(key, out User storedHash))
            {
                Full_Name.Text = logedUser.First_Name + " " + logedUser.Last_Name;
                Job_Title_txtbox.Text = logedUser.JobTitle;
                Address_txtbox.Text = logedUser.Address;
            }
        }

        public void changeInfo(string name, string Jtitle, string address)
        {
            Full_Name.Text = name;
            Job_Title_txtbox.Text = Jtitle;
            Address_txtbox.Text = address;
        }

        private void Edit_Experience_btn_Click(object sender, RoutedEventArgs e)
        {
            ToggleExperienceEditable(true);
        }

        private void Education_Edit_btn_Click(object sender, RoutedEventArgs e)
        {
            ToggleEducationEditable(true);
        }

        private void abt_you_btn_Click(object sender, RoutedEventArgs e)
        {
            About_RichTextBox.IsReadOnly = false;
        }

        private void ToggleExperienceEditable(bool isEditable)
        {
            ExperienceTextBox1.IsReadOnly = !isEditable;
            ExperienceTextBox2.IsReadOnly = !isEditable;
            ExperienceTextBox3.IsReadOnly = !isEditable;
            ExperienceTextBox4.IsReadOnly = !isEditable;

            if (isEditable)
            {
                TextBox newExperience = new TextBox { Margin = new Thickness(0, 5, 0, 5), FontSize = 14, TextWrapping = TextWrapping.Wrap };
                (ExperienceTextBox1.Parent as StackPanel).Children.Add(newExperience);
            }
        }

        private void ToggleEducationEditable(bool isEditable)
        {
            EducationTextBox1.IsReadOnly = !isEditable;
            EducationTextBox2.IsReadOnly = !isEditable;
            EducationTextBox3.IsReadOnly = !isEditable;

            if (isEditable)
            {
                TextBox newEducation = new TextBox { Margin = new Thickness(0, 5, 0, 5), FontSize = 14, TextWrapping = TextWrapping.Wrap };
                (EducationTextBox1.Parent as StackPanel).Children.Add(newEducation);
            }
        }

        private void AddFriend_Btn(object sender, RoutedEventArgs e)
        {
            MySqlConnection connection = new MySqlConnection("Server=localhost;" + "Database=project_database;UserName= root;" + "Password=Cedric1234%%");
            int Connect_Key = DB.otherUserKey;
            int Logged_In_UserKeey = MainWindow.userId;
            string query =
                "INSERT INTO friends (user_id, friend_id) VALUES (@UserId, @FriendId)";
            try
            {
                using (connection)
                {
                    connection.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@UserId", Logged_In_UserKeey);
                        cmd.Parameters.AddWithValue("@FriendId", Connect_Key);

                        int rowsAffected = cmd.ExecuteNonQuery();


                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Friend request sent successfully!");
                        }
                        else
                        {
                            MessageBox.Show("Failed to send friend request.");
                        }

                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("An error occurred: " + ex.Message); }
        }
       
  
        private void addFriendList()
        {
            string query = "SELECT friend_id FROM friends WHERE user_id = @UserId";
            try
            {
                using (MySqlConnection connection = new MySqlConnection("Server=127.0.0.1;" +
                    "Database=project_database;UserName=root;Password=127.0.0.1"))
                {
                    connection.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@UserId", MainWindow.userId);
                        using (MySqlDataReader rd = cmd.ExecuteReader())
                        {
                            while (rd.Read())
                            {
                                if (!rd.IsDBNull(0))
                                {
                                    keys.Add(rd.GetInt32(0));
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while retrieving the friend list: " + ex.Message);
            }
        }
        
        private void showFriendList_btn(object sender, RoutedEventArgs e)
        {
           FriendsListWindow flw = new FriendsListWindow();
            flw.Show();

        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            switch (windowNumber)
            {
                case 1:
                    DB db = new DB();
                    this.Hide();
                    db.Show();
                    break;
                    
                case 2:
                    Applicant_search sjw = new Applicant_search();
                    this.Hide();
                    sjw.Show();
                    break;
            }
        }
    }
}
