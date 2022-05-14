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
    /// Логика взаимодействия для TrainerManagmentWindow.xaml
    /// </summary>
    public partial class TrainerManagmentWindow : Window
    {
        public TrainerManagmentWindow()
        {
            InitializeComponent();
            ReadInfo("All");
        }
        static int timeout = 100;

        private void ReadInfo(string info)
        {
            ClientObject.SendRequestToServer("READ TRAINERS");
            System.Threading.Thread.Sleep(timeout);
            DataTable dataTable = ClientObject.SendSelectRequestToServer(info);
            System.Threading.Thread.Sleep(timeout);
            if (info == "All")
                TrainersTable.ItemsSource = dataTable.DefaultView;
            else
                TrainersTable.ItemsSource = dataTable.DefaultView;
        }

        private void Refresh(object sender, RoutedEventArgs e)
        {

        }

        private void Find(object sender, RoutedEventArgs e)
        {
            if (ID.Text != "")
            {
                ClientObject.SendRequestToServer("SELECT TRAINER");
                System.Threading.Thread.Sleep(timeout);
                DataTable dataTable = ClientObject.SendSelectRequestToServer(ID.Text);
                System.Threading.Thread.Sleep(timeout);
                TrainersTable.ItemsSource = dataTable.DefaultView;
            }
            else
            {
                Info.Content = "Поле ID не должно быть пустым";
            }
        }

        private void Edit(object sender, RoutedEventArgs e)
        {

        }

        private void Delete(object sender, RoutedEventArgs e)
        {

        }
    }
}
