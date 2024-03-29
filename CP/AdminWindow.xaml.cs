﻿using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        private static AdminWindow instance;
        public string workLogin = "";
        public AdminWindow(string login)
        {
            InitializeComponent();
            workLogin = login;
        }
        public static AdminWindow getInstance(string login)
        {
            if (instance == null)
                instance = new AdminWindow(login);
            return instance;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ReportWindow repWindow = new ReportWindow();
            repWindow.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            CRUDWindow crWindw = new CRUDWindow();
            crWindw.Show();
        }

        private void Button_Click2(object sender, RoutedEventArgs e)
        {

        }
    }
}
