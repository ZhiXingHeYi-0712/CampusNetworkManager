using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SimpleWifi;
using SimpleWifi.Win32;

namespace CampusNetworkManager.src.core
{
    class WifiController
    {
        
        Wifi wifi = new Wifi();

        private void disConnect()
        {
            wifi.Disconnect();
        }

        private void connect()
        {
            Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            string[] ssids = configuration.AppSettings.Settings["SSID"].Value.Split('&');
            if (ssids.Length == 0)
            {
                ssids ="None&Null".Split('&');
            }

            AccessPoint accessPoint = wifi.GetAccessPoints().Find(
                delegate(AccessPoint ap)
                {
                    AuthRequest authRequest = new AuthRequest(ap);  // 防止那个开热点叫BNUZ的缺德仔

                    foreach (string ssid in ssids)
                    {
                        if (ap.Name == ssid && !authRequest.IsPasswordRequired)
                        {
                            return true;
                        }
                    }
                    return false;
                });

            if (accessPoint != null)
            {
                AuthRequest auth = new AuthRequest(accessPoint);
                accessPoint.Connect(auth);
            }
        }

        public void reconnect()
        {
            disConnect();
            Thread.Sleep(2000);
            connect();
        }
    }
}
