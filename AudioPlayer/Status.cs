using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AudioPlayer
{
    class Status : Command
    {
        public Status()
        {
            nameStore = "status";
        }

        public override bool preform(string[] args)
        {
            if (MainClass.currentTrack != null)
            {
                Console.WriteLine("Currently playing \"" + MainClass.currentTrack.name + "\"\n");

                Console.Write("Current time: " + MainClass.currentTime +"/" + MainClass.songTime.ToString().Substring(0, 8) + "  ");

                int numberOfBars = (int) ((MainClass.currentTime.TotalMilliseconds / MainClass.songTime.TotalMilliseconds) * 20);

                for (int i = 1; i <= 20; i++)
                {
                    if (i <= numberOfBars)
                    {
                        Console.Write("|");
                    }
                    else
                    {
                        Console.Write(":");
                    }
                }

                Console.WriteLine("\n");

                Console.WriteLine("Track's file location: \"" + MainClass.currentTrack.fileLocation + "\"\n");

                Console.WriteLine("Volume: " + MainClass.currentTrack.volume + "        Referance: " + MainClass.currentTrack.isReferance + "\n");

                if (MainClass.nextTrack != null)
                {
                    Console.WriteLine("The track that will be played after this is \"" + MainClass.nextTrack.name + "\"\n");
                    Console.WriteLine("The track that will be played after that is \"" + MainClass.trackAfterNext.name + "\"");
                }
                else
                {
                    Console.WriteLine("Since the playmode is set to stop, no track will be played after this one");
                }
            }
            else
            {
                Console.WriteLine("Not currently playing any track");
            }
            return true;
        }
    }
}
