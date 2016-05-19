using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;
using System.IO;
using AudioPlayer.Storage;
using Microsoft.Xna.Framework.Media;
using System.Reflection;

namespace AudioPlayer
{
    class Play : Command
    {


        public Play()
        {
            nameStore = "play";
            helpInfoStore = "Plays a file or track in the current play list";
        }

        public override bool preform(string[] args)
        {
            MainClass.queingTrack = true;
            if (args.Length == 1)
            {
                switch (MainClass.playMode)
                {
                    case SongState.Shuffle:
                    case SongState.Loop:
                    case SongState.Stop:
                        MainClass.playNext = true;
                        return playRandomTrack();

                    
                    case SongState.PlayNext:
                        Track tr = MainClass.currentlyViewedPlayList.tracks[0];
                        Utils.Utils.playTrack(tr);
                        MainClass.currentPlayList = MainClass.currentlyViewedPlayList;
                        Utils.Utils.regenerateNextTracks(tr);
                        MainClass.playNext = true;
                        Console.WriteLine("Started playing \"{0}\"", tr.name);
                        return true;

                        
                }

            }
            else if (args[1].Equals("-r"))
            {
                return playRandomTrack();
            }
            else if (MainClass.currentlyViewedPlayList.getTrack(args[1]) != null)
            {

                Utils.Utils.playTrack(MainClass.currentlyViewedPlayList.getTrack(args[1]));
                MainClass.currentPlayList = MainClass.currentlyViewedPlayList;
                MainClass.playNext = true;
                Console.WriteLine("Started playing \"{0}\"", args[1]);
            }
            else
            {
                try
                {
                    MediaPlayer.Stop();
                    MainClass.currentSong = null;
                    MediaPlayer.Volume = 1F;
                    //Uri uri = new Uri(args[1]);
                    Song song;
                    var ctor = typeof(Song).GetConstructor(
                    BindingFlags.NonPublic | BindingFlags.Instance, null,
                    new[] { typeof(string), typeof(string), typeof(int) }, null);


                    song = (Song)ctor.Invoke(new object[] { args[1], args[1], 0 });
                    MediaPlayer.Play(song);
                    MainClass.currentSong = song;
                    MainClass.currentTrack = new Track("track from file", args[1]);
                    MainClass.playNext = false;
                    Console.WriteLine("Started playing \"{0}\"", args[1]);
                }
                catch (IOException)
                {
                    Console.WriteLine("File not found or track doesn't exist");
                }
            }
            MainClass.queingTrack = false;
            return true;

        }


        private bool playRandomTrack()
        {
            if (MainClass.currentlyViewedPlayList.trackCollenction.Count == 0)
            {
                Console.WriteLine("No tracks in this play list");
                return true;
            }
            Track tr = Utils.Utils.randomTrack(MainClass.currentlyViewedPlayList);
            Utils.Utils.playTrack(tr);
            MainClass.currentPlayList = MainClass.currentlyViewedPlayList;
            Utils.Utils.regenerateNextTracks(tr);
            Console.WriteLine("Started playing \"{0}\"", tr.name);
            return true;
        }

    }
}
