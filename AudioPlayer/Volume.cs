using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AudioPlayer.Storage;

namespace AudioPlayer
{
    class Volume : Command
    {
        public Volume()
        {
            nameStore = "volume";
            aliases = new string[1];
            aliases[0] = "vol";
        }

        public override bool preform(string[] args)
        {
            if (args.Length < 3)
                return false;
            double volume = Double.Parse(args[2]);

            if (volume > 100 || volume < 0)
            {
                Console.WriteLine("No track can have a volume that is greater than 100 or less than 0");
                return false;
            }

            Track tr = MainClass.currentlyViewedPlayList.getTrack(args[1]);
            if (tr != null)
            {
                tr.volume = volume;
            }
            else
            {
                return false;
            }
            Console.WriteLine("The volume of \"" + tr.name + "\" has been set to " + volume);
            MainClass.library.saveToFile();
            return true;
        }
    }
}
