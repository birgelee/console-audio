using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AudioPlayer
{
    class Params
    {
        private List<string> flagsStore = new List<string>();
        public string[] flags
        {
            get
            {
                return flagsStore.ToArray();
            }
        }

        private List<string> argsStore = new List<string>();
        public string[] args
        {
            get
            {
                return argsStore.ToArray();
            }
        }

        public Params(string[] paramiters)
        {
            var firstTime = true;
            foreach (string param in paramiters)
            {
                if (firstTime)
                {
                    firstTime = false;
                    continue;
                }
                if (param.StartsWith("-"))
                {
                    flagsStore.Add(param);
                }
                else if (!param.Equals(""))
                {
                    argsStore.Add(param);
                }
            }
        }

        public bool hasFlag(params string[] flag)
        {
            foreach (string tempFlag in flags)
            {
                if (tempFlag.Equals(flag))
                    return true;
            }
            return false;
            
        }

        public override string ToString()
        {
            var returnstring = "";
            returnstring += "Args: ";
            foreach (string arg in args)
            {
                returnstring += arg + " ";
            }
            returnstring += "     flags: ";
            foreach (string flg in flags)
            {
                returnstring += flg + " ";
            }
            return returnstring;
        }

    }
}
