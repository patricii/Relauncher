using System;
using System.Text;
using System.Diagnostics;
using System.IO;

namespace Relauncher
{
    class CopySingleFile : IAction
    {
        private String _source_file;
        private String _target_file;

        public int LoadParm(String parm, string[] args)
        {
            String[] parm_list;
            parm_list = parm.Split('|');

            _source_file = parm_list[0];
            _target_file = parm_list[1];

            return 0;
        }

        public int Execute()
        {
            Console.WriteLine("Executing Copy Single File Action.");

            if (File.Exists(_source_file))
            {
                if (!File.Exists(_target_file))
                {
                    Console.WriteLine("Copying {0} to {1} ... ", _source_file, _target_file);
                    File.Copy(_source_file, _target_file);
                }
                else
                {
                    if (_source_file == _target_file)
                    {
                        Console.WriteLine("Source File and Target File Are Equal. Copy Not Made.");
                    }
                    else
                    {
                        Console.WriteLine("File {0} Already Exists. Overwriting.", _target_file);
                    }

                }
            }
            else
            {
                Console.WriteLine("Source File {0} Does Not Exist. Copy Not Made.", _source_file);
            }

            return 0;
        }
    }
}
