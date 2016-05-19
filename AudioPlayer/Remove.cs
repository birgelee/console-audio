using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AudioPlayer.Storage;
using System.IO;

namespace AudioPlayer
{
    class Remove : Command
    {
        public Remove()
        {
            nameStore = "remove";
            helpInfoStore = "Removes a track from the current play list";
        }
        public override bool preform(string[] args)
        {
            if (args.Length < 2)
                return false;
            Track track = MainClass.currentlyViewedPlayList.getTrack(args[1]);
            if (track == null)
            {
                Console.WriteLine("There is no track in this playlist under that name");
                return true;
            }
            if (MainClass.currentlyViewedPlayList == MainClass.library.playLists[0])
            {
                foreach (PlayList pl in MainClass.library.playLists)
                {
                    pl.removeTrack(track);
                    
                }
                if (track.isReferance)
                File.Delete(track.fileLocation);
                Console.WriteLine("Seccessfully removed track \"" + track.name + "\" from all play lists");
            }
            else
            {
                MainClass.currentlyViewedPlayList.removeTrack(track);
                Console.WriteLine("Seccessfully removed track \"" + track.name + "\" from \"" + MainClass.currentlyViewedPlayList.name + "\"");
            }
            MainClass.library.saveToFile();
            return true;
        }
    }
}
