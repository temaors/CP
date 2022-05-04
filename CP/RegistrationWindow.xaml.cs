using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        private int timeout = 100;

        public RegistrationWindow()
        {
            InitializeComponent();
        }

        private void RegButton_Click(object sender, RoutedEventArgs e)
        {
            if ((Login1.Text == "") || (Password1.Text == "") || (Surname1.Text == "")
               || (Name1.Text == "") || (Thirdname.Text == "") || (Email1.Text == "") ||
               (Address1.Text == "") || (PhoneCode1.Text == "") || (Phone1.Text == "") ||
               (Sex.Text == "") || (Age1.Text == ""))
            {
                RegLable.Content = "Одно из полей является пустым";
            }
            else
            {
                if (!Int32.TryParse(Phone1.Text, out int res1) || !Int32.TryParse(Age1.Text, out int res2))
                {
                    RegLable.Content = "Возраст и телефон должны быть числом!";
                }
                else if (!Check.IsValidEmailAddress(Email1.Text))
                {
                    RegLable.Content = "Поле Email заполнено неверно!";
                }
                else
                {
                    ClientObject.SendRequestToServer("REGISTRATION");
                    System.Threading.Thread.Sleep(timeout);
                    ClientObject.SendRequestToServer(Login1.Text);
                    System.Threading.Thread.Sleep(timeout);
                    ClientObject.SendRequestToServer(Password1.Text);
                    System.Threading.Thread.Sleep(timeout);
                    ClientObject.SendRequestToServer(Surname1.Text);
                    System.Threading.Thread.Sleep(timeout);
                    ClientObject.SendRequestToServer(Name1.Text);
                    System.Threading.Thread.Sleep(timeout);
                    ClientObject.SendRequestToServer(Thirdname.Text);
                    System.Threading.Thread.Sleep(timeout);
                    ClientObject.SendRequestToServer(Email1.Text);
                    System.Threading.Thread.Sleep(timeout);
                    ClientObject.SendRequestToServer(Address1.Text);
                    System.Threading.Thread.Sleep(timeout);
                    ClientObject.SendRequestToServer(PhoneCode1.Text);
                    System.Threading.Thread.Sleep(timeout);
                    ClientObject.SendRequestToServer(Phone1.Text);
                    System.Threading.Thread.Sleep(timeout);
                    ClientObject.SendRequestToServer(Sex.Text);
                    System.Threading.Thread.Sleep(timeout);
                    ClientObject.SendRequestToServer(Age1.Text);
                    System.Threading.Thread.Sleep(timeout);
                    RegLable.Content = ClientObject.SendRequestToServer("1");

                    System.Threading.Thread.Sleep(1000);
                    this.Close();
                }
            }
        }

        private void TextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter /*|| e.Key == Key.Down*/)
            {
                Password1.Focus();
            }
            if(e.Key == Key.Down)
            {
                Password1.Focus();
            }
        }

        private void TextBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Down)
            {
                Surname1.Focus();
            }
        }

        private void TextBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Down)
            {
                Name1.Focus();
            }
        }

        private void TextBox4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Down)
            {
                Thirdname.Focus();
            }
        }

        private void TextBox5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Down)
            {
                Email1.Focus();
            }
        }

        private void TextBox6_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Down)
            {
                Address1.Focus();
            }
        }

        private void TextBox7_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Down)
            {
                PhoneCode1.Focus();
            }
        }

        private void TextBox8_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Down)
            {
                Phone1.Focus();
            }
        }

        private void TextBox9_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Down)
            {
                Sex.Focus();
            }
        }

        private void TextBox10_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Down)
            {
                Age1.Focus();
            }
        }

        private void TB2_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Up)
            {
                Login1.Focus();
            }
        }

        private void TB3_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up)
            {
                Password1.Focus();
            }
        }

        private void TB4_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up)
            {
                Surname1.Focus();
            }
        }

        private void TB5_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up)
            {
                Name1.Focus();
            }
        }

        private void TB6_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up)
            {
                Thirdname.Focus();
            }
        }

        private void TB7_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up)
            {
                Email1.Focus();
            }
        }

        private void TB8_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up)
            {
                Address1.Focus();
            }
        }

        private void TB9_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up)
            {
                PhoneCode1.Focus();
            }
        }

        private void TB10_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up)
            {
                Phone1.Focus();
            }
        }

        private void TB11_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up)
            {
                Sex.Focus();
            }
        }
    }
    public static class Check
    {
        public static bool IsValidEmailAddress(this string s)
        {
            Regex regex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
            return regex.IsMatch(s);
        }
    }
}
