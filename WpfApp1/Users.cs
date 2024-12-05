using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class Users
    {
        MySqlConnection connection = new MySqlConnection("Server=localhost;" +
        "Database=dsa_project;UserName= root;" +
"Password=Cedric43210$");
        public int Id { get; set; }

        public string First_Name { get; set; }

        public string Last_Name { get; set; }

        public string email { get; set; }

        public static List<Users> list = new List<Users>();
        public Users() { }

        public Users(int id, string fname, string lname, string e_mail)
        {
            Id = id;
            First_Name = fname;
            Last_Name = lname;
            email = e_mail;
        }

        public void search(string key)
        {
            connection.Open();
            string sql = "Select * from applicant_table where first_name like ?";
            MySqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("first_name", key + "%");
            MySqlDataReader reader = cmd.ExecuteReader();
            list.Clear();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Users user = new Users()
                    {
                        First_Name = reader["first_name"].ToString(),
                        Last_Name = reader["last_name"].ToString(),
                        Id = Convert.ToInt32(reader["id"]),
                        email = reader["email"].ToString()
                    };
                    list.Add(user);
                }
            }
            reader.Close();
            cmd.Dispose();
            connection.Close();
        }
    }
}
