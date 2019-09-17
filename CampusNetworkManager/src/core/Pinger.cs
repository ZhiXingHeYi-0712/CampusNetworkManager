using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace CampusNetworkAutoReconnector.src.core
{
    class Pinger
    {
        private int getAveragePing(string PingResult)
        {
            string result = Regex.Match(PingResult, @"= [0-9]*ms", RegexOptions.RightToLeft).Value;
            result = result.Replace("= ", "").Replace("ms", "");
            return int.Parse(result);
        }

        private int getLostRate(string PingResult)
        {
            string result = Regex.Match(PingResult, @"\([0-9]*%", RegexOptions.RightToLeft).Value;
            result = result.Replace("(", "").Replace("%", "");
            return int.Parse(result);
        }

        public bool isConnectionUnstable()
        {
            try
            {
                string PingResult = Cmd.RunCommand("ping www.baidu.com");
                if (getLostRate(PingResult) > 30)
                    return true;
                else if (getAveragePing(PingResult) > 200)
                    return true;
                else
                    return false;
            }
            catch
            {
                return true;
            }
        }
    }
}
