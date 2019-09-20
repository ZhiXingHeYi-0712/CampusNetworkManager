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
            Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            InitializeComponent();
            this.Hide();
            TextBox_SSID.Text = configuration.AppSettings.Settings["SSID"].Value;

            CoreMain core = new CoreMain();
            Thread coreThread = new Thread(new ThreadStart(core.runCore));
            coreThread.IsBackground = true;
            coreThread.Start();     // 主程序运行


        }

        private void Button_Save_Click(object sender, RoutedEventArgs e)
        {
            Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            Toast.showNotification("保存成功！");

            configuration.AppSettings.Settings["SSID"].Value = TextBox_SSID.Text;
            configuration.Save();
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

        private void Debug_Button_Click(object sender, RoutedEventArgs e)
        {
            Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            Toast.showNotification(configuration.AppSettings.Settings["SSID"].Value);

        }
    }
}
