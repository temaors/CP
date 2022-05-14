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
    /// Логика взаимодействия для AbonementManagmentWindow.xaml
    /// </summary>
    public partial class AbonementManagmentWindow : Window
    {

        static int timeout = 100;
        public AbonementManagmentWindow()
        {
            InitializeComponent();
            ReadInfo("All");
        }

        private void ReadInfo(string info)
        {
            ClientObject.SendRequestToServer("READ ABONEMENTS");
            System.Threading.Thread.Sleep(timeout);
            DataTable dataTable = ClientObject.SendSelectRequestToServer(info);
            System.Threading.Thread.Sleep(timeout);
            if (info == "All")
                AbonementsTable.ItemsSource = dataTable.DefaultView;
            else
                AbonementsTable.ItemsSource = dataTable.DefaultView;
        }
        private void AbEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            if (e.Column.DisplayIndex != 1)
            {
                e.Cancel = true;
            }
        }
    }
}
