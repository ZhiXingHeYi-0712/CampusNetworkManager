using CampusNetworkAutoReconnector.src.core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CampusNetworkManager.src.core
{
    class CoreMain
    {
        public void runCore()
        {
            Toast.showNotification("开始运行");

            int unstableTime = 0;
            Pinger pinger = new Pinger();
            WifiController wifiController = new WifiController();

            while (true)
            {
                if (pinger.isConnectionUnstable())
                {
                    try
                    {
                        if (unstableTime == 0 || unstableTime == 2)
                        {
                            Toast.sayReconnect();
                            wifiController.reconnect();
                        }
                        if (unstableTime > 3)
                        {
                            Toast.sayCheck();
                            Thread.Sleep(10000);
                        }
                    }
                    finally
                    {
                        unstableTime++;
                        Thread.Sleep(5000);
                    }
                }
                else
                {
                    if(unstableTime == 0)
                    {
                        Thread.Sleep(30000);
                    }
                    else
                    {
                        Toast.sayConnectSuccess();
                        unstableTime = 0;
                        Thread.Sleep(15000);
                    }
                }
            }
        }
    }
}
