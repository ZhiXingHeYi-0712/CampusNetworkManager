using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;

namespace CampusNetworkManager.src.core
{
    // 我哭了，终于整好了
    class Toast
    {
        public static void showNotification(string text)
        {
            // Get a toast XML template
            XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastImageAndText03);

            // Fill in the text elements
            XmlNodeList stringElements = toastXml.GetElementsByTagName("text");

            stringElements[0].AppendChild(toastXml.CreateTextNode("校园网断线重连"));
            stringElements[1].AppendChild(toastXml.CreateTextNode(text));

            // Specify the absolute path to an image
            String imagePath = "file:///" + Path.GetFullPath("toastImageAndText.png");
            XmlNodeList imageElements = toastXml.GetElementsByTagName("image");
            imageElements[0].Attributes.GetNamedItem("src").NodeValue = imagePath;

            // Create the toast and attach event listeners
            ToastNotification toast = new ToastNotification(toastXml);

            // Show the toast. Be sure to specify the AppUserModelId on your application's shortcut!
            ToastNotificationManager.CreateToastNotifier("校园网断线重连").Show(toast);
        }

        public static void sayReconnect()
        {
            showNotification("网络不稳定，正在自动重连……");
        }

        public static void sayCheck()
        {
            showNotification("多次重连仍然不稳定，请手动检查");
        }

        public static void sayConnectSuccess()
        {
            showNotification("重连成功！");
        }
    }
}
