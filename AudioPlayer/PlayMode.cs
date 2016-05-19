using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AudioPlayer
{
    class PlayMode : Command
    {
        public PlayMode()
        {
            nameStore = "playmode";
            helpInfoStore = "Set playmode to either shuffle (s) or loop (l). Example: playmode shuffle";
        }

        public override bool preform(string[] args)
        {
            if (args.Length < 2)
                return false;
            string mode = args[1].ToLower();
            if (mode.Equals("shuffle") || mode.Equals("s"))
            {
                if (MainClass.playMode != SongState.Shuffle)
                {
                    MainClass.playMode = SongState.Shuffle;
                    Utils.Utils.regenerateNextTracks(MainClass.currentTrack);
                }
            }

            else if (mode.Equals("loop") || mode.Equals("l"))
            {
                if (MainClass.playMode != SongState.Loop)
                {
                    MainClass.playMode = SongState.Loop;
                    Utils.Utils.regenerateNextTracks(MainClass.currentTrack);
                }
            }
            else if (mode.Equals("stop") || mode.Equals("st"))
            {
                if (MainClass.playMode != SongState.Stop)
                {
                    MainClass.playMode = SongState.Stop;
                    Utils.Utils.regenerateNextTracks(MainClass.currentTrack);
                }
            }
            else if (mode.Equals("play-next") || mode.Equals("pn"))
            {
                if (MainClass.playMode != SongState.PlayNext)
                {
                    MainClass.playMode = SongState.PlayNext;
                    Utils.Utils.regenerateNextTracks(MainClass.currentTrack);
                }
            }
            else
                return false;
            Console.WriteLine("Play mode set to " + mode);
            return true;
        }

        public override string[] getAliases()
        {
            string[] aliases = new string[1];
            aliases[0] = "pm";
            return aliases;
        }
    }
}
