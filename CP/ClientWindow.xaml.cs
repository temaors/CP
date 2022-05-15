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
            ClientObject.SendRequestToServer("READ CLIENTS");
            System.Threading.Thread.Sleep(timeout);
            ClientObject.SendRequestToServer(workLogin);
            System.Threading.Thread.Sleep(timeout);
            DataTable dataTable = ClientObject.SendSelectRequestToServer("Логину");
            System.Threading.Thread.Sleep(timeout);
            surname.Content = dataTable.Rows[0][3].ToString();
            name.Content = dataTable.Rows[0][4].ToString();
            thirdname.Content = dataTable.Rows[0][5].ToString();
            email.Content = dataTable.Rows[0][6].ToString();
            age.Content = dataTable.Rows[0][8].ToString();
            sex.Content = dataTable.Rows[0][7].ToString();
            abonement.Content = dataTable.Rows[0][9].ToString();
            trainer.Content = dataTable.Rows[0][10].ToString();
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
