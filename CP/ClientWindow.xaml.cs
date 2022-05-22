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
using System.Globalization;

namespace CP
{
    /// <summary>
    /// Логика взаимодействия для ClientWindow.xaml
    /// </summary>
    public partial class ClientWindow : Window
    {
        public string workLogin = "";
        static int timeout = 100;
        string abID = "", trID = "", currentID = "", currentCost = "";
        public ClientWindow(string login)
        {
            InitializeComponent();
            workLogin = login;
            readCLInfo();
            readAbTr();
            if(abID != null || abID != "")
            {
                readAb();
            }
            if (trID != null || trID != "")
            {
                readTr();
            }
        }

        public void readAb()
        {
            ClientObject.SendRequestToServer("READ ABONEMENTS");
            System.Threading.Thread.Sleep(timeout);
            ClientObject.SendRequestToServer("ID");
            System.Threading.Thread.Sleep(timeout);
            DataTable dataTable2 = ClientObject.SendSelectRequestToServer(abID);
            System.Threading.Thread.Sleep(timeout);
            abToolTipType.Content = dataTable2.Rows[0][3].ToString();
            abToolTipCost.Content = dataTable2.Rows[0][2].ToString();
            currentCost = dataTable2.Rows[0][2].ToString();
            abToolTipCount.Content = dataTable2.Rows[0][1].ToString();
            abToolTipTerm.Content = dataTable2.Rows[0][4].ToString();
        }

        public void readTr()
        {
            ClientObject.SendRequestToServer("READ TRAINERS");
            System.Threading.Thread.Sleep(timeout);
            ClientObject.SendRequestToServer("ID");
            System.Threading.Thread.Sleep(timeout);
            DataTable dataTable1 = ClientObject.SendSelectRequestToServer(trID);
            System.Threading.Thread.Sleep(timeout);
            trToolTipSurname.Content = dataTable1.Rows[0][1].ToString();
            trToolTipName.Content = dataTable1.Rows[0][2].ToString();
            trToolTipThirdname.Content = dataTable1.Rows[0][3].ToString();
            trToolTipType.Content = dataTable1.Rows[0][4].ToString();
            trToolTipCost.Content = dataTable1.Rows[0][5].ToString();
            currentCost = dataTable1.Rows[0][5].ToString();
        }

        public void readAbTr()
        {
            ClientObject.SendRequestToServer("READ ABONEMENTS");
            System.Threading.Thread.Sleep(timeout);
            ClientObject.SendRequestToServer("All");
            System.Threading.Thread.Sleep(timeout);
            DataTable dataTableAbonements = ClientObject.SendSelectRequestToServer("All");
            System.Threading.Thread.Sleep(timeout);
            AbonementsTable.ItemsSource = dataTableAbonements.DefaultView;
            ClientObject.SendRequestToServer("READ TRAINERS");
            System.Threading.Thread.Sleep(timeout);
            ClientObject.SendRequestToServer("All");
            System.Threading.Thread.Sleep(timeout);
            DataTable dataTableTrainers = ClientObject.SendSelectRequestToServer("All");
            System.Threading.Thread.Sleep(timeout);
            TrainersTable.ItemsSource = dataTableTrainers.DefaultView;

        }
        public void readCLInfo()
        {
            ClientObject.SendRequestToServer("READ CLIENTS");
            System.Threading.Thread.Sleep(timeout);
            ClientObject.SendRequestToServer(workLogin);
            System.Threading.Thread.Sleep(timeout);
            DataTable dataTable = ClientObject.SendSelectRequestToServer("Логину");
            System.Threading.Thread.Sleep(timeout);
            currentID = dataTable.Rows[0][0].ToString();
            surname.Content = dataTable.Rows[0][3].ToString();
            name.Content = dataTable.Rows[0][4].ToString();
            thirdname.Content = dataTable.Rows[0][5].ToString();
            email.Content = dataTable.Rows[0][6].ToString();
            age.Content = dataTable.Rows[0][8].ToString();
            sex.Content = dataTable.Rows[0][7].ToString();
            abID = dataTable.Rows[0][10].ToString();
            trID = dataTable.Rows[0][11].ToString();
        }

        private void newAbonement(object sender, RoutedEventArgs e)
        {
                ClientObject.SendRequestToServer("SET ABONEMENT");
                System.Threading.Thread.Sleep(timeout);
                ClientObject.SendRequestToServer(workLogin);
                System.Threading.Thread.Sleep(timeout);
                ClientObject.SendRequestToServer(newAbID.Text);
                System.Threading.Thread.Sleep(timeout);
            readCLInfo();
            readAb();
            ClientObject.SendRequestToServer("UPDATE REPORT");
            System.Threading.Thread.Sleep(timeout);
            Random rnd = new Random();
            ClientObject.SendRequestToServer(rnd.Next(1,99999).ToString());
            System.Threading.Thread.Sleep(timeout);
            ClientObject.SendRequestToServer(currentCost);
            System.Threading.Thread.Sleep(timeout);
            DateTime dt = DateTime.Today;
            ClientObject.SendRequestToServer(dt.ToString());
            System.Threading.Thread.Sleep(timeout);
            ClientObject.SendRequestToServer(currentID);
        }

        private void newTrainer(object sender, RoutedEventArgs e)
        {
                ClientObject.SendRequestToServer("SET TRAINER");
                System.Threading.Thread.Sleep(timeout);
                ClientObject.SendRequestToServer(workLogin);
                System.Threading.Thread.Sleep(timeout);
                ClientObject.SendRequestToServer(newTrID.Text);
                System.Threading.Thread.Sleep(timeout);
            
            readCLInfo();
            readTr();

            ClientObject.SendRequestToServer("UPDATE REPORT");
            System.Threading.Thread.Sleep(timeout);
            Random rnd = new Random();
            ClientObject.SendRequestToServer(rnd.Next(1, 99999).ToString());
            System.Threading.Thread.Sleep(timeout);
            ClientObject.SendRequestToServer(currentCost);
            System.Threading.Thread.Sleep(timeout);
            DateTime dt = DateTime.Today;
            ClientObject.SendRequestToServer(dt.ToString());
            System.Threading.Thread.Sleep(timeout);
            ClientObject.SendRequestToServer(currentID);
        }
    }
}
