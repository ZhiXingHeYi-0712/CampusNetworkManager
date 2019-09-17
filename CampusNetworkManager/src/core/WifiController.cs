using System;
using System.Collections.Generic;
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
            AccessPoint accessPoint = wifi.GetAccessPoints().Find(
                delegate(AccessPoint ap)
                {
                    AuthRequest authRequest = new AuthRequest(ap);  // 防止那个开热点叫BNUZ的缺德仔
                   
                    return ap.Name == "BNUZ-Student" && !authRequest.IsPasswordRequired;
                });

            AuthRequest auth = new AuthRequest(accessPoint);
            accessPoint.Connect(auth);
        }

        public void reconnect()
        {
            disConnect();
            Thread.Sleep(2000);
            connect();
        }
    }
}
