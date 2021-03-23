using System;
using System.IO;

namespace Relauncher
{
    class CopyDirectory : IAction
    {
        private String _source_dir;
        private String _target_dir;
        private String _copy_sub_dir;

        public int LoadParm(String parm, string[] args)
        {
            String[] parm_list;
            parm_list = parm.Split('|');

            _source_dir = parm_list[0];
            _target_dir = parm_list[1];
            _copy_sub_dir = parm_list[2];

            return 0;
        }

        public int Execute()
        {
            Console.WriteLine("Executing Copy Directory Action.");

            DirectoryCopy(_source_dir, _target_dir, (_copy_sub_dir == "true"));

            return 0;
        }

        private static void DirectoryCopy(String source_dir, String target_dir, bool copy_sub_dir)
        {
            DirectoryInfo dir = new DirectoryInfo(source_dir);

            if (!dir.Exists)
            {
                Console.WriteLine("Source Directory {0} Does Not Exist. Copy Not Made.", source_dir);
            }
            else
            {
                DirectoryInfo[] dirs = dir.GetDirectories();
                if (!Directory.Exists(target_dir))
                {
                    Console.WriteLine("Target Directory {0} Does Not Exist. Creating ...", target_dir);
                    Directory.CreateDirectory(target_dir);
                }
                else
                {
                    Console.WriteLine("Target Directory {0} Already Exists.", target_dir);
                }

                FileInfo[] files = dir.GetFiles();
                foreach (FileInfo file in files)
                {
                    string temp_path = Path.Combine(target_dir, file.Name);

                    Console.WriteLine("Copying File {0}.", temp_path);

                    file.CopyTo(temp_path, true);
                }

                if (copy_sub_dir)
                {
                    foreach (DirectoryInfo subdir in dirs)
                    {
                        string temp_path = Path.Combine(target_dir, subdir.Name);

                        Console.WriteLine("Copying Directory {0}.", temp_path);

                        DirectoryCopy(subdir.FullName, temp_path, copy_sub_dir);
                    }
                }
            }
        }
    }
}
