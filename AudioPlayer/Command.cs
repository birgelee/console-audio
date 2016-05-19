using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AudioPlayer
{
    class Command
    {
        protected string[] aliases = new string[0];
        protected string helpInfoStore;
        public string helpInfo
        {
            get
            {
                return helpInfoStore;
            }
        }
        protected string nameStore;
        public Command()
        {
            nameStore = "null command";
            helpInfoStore = "There is no help for this command";
        }


        public string name
        {
            get
            {
                return nameStore;
            }
        }

        public virtual bool preform(string[] args)
        {
            Console.WriteLine("null command");
            return false;
        }

        public virtual string[] getAliases()
        {
            return aliases;
        }

    }
}
