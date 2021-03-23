using System;
using System.Text;
using System.Diagnostics;
using System.IO;

namespace Relauncher
{
    class SettingAction : IAction
    {
        private String _section;
        private String _key;
        private String _value;

        private String _ini_file;

        private String GetValueFromMap(String value, String xml_value_map)
        {
            String new_value;
            String[] map_list;
            String from_value;
            String to_value;

            new_value = value;
            map_list = xml_value_map.Split(';');
            for (int i = 0; i < map_list.Length; i++ )
            {
                if (map_list[i].Contains("="))
                {
                    from_value = map_list[i].Split('=')[0];
                    to_value = map_list[i].Split('=')[1];
                    if (from_value == value)
                    {
                        new_value = to_value;
                    }
                }
            }

            return new_value;
        }

        public int LoadParm(String parm, string[] args)
        {
            String[] parm_list;
            String xml_key = "";
            String xml_source_key = "";
            String xml_value = "";
            String xml_value_map = "";

            parm_list = parm.Split('|');

            _ini_file = parm_list[0];
            _section = parm_list[1];

            xml_key = parm_list[2];
            xml_value = parm_list[3];
            xml_value_map = parm_list[4];

            if (xml_key.Contains("="))
            {
                xml_source_key = xml_key.Split('=')[0];
                _key = xml_key.Split('=')[1];
            }
            else
            {
                xml_source_key = xml_key;
                _key = xml_key;
            }


            if (xml_value == "$ARG$")
            {
                for (int i = 0; i < args.Length; i++)
                {
                    if (args[i].Contains("/" + xml_source_key + ":"))
                    {
                        _value = GetValueFromMap(args[i].Split(':')[1], xml_value_map);

                        Console.WriteLine("Found New Setting Value - /{0}:{1}", xml_source_key, _value);
                    }
                }
            }
            else
            {
                _value = xml_value;
            }

            return 0;
        }

        public int Execute()
        {
            Console.WriteLine("Executing Setting Action.");
            IniFile ini_file = new IniFile(_ini_file);

            ini_file.Write(_key, _value.ToUpper(), _section);

            return 0;
        }
    }
}
