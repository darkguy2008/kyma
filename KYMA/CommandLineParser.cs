using System.Collections.Generic;
using System.Linq;

namespace KYMA
{
    public class CommandLineParser
    {
        private Dictionary<string, string> _cmd = new Dictionary<string, string>();

        public CommandLineParser(string[] args)
        {
            string lastKey = string.Empty;
            for (int i = 0; i < args.Length; i++)
            {
                string arg = args[i];
                if (arg.StartsWith("\"")) { arg = arg.Substring(1); }
                if (arg.EndsWith("\"")) { arg = arg.Substring(0, arg.Length - 1); }
                if (arg.StartsWith("-"))
                {
                    lastKey = arg.Substring(1);
                    _cmd.Add(lastKey, null);
                }
                else
                {
                    _cmd[lastKey] = arg;
                }
            }
        }

        public bool ContainsArg(string key)
        {
            return _cmd.ContainsKey(key);
        }
        public int Length { get { return _cmd.Count; } }

        public string this[string key]
        {
            get
            {
                return _cmd[key];
            }
            set
            {
                _cmd[key] = value;
            }
        }

        public string this[int index]
        {
            get
            {
                return _cmd.ToList()[index].Value;
            }
        }
    }
}