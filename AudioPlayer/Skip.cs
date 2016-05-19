using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AudioPlayer
{
    class Skip : Command
    {
        public Skip()
        {
            nameStore = "skip";
        }

        public override bool preform(string[] args)
        {
            Utils.Utils.playNextTrack();
            Console.WriteLine("The current track has been skipped.  Now playing \"" + MainClass.currentTrack.name + "\"");
            return true;
        }

        public override string[] getAliases()
        {
            string[] aliases = new string[1];
            aliases[0] = "sk";
            return aliases;
        }
    }
}
