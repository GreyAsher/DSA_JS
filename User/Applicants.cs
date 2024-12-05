using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User
{
    public class Applicants
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Lastname { get; set; }

        public Dictionary<Applicants, string> allUser = new Dictionary<Applicants, string>();

        public Applicants() { }
        public Applicants(int id, string name, string lastname)
        {
            Id = id;
            Name = name;
            Lastname = lastname;
        }
        MySqlConnection connection = new MySqlConnection("Server=localhost;" +
"Database=sakila;UserName= root;" +
"Password=Cedric1234%%");
        public void AddIntoMemory()
        {
            if (allUser.Count > 0) return;

            try
            {
                connection.Open();
                string query = "SELECT first_name, last_name FROM actor ";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int i = 0;
                        i++;
                        string userName = reader.IsDBNull(reader.GetOrdinal("first_name")) ? "" : reader.GetString("first_name").Trim();
                        string passWord = reader.IsDBNull(reader.GetOrdinal("last_name")) ? "" : reader.GetString("last_name");
                        Applicants user = new Applicants(i, userName, passWord);

                        allUser.Add(user, userName);

                    }
                }
            }

            catch (Exception ex) { Console.WriteLine("Hello World"); ; } 
        }

    }
}
