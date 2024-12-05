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
using System.Windows.Shapes;

namespace DSA1
{
    /// <summary>
    /// Interaction logic for FriendsListWindow.xaml
    /// </summary>
    public partial class FriendsListWindow : Window
    {
        public FriendsListWindow()
        {
            InitializeComponent();
            Display(UserProfile.keys);
        }

        public void Display(List<int> list)
        {
            List<User> usersList = new List<User>();

            foreach (var i in list)
            {
                if (MainWindow.applicantUserMapList2.TryGetValue(i, out User user))
                {
                    usersList.Add(user);
                }
            }
            friendListGrid.ItemsSource = usersList;
        }
    }
}
