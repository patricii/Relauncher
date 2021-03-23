using System;
using System.Text;
using System.Diagnostics;
using System.IO;

namespace Relauncher
{
    class RenameAction : IAction
    {
        private String _path;
        private String _old;
        private String _new;

        public int LoadParm(String parm, string[] args)
        {
            String[] parm_list;
            parm_list = parm.Split('|');

            _path = parm_list[0];
            _old = parm_list[1];
            _new = parm_list[2];

            return 0;
        }

        public int Execute()
        {
            Console.WriteLine("Executing Rename Action.");

            File.Move(String.Concat(_path, @"\", _old), String.Concat(_path, @"\", _new));

            return 0;
        }
    }
}
