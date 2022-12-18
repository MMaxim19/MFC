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
    /// Логика взаимодействия для Server.xaml
    /// </summary>
    public partial class Server : Window
    {
        AccountingEntities Acc;
        public Server()
        {
            Acc = new AccountingEntities();
            InitializeComponent();
            ServerList.ItemsSource = Acc.ServerAccounting.ToList();
            cbStatus.ItemsSource = Acc.EquipmentStatus.ToList();
            cbServer.ItemsSource = Acc.ServerModel.ToList();
        }

        private void btnBack(object sender, RoutedEventArgs e)
        {
            MainWindow MN = new MainWindow();
            MN.Show();
            Close();
        }

        private void btnAdd(object sender, RoutedEventArgs e)
        {
            ServerAccounting server = new ServerAccounting();

            //Добавление вводимых данных в базу
            var model = cbServer.SelectedItem as ServerModel;
            server.Server = model.ID;
            server.SerialNumber = tbSerial.Text;
            server.InventoryNumber = Convert.ToInt32(tbInventory.Text);
            var upsStatus = cbStatus.SelectedItem as EquipmentStatus;
            server.Status = upsStatus.ID;
            server.ArrivalDate = DateTime.Parse(Convert.ToString(dpArrival));

            MessageBox.Show("Сервер успешно добавлен в базу!");

            try
            {
                Acc.ServerAccounting.Add(server);
                Acc.SaveChanges();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            ServerList.ItemsSource = Acc.ServerAccounting.ToList();
            cbServer.SelectedValue = -1;
            tbSerial.Clear();
            tbInventory.Clear();
            cbStatus.SelectedValue = -1;
            dpArrival.Text = null;
        }

        private void btnEdit(object sender, RoutedEventArgs e)
        {
            int num = Convert.ToInt32(tbID.Text);
            var eRow = Acc.ServerAccounting.Where(x => x.ID == num).FirstOrDefault();
            var model = cbServer.SelectedItem as ServerModel;
            eRow.Server = model.ID;
            eRow.SerialNumber = tbSerial.Text;
            eRow.InventoryNumber = Convert.ToInt32(tbInventory.Text);
            var Status = cbStatus.SelectedItem as EquipmentStatus;
            eRow.Status = Status.ID;
            eRow.ArrivalDate = DateTime.Parse(Convert.ToString(dpArrival));

            MessageBox.Show("Данные обновлены!");

            try
            {
                Acc.SaveChanges();
                ServerList.ItemsSource = Acc.ServerAccounting.ToList();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);

            }
            cbServer.SelectedValue = -1;
            tbSerial.Clear();
            tbInventory.Clear();
            cbStatus.SelectedValue = -1;
            dpArrival.Text = null;
        }
    }
}
