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
    /// Interaction logic for ApplicantUI.xaml
    /// </summary>
    public partial class ApplicantUI : Window
    {
        public ApplicantUI()
        {
            InitializeComponent();
        }

        private void Back_Btn_Click(object sender, RoutedEventArgs e)
        {
            DB db = new DB();
            this.Hide();
            db.Show();
        }
    }
}
