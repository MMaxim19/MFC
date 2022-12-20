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

namespace MFC
{
    /// <summary>
    /// Логика взаимодействия для IBP.xaml
    /// </summary>
    public partial class IBP : Window
    {
        AccountingEntities Acc;
        public IBP()
        {
            Acc = new AccountingEntities();
            InitializeComponent();
            IBP_List.ItemsSource = Acc.UPS_Accounting.ToList();
            cbStatus.ItemsSource = Acc.EquipmentStatus.ToList();
            cbFilter.ItemsSource = Acc.EquipmentStatus.ToList();
            cbUPS.ItemsSource = Acc.UPSModel.ToList();
        }

        private void btnBack(object sender, RoutedEventArgs e)
        {
            MainWindow mn = new MainWindow();
            mn.Show();
            Close();
        }

        private void btnRefresh(object sender, RoutedEventArgs e)
        {
            IBP_List.ItemsSource = Acc.UPS_Accounting.ToList();
            cbUPS.SelectedValue = -1;
            tbSerial.Clear();
            tbInventory.Clear();
            cbStatus.SelectedValue = -1;
            dpArrival.Text = null;
        }

        private void btnAdd(object sender, RoutedEventArgs e)
        {
            UPS_Accounting ups = new UPS_Accounting();

            //Добавление вводимых данных в базу
            var model = cbUPS.SelectedItem as UPSModel;
            ups.UPS = model.ID;
            ups.SerialNumber = tbSerial.Text;
            ups.InventoryNumber = Convert.ToInt32(tbInventory.Text);
            var upsStatus = cbStatus.SelectedItem as EquipmentStatus;
            ups.Status = upsStatus.ID;
            ups.ArrivalDate = DateTime.Parse(Convert.ToString(dpArrival));

            MessageBox.Show("ИБП успешно добавлен в базу!");

            try
            {
                Acc.UPS_Accounting.Add(ups);
                Acc.SaveChanges();
                IBP_List.ItemsSource = Acc.UPS_Accounting.ToList();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            cbUPS.SelectedValue = -1;
            tbSerial.Clear();
            tbInventory.Clear();
            cbStatus.SelectedValue = -1;
            dpArrival.Text = null;
        }

        private void btnEdit(object sender, RoutedEventArgs e)
        {
            int num = Convert.ToInt32(tbID.Text);
            var eRow = Acc.UPS_Accounting.Where(x => x.ID == num).FirstOrDefault();
            var model = cbUPS.SelectedItem as UPSModel;
            eRow.UPS = model.ID;
            eRow.SerialNumber = tbSerial.Text;
            eRow.InventoryNumber = Convert.ToInt32(tbInventory.Text);
            var Status = cbStatus.SelectedItem as EquipmentStatus;
            eRow.Status = Status.ID;
            eRow.ArrivalDate = DateTime.Parse(Convert.ToString(dpArrival));

            MessageBox.Show("Данные обновлены!");

            try
            {
                Acc.SaveChanges();
                IBP_List.ItemsSource = Acc.UPS_Accounting.ToList();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);

            }
            cbUPS.SelectedValue = -1;
            tbSerial.Clear();
            tbInventory.Clear();
            cbStatus.SelectedValue = -1;
            dpArrival.Text = null;
        }

        private void SearchTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            var input = (sender as TextBox).Text.ToLower();
            if (!(string.IsNullOrEmpty(input)))
            {
                int resultCount = Acc.UPS_Accounting.Count(x => x.UPSModel.UPSName.Contains(input));
                IBP_List.ItemsSource = Acc.UPS_Accounting.Where(x => x.UPSModel.UPSName.Contains(input)).ToList();
            }
            else
            {
                IBP_List.ItemsSource = Acc.UPS_Accounting.ToList();
            }
        }

        private void filterCB(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void btnLoadData(object sender, RoutedEventArgs e)
        {
            IBP_List.ItemsSource = Acc.UPS_Accounting.ToList();
        }
    }
}
