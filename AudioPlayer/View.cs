using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AudioPlayer.Storage;

namespace AudioPlayer
{
    class View : Command
    {
        public View()
        {
            nameStore = "view";
            
        }

        public override bool preform(string[] args)
        {
            Console.WriteLine("------------------------------------Play Lists------------------------------------\n");
            foreach (PlayList pl in MainClass.library.playLists)
            {
                Console.WriteLine(pl.name + "           " + pl.trackCollenction.Count + " tracks\n");
            }
            Console.WriteLine("------------------------------------Tracks in this Play List------------------------------------\n");
            foreach (Track tr in MainClass.currentlyViewedPlayList.tracks)
            {
                Console.WriteLine(tr.name + "           " + "weight: " + MainClass.currentlyViewedPlayList.getWeight(tr) + "\n");
            }
            return true;
        }

        public override string[] getAliases()
        {
            string[] aliases = new string[2];
            aliases[0] = "vw";
            aliases[1] = "v";
            return aliases;
        }
    }
}
