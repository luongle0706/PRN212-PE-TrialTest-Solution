using Microsoft.IdentityModel.Tokens;
using Repositories.Models;
using Services;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AirConditionerShop_LuongLeVuDuc
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        StaffMemberService staffMemberService = new StaffMemberService();

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (tbEmailAddress.Text.IsNullOrEmpty() || tbPassword.Password.IsNullOrEmpty()) 
            {
                MessageBox.Show("Please fill in both Email and Password!", "Login Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            StaffMember? staffMember = staffMemberService.CheckLoginUser(tbEmailAddress.Text, tbPassword.Password);
            if (staffMember == null)
            {
                MessageBox.Show("Incorrect Email or Password!", "Login Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (staffMember.Role == 1 || staffMember.Role == 2)
            {
                App.Current.Properties["UserRole"] = staffMember.Role;
                AirConditionerManagement airConditionerManagement = new AirConditionerManagement();
                airConditionerManagement.Show();
                this.Close();
            } 
            else
            {
                MessageBox.Show("You have no permission to access this function!", "Wrong Credential", MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
            
        }
    }
}