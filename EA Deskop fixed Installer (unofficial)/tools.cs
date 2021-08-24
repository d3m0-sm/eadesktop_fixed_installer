using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Net.NetworkInformation;

namespace EA_Deskop_fixed_Installer__unofficial_
{
    class tools
    {

        private string eadName = "EADesktopInstaller";

        public void eadkill()
        {
            foreach (var process in Process.GetProcessesByName("EADesktopInstaller"))
            {
                process.Kill();
            }
        }

        public bool Ping()
        {
            bool success;
            Ping ping = new Ping();
            PingOptions options = new PingOptions();

            options.DontFragment = true;

            string data = "test_ping";
            byte[] buffer = Encoding.UTF8.GetBytes(data);
            int timeout = 120;
            PingReply replay = ping.Send("origin-a.akamaihd.net", timeout, buffer, options);
            if (replay.Status == IPStatus.Success)
            {
                success = true;
            }
            else
            {
                success = false;
            }
            return success;
        }
    
    }
}
