using System;
using System.Text;
using System.Diagnostics;
using System.IO;

namespace Relauncher
{
    class StartAction : IAction
    {
        private String _program_name;
        private String _program_args;

        public int LoadParm(String parm, string[] args)
        {
            String[] parm_list;
            parm_list = parm.Split('|');

            _program_name = parm_list[0];
            _program_args = parm_list[1];

            return 0;

        }

        public int Execute()
        {
            int ExitCode;
            ProcessStartInfo ProcessInfo;
            Process Process;

            Console.WriteLine("Executing Start Action.");

            ProcessInfo = new ProcessStartInfo("cmd.exe", String.Concat("/c ", _program_name, " ", _program_args));
            ProcessInfo.CreateNoWindow = true;
            ProcessInfo.UseShellExecute = false;

            Process = Process.Start(ProcessInfo);
            Process.WaitForExit();

            ExitCode = Process.ExitCode;
            Process.Close();

            Console.WriteLine("Executing Start Action. Done. Exit Code = {0}", ExitCode);

            return 0;
        }
    }
}
