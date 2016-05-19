using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Media;

namespace AudioPlayer
{
    class Stop : Command
    {
        public Stop()
        {
            nameStore = "stop";
            helpInfoStore = "Stops the currently playing track";
        }

        public override bool preform(string[] args)
        {
            if (MainClass.currentSong == null)
            {
                Console.WriteLine("Nothing to stop");
            }
            else
            {
                MediaPlayer.Stop();
                MainClass.currentSong = null;
                MainClass.currentTrack = null;
            }
            return true;
            
        }
    }
}
