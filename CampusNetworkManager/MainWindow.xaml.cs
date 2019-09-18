using CampusNetworkManager.src.core;
using System;
using System.Threading;
using System.Windows;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;
using Windows.Devices.WiFi;
using System.Windows.Forms;
using System.Drawing;
using System.Configuration;

namespace CampusNetworkManager
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    /// 
    
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            this.Hide();

            CoreMain core = new CoreMain();
            Thread coreThread = new Thread(new ThreadStart(core.runCore));
            coreThread.IsBackground = true;
            coreThread.Start();     // 主程序运行

        }

        private void Button_Save_Click(object sender, RoutedEventArgs e)
        {
            TextBlock_SaveSuccess.Visibility = Visibility.Visible;
            string ssid = TextBox_SSID.Text;
            Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            configuration.AppSettings.Settings["SSID"].Value = ssid;
            configuration.Save();

            Thread.Sleep(2000);
            TextBlock_SaveSuccess.Visibility = Visibility.Hidden;


        }

        private void Button_Exit_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Visibility = Visibility.Hidden;
        }
    }
}
