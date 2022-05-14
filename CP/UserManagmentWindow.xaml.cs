using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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

namespace CP
{
    /// <summary>
    /// Логика взаимодействия для UserManagmentWindow.xaml
    /// </summary>
    public partial class UserManagmentWindow : Window
    {
        int timeout = 100;
        public UserManagmentWindow()
        {
            InitializeComponent();
            ReadInfo("All");
            ReadInfo("Users");
        }

        private void ReadInfo(string info)
        {
            ClientObject.SendRequestToServer("READ CLIENTS");
            System.Threading.Thread.Sleep(timeout);
            DataTable dataTable = ClientObject.SendSelectRequestToServer(info);
            System.Threading.Thread.Sleep(timeout);
            if (info == "All")
                ClientsTable_Del.ItemsSource = dataTable.DefaultView;
       
        }

        private void RefreshRed_Click(object sender, RoutedEventArgs e)
        {
            ReadInfo("Users");
        }

        private void RefreshDel_Click(object sender, RoutedEventArgs e)
        {
            ReadInfo("All");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (DelID.Text == "")
            {
                DelClLabel.Content = "Поле Id пустое";
            }
            else
            {
                if (!Int32.TryParse(DelID.Text, out int res))
                {
                    DelClLabel.Content = "Id должно быть числом";
                }
                else
                {
                    ClientObject.SendRequestToServer("DELETE CLIENT");
                    System.Threading.Thread.Sleep(timeout);
                    DelClLabel.Content = ClientObject.SendRequestToServer(DelID.Text);
                }
            }
        }
    }
}
