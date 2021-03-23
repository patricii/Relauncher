using System;
using System.Text;
using System.Diagnostics;
using System.IO;

namespace Relauncher
{
    class CopyFile : IAction
    {
        private String _source_dir;
        private String _file_pattern;
        private String _target_dir;

        public int LoadParm(String parm, string[] args)
        {
            String[] parm_list;
            parm_list = parm.Split('|');

            _source_dir = parm_list[0];
            _file_pattern = parm_list[1];
            _target_dir = parm_list[2];

            return 0;
        }

        public int Execute()
        {
            Console.WriteLine("Executing Copy File Action.");

            String[] file_list = Directory.GetFiles(_source_dir, _file_pattern);
            String target_file = "";

            foreach (String f in file_list)
            {
                target_file = _target_dir + "\\" + Path.GetFileName(f);

                Console.WriteLine("Copying File {0} to {1}.", f, target_file);

                File.Copy(f, target_file, true);
            }

            return 0;
        }
    }
}
