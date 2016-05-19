using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AudioPlayer
{
    class Reshuffle : Command
    {
        public Reshuffle()
        {
            nameStore = "reshuffle";
        }

        public override bool preform(string[] args)
        {
            if (args.Length == 1)
            {
                Utils.Utils.regenerateNextTracks(MainClass.currentTrack);
                Console.WriteLine("All of the upcomming tracks have been regenerated\n");
                Console.WriteLine("The track that will be played after this is \"" + MainClass.nextTrack.name + "\"\n");
                Console.WriteLine("The track that will be played after that is \"" + MainClass.trackAfterNext.name + "\"");
            }
             else if (args[1].Equals("1") || args[1].Equals("next"))
            {
                MainClass.nextTrack = Utils.Utils.getNextTrack(MainClass.currentTrack);
                Console.WriteLine("The next track has been regenerated\n");
                Console.WriteLine("The track that will be played after this is \"" + MainClass.nextTrack.name + "\"");
            }
            else if (args[1].Equals("2") || args[1].Equals("after-next"))
            {
                MainClass.trackAfterNext = Utils.Utils.getNextTrack(MainClass.nextTrack);
                Console.WriteLine("The track after next has been regenerated\n");
                Console.WriteLine("The track that will be played after the next track is \"" + MainClass.trackAfterNext.name + "\"");
            }
            return true;
        }

        public override string[] getAliases()
        {
            string[] aliases = new string[3];
            aliases[0] = "resh";
            aliases[1] = "rs";
            aliases[2] = "res";
            return aliases;
        }

    }
}
