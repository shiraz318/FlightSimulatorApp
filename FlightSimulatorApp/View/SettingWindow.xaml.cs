﻿using System;
using System.Windows;

namespace FlightSimulatorApp.View
{
    /// <summary>
    /// Interaction logic for SettingWindow.xaml
    /// </summary>
    public partial class SettingWindow : Window
    {
        private bool isOk = false;
        // Property.
        public bool IsOk { get { return isOk; } set { isOk = value; } }

        public SettingWindow()
        {
            InitializeComponent();
        }

        // Define the action when the cancel button is pressed.
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            IsOk = false;
            this.Close();
        }

        // Define the action when the ok button is pressed.
        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            string ip = ipText.Text;
            int port = -1;
            if (Int32.TryParse(portText.Text, out int j))
            {
                port = j;
            }

            //  And other validation checks if needed.
           if ((port == -1) || (ip == "") || (portText.Text == ""))
            {
                // Invalid.
                validation.Content = "Invalid port or ip, please try again";
            } else
            {
                IsOk = true;
                this.Close();
            }           
        }
    }
}
