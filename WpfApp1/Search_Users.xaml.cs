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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp1;

namespace DSA1
{
    /// <summary>
    /// Interaction logic for Search_Users.xaml
    /// </summary>
    public partial class Search_Users : UserControl
    {
        public Search_Users()
        {
            InitializeComponent();
        }

        private void search_User_Load(object sender,EventArgs e)
        {

        }

        public void details(Users d)
        {
            Full_name_Label.Content = d.First_Name + " " + d.Last_Name;
        }

        public void search_Result(string key)
        {
            Users user = new Users();
            user.search(key);
            Full_name_Label.Content = user.First_Name + " " + user.Last_Name;
        }
    }
}
