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
    /// Логика взаимодействия для ReportWindow.xaml
    /// </summary>
    public partial class ReportWindow : Window
    {
        int timeout = 100;
        public ReportWindow()
        {
            InitializeComponent();
            readInfo();
        }

        public void readInfo()
        {
            ClientObject.SendRequestToServer("READ REPORT");
            System.Threading.Thread.Sleep(timeout);
            System.Threading.Thread.Sleep(timeout);
            DataTable table = ClientObject.SendSelectRequestToServer("all");
            ReportTable.ItemsSource = table.DefaultView;
        }
    }
}
