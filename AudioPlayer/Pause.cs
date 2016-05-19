using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace AudioPlayer
{
    class Pause : Command
    {
        public Pause()
        {
            nameStore = "pause";
        }

        public override bool preform(string[] args)
        {
            if (MediaPlayer.State == MediaState.Paused)
            {
                Console.WriteLine("Already paused");
            }
            else if (MediaPlayer.State == MediaState.Stopped)
            {
                Console.WriteLine("Nothing playing");
            }
            else
            {

                MediaPlayer.Pause();
                Console.WriteLine("Paused");
            }
            return true;
        }

    }

    class Resume : Command
    {
        public Resume()
        {
            nameStore = "resume";
        }

        public override bool preform(string[] args)
        {
            if (MediaPlayer.State == MediaState.Playing)
            {
                Console.WriteLine("Currently playing.  Nothing to resume.");
            }
            else if (MediaPlayer.State == MediaState.Stopped)
            {
                Console.WriteLine("No track is currently playing.  Nothing to resume.");
            }
            else
            {
                MediaPlayer.Resume();
                Console.WriteLine("Resumed");
            }
            return true;

        }

    }

}
