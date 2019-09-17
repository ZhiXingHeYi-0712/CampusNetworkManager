using CampusNetworkManager.src.core;
using System;
using System.Threading;
using System.Windows;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;
using Windows.Devices.WiFi;
using System.Windows.Forms;
using System.Drawing;

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

            //initTray();
        }

        private NotifyIcon _notifyIcon = null;

        private void initTray()
        {
            this.Visibility = Visibility.Hidden;
            //设置托盘的各个属性
            _notifyIcon = new NotifyIcon();
            _notifyIcon.BalloonTipText = "服务运行中...";//托盘气泡显示内容
            _notifyIcon.Text = "ServerApp";
            _notifyIcon.Visible = true;//托盘按钮是否可见
            _notifyIcon.ShowBalloonTip(2000);//托盘气泡显示时间
        }
    }
}
