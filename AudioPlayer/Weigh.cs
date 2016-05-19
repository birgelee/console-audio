using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AudioPlayer
{
    class Weigh : Command
    {
        public Weigh()
        {
            nameStore = "weigh";
            
        }

        public override bool preform(string[] args)
        {
            if (args.Length < 3)
                return false;
            //if (!Double.Parse(args[2]).ToString().Equals(Double.Parse(args[2])))
            //    return false;
            MainClass.currentlyViewedPlayList.setWeight(MainClass.currentlyViewedPlayList.getTrack(args[1]), Double.Parse(args[2]));
            MainClass.library.saveToFile();
            return true;
        }
        public override string[] getAliases()
        {
            string[] al = new string[3];
            al[0] = "w";
            al[1] = "setw";
            al[2] = "setweight";

            return al;
        }
    }
}
