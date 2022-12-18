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

namespace MFC
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnClose(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btn_WindowIBP(object sender, RoutedEventArgs e)
        {
            IBP ups = new IBP();
            ups.Show();
            Close();
        }

        private void btn_WindowServer(object sender, RoutedEventArgs e)
        {
            Server sr = new Server();
            sr.Show();
            Close();
        }
    }
}
