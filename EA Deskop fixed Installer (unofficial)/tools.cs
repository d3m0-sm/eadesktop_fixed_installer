using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Net.NetworkInformation;
using IWshRuntimeLibrary;

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

        public static void CreateShortcut(string shortcutName, string shortcutPath, string targetFileLocation)
        {
            string shortcutLocation = System.IO.Path.Combine(shortcutPath, shortcutName + ".lnk");
            WshShell shell = new WshShell();
            IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutLocation);

            //shortcut.Description = "My shortcut description";   // The description of the shortcut
            //shortcut.IconLocation = @"c:\myicon.ico";           // The icon of the shortcut
            shortcut.TargetPath = targetFileLocation;                 // The path of the file that will launch when the shortcut is run
            shortcut.Save();                                    // Save the shortcut
        }

    }
}
