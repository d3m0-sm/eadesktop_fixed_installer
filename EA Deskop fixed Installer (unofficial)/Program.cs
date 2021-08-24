using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows;
using System.Runtime.InteropServices;

namespace EA_Deskop_fixed_Installer__unofficial_
{
    class Program
    {

        static void Main(string[] args)
        {
            tools eadt = new tools();
            
            try
            {
                eadt.Ping();
            }
            catch (Exception)
            {
                Console.WriteLine("Please Check your Internet connection and try again");
                Console.ReadKey();

            }

            var ping = eadt.Ping();

            if (ping == false)
            {
                Console.WriteLine("The EA Desktop download servers are currently not reachable!");
                Console.WriteLine("Please try again later");

                Console.ReadKey();
            }

            Console.WriteLine("Initialising...");
            string eadURL = "https://origin-a.akamaihd.net/EA-Desktop-Client-Download/installer-releases/";
            string installerURL;
            string installerName;
            string eadFilename = "EADesktopInstaller.exe";
            string eadSrc = eadURL + eadFilename;
            string temp = "C:\\Users\\" + Environment.UserName + "\\AppData\\Local\\Temp";
            string wadMSI;
            string[] tempdir;
            string[] xmlLines;
            string tempPath = string.Empty;
            string msiLine = string.Empty;
            string defaultDir = @"C:\Program Files\Electronic Arts\EA Desktop\EA Desktop";
            string installDir = string.Empty;
            string ny = string.Empty;
            
            eadt.eadkill();
            
            Thread.Sleep(1200);
            WebClient ead = new WebClient();
            if (File.Exists(eadFilename) == true)
            {
                File.Delete(eadFilename);
            }
            Thread.Sleep(1200);
            Console.WriteLine("Download EA Destop Installer...");
            ead.DownloadFile(eadSrc, eadFilename);
            Console.WriteLine("Download Finished");
            Console.WriteLine();
            Console.WriteLine("Starting Installer...");
            Process.Start(eadFilename.Replace(".exe", null));
            Thread.Sleep(1200);
            eadt.eadkill();
            Thread.Sleep(420);
            Console.WriteLine("Try to get newest EADesktop Release...");

            tempdir = Directory.GetDirectories(temp);
            

            for (int i = 0; i < tempdir.Length; i++)
            {
                if (Directory.Exists(tempdir[i] + "\\.ba"))
                {
                    tempPath = tempdir[i];
                }
                
            }

            xmlLines = File.ReadAllLines(tempPath + "\\.ba\\BootstrapperApplicationData.xml");

            for (int i = 0; i < xmlLines.Length; i++)
            {
                if (xmlLines[i].ToString().Contains("Payload=\"EADesktop.msi\""))
                {
                    
                    msiLine = xmlLines[i];
                }
            }

            int startInt = msiLine.IndexOf("DownloadUrl");
            msiLine = msiLine.Substring(startInt).Replace("DownloadUrl=\"", null);
            startInt = msiLine.IndexOf("\"");
            installerURL = msiLine.Remove(startInt);
            startInt = installerURL.LastIndexOf("/");
            installerName = installerURL.Substring(startInt).Replace("/", null);
            installerURL = installerURL.Remove(startInt);
            installerURL = installerURL + "/";

            Console.WriteLine("Downloading newest release, please wait...");

            ead.DownloadFile(installerURL + installerName, installerName);

            Console.WriteLine("Download finished");

            Console.WriteLine();
            Console.WriteLine("Starting installer...");
            Console.WriteLine();
            Console.WriteLine("Please go trough the EA Desktop installation setup and come back if its finished!");
            Process.Start(installerName.Replace(".exe", null));

            Console.WriteLine();
            Console.WriteLine("If the setup is finished, please press return...");
            Console.ReadLine();
            
            if (Directory.Exists(defaultDir) == true)
            {
                installDir = defaultDir;
            }
            else
            {
                Console.WriteLine("EA Desktop isnt't installed at the default location.");
                Console.WriteLine("Do you use a custom instalation location? (y/n)");
                ny = Console.ReadLine();
                do
                {
                    ny = Console.ReadLine();
                } while (ny != "y" || ny != "n");

                Console.WriteLine();

                if (ny == "y")
                {
                    
                    Console.WriteLine("Please copy paste your EA Desktop installation Path");
                    installDir = Console.ReadLine();
                }
                else if (ny == "n")
                {
                    Console.WriteLine("EA Desktop installation not found, please try again!");
                    Console.ReadLine();
                    Environment.Exit(0);
                }
                else
                {
                    Console.WriteLine("Error");
                    Console.ReadLine();
                    Environment.Exit(0);
                }
            }

            File.Delete(@"C:\ProgramData\Microsoft\Windows\Start Menu\Programs\EA\EA Desktop");
            tools.CreateShortcut("EA Desktop", @"C:\ProgramData\Microsoft\Windows\Start Menu\Programs\EA", installDir + @"\EADesktop.exe");
            Console.WriteLine("Fixed Start Menu entry");
            Console.WriteLine();

            ny = string.Empty;
            if (installDir != string.Empty)
            {
                Console.WriteLine("Do you want to create a Desktop shortcut for EA Desktop? (y/n)");
                do
                {
                    ny = Console.ReadLine();
                } while (ny != "y" && ny != "n");

                if (ny == "y")
                {
                    tools.CreateShortcut("EA Desktop", @"C:\Users\" + Environment.UserName + @"\Desktop", installDir + @"\EADesktop.exe");
                    Console.WriteLine("Shortcut created");
                }
                else
                {
                    return;
                }
            }




            Console.ReadKey();


        }
    }
}
