using System;
using System.Collections.Generic;
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
            if (info == "All")
                EmployesTable_Del.ItemsSource = dataTable.DefaultView;
            else
                EmployesTable_Red.ItemsSource = dataTable.DefaultView;
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
                DelEmpLabel.Content = "Поле Id пустое";
            }
            else
            {
                if (!Int32.TryParse(DelID.Text, out int res))
                {
                    DelEmpLabel.Content = "Id должно быть числом";
                }
                else
                {
                    ClientObject.SendRequestToServer("DELETE EMPLOYEE");
                    System.Threading.Thread.Sleep(timeout);
                    DelEmpLabel.Content = ClientObject.SendRequestToServer(DelID.Text);
                }
            }
        }

        private void RedEmploye_Click(object sender, RoutedEventArgs e)
        {
            if (RedID.Text == "")
            {
                RedEmpLabel.Content = "Поле Id пустое";
            }
            else
            {
                if (!Int32.TryParse(RedID.Text, out int res))
                {
                    RedEmpLabel.Content = "Id должно быть числом";
                }
                else
                {
                    ClientObject.SendRequestToServer("RED EMPLOYEE ACCESS");
                    System.Threading.Thread.Sleep(timeout);
                    RedEmpLabel.Content = ClientObject.SendRequestToServer(RedID.Text);
                }
            }

        }
    }
}
