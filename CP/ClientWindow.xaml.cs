using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CP
{
    /// <summary>
    /// Логика взаимодействия для ClientWindow.xaml
    /// </summary>
    public partial class ClientWindow : Window
    {
        public string workLogin = "";
        static int timeout = 100;

        public ClientWindow(string login)
        {
            InitializeComponent();
            workLogin = login;
            readInfo();
        }

        public void readInfo()
        {
            //DataTable clientTab = ClientObject.SendSelectRequestToServer("SELECT CLIENT");
            //System.Threading.Thread.Sleep(timeout);
            ClientObject.SendRequestToServer("READ ABONEMENTS");
            System.Threading.Thread.Sleep(timeout);
            DataTable dataTable2 = ClientObject.SendSelectRequestToServer("All");
            System.Threading.Thread.Sleep(timeout);
            AbonementsTable.ItemsSource = dataTable2.DefaultView;
            ClientObject.SendRequestToServer("READ TRAINERS");
            System.Threading.Thread.Sleep(timeout);
            DataTable dataTable1 = ClientObject.SendSelectRequestToServer(workLogin);
            System.Threading.Thread.Sleep(timeout);

            TrainersTable.ItemsSource = dataTable1.DefaultView;
        }

        private void newAbonement(object sender, RoutedEventArgs e)
        {

        }

        private void newTrainer(object sender, RoutedEventArgs e)
        {

        }
    }
}
