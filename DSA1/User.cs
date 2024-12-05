using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSA1
{
    public class User
    {
        public int Id { get; set; }

        public string First_Name { get; set; }

        public string Last_Name { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public string JobTitle { get; set; }

        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        public string Gender { get; set; }

        public static List<User> list = new List<User>();
        public User() { }

        public User(int id, string fname, string lname, string email, 
            string jobTitle, string phoneNumber, string address, string gender, string password)
        {
            Id = id;
            First_Name = fname;
            Last_Name = lname;
            Email = email;
            JobTitle = jobTitle;
            PhoneNumber = phoneNumber;
            Address = address;
            Gender = gender;
            Password = password;
        }

        public override string ToString()
        {
            return $"{First_Name} {Last_Name} - {JobTitle}";
        }

        public static void SetUserDetails(User userProfile, User selectedUser)
        {
            userProfile.First_Name = selectedUser.First_Name;
            userProfile.Last_Name = selectedUser.Last_Name;
            // Assign other properties as needed.
        }

    }
}
