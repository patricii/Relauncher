using System;
using System.Text;
using System.Diagnostics;
using System.Threading;

namespace Relauncher
{
    class DelayAction : IAction
    {
        private Int32 _milliseconds;

        public int LoadParm(String parm, string[] args)
        {
            String[] parm_list;
            parm_list = parm.Split('|');

            _milliseconds = Convert.ToInt32(parm_list[0]);

            return 0;
        }

        public int Execute()
        {

            Console.WriteLine("Executing Delay Action.");

            Thread.Sleep(_milliseconds);

            return 0;
        }
    }
}
