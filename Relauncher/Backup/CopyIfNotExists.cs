using System;
using System.Text;
using System.Diagnostics;
using System.IO;

namespace Relauncher
{
    class CopyIfNotExists : IAction
    {
        private String _exists_file;
        private String _source_file;
        private String _target_file;

        public int LoadParm(String parm, string[] args)
        {
            String[] parm_list;
            parm_list = parm.Split('|');

            _exists_file = parm_list[0];
            _source_file = parm_list[1];
            _target_file = parm_list[2];

            return 0;
        }

        public int Execute()
        {
            Console.WriteLine("Executing Copy If Not Exists Action.");

            if (!File.Exists(_exists_file))
            {
                Console.WriteLine("File {0} Does Not Exist, Copying {1} to {2} ... ", _exists_file, _source_file, _target_file);

                if (File.Exists(_source_file))
                {
                    Console.WriteLine("Copying {0} to {1} ... ", _source_file, _target_file);
                    File.Copy(_source_file, _target_file);
                }
                else {
                    Console.WriteLine("Source File {0} Does Not Exist. Copy Not Made.", _source_file);
                }

                
            }
            else
            {
                Console.WriteLine("File {0} Already Exists. Copy Not Made.", _exists_file);
            }

            return 0;
        }
    }
}
