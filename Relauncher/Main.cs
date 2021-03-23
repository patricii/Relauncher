using System;
using System.Text;
using System.Xml;

namespace Relauncher
{
    class Main
    {
        string[] _args;

        public void Start(string[] args)
        {
            _args = args;

            Console.WriteLine("Started.");

            ProcessXML();

            Console.WriteLine("Finished.");

        }

        private void ProcessXML() 
        {
            XmlDocument xml_file = new XmlDocument();
            XmlNodeList node_list;
            XmlNode root;
            XmlNode program_node;
            IAction action;
            String stationName = "";
            String siteName = "";

            int i = 0;

            Console.WriteLine("Processing XML File ... ");

            for (i = 0; i < _args.Length; i++)
            {
                if (_args[i].Contains("/stationName:"))
                {
                    stationName = _args[i].Split(':')[1];
         
                    Console.WriteLine("Found /stationName {0}", stationName);
                }

                if (_args[i].Contains("/dataSourceNameEquipment:"))
                {
                    siteName = _args[i].Split(':')[1].Split('_')[3]; 
                    
                    Console.WriteLine("Found /siteName {0}", siteName);
                }

            }
 
            xml_file.Load(@"Actions.xml");

            root = xml_file.DocumentElement;

           // node_list = root.SelectNodes(String.Concat("/relauncher/actions[translate(@stationName, 'abcdefghijklmnopqrstuvwxyz', 'ABCDEFGHIJKLMNOPQRSTUVWXYZ')='", stationName.ToUpper(), "' and string-length(@productName)!=0]"));           
            
            node_list = root.SelectNodes(String.Concat("/relauncher/actions[translate(@stationName,'abcdefghijklmnopqrstuvwxyz','ABCDEFGHIJKLMNOPQRSTUVWXYZ')= '", stationName.ToUpper(), "' and translate(@siteName,'abcdeghijklmnopqrstuvwxyz','ABCDEFGHIJKLMNOPQRSTUVWXYZ')= '", siteName.ToUpper(), "' and string-length(@productName)!=0]"));

           
            Console.WriteLine("{0} <actions> Node(s) Found.", node_list.Count);

            if (node_list.Count <= 0)
            {
                Console.WriteLine("<actions> Node(s) Not Found.");
                return;
            }

            program_node = node_list.Item(0);

            if (!program_node.HasChildNodes)
            {
                Console.WriteLine("Nothing To Do.");
                return;
            }
            else
            {
                Console.WriteLine("Found {0} Action Items.", program_node.ChildNodes.Count);
            }

            i = 0;
            foreach (XmlNode xn in program_node.ChildNodes)
            {
                if (xn.NodeType != XmlNodeType.Comment)
                {
                    Console.WriteLine("Action Item ({0}) = {1}", i, xn.Attributes["name"].Value);

                    action = CreateAction(xn.Attributes["name"].Value);
                    action.LoadParm(xn.Attributes["parm"].Value, _args);

                    Console.WriteLine("Execute  = ({0})", action.Execute());
                }
                else
                {
                    Console.WriteLine("Action Item ({0}) Is Commented Out.", i);
                }

                i++;
            }

            action = null;
            root = null;
            program_node = null;
            node_list = null;
            xml_file = null;

            Console.WriteLine("Processing XML File ... Done. ");

            return;
        }

        private IAction CreateAction(String action_name)
        {
            IAction action;

            switch (action_name)
            {
                case "delete":
                    action = new DeleteAction();
                    break;
                case "setting":
                    action = new SettingAction();
                    break;
                case "delay":
                    action = new DelayAction();
                    break;
                case "move":
                    action = new MoveAction();
                    break;
                case "rename":
                    action = new RenameAction();
                    break;
                case "start":
                    action = new StartAction();
                    break;
                case "copy_if_not_exists":
                    action = new CopyIfNotExists();
                    break;
                case "copy_file":
                    action = new CopyFile();
                    break;
                case "copy_directory":
                    action = new CopyDirectory();
                    break;
                case "copy_single_file":
                    action = new CopySingleFile();
                    break;
                default:
                    action = null;
                    break;
            }

            return action;
        }

    }
}
