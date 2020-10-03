using System.Diagnostics;

namespace TraceMap.Integration.Tracert.Helpers
{
    public static class CmdHelper
    {
        public static string Cmd(this string command)
        {
            var cmd = new Process
            {
                StartInfo =
                {
                    FileName = "cmd.exe",
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true,
                    UseShellExecute = false
                }
            };
            cmd.Start();
            cmd.StandardInput.WriteLine(command);
            cmd.StandardInput.Flush();
            cmd.StandardInput.Close();
            cmd.WaitForExit();
            var response = cmd.StandardOutput.ReadToEnd();

            return response;
        }
    }
}
