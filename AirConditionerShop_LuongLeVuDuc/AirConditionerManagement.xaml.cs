using Microsoft.IdentityModel.Tokens;
using Repositories.Models;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AirConditionerShop_LuongLeVuDuc
{
    /// <summary>
    /// Interaction logic for AirConditionerManagement.xaml
    /// </summary>
    public partial class AirConditionerManagement : Window
    {
        private AirConditioner _selected = null;

        public AirConditionerManagement()
        {
            InitializeComponent();
        }

        public IEnumerable<object> Table { get; set; }
        private AirConditionerService airConditionerService = new();
        private SupplierCompanyService supplierCompanyService = new();
        int loginUserRole = (int)App.Current.Properties["UserRole"];

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (loginUserRole == 1)
            {
                AirConditionerDetail airConditionerDetail = new AirConditionerDetail();
                airConditionerDetail.ShowDialog();
                RefreshDataGridView();
            }
            else
            {
                MessageBox.Show("You have no permission to access this function!", "Wrong Credential", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (_selected != null)
            {
                AirConditionerDetail detail = new AirConditionerDetail();
                detail.SelectedAirConditioner = _selected;
                detail.ShowDialog();
                RefreshDataGridView();
                _selected = null;
            }
            else
            {
                MessageBox.Show("You must choose an item to update!", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private async void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (_selected != null)
            {
                MessageBoxResult result = await ShowDeleteConfirmationBox();
                if (result == MessageBoxResult.Yes)
                {
                    airConditionerService.DeleteAirConditioner(_selected);
                }
                RefreshDataGridView();
                _selected = null;
            }
            else
            {
                MessageBox.Show("You must choose an item to delete!", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            
        }

        private Task<MessageBoxResult> ShowDeleteConfirmationBox()
        {
            return Task.Run(() =>
            {
                return MessageBox.Show(
                    "Are you sure you want to delete?",
                    "Confirm delete",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);
            });
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            if (loginUserRole == 1 || loginUserRole == 2)
            {
                string feature = tbSearchFeature.Text;
                int quantity = int.Parse(tbSearchQuantity.Text);

                dgAirConditionerList.ItemsSource = null;
                dgAirConditionerList.ItemsSource = airConditionerService.SearchByFeatureOrQuantity(feature, quantity);
            }
            else
            {
                MessageBox.Show("You have no permission to access this function!", "Wrong Credential", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void dgAirConditionerList_Loaded(object sender, RoutedEventArgs e)
        {
            if (loginUserRole == 1 || loginUserRole == 2)
            {
                RefreshDataGridView();
            }
            else
            {
                MessageBox.Show("You have no permission to access this function!", "Wrong Credential", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ShowCustomDataGridView(List<AirConditioner> airConditionerList)
        {
            List<SupplierCompany> supplierCompanies = supplierCompanyService.GetAllSupplierCompanies();
            Table = from airConditioner in airConditionerList
                    join supplierCompany in supplierCompanies on airConditioner.SupplierId equals supplierCompany.SupplierId
                    select new
                    {
                        AirConditionerId = airConditioner.AirConditionerId,
                        AirConditionerName = airConditioner.AirConditionerName,
                        Warranty = airConditioner.Warranty,
                        SoundPressureLevel = airConditioner.SoundPressureLevel,
                        FeatureFunction = airConditioner.FeatureFunction,
                        Quantity = airConditioner.Quantity,
                        DollarPrice = airConditioner.DollarPrice,
                        SupplierId = airConditioner.SupplierId,
                        SupplierName = supplierCompany.SupplierName
                    };
            dgAirConditionerList.ItemsSource = null;
            dgAirConditionerList.ItemsSource = Table;
        }

        private void RefreshDataGridView()
        {
            //List<AirConditioner> airConditionerList = airConditionerService.GetAllAirConditioners();
            //List<SupplierCompany> supplierCompanies = supplierCompanyService.GetAllSupplierCompanies();
            //Table = from airConditioner in airConditionerList
            //        join supplierCompany in supplierCompanies on airConditioner.SupplierId equals supplierCompany.SupplierId
            //        select new
            //        {
            //            AirConditionerId = airConditioner.AirConditionerId,
            //            AirConditionerName = airConditioner.AirConditionerName,
            //            Warranty = airConditioner.Warranty,
            //            SoundPressureLevel = airConditioner.SoundPressureLevel,
            //            FeatureFunction = airConditioner.FeatureFunction,
            //            Quantity = airConditioner.Quantity,
            //            DollarPrice = airConditioner.DollarPrice,
            //            SupplierId = airConditioner.SupplierId,
            //            SupplierName = supplierCompany.SupplierName
            //        };
            dgAirConditionerList.ItemsSource = null;
            dgAirConditionerList.ItemsSource = airConditionerService.GetAllAirConditioners();
        }

        private void dgAirConditionerList_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (dgAirConditionerList.SelectedItems.Count > 0)
            {
                _selected = (AirConditioner)dgAirConditionerList.SelectedItems[0];
            }
        }
    }
}
