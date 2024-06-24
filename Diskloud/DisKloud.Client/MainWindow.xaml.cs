﻿using DisKloud.Client.Core;
using System.ComponentModel;
using System.IO;
using System.Reflection.PortableExecutable;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DisKloud.Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        [DllImport("Kernel32")]
        public static extern void AllocConsole();

        [DllImport("Kernel32", SetLastError = true)]
        public static extern void FreeConsole();


        private Thread worker;

        private Worker bkworker;
        private bool connected = false;
        
        public MainWindow()
        {
            InitializeComponent();
            AllocConsole();
             


        }
        

        private void ConnectButton_Click(object sender, RoutedEventArgs e)
        {
            if (connected)
            {
                connected = false;
                bkworker.r();
            }
            else
            {
                bkworker = new Worker(Server_address.Text);
                worker = new Thread(bkworker.work);
                worker.SetApartmentState(ApartmentState.STA);
                worker.Start();
                connected = true;

            }
            


        }

        

        

       
    }
}