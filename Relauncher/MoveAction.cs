using System;
using System.Text;
using System.Diagnostics;
using System.IO;

namespace Relauncher
{
    class MoveAction : IAction
    {
        private String _source;
        private String _target;

        public int LoadParm(String parm, string[] args)
        {
            String[] parm_list;
            parm_list = parm.Split('|');

            _source = parm_list[0];
            _target = parm_list[1];

            return 0;
        }

        public int Execute()
        {
            Console.WriteLine("Executing Move Action.");

            if (File.Exists(_target))
            {
                Console.WriteLine("File {0} Already Exists. File Will Be Overwritten.", _target);
                File.Delete(_target);
            }

            File.Move(_source, _target);

            return 0;
        }
    }
}
