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
    /// Логика взаимодействия для CRUDWindow.xaml
    /// </summary>
    public partial class CRUDWindow : Window
    {
        static int timeout = 100;
        public CRUDWindow()
        {
            InitializeComponent();
            ReadInfo();
        }

        private void AddClient(object sender, RoutedEventArgs e)
        {
            if(addID.Text=="" || addSurname.Text == "" || addName.Text == "" || addThirdname.Text == "" || addLogin.Text == "" || addPassword.Text == "" || addAccess.Text == "" || addAge.Text == "" || addGender.Text == "" || addEmail.Text == "")
            {
                errAddLabel.Content = "Поля для ввода должны быть заполнены!";
            }
            else
            {
                ClientObject.SendRequestToServer("ADD CLIENT");
                System.Threading.Thread.Sleep(timeout);
                ClientObject.SendRequestToServer(addName.Text);
                System.Threading.Thread.Sleep(timeout);
                ClientObject.SendRequestToServer(addSurname.Text);
                System.Threading.Thread.Sleep(timeout);
                ClientObject.SendRequestToServer(addThirdname.Text);
                System.Threading.Thread.Sleep(timeout);
                ClientObject.SendRequestToServer(addGender.Text);
                System.Threading.Thread.Sleep(timeout);
                ClientObject.SendRequestToServer(addEmail.Text);
                System.Threading.Thread.Sleep(timeout);
                ClientObject.SendRequestToServer(addID.Text);
                System.Threading.Thread.Sleep(timeout);
                ClientObject.SendRequestToServer(addLogin.Text);
                System.Threading.Thread.Sleep(timeout);
                ClientObject.SendRequestToServer(addPassword.Text);
                System.Threading.Thread.Sleep(timeout);
                ClientObject.SendRequestToServer(addAge.Text);
                System.Threading.Thread.Sleep(timeout);
                errAddLabel.Content = ClientObject.SendRequestToServer(addAccess.Text);
            }
        }

        private void findClients(object sender, RoutedEventArgs e)
        {
            ClientObject.SendRequestToServer("READ CLIENTS");
            System.Threading.Thread.Sleep(timeout);
            if (findID.Text == "")
            {
                ClientObject.SendRequestToServer("All");
                System.Threading.Thread.Sleep(timeout);
                DataTable dataTable = ClientObject.SendSelectRequestToServer("All");
                System.Threading.Thread.Sleep(timeout);
                ClientsTable_Find.ItemsSource = dataTable.DefaultView;
            }
            else
            {
                ClientObject.SendRequestToServer(findID.Text);
                System.Threading.Thread.Sleep(timeout);
                DataTable dataTable = ClientObject.SendSelectRequestToServer(ComboBoxFind.Text);
                System.Threading.Thread.Sleep(timeout);
                ClientsTable_Find.ItemsSource = dataTable.DefaultView;
            }
        }

        private void ReadInfo()
        {
            ClientObject.SendRequestToServer("READ CLIENTS");
            System.Threading.Thread.Sleep(timeout);
            ClientObject.SendRequestToServer("All");
            System.Threading.Thread.Sleep(timeout);
            DataTable dataTable = ClientObject.SendSelectRequestToServer("All");
            System.Threading.Thread.Sleep(timeout);
            ClientsTable_Delete.ItemsSource = dataTable.DefaultView;
        }
        private void DeleteClient(object sender, RoutedEventArgs e)
        {
            if (delID.Text != "")
            {
                ClientObject.SendRequestToServer("DELETE CLIENT");
                System.Threading.Thread.Sleep(timeout);
                delLabel.Content = ClientObject.SendRequestToServer(delID.Text);
            }
            else
            {
                delLabel.Content = "Поле ID не может быть пустым";
            }
            System.Threading.Thread.Sleep(timeout);
            ReadInfo();
        }
    }
}
