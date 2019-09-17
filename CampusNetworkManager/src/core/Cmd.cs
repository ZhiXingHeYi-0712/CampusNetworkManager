using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace CampusNetworkAutoReconnector.src.core
{
    public class Cmd
    {
        private static string CmdPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.System) + "\\cmd.exe";

        public static string RunCommand(string command)
        {
            using (Process process = new Process())
            {
                command = command.Trim().TrimEnd('&') + "&exit";

                process.StartInfo.FileName = CmdPath;
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.RedirectStandardInput = true;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.UseShellExecute = false;

                process.Start();

                process.StandardInput.WriteLine(command);
                process.StandardInput.AutoFlush = true;
                string output = process.StandardOutput.ReadToEnd();

                process.WaitForExit();
                process.Close();
                output = output.Replace("\n", "");
                return output;
            }
        }
    }
}
