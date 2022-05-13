using System;
using System.Net.Sockets;
using System.Windows;
using System.Threading;
using System.Text;

namespace CP
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        const int port = 4020;
        const string address = "127.0.0.1";
        public string workLogin = "";
        public int timeout = 100;
        TcpClient client = new TcpClient();
        public MainWindow()
        {
            InitializeComponent();
            ClientObject.SendRequestToServer("Клиент подключён");
        }  

        private void Button_LogIn(object sender, RoutedEventArgs e)
        {
            if ((TextBoxLogin.Text == "") || (TextBoxPassword.Password == ""))
            {
                LogLabel.Content = "Одно из полей является пустым";
            }
            else
            {
                ClientObject.SendRequestToServer("LOG IN");
                System.Threading.Thread.Sleep(timeout);
                ClientObject.SendRequestToServer(TextBoxLogin.Text);
                System.Threading.Thread.Sleep(timeout);
                LogLabel.Content = "Введите данные для входа";
                string answer = ClientObject.SendRequestToServer(TextBoxPassword.Password);
                if (answer == "ADMIN")
                {
                    LogLabel.Content = "";
                    workLogin = TextBoxLogin.Text;
                    AdminWindow adminWindow = new AdminWindow(workLogin);
                    adminWindow.Show();
                }
                else if (answer == "USER")
                {
                    LogLabel.Content = "";
                    workLogin = TextBoxLogin.Text;
                    ClientWindow clientWindow = new ClientWindow(workLogin);
                    clientWindow.Show();
                }
                else 
                    LogLabel.Content = "Введён неверный логин или пароль";
            }
        }

        private void Button_Reg(object sender, RoutedEventArgs e)
        {
            RegistrationWindow regWindow = new RegistrationWindow();
            regWindow.Show();
        }

        private void KD(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                TextBoxPassword.Focus();
            }
        }

        private void KU(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                TextBoxLogin.Focus();
            }
        }
    }
}
