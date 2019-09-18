using CampusNetworkManager.src.core;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;

namespace CampusNetworkManager
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : System.Windows.Application
    {
        private static NotifyIcon trayIcon;

        private void ApplicationStartup(object sender, StartupEventArgs e)
        {
            RemoveTrayIcon();
            AddTrayIcon();
        }

        private void AddTrayIcon()
        {
            if (trayIcon != null)
            {
                return;
            }
            trayIcon = new NotifyIcon
            {
                Icon = new System.Drawing.Icon(AppDomain.CurrentDomain.BaseDirectory+"\\resource\\resource.ico"),
                Text = "校园网断线重连"
            };
            trayIcon.Visible = true;

            ContextMenu menu = new ContextMenu();

            MenuItem closeItem = new MenuItem();
            closeItem.Text = "退出";
            closeItem.Click += new EventHandler(delegate { this.Shutdown(); });

            MenuItem settings = new MenuItem();
            settings.Text = "设置";
            settings.Click += new System.EventHandler(this.showMainWindow);

            menu.MenuItems.Add(settings);
            menu.MenuItems.Add(closeItem);

            trayIcon.ContextMenu = menu;    //设置NotifyIcon的右键弹出菜单
        }

        private void RemoveTrayIcon()
        {
            if (trayIcon != null)
            {
                trayIcon.Visible = false;
                trayIcon.Dispose();
                trayIcon = null;
            }
        }

        private void ApplicationExit(object sender, ExitEventArgs e)
        {
            RemoveTrayIcon();
        }

        private void showMainWindow(object sender, System.EventArgs e)
        {
            MainWindow.Visibility = Visibility.Visible;
        }
    }
}
