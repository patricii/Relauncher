using System;
using System.Text;
using System.IO;

namespace Relauncher
{
    class DeleteAction : IAction
    {
        private String _path;
        private String _name;

        public int LoadParm(String parm, string[] args)
        {
            String[] parm_list;
            parm_list = parm.Split('|');

            _path = parm_list[0];
            _name = parm_list[1];

            return 0;
        }

        public int Execute()
        {
            String[] file_list = Directory.GetFiles(_path, _name);

            Console.WriteLine("Executing Delete Action.");

            foreach (String f in file_list)
            {
                Console.WriteLine("Delete File {0}", f);
                File.Delete(f);
            }

            return 0;
        }
    }
}
