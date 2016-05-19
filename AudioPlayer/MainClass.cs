using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;
using AudioPlayer.Storage;
using Microsoft.Xna.Framework.Media;
using System.IO;
using System.Windows.Forms;
using System.Threading;

namespace AudioPlayer
{
    class MainClass
    {
        public static bool playNext;
        public static TimeSpan songTime;
        public static TimeSpan currentTime;
        public static SongState playMode = SongState.Shuffle;
        private static Thread updateThread;
        public static Library library;
        public static PlayList currentPlayList;
        public static PlayList currentlyViewedPlayList;
        public static Track currentTrack;
        public static Track nextTrack;
        public static Track trackAfterNext
        {
            get;
            set;
        }
        public static Track[] history = new Track[0];
        public static Song currentSong;
        public static bool queingTrack = false;

        public static void Main()
        {
            Microsoft.Xna.Framework.FrameworkDispatcher.Update();
            CommandList cl = new CommandList();
            library = Library.loadFromFile("Defualt.store");
            currentPlayList = library.playLists[0];
            currentlyViewedPlayList = currentPlayList;
            if (!Directory.Exists(Directory.GetCurrentDirectory() + "\\Defualt-songs"))
                Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\Defualt-songs");
            updateThread = new Thread(new ThreadStart(Update.onSongChange));
            updateThread.Start(); 
            Utils.Utils.regenerateNextTracks(null);
            Console.Write(library.name + ":" + currentlyViewedPlayList.name + ">");
            string input = Console.ReadLine();

            while (!(input.ToLower().Equals("quit") | input.ToLower().Equals("q")))
            {
                if (input.Equals("?") || input.Equals("help"))
                {
                    cl.printCommands();
                }
                else
                {
                    string[] inputs = Utils.Utils.commandParser(input);

                    if (cl[inputs[0].ToLower()] != null)
                    {
                        if (!cl[inputs[0].ToLower()].preform(inputs))//Executes command. note that all command needs to do to indicate failure is return false
                            Console.WriteLine(cl[inputs[0].ToLower()].helpInfo);
                    }
                }
                Console.WriteLine();
                Console.Write(library.name + ":" + currentlyViewedPlayList.name + ">");
                input = Console.ReadLine();
            }
            //updateThread.Abort();

        }

        

    }
}