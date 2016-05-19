using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AudioPlayer.Storage;
using System.IO;

namespace AudioPlayer
{
    class Rename : Command
    {
        public Rename()
        {
            nameStore = "rename";
        }

        public override bool preform(string[] args)
        {
            if (args.Length < 3)
                return false;
            Track track = MainClass.currentlyViewedPlayList.getTrack(args[1]);
            if (track.isReferance)
            {
                track.name = args[2];
            }
            else
            {
                track.name = args[2];
                string[] parhParts = track.fileLocation.Split('\\', '/');
                string fileName;
                fileName = args[2] + ".m4a";
                parhParts[parhParts.Length - 1] = fileName;
                string newFileLocation = parhParts[0];
                for (int i = 1; i < parhParts.Length; i++)
                {
                    newFileLocation = newFileLocation + "/" + parhParts[i];
                }
                File.Move(track.fileLocation, newFileLocation);
                track.fileLocation = newFileLocation;
            }
            MainClass.library.saveToFile();
            Console.WriteLine("Track \"" + args[1] + "\" has been renamed \"" + args[2] + "\"");
            return true;
        }

        public override string[] getAliases()
        {
            string[] aliases = new string[1];
            aliases[0] = "ren";
            return aliases;
        }
    }
}
