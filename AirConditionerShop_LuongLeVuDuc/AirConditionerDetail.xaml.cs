using Microsoft.Extensions.FileSystemGlobbing.Internal;
using Repositories.Models;
using Services;
using System.Diagnostics.Metrics;
using System.Text.RegularExpressions;
using System.Windows;

namespace AirConditionerShop_LuongLeVuDuc
{
    /// <summary>
    /// Interaction logic for AirConditionerDetail.xaml
    /// </summary>
    public partial class AirConditionerDetail : Window
    {
        public AirConditioner SelectedAirConditioner { get; set; } = null;
        public AirConditionerDetail()
        {
            InitializeComponent();
        }

        private void btnAction_Click(object sender, RoutedEventArgs e)
        {
            int loginUserRole = (int)App.Current.Properties["UserRole"];

            if (loginUserRole == 1)
            {
                if (string.IsNullOrEmpty(tbAirConditionerId.Text) ||
                    string.IsNullOrEmpty(tbAirConditionerName.Text) ||
                    string.IsNullOrEmpty(tbWarranty.Text) ||
                    string.IsNullOrEmpty(tbSoundPressureLevel.Text) ||
                    string.IsNullOrEmpty(tbFeatureFunction.Text) ||
                    string.IsNullOrEmpty(tbQuantity.Text) ||
                    string.IsNullOrEmpty(tbDollarPrice.Text) ||
                    string.IsNullOrEmpty(cboSupplierId.Text))
                {
                    MessageBox.Show("Please fill in all the required fields.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                int quantity = 0;
                double dollarPrice = 0;
                if (!int.TryParse(tbAirConditionerId.Text, out int airConditionerId) ||
                    !int.TryParse(tbQuantity.Text, out quantity) ||
                    !double.TryParse(tbDollarPrice.Text, out dollarPrice))
                {
                    MessageBox.Show("Please enter valid numeric values.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (dollarPrice < 0 || dollarPrice >= 4000000 || quantity < 0 || quantity >= 4000000)
                {
                    MessageBox.Show("Field must be positive and smaller than 4000000!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (tbAirConditionerName.Text.Length < 5 || tbAirConditionerName.Text.Length > 90)
                {
                    MessageBox.Show("Air Conditioner Name must be in the range of 5 – 90 characters", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                string pattern = @"^[A-Z1-9][a-zA-Z0-9]*(\s[A-Z1-9][a-zA-Z0-9]*)*$";
                if (!Regex.IsMatch(tbAirConditionerName.Text, pattern))
                {
                    MessageBox.Show("Each word of the AirConditionerName must begin with a capital letter or a digit (1 - 9)", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                    
                }

                AirConditioner airConditioner = new AirConditioner()
                {
                    AirConditionerId = int.Parse(tbAirConditionerId.Text),
                    AirConditionerName = tbAirConditionerName.Text,
                    Warranty = tbWarranty.Text,
                    SoundPressureLevel = tbSoundPressureLevel.Text,
                    FeatureFunction = tbFeatureFunction.Text,
                    Quantity = int.Parse(tbQuantity.Text),
                    DollarPrice = double.Parse(tbDollarPrice.Text),
                    SupplierId = cboSupplierId.SelectedValue.ToString()
                };

                AirConditionerService airConditionerService = new AirConditionerService();
                if (SelectedAirConditioner != null)
                    airConditionerService.UpdateAirConditioner(airConditioner);
                else
                    airConditionerService.AddAirConditioner(airConditioner);

                Close();
            }
            else
            {
                System.Windows.MessageBox.Show("You have no permission to access this function!", "Wrong Credential", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SupplierCompanyService supplierCompanyService = new SupplierCompanyService();
            cboSupplierId.ItemsSource = supplierCompanyService.GetAllSupplierCompanies();
            cboSupplierId.DisplayMemberPath = "SupplierName";
            cboSupplierId.SelectedValuePath = "SupplierId";
            cboSupplierId.SelectedValue = "SC0005";

            if (SelectedAirConditioner != null)
            {
                lblTittle.Content = "Update Air Conditioner";
                tbAirConditionerId.Text = SelectedAirConditioner.AirConditionerId.ToString();
                tbAirConditionerId.IsEnabled = false;
                tbAirConditionerName.Text = SelectedAirConditioner.AirConditionerName;
                tbWarranty.Text = SelectedAirConditioner.Warranty;
                tbSoundPressureLevel.Text = SelectedAirConditioner.SoundPressureLevel;
                tbFeatureFunction.Text = SelectedAirConditioner.FeatureFunction;
                tbQuantity.Text = SelectedAirConditioner.Quantity.ToString();
                tbDollarPrice.Text = SelectedAirConditioner.DollarPrice.ToString();
                cboSupplierId.SelectedValue = SelectedAirConditioner.SupplierId;

                btnAction.Content = "Update";
            }
        }
    }
}
